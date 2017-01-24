using Itb.CodeCommentFinder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itb.CodeCommentFinder.CommentParser
{
    public interface IFileParser
    {
        string FindComments(List<RepositoryFile> files, List<ICodeLineParser> lineParsers);
    }
}
