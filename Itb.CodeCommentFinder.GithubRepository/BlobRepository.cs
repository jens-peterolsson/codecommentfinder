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
        public IEnumerable<string> ActiveFileExtensions { get; }

        private const string GitFileType = "blob";

        public BlobRepository(IEnumerable<string> fileExtensions)
        {

            /* TODO: this should reside in common since it will be the same for all repo types,
             but a more sophisticated solution for determining file extensions, comment style etc. 
             is needed anyway */

            ActiveFileExtensions = FileExtensions.FormatExtensions(fileExtensions);
        }

        public async Task<IEnumerable<RepositoryFile>> GetAllFilesAsync(string userName, string password, string repositoryName)
        {

            // TODO: user should not be same for both auth and repo, should be possible to
            // parse public github repos
            // also algorithm storing everything found in repo in memory is not very efficient...

            var result = new List<RepositoryFile>();

            var gitTree = await GetRepositoryTree(userName, password, repositoryName);

            /* TODO: should check whether gitTree.truncated is true or not
             and do some error handling... */

            foreach (var node in gitTree.tree)
            {
                if (!node.type.Equals(GitFileType)) continue;
                if (!FileExtensions.IsSelectedFileType(node.path, ActiveFileExtensions)) continue;

                var repoFile = await GetBlobContentAsync(node, userName, password);
                result.Add(repoFile);
            }

            return result;
        }

        private static async Task<TreeRoot> GetRepositoryTree(string userName, string password, string repositoryName)
        {
            var baseUrl = $@"https://api.github.com/repos/{userName}/{repositoryName}/git";

            //TODO: hard-coded branch = master
            var url = $"{baseUrl}"
                .AppendPathSegment("trees/master")
                .SetQueryParam("recursive", "1")
                .WithHeader("User-Agent", "codecommentfinder")
                .WithBasicAuth(userName, password);

            var gitTree = await url.GetJsonAsync<TreeRoot>();
            return gitTree;
        }

        private async Task<RepositoryFile> GetBlobContentAsync(TreeNode node, string userName, string password)
        {
            var blob = await node.url
                        .WithHeader("User-Agent", "codecommentfinder")
                        .WithBasicAuth(userName, password)
                        .GetJsonAsync<Blob>();

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
