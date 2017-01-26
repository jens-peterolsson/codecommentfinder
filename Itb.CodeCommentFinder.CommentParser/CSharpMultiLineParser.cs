using System;
using Itb.CodeCommentFinder.Common;

namespace Itb.CodeCommentFinder.CommentParser
{
    public class CSharpMultiLineParser : ICodeLineParser
    {
        public Func<string, ParseResult> Parse
        {
            get
            {
                return ParseLines;
            }
        }

        private ParseResult ParseLines(string code)
        {
            var result = new ParseResult
            {
                CommentStatus = CommentStatus.None
            };

            var hasCommentStart = (code.IndexOf("/*") >= 0);
            var hasCommentEnd = (code.IndexOf("*/") >= 0);

            if (hasCommentStart && !hasCommentEnd)
            {
                result.CommentStatus = CommentStatus.Unfinished;
            }

            if (hasCommentStart && hasCommentEnd)
            {
                // TODO: a code block could of course contain more than one comment, e.g.
                // /* I will declare */ int x = 5; /* ...and now it's done */ 
                result.CommentStatus = CommentStatus.Finished; 
                result.Comment = ParseComment(code);
            }

            return result;
        }

        private string ParseComment(string code)
        {
            var startIndex = code.IndexOf("/*");
            var result = code.Substring(startIndex);

            var stopIndex = result.IndexOf("*/");
            if(result.Length > stopIndex + 2)
            {
                result = result.Remove(stopIndex + 2);
            }

            return result;
        }

    }
}
