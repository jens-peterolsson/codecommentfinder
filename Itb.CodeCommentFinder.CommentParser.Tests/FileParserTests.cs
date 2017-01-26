using Itb.CodeCommentFinder.Common;
using System;
using System.Collections.Generic;
using Xunit;

namespace Itb.CodeCommentFinder.CommentParser.Tests
{
    public class FileParserTests
    {
        private string ExpectedSingleLineComment = "1.cs: // That's five again!" + Environment.NewLine;
        private string ExpectedMultiLineComment = "2.cs: /* It will be five again! => */" + Environment.NewLine;

        //TODO: add test that quoted // should not be included as comments, e.g. url:s

        [Fact]
        public void ShouldFindSingleLineComment()
        {
            var tested = new FileParser(new List<ICodeLineParser> { new CSharpSingleLineParser() });

            var result = tested.FindComments(GetTestData());
            Assert.Equal(ExpectedSingleLineComment, result);
        }

        [Fact]
        public void ShouldFindMultiLineComment()
        {
            var tested = new FileParser(new List<ICodeLineParser> { new CSharpMultiLineParser() });

            var result = tested.FindComments(GetTestData());
            Assert.Equal(ExpectedMultiLineComment, result);
        }

        [Fact]
        public void ShouldFindBoth()
        {
            var tested = new FileParser(new List<ICodeLineParser> { new CSharpSingleLineParser(), new CSharpMultiLineParser() });

            var result = tested.FindComments(GetTestData());

            Assert.True(result.Contains(ExpectedSingleLineComment));
            Assert.True(result.Contains(ExpectedMultiLineComment));
        }


        private List<RepositoryFile> GetTestData()
        {
            var result = new List<RepositoryFile>
            {
                new RepositoryFile 
                {
                    Name = "1.cs",
                    Content =
                        "int x = 5;"
                        + "\n"
                        + "double y = 5.0; // That's five again!"
                },
                new RepositoryFile
                {
                    Name = "2.cs",
                    Content =
                        "int x = 5; /*"
                        + "\n"
                        + " It will be five again! => */ double y = 5.0;"
                }
            };

            return result;
        }
    }
}

