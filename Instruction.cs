namespace dockerfile
{
    public class Instruction
    {
        public Instruction(string name, string args = null, int lineNumber = -1)
        {
            InstructionName = name;
            Arguments = args;
            LineNumber = LineNumber;
        }
        public string InstructionName { set; get; }
        public string Arguments { set; get; }

        public int LineNumber { set; get; }

        public override string ToString() {
            return string.Format("{0} {1}", InstructionName, Arguments); 
        }

        public static Instruction Parse(string line, int lineNumber = -1)
        {
            var pieces = line.Split(new []{' '}, 2);
            if (pieces.Length == 1)
            {
                return new Instruction(pieces[0], lineNumber: lineNumber);
            }
            return new Instruction(pieces[0], pieces[1], lineNumber);
        }
    }
}

