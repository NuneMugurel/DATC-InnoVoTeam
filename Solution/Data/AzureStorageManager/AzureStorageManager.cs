using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.Azure.Storage; // Namespace for CloudStorageAccount
using Microsoft.Azure.Storage.Queue; // Namespace for Queue storage types
using Newtonsoft.Json;

namespace Data.AzureStorageManager
{
    public class AzureStorageManager<TEntity> where TEntity : class
    {
        private CloudQueue queue;
        public AzureStorageManager()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            queue = queueClient.GetQueueReference("votingqueue");
            queue.CreateIfNotExists();
        }

        public void InsertMessage(TEntity entity)
        {
            CloudQueueMessage message = new CloudQueueMessage(JsonConvert.SerializeObject(entity));
            queue.AddMessage(message);
        }

        public TEntity RetrieveMessage()
        {
            CloudQueueMessage retrievedMessage = queue.GetMessage();
            var count = CachedMessagesCount();
            if (CachedMessagesCount() > 0)
            {
                queue.DeleteMessage(retrievedMessage);
                return JsonConvert.DeserializeObject<TEntity>(retrievedMessage.AsString);
            }
            return null;
        }

        private int CachedMessagesCount()
        {
            queue.FetchAttributes();
            return (int)queue.ApproximateMessageCount;
        }
    }
}
