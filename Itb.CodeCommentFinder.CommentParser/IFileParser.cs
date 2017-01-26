using Itb.CodeCommentFinder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itb.CodeCommentFinder.CommentParser
{
    public interface IFileParser
    {
        IEnumerable<ICodeLineParser> LineParsers { get; }
        string FindComments(IEnumerable<RepositoryFile> files);
    }
}
