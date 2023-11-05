using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace WhoamI.Business.Managers
{
    public class PhotoManager
    {
        private readonly IHostEnvironment _hostEnvironment;

        public PhotoManager(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public string UploadPhoto(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Geçersiz dosya");
            }

            string webRootPath = _hostEnvironment.ContentRootPath;
            string uploadFolder = Path.Combine(webRootPath, "wwwroot", folderName);

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine("/", folderName, uniqueFileName);
        }

        public void DeletePhoto(string photoPath)
        {
            string webRootPath = _hostEnvironment.ContentRootPath;
            string filePath = Path.Combine(webRootPath, "wwwroot", photoPath.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
