namespace dockerfile
{
    public class Instruction : DockerfileLine
    {
        public string InstructionName { set; get; }
        public string Arguments { set; get; }

        public Instruction(string name, string args = null)
        {
            Type = LineType.Instruction;
            InstructionName = name;
            Arguments = args;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", InstructionName, Arguments);
        }

        public static Instruction Parse(string line)
        {
            var pieces = line.Split(new[] { ' ' }, 2);
            if (pieces.Length == 1)
            {
                return new Instruction(pieces[0]);
            }
            return new Instruction(pieces[0], pieces[1]);
        }
    }
}

