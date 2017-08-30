using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace dockerfile {
    public class Dockerfile {
        public Comment[] Comments { set; get; }
        public Instruction[] Instructions { set; get; }
        public Directive[] Directives { set; get; }

        public Dockerfile(Instruction[] instructions, Comment[] comments, Directive[] directives=null) {
            Instructions = instructions;
            Comments = comments;
            Directives = directives;
        }

        public string Contents() {
            var b = new StringBuilder();
            if (Directives != null) {
                foreach(var dir in Directives) {
                    b.Append(dir.Generate());
                    b.Append('\n');
                }
            }
            if (Instructions != null) {
                foreach(var inst in Instructions) {
                    b.Append(inst.ToString());
                    b.Append('\n');
                }
            }
            return b.ToString();
        }

        public static Dockerfile Parse(TextReader reader) {
            var t = ParseAsync(reader);
            return t.GetAwaiter().GetResult();
        }

        public static async Task<Dockerfile> ParseAsync(TextReader reader) {
            var instructions = new List<Instruction>();
            var comments = new List<Comment>();
            Directive escape = null;
            int number = 0;
            for (string line = await reader.ReadLineAsync();
                 line != null;
                 line = await reader.ReadLineAsync()) {
                line.Trim();
                number++;
                if (line.Length == 0) {
                    continue;                    
                }
                if (line[0] == '#') {
                    comments.Add(new Comment(line, number));
                    continue;
                }
                if (line.StartsWith("# escape=")) {
                    var parts = line.Substring(2).Trim().Split(new []{'='}, 2);
                    escape = Directive.Parse(line.Substring(2).Trim(), number);
                    continue;
                }
                instructions.Add(Instruction.Parse(line, number));
            }
            return new Dockerfile(instructions.ToArray(), comments.ToArray(), escape != null ? new []{escape} : null);
        }
    }
}