using System;
using System.Collections.Generic;
using System.Linq;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Goal.Core.Helpers;
using Goal.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Goal.Data.Services
{
    public class ImageServices : IImageServices
    {
        private readonly Cloudinary _cloudinary;

        public ImageServices(IOptions<CloudinarySettings> config)
        {
            var settings = config.Value;
            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deletionParams);

            return result;
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }

    }
}
