/*
<file>
  Description:
    Azure Storage Blob Quickstart: Demonstrate how to upload, list, download, and delete blobs

  Authors:
    Vadim Osovitny
    Anatoly Osovitny

  Created:
    22 Jun 2018

  Copyright (c) 2018 Osovitny Inc. All rights reserved.
</file>
*/

using Microsoft.WindowsAzure.Storage;
using System;

namespace Osovitny.Azure.Storage.Blob.QuickStart
{
  class Program
  {
    private static string STORAGE_CONNECTIONSTRING = "PUT-YOUR-STORAGE-CONNECTIONSTRING-HERE";

    static void Main(string[] args)
    {
      /*
        All Samples for fictional company: Alex Corporation
      */

      CloudStorageAccount storageAccount = null;

      if (!CloudStorageAccount.TryParse(STORAGE_CONNECTIONSTRING, out storageAccount))
      {
          Console.WriteLine("A connection string is not correct");
          Console.WriteLine("Press any key to exit application.");
          Console.ReadLine();
          return;
      }

      Basics.ProcessSamplesAsync(storageAccount).Wait();
      Advanced.ProcessSamplesAsync(storageAccount).Wait();

      Console.WriteLine("Press any key to exit the sample application.");
      Console.ReadLine();
    }
  }
}
