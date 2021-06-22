using Azure.Storage.Blobs;

namespace EShop.Azure
{
    public class BlobStorageSettings
    {
        public BlobStorageSettings(BlobServiceClient blobServiceClient, string containerName)
        {
            BlobServiceClient = blobServiceClient;
            ContainerName = containerName;
        }

        public BlobServiceClient BlobServiceClient { get; }

        public string ContainerName { get; }
    }
}