using Microsoft.AspNetCore.Http;
using System.IO;

namespace Application.Extensions
{
    public static class ExtensionPictureMethod
    {
        public static byte[] GetBytes(this IFormFile formFile)
        {
            using(var memoryStream=new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
