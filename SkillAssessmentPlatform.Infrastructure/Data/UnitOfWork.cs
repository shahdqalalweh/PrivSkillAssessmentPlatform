using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Core.Interfaces.Repository;
using SkillAssessmentPlatform.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly IServiceProvider _serviceProvider; // Use DI to create repositories
        private IDbContextTransaction _transaction;
        private bool _disposed = false;

        #region Injecting custom repositories via DI

        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly IApplicantRepository _applicantRepository;
        private readonly IExaminerRepository _examinerRepository;

        #endregion


        public UnitOfWork(AppDbContext context,
                IAuthRepository authRepository,
                IUserRepository userRepository,
                IApplicantRepository applicantRepository,
                IExaminerRepository examinerRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _authRepository = authRepository;
            _userRepository = userRepository;
            _applicantRepository = applicantRepository;
            _examinerRepository = examinerRepository;

        }


        #region customRepo
        // Custom Repositories
        public IAuthRepository AuthRepository => _authRepository;
        public IUserRepository UserRepository => _userRepository;
        public IApplicantRepository ApplicantRepository => _applicantRepository;
        public IExaminerRepository ExaminerRepository => _examinerRepository;
        #endregion

        // دعم Repositories العامة
        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out var repository))
            {
                return (IGenericRepository<T>)repository;
            }
            var newRepo = new GenericRepository<T>(_context);
            _repositories.Add(typeof(T), newRepo);
            return newRepo;
        }

        // لدعم الـ Custom Repositories
        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            if (_repositories.TryGetValue(typeof(TRepository), out var repository))
            {
                return (TRepository)repository;
            }

            // نحاول إنشاء Repository من خلال Activator
            try
            {
                var newRepo = Activator.CreateInstance(typeof(TRepository), _context) as TRepository;
                if (newRepo != null)
                {
                    _repositories.Add(typeof(TRepository), newRepo);
                    return newRepo;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Cannot create a repository of type {typeof(TRepository).Name}. Ensure that there is a constructor that accepts DbContext", ex);
            }

            throw new InvalidOperationException($"Cannot create repository of type {typeof(TRepository).Name}");
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress");
            }
            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction to commit");
            }
            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction to rollback");
            }
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
