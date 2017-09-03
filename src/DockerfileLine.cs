namespace dockerfile
{
    public enum LineType
    {
        Empty,
        Comment,
        Directive,
        Instruction
    }

    public abstract class DockerfileLine
    {
        public LineType Type { get; set; }
        public int LineNumber { get; set; }

        public override abstract string ToString();
    }
}
