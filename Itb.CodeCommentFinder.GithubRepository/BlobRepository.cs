using Itb.CodeCommentFinder.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Itb.CodeCommentFinder.GithubRepository.Entities;
using System.Text;

namespace Itb.CodeCommentFinder.GithubRepository
{
    public class BlobRepository : ICodeRepository
    {
        public List<string> ActiveFileExtensions { get; private set; }

        private const string GitFileType = "blob";

        public BlobRepository(List<string> fileExtensions)
        {

            /* TODO: this should reside in common since it will be the same for all repo types,
             but a more sophisticated solution for determining file extensions is needed anyway */

            ActiveFileExtensions = FileExtensions.FormatExtensions(fileExtensions);
        }

        public async Task<List<RepositoryFile>> GetAllFilesAsync(string userName, string repositoryName)
        {

            // TODO: should use DI for http?
            // flurl has ultra cool fluent syntax though + support for faking http context for unit tests!
            // also algorithm storing all code found in repo in memory is not very efficient...

            var result = new List<RepositoryFile>();

            var gitTree = await GetRepositoryTree(userName, repositoryName);

            /* TODO: should check whether gitTree.truncated is true or not
             and do some error handling... */

            foreach (var node in gitTree.tree)
            {
                if (!node.type.Equals(GitFileType)) continue;
                if (!FileExtensions.IsSelectedFileType(node.path, ActiveFileExtensions)) continue;

                var repoFile = await GetBlobContentAsync(node);
                result.Add(repoFile);
            }

            return result;
        }

        private static async Task<TreeRoot> GetRepositoryTree(string userName, string repositoryName)
        {
            var baseUrl = $@"https://api.github.com/repos/{userName}/{repositoryName}/git";

            //TODO: hard-coded branch = master
            var url = $"{baseUrl}".AppendPathSegment("trees/master?recursive=1");

            var gitTree = await url.GetJsonAsync<TreeRoot>();
            return gitTree;
        }

        private async Task<RepositoryFile> GetBlobContentAsync(TreeNode node)
        {
            var blob = await node.url.GetJsonAsync<Blob>();

            // TODO: assumes base64 and utf8...
            byte[] data = Convert.FromBase64String(blob.content);
            string decodedString = Encoding.UTF8.GetString(data);

            var name = System.IO.Path.GetFileName(node.path);

            var result = new RepositoryFile {
                Content = decodedString,
                Name = name
            };

            return result;
        }

    }
}
