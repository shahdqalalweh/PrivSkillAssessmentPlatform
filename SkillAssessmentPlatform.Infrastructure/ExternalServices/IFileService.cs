using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Infrastructure.ExternalServices
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderPath);
        Task DeleteFileAsync(string filePath);
        bool ValidateFileSize(IFormFile file, long maxSizeInBytes);
        bool ValidateFileType(IFormFile file, string[] allowedFileTypes);
    }
}
