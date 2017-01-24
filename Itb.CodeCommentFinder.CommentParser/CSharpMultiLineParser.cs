using System;
using Itb.CodeCommentFinder.Common;

namespace Itb.CodeCommentFinder.CommentParser
{
    public class CSharpMultiLineParser : ICodeLineParser
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
            throw new NotImplementedException();
        }
    }
}
