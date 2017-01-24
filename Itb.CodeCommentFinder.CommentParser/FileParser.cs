using Itb.CodeCommentFinder.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Itb.CodeCommentFinder.CommentParser
{
    public class FileParser
    {

        public string FindComments(List<RepositoryFile> files, List<ICodeLineParser> lineParsers)
        {
            //TODO: refactor and extract...
            var result = new StringBuilder();

            foreach (var file in files)
            {
                var multiLineStore = new Dictionary<string, string>();

                var lines = file.Content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach(var line in lines)
                {
                    foreach(var lineParser in lineParsers)
                    {

                        var existingLines = string.Empty;
                        var key = lineParser.GetType().Name;

                        if (multiLineStore.ContainsKey(key))
                        {
                            existingLines = multiLineStore[key];
                        }

                        var parsedLine = lineParser.Parse(existingLines + line);

                        if(parsedLine.CommentStatus == CommentStatus.Finished)
                        {
                            result.AppendLine($"{file.Name}: {parsedLine.Comment}");
                            multiLineStore[key] = string.Empty;
                        }

                        if (parsedLine.CommentStatus == CommentStatus.Unfinished)
                        {
                            multiLineStore[key] = existingLines + line;
                        }

                    }
                }
            }

            return result.ToString();
        }

    }
}
