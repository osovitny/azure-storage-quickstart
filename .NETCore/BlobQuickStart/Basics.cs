/*
<file>
  Description:

	Authors:
		Vadim Osovitny
    Anatoly Osovitny

	Created:
		22 Jun 2018

	Copyright (c) 2018 Osovitny Inc. All rights reserved.
</file>
*/

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Osovitny.Azure.Storage.Blob.QuickStart
{
  public static class Basics
  {
    public static async Task ProcessSamplesAsync(CloudStorageAccount storageAccount)
    {
      Console.WriteLine();
      Console.WriteLine("Azure Blob Storage. Basic Samples:");

      //1.
      await CreateContainer(storageAccount);

      //2.
      await UploadBlobs(storageAccount);

      //3.
      await ListBlobsInContainer(storageAccount);
    }

    public static async Task CreateContainer(CloudStorageAccount storageAccount)
    {
      var cloudBlobClient = storageAccount.CreateCloudBlobClient();
      var container = cloudBlobClient.GetContainerReference("templates");

      await container.CreateIfNotExistsAsync();

      //Set default permissions
      await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
      await container.SetMetadataAsync();

      Console.WriteLine("1. Container created '{0}'", container.Name);
      Console.WriteLine();
    }

    public static async Task UploadBlobs(CloudStorageAccount storageAccount)
    {
      var cloudBlobClient = storageAccount.CreateCloudBlobClient();
      var container = cloudBlobClient.GetContainerReference("templates");

      var fullBlobName = BlobUtils.GetFullBlobName("99b8dc98-449c-44a7-8965-9e84ac6fc289/images", "Car.jpg");
      var blob = container.GetBlockBlobReference(fullBlobName);
      var filePath = @"C:\Users\VadimOS\Pictures\Wallpapers\Car.jpg";
      await UploadBlob(filePath, blob);

      fullBlobName = BlobUtils.GetFullBlobName("99b8dc98-449c-44a7-8965-9e84ac6fc289/images", "Bridge.jpg");
      blob = container.GetBlockBlobReference(fullBlobName);
      filePath = @"C:\Users\VadimOS\Pictures\Wallpapers\Bridge.jpg";
      await UploadBlob(filePath, blob);

      fullBlobName = BlobUtils.GetFullBlobName("99b8dc98-449c-44a7-8965-9e84ac6fc289/images", "Girl.jpg");
      blob = container.GetBlockBlobReference(fullBlobName);
      filePath = @"C:\Users\VadimOS\Pictures\Wallpapers\Girl.jpg";
      await UploadBlob(filePath, blob);

      fullBlobName = BlobUtils.GetFullBlobName("99b8dc98-449c-44a7-8965-9e84ac6fc289", "template.html");
      blob = container.GetBlockBlobReference(fullBlobName);
      await blob.UploadTextAsync("<html><body><div>Hello Wolrd!</div></body></html>");

      Console.WriteLine("2. Files uploaded");
      Console.WriteLine();
    }

    public static async Task UploadBlob(string filePath, CloudBlockBlob blob)
    {
      if (!File.Exists(filePath))
          return;

      using (FileStream stream = File.OpenRead(filePath))
      {
        await blob.UploadFromStreamAsync(stream);
      }
    }

    public static async Task ListBlobsInContainer(CloudStorageAccount storageAccount)
    {
      /*
        Samples Directory Structure:
        https://alexcorpstorage.blob.core.windows.net/...

        templates/
          99b8dc98-449c-44a7-8965-9e84ac6fc289/
            images/
              Car.jpg
              Bridge.jpg
              Girl.jpg
            template.html
      */
      string containerName = "templates";
      string parentDirectoryName = "99b8dc98-449c-44a7-8965-9e84ac6fc289";

      Console.WriteLine("3. List blobs in Container: '{0}'", containerName);

      var cloudBlobClient = storageAccount.CreateCloudBlobClient();
      var container = cloudBlobClient.GetContainerReference(containerName);

      BlobContinuationToken blobContinuationToken = null;
      CloudBlobDirectory parentDirectory = container.GetDirectoryReference(parentDirectoryName);

      do
      {
        var results = await parentDirectory.ListBlobsSegmentedAsync(true, BlobListingDetails.None, 500, blobContinuationToken, null, null);
        blobContinuationToken = results.ContinuationToken;

        foreach (var blobitem in results.Results)
        {
          Console.WriteLine(blobitem.Uri);
        }

      } while (blobContinuationToken != null);
    }
  }
}
