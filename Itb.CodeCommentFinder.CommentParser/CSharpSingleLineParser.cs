using System;
using Itb.CodeCommentFinder.Common;

namespace Itb.CodeCommentFinder.CommentParser
{
    public class CSharpSingleLineParser : ICodeLineParser
    {
        public Func<string, CodeLine> Parse
        {
            get
            {
                return ParseLine;
            }
        }

        private CodeLine ParseLine(string line)
        {
            var result = new CodeLine
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
