namespace dockerfile
{
    public class EmptyLine : DockerfileLine
    {
        public EmptyLine()
        {
            Type = LineType.Empty;
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
