using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itb.CodeCommentFinder.Common
{
    public class ParseResult
    {
        public CommentStatus CommentStatus { get; set; }
        public string Comment { get; set; }
    }
}
