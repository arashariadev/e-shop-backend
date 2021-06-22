using System.IO;
using System.Threading.Tasks;
using EShop.Domain.Azure;

namespace EShop.Azure
{
    public class ImagesStorage : IImagesStorage
    {
        private readonly BlobStorageSettings _settings;

        public ImagesStorage(BlobStorageSettings settings)
        {
            _settings = settings;
        }
        
        public async Task<string> UploadImageAsync(Stream stream, string fileName)
        {
            var containerClient = _settings.BlobServiceClient.GetBlobContainerClient(_settings.ContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(stream);
            return containerClient.Uri.AbsoluteUri + "/" + fileName;
        }

        public async Task DeleteImageAsync(string imageUri)
        {
            var containerClient = _settings.BlobServiceClient.GetBlobContainerClient(_settings.ContainerName);
            var arr = imageUri.Split(new char[] { '/' });
            await containerClient.DeleteBlobIfExistsAsync(arr[^1]);
        }
    }
}