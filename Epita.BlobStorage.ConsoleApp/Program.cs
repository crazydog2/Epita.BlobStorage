using System;
using System.IO;
using System.Threading.Tasks;

namespace Epita.BlobStorage.ConsoleApp
{
    class Program
    {
        #region ConnectionString

        private static string connectionString = "UseDevelopmentStorage=true";

        #endregion

        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting ...");

            var container = "epita";
            var fileName = "Realytics.png";

            var blobService = new BlobService(connectionString);

            await using FileStream stream = File.OpenRead(fileName);

            bool success = await blobService.UploadFromStreamAsync(container, fileName, stream).ConfigureAwait(false);

            Console.WriteLine($"Is Success : [{success}]");

            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();
        }
    }
}