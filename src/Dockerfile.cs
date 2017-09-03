using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dockerfile
{
    public class Dockerfile
    {

        public IList<DockerfileLine> Lines { get; set; }

        public Dockerfile()
        {
            Lines = new List<DockerfileLine>();
        }

        public Comment[] Comments
        {
            get
            {
                return Lines
                    .Where(l => l.Type == LineType.Comment)
                    .Select(l => l as Comment)
                    .ToArray();
            }
        }

        public Instruction[] Instructions
        {
            get
            {
                return Lines
                    .Where(l => l.Type == LineType.Instruction)
                    .Select(l => l as Instruction)
                    .ToArray();
            }
        }

        public Directive[] Directives
        {
            get
            {
                return Lines
                    .Where(l => l.Type == LineType.Directive)
                    .Select(l => l as Directive)
                    .ToArray();
            }
        }

        public string Content
        {
            get
            {
                var builder = new StringBuilder();
                foreach (var line in Lines)
                {
                    builder.Append(line.ToString()).AppendLine();
                }
                return builder.ToString();
            }
        }

        public Dockerfile WithLine(DockerfileLine line)
        {
            line.LineNumber = Lines.Count + 1;
            Lines.Add(line);
            return this;
        }

        public Dockerfile WithEmptyLine()
        {
            return WithLine(new EmptyLine());
        }

        public static Dockerfile Parse(TextReader reader)
        {
            var t = ParseAsync(reader);
            return t.GetAwaiter().GetResult();
        }

        public static async Task<Dockerfile> ParseAsync(TextReader reader)
        {
            var result = new Dockerfile();

            for (string line = await reader.ReadLineAsync();
                 line != null;
                 line = await reader.ReadLineAsync())
            {

                line.Trim();

                if (line.Length == 0)
                {
                    result.WithEmptyLine();
                    continue;
                }

                if (line.StartsWith("# escape="))
                {
                    var parts = line.Substring(2).Trim().Split(new[] { '=' }, 2);
                    result.WithLine(Directive.Parse(line.Substring(2).Trim()));
                    continue;
                }

                if (line[0] == '#')
                {
                    result.WithLine(new Comment(line));
                    continue;
                }

                result.WithLine(Instruction.Parse(line));
            }
            return result;
        }
    }
}