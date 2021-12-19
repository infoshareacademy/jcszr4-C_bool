using System;
using System.IO;
using C_bool.BLL.Models.Places;
using Microsoft.AspNetCore.Http;

namespace C_bool.WebApp.Helpers
{
    public class ImageConverter
    {

        public static PhotoBase64 ConvertImage(IFormFile files)
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

                    var objfiles = new PhotoBase64()
                    {
                        Name = newFileName,
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objfiles.Data = Convert.ToBase64String(target.ToArray());
                    }
                    return objfiles;
                }
            }
            return null;
        }
    }
}