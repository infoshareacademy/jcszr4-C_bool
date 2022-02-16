using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace C_bool.WebApp.Helpers
{
    public class ImageConverter
    {

        public static string ConvertImage(IFormFile files, out string message)
        {
            List<string> acceptedTypes = new List<string>()
            {
                "image/jpeg",
                "image/jpeg",
                "image/pjpeg",
                "image/x-png",
                "image/png",
                "image/gif"
            };

            if (files != null)
            {
                if (files.Length > 0)
                {
                    if (!acceptedTypes.Contains(files.ContentType))
                    {
                        message = "Image has unknown content type, or it's not a image";
                        return null;
                    }
                    var fileName = Path.GetFileName(files.FileName);
                    var fileExtension = Path.GetExtension(fileName);
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                    string base64String;
                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        base64String = Convert.ToBase64String(target.ToArray());
                    }

                    message = "Image converted";
                    return base64String;
                }
            }

            message = "Image is null";
            return null;
        }
    }
}