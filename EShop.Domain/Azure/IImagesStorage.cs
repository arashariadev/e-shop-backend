using System.IO;
using System.Threading.Tasks;

namespace EShop.Domain.Azure
{
    public interface IImagesStorage
    {
        Task<string> UploadImageAsync(Stream stream, string fileName);

        Task DeleteImageAsync(string imageUri);
    }
}