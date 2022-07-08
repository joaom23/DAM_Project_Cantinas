using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DAM_API.Helper
{
    public static class Images
    {
        public static async Task<string> SaveImage(IFormFile image, IWebHostEnvironment env, string folderName)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(image.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(env.ContentRootPath, "Recursos/" + folderName, imageName);
            using (var filestream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(filestream);
            }
            return imageName;
        }
    }
}
