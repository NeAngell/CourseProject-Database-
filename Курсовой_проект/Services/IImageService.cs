using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Курсовой_проект.Services
{
    public interface IImagesService
    {
        Task<string> SaveImage(IFormFile file);
        FileStream GetImage(string fileName);
    }
}
