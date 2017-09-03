namespace dockerfile
{
    public class Directive : DockerfileLine
    {
        public string Name { set; get; }

        public string Value { set; get; }

        public Directive(string name, string value)
        {
            Type = LineType.Directive;
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("# {0}={1}", Name, Value);
        }

        public static Directive Parse(string line)
        {
            var parts = line.Split(new[] { '=' }, 2);
            return new Directive(parts[0], parts[1]);
        }
    }
}