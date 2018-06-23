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

namespace Osovitny.Azure.Storage.Blob.QuickStart
{
  public static class BlobUtils
  {
    internal static string GetFullBlobName(string folderPath, string blobName)
    {
      /*
				Details: http://johnatten.com/2013/05/24/modeling-a-directory-structure-on-azure-blob-storage/
			*/
      if (!string.IsNullOrEmpty(folderPath) && !string.IsNullOrWhiteSpace(folderPath))
        blobName = folderPath.TrimEnd('/') + "/" + blobName;

      return blobName;
    }
  }
}
