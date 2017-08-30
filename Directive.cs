namespace dockerfile {
    public class Directive {
        public string Name { set; get; }

        public string Value { set; get; }

        public int LineNumber { set; get; }

        public Directive(string name, string value, int lineNumber) {
            Name = name;
            Value = value;
            LineNumber = lineNumber;
        }

        public override string ToString() {
            return string.Format("{0}={1}", Name, Value);
        }

        public string Generate() {
            return string.Format("# {0}={1}", Name, Value);
        }

        public static Directive Parse(string line, int lineNumber=-1) {
            var parts = line.Split(new []{'='}, 2);
            return new Directive(parts[0], parts[1], lineNumber);
        }
    }
}