using Itb.CodeCommentFinder.CommentParser;
using Itb.CodeCommentFinder.Common;
using Itb.CodeCommentFinder.GithubRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itb.CodeCommentFinder.ConsoleGui
{
    public static class SimplisticDi
    {
        public static IFileParser GetFileParser() => new FileParser(GetLineParsers());
        public static ICodeRepository GetRepository() => new BlobRepository(new [] { ".cs" });

        private static List<ICodeLineParser> GetLineParsers() =>
            new List<ICodeLineParser> { new CSharpSingleLineParser(), new CSharpMultiLineParser() };
    }
}
