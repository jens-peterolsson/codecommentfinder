using Itb.CodeCommentFinder.Common;
using System;

namespace Itb.CodeCommentFinder.CommentParser
{
    public interface ICodeLineParser
    {
        Func<string, ParseResult> Parse { get; }
    }
}
