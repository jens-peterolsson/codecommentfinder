using System;
using System.Collections.Generic;

namespace Itb.CodeCommentFinder.GithubRepository
{
    public class BlobRepository
    {
        public List<string> ActiveFileExtensions { get; private set; }

        public BlobRepository(List<string> fileExtensions)
        {

            /* TODO: this should reside in common since it will be the same for all repo types,
             but a more sophisticated solution for determining file extensions is needed anyway */

            ActiveFileExtensions = FormatExtensions(fileExtensions);
        }
        public List<string> FormatExtensions(List<string> fileExtensions)
        {
            // make sure extensions start with . for easy comparison 
            throw new NotImplementedException();
        }

        public bool ShouldProcessFile(string fileName)
        {
            throw new NotImplementedException();
        }

    }
}
