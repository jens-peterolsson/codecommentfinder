using Itb.CodeCommentFinder.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Itb.CodeCommentFinder.CommentParser
{
    public class FileParser : IFileParser
    {
        public IEnumerable<ICodeLineParser> LineParsers { get; }

        public FileParser(IEnumerable<ICodeLineParser> lineParsers)
        {
            LineParsers = lineParsers;
        }

        public string FindComments(IEnumerable<RepositoryFile> files)
        {
            var result = new StringBuilder();

            foreach (var file in files)
            {
                var fileComments = ProcessFile(file);

                if (!string.IsNullOrWhiteSpace(fileComments))
                {
                    result.Append(fileComments);
                }
            }

            return result.ToString();
        }

        private string ProcessFile(RepositoryFile file)
        {
            var multiLineStore = new Dictionary<string, string>();
            var result = string.Empty;

            var lines = file.Content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                result += ProcessLine(file.Name, line, multiLineStore);
            }

            return result;
        }

        private string ProcessLine(string fileName, string line, Dictionary<string, string> multiLineStore)
        {
            var lineResult = string.Empty;

            foreach (var lineParser in LineParsers)
            {
                var key = lineParser.GetType().Name;
                var existingLines = GetParsersExistingLines(key, multiLineStore);

                var parseResult = lineParser.Parse(existingLines + line);

                lineResult += SetComment(parseResult, fileName);
                multiLineStore[key] = SetMultiLine(parseResult, line, existingLines);
            }

            return lineResult;
        }

        private static string GetParsersExistingLines(string key, Dictionary<string, string> multiLineStore)
        {
            var result = string.Empty;

            if (multiLineStore.ContainsKey(key))
            {
                result = multiLineStore[key];
            }

            return result;
        }

        private string SetComment(ParseResult parseResult, string fileName)
        {
            var result = string.Empty;

            if (parseResult.CommentStatus == CommentStatus.Finished)
            {
                //TODO: should be an entity with line number, method name etc.
                // quick and dirty formatting for now ;-)
                result = $"{fileName}: {parseResult.Comment}{Environment.NewLine}";
            }

            return result;
        }

        private string SetMultiLine(ParseResult parseResult, string currentLine, string existingLines)
        {
            if (parseResult.CommentStatus == CommentStatus.Unfinished)
            {
                return existingLines + currentLine;
            }

            return string.Empty;
        }

    }
}
