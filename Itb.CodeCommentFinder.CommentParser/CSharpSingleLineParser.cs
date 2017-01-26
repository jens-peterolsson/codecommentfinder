using System;
using Itb.CodeCommentFinder.Common;

namespace Itb.CodeCommentFinder.CommentParser
{
    public class CSharpSingleLineParser : ICodeLineParser
    {
        public Func<string, ParseResult> Parse
        {
            get
            {
                return ParseLine;
            }
        }

        private ParseResult ParseLine(string line)
        {
            var result = new ParseResult
            {
                CommentStatus = CommentStatus.None
            };

            var commentPosition = line.IndexOf("//");
            if(commentPosition >= 0)
            {
                result.CommentStatus = CommentStatus.Finished;
                result.Comment = line.Substring(commentPosition);
            }

            return result;
        }
    }
}
