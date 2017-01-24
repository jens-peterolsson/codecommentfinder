using Itb.CodeCommentFinder.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Itb.CodeCommentFinder.GithubRepository
{
    public class BlobRepository
    {
        public List<string> ActiveFileExtensions { get; private set; }

        public BlobRepository(List<string> fileExtensions)
        {

            /* TODO: this should reside in common since it will be the same for all repo types,
             but a more sophisticated solution for determining file extensions is needed anyway */

            ActiveFileExtensions = FileExtensions.FormatExtensions(fileExtensions);
        }

        public List<RepositoryFile> GetAllFiles(string userName, string repositoryName)
        {

            // TODO: should use DI for http and something better (e.g. RestSharp, though not supported by .net standard yet)
            using (var client = new HttpClient())
            {
                // FileExtensions.ShouldProcessFile(fileName)
            }
            throw new NotImplementedException();
        }

    }
}
