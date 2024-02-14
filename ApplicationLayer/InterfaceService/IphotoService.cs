using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InterfaceService
{
    internal interface IphotoService
    {
        Task<string> addPhoto(IFormFile file);
        Task<DeletionResult> DeletPhoto(string file);
        Task<string> WriteFile(IFormFile file);
        Task DeleteFile(string filename);
    }
}
