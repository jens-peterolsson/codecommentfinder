using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Flurl.Http.Testing;
using System.Linq;

namespace Itb.CodeCommentFinder.GithubRepository.Tests
{
    public class BlobRepositoryTests
    {

        [Fact]
        public async Task ShouldReturnCSharpFile()
        {
            using (var httpTest = new HttpTest())
            {
                
                /* Flurl is now in test mode since
                 Flurl.Http.Testing.HttpTest will fake all http calls within the using statement
                 1st response will be 1st RespondWith, 2nd response 2nd RespondWith etc. */

                httpTest
                    .RespondWith(TestDataTreeRoot)
                    .RespondWith(TestDataTreeNode);

                var tested = new BlobRepository(new List<string> { ".cs" });
                var result = await tested.GetAllFilesAsync("user", "pass", "repo");

                Assert.Equal(1, result.Count());
                Assert.Equal("TreeNode.cs", result.First().Name);
                Assert.True(result.First().Content.Trim().StartsWith("namespace Itb.CodeCommentFinder.GithubRepository.Entities"));
            }
        }

        private const string TestDataTreeRoot = @"
        {
          ""sha"": ""9b39b22596107716a0735de0eecb751575b095bf"",
          ""url"": ""https://api.github.com/repos/jens-peterolsson/codecommentfinder/git/trees/9b39b22596107716a0735de0eecb751575b095bf"",
          ""tree"": [
            {
              ""path"": ""Itb.CodeCommentFinder.GithubRepository/Entities/TreeNode.cs"",
              ""mode"": ""100644"",
              ""type"": ""blob"",
              ""sha"": ""5614df141e4558a4d00b2cad2b36951ede26a607"",
              ""size"": 340,
              ""url"": ""https://api.github.com/repos/jens-peterolsson/codecommentfinder/git/blobs/5614df141e4558a4d00b2cad2b36951ede26a607""            
            },
            {
              ""path"": ""README.md"",
              ""mode"": ""100644"",
              ""type"": ""blob"",
              ""sha"": ""aca7d64cf593f6f85c479ee14d1efde20b0403ed"",
              ""size"": 59,
              ""url"": ""https://api.github.com/repos/jens-peterolsson/codecommentfinder/git/blobs/aca7d64cf593f6f85c479ee14d1efde20b0403ed""
            }
          ],
          ""truncated"": false
        }
    ";

    private const string TestDataTreeNode = @"
    {
        ""sha"": ""5614df141e4558a4d00b2cad2b36951ede26a607"",
        ""size"": 340,
        ""url"": ""https://api.github.com/repos/jens-peterolsson/codecommentfinder/git/blobs/5614df141e4558a4d00b2cad2b36951ede26a607"",
        ""content"": ""bmFtZXNwYWNlIEl0Yi5Db2RlQ29tbWVudEZpbmRlci5HaXRodWJSZXBvc2l0\nb3J5LkVudGl0aWVzCnsKICAgIHB1YmxpYyBjbGFzcyBUcmVlTm9kZQogICAg\newogICAgICAgIHB1YmxpYyBzdHJpbmcgcGF0aCB7IGdldDsgc2V0OyB9CiAg\nICAgICAgcHVibGljIHN0cmluZyBtb2RlIHsgZ2V0OyBzZXQ7IH0KICAgICAg\nICBwdWJsaWMgc3RyaW5nIHR5cGUgeyBnZXQ7IHNldDsgfQogICAgICAgIHB1\nYmxpYyBzdHJpbmcgc2hhIHsgZ2V0OyBzZXQ7IH0KICAgICAgICBwdWJsaWMg\naW50IHNpemUgeyBnZXQ7IHNldDsgfQogICAgICAgIHB1YmxpYyBzdHJpbmcg\ndXJsIHsgZ2V0OyBzZXQ7IH0KICAgIH0KfQ==\n"",
        ""encoding"": ""base64""
    }
    ";

    }

}