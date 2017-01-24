using Itb.CodeCommentFinder.Common;
using System;
using System.Collections.Generic;
using Xunit;

namespace Itb.CodeCommentFinder.CommentParser.Tests
{
    public class FileParserTests
    {
        private string ExpectedSingleLineComment = "1.cs: // That's five again!" + Environment.NewLine;
        private string ExpectedMultiLineComment = "2.cs:  /* It will be five again! => */" + Environment.NewLine;

        [Fact]
        public void ShouldFindSingleLineComment()
        {
            var tested = new FileParser();
            var result = tested.FindComments(GetTestData(), new List<ICodeLineParser> { new CSharpSingleLineParser() });
            Assert.Equal(ExpectedSingleLineComment, result);
        }

        [Fact]
        public void ShouldFindMultiLineComment()
        {
            var tested = new FileParser();
            var result = tested.FindComments(GetTestData(), new List<ICodeLineParser> { new CSharpMultiLineParser() });
            Assert.Equal(ExpectedMultiLineComment, result);
        }

        [Fact]
        public void ShouldFindBoth()
        {
            var tested = new FileParser();
            var result = tested.FindComments(GetTestData(), new List<ICodeLineParser> { new CSharpSingleLineParser(), new CSharpMultiLineParser() });

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
                        + Environment.NewLine
                        + "double y = 5.0; // That's five again!"
                },
                new RepositoryFile
                {
                    Name = "2.cs",
                    Content =
                        "int x = 5; /*"
                        + Environment.NewLine
                        + " It will be five again! => */ double y = 5.0;"
                }
            };

            return result;
        }
    }
}

