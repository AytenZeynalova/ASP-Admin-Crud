using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Pustok.Helpers
{
    public static class FileManager
    {
        public static string Save(string root ,string folder, IFormFile file)
        {

            var guid = Guid.NewGuid().ToString();

            var newFileName = guid + file.FileName;

            string path = Path.Combine(root, folder, newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);

            }
            return newFileName;
        }

        public static bool Delete(string root, string folder, string filename)
        {
            string path = Path.Combine(root, folder, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;

            }
            return false;

        }
    }
}
