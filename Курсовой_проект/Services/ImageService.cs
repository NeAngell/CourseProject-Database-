using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Курсовой_проект.Services
{
    public class ImagesService: IImagesService
    {
        public static string noCarImage = "no-image-car.png";
        public static string imagePath = "Images";

        public async Task<string> SaveImage(IFormFile file)
        {
            var imageName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            using (var stream = new FileStream(Path.Combine(imagePath, imageName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return imageName;
        }

        public FileStream GetImage(string fileName)
        {
            var fullPath = Path.Combine(imagePath, fileName);

            if (File.Exists(fullPath))
            {
                return new FileStream(fullPath, FileMode.Open);
            }

            return null;
        }
    }
}
