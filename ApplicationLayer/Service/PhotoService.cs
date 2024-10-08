﻿using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using DomainLayer.Core;
using ApplicationLayer.InterfaceService;

namespace ApplicationLayer.Service
{
    public class PhotoService:IphotoService
    {
        private readonly Cloudinary _c;
        public PhotoService(IOptions<CloudKey> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                  config.Value.ApiKey,
                    config.Value.ApiSecret
                );
            _c = new Cloudinary(acc);
        }
        public async Task<DeletionResult> DeletPhoto(string publicId)
        {
            var deletParams = new DeletionParams(publicId);
            var result = await _c.DestroyAsync(deletParams);
            return result;
        }

        public async Task<string> addPhoto(IFormFile file)
        {
            var uploadresult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stre = file.OpenReadStream();
                var uploadparam = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stre),
                    Transformation = new Transformation().Height(500).Width(500)
                };
                uploadresult = await _c.UploadAsync(uploadparam);
            }
            return uploadresult.SecureUrl.ToString();
        }
    }
}
