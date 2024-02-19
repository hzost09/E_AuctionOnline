using ApplicationLayer.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Service
{
    internal class DocumentService :IDocument
    {
        public async Task<string> WriteFile(IFormFile file)
        {
            string filename = "";
            string exactpath = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                if (extension != ".doc" && extension != ".txt" && extension != ".docx")
                {
                    throw new Exception("Invalid file type. Only .doc and .txt files are allowed.");
                }
               
                filename = DateTime.Now.Ticks.ToString() + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return exactpath;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString(); ;
            }
            
        }
    }
}
