using System;
using System.Collections.Generic;
using Xunit;

namespace Itb.CodeCommentFinder.GithubRepository.Tests

{
    public class FileExtensionsTests
    {

        [Fact]
        public void ShouldAddExtensions() 
        {
            var testData = new List<string>{".cs", ".js"};
            VerifyExtensions(testData);
        }

        [Fact]
        public void ShoulAddDotIfNeededToExtensions()
        {
            var testData = new List<string> { "cs", "js" };
            VerifyExtensions(testData);
        }

        private void VerifyExtensions(List<string> extensions)
        {
            var result = FileExtensions.FormatExtensions(extensions);
            Assert.Equal(2, result.Count);
            Assert.True(result.Contains(".cs"));
            Assert.True(result.Contains(".js"));
        }

        [Fact]
        public void ShoulProcessRegisteredExtensions()
        {
            var testData = new List<string> { ".cs", ".js" };

            Assert.True(FileExtensions.IsSelectedFileType("file.cs", testData));
            Assert.False(FileExtensions.IsSelectedFileType("file.ts", testData));

            Assert.True(FileExtensions.IsSelectedFileType("somepath/subdir/file.cs", testData), 
                "Should parse full path in file name");
            Assert.True(FileExtensions.IsSelectedFileType(@"somepath\subdir\file.cs", testData), 
                "Should parse full path in file name");
        }

    }
}
