namespace dockerfile {
    public class Comment {
        public string Line { set; get; }

        public int LineNumber { set; get; }

        public override string ToString() {
            return Line;
        }

        public Comment(string line, int lineNumber) {
            Line = line;
            LineNumber = lineNumber;
        }

        public string Generate() {
            return Line;
        }
    }
}