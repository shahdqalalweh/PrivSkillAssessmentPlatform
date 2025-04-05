using AutoMapper;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Core.Common;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Application.Services
{
    public class ExaminerService 
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ITrackRepository _trackRepository;
        //private readonly IWorkloadRepository _workloadRepository;
        private readonly IMapper _mapper;

        public ExaminerService(
                    IUnitOfWork unitOfWork,
                    IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ExaminerDTO>> GetAllExaminersAsync(int page = 1, int pageSize = 10)
        {
            var examiners = await _unitOfWork.ExaminerRepository.GetPagedAsync(page, pageSize);
            var totalCount = await _unitOfWork.ExaminerRepository.GetCount();

            return new PagedResponse<ExaminerDTO>(
                _mapper.Map<List<ExaminerDTO>>(examiners),
                page,
                pageSize,
                totalCount
            );
        }

        public async Task<ExaminerDTO> GetExaminerByIdAsync(string id)
        {
            var examiner = await _unitOfWork.ExaminerRepository.GetByIdAsync(id);
            return _mapper.Map<ExaminerDTO>(examiner);
        }

        public async Task<ExaminerDTO> UpdateExaminerAsync(string id, UpdateExaminerDTO examinerDto)
        {
            var examiner = await _unitOfWork.ExaminerRepository.GetByIdAsync(id);

            examiner.Specialization = examinerDto.Specialization;
            //examiner.Bio = examinerDto.Bio;

            var updatedExaminer = await _unitOfWork.ExaminerRepository.UpdateAsync(examiner);
            return _mapper.Map<ExaminerDTO>(updatedExaminer);
        }
        /*
        public async Task<PagedResponse<TrackDTO>> GetExaminerTracksAsync(string examinerId, int page = 1, int pageSize = 10)
        {
            var tracks = await _unitOfWork.TrackRepository.GetByExaminerIdAsync(examinerId, page, pageSize);
            var totalCount = await _unitOfWork.TrackRepository.CountByExaminerIdAsync(examinerId);

            return new PagedResponse<TrackDTO>(
                _mapper.Map<List<TrackDTO>>(tracks),
                page,
                pageSize,
                totalCount
            );
        }
        */

        /*
        public async Task<bool> AddTrackToExaminerAsync(string examinerId, AddTrackDTO trackDto)
        {
            var examiner = await _unitOfWork.ExaminerRepository.GetByIdAsync(examinerId);
            var track = await _unitOfWork.TrackRepository.GetByIdAsync(trackDto.TrackId);

            examiner.Tracks.Add(track);
            await _unitOfWork.ExaminerRepository.UpdateAsync(examiner);

            return true;
        }
        */
        /*
        public async Task<ExaminerLoadDTO> GetExaminerWorkloadAsync(string examinerId)
        {
            var workload = await _unitOfWork.WorkloadRepository.GetByExaminerIdAsync(examinerId);
            return _mapper.Map<ExaminerLoadDTO>(workload);
        }
        */

        public async Task<bool> RemoveTrackFromExaminerAsync(string examinerId, int trackId)
        {
            var examiner = await _unitOfWork.ExaminerRepository.GetByIdAsync(examinerId);
            var track = examiner.WorkingTracks.FirstOrDefault(t => t.Id == trackId);

            if (track != null)
            {
                examiner.WorkingTracks.Remove(track);
                await _unitOfWork.ExaminerRepository.UpdateAsync(examiner);
            }

            return true;
        }
    }

}
