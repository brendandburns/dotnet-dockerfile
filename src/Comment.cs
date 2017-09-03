namespace dockerfile
{
    public class Comment : DockerfileLine
    {
        public string Line { set; get; }

        public Comment(string line)
        {
            Type = LineType.Comment;
            Line = line;
        }

        public override string ToString()
        {
            return "# " + Line;
        }
    }
}