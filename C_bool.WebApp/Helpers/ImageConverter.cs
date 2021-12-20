using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace C_bool.WebApp.Helpers
{
    public class ImageConverter
    {

        public static string ConvertImage(IFormFile files)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    string base64String;
                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        base64String = Convert.ToBase64String(target.ToArray());
                    }
                    return base64String;
                }
            }
            return null;
        }
    }
}