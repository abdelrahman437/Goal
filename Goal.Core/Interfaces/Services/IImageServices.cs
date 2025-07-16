using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Goal.Core.Interfaces.Services
{
    public interface IImageServices
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile Image);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}
