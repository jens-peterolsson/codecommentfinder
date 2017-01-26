using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itb.CodeCommentFinder.GithubRepository
{
    public static class FileExtensions
    {
        public static IEnumerable<string> FormatExtensions(IEnumerable<string> fileExtensions)
        {
            // make sure extensions start with . for easy comparisons
            var result = new List<string>();

            fileExtensions?.ToList().ForEach(ext =>
            {
                result.Add(ext.StartsWith(".") ? ext : "." + ext);
            });

            return result;
        }

        public static bool IsSelectedFileType(string fileName, IEnumerable<string> activeFileExtensions)
        {
            var extension = System.IO.Path.GetExtension(fileName);
            var result = (!string.IsNullOrWhiteSpace(extension) && activeFileExtensions.Contains(extension));

            return result;
        }


    }
}
