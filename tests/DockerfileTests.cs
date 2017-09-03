using System.IO;
using System.Linq;
using dockerfile;
using Xunit;

namespace Dockerfile.Tests
{
    public class DockerfileTests
    {
        [Fact]
        public void GenerateDockerfileTest()
        {
            var df = new dockerfile.Dockerfile()
                .WithLine(new Directive("escape", "\\"))
                .WithLine(new Instruction("FROM", "debian:9"))
                .WithEmptyLine()
                .WithLine(new Comment("Some comment"))
                .WithLine(new Instruction("RUN", "apt-get update && apt-get install libunwind8 libicu57"))
                .WithEmptyLine()
                .WithLine(new Instruction("COPY", "* /scripts"))
                .WithEmptyLine()
                .WithLine(new Instruction("CMD", "/scripts/run.sh -p1 -p2"));

            string expected = File.ReadAllText(@".\TestFiles\Dockerfile");
            Assert.Equal(expected, df.Content);
        }

        [Fact]
        public void ParseDockerfileTest()
        {
            var df = dockerfile.Dockerfile.Parse(File.OpenText(@".\TestFiles\Dockerfile"));
            Assert.Single(df.Directives);
            Assert.Equal(1, df.Directives[0].LineNumber);
            Assert.Single(df.Comments);
            Assert.Equal(4, df.Comments[0].LineNumber);
            Assert.Equal(4, df.Instructions.Length);
            Assert.True(df.Instructions.Where(inc => inc.InstructionName == "FROM").Any());
            Assert.Equal(2, df.Instructions.Where(inc => inc.InstructionName == "FROM").FirstOrDefault().LineNumber);
            Assert.True(df.Instructions.Where(inc => inc.InstructionName == "RUN").Any());
            Assert.Equal(5, df.Instructions.Where(inc => inc.InstructionName == "RUN").FirstOrDefault().LineNumber);
            Assert.True(df.Instructions.Where(inc => inc.InstructionName == "COPY").Any());
            Assert.Equal(7, df.Instructions.Where(inc => inc.InstructionName == "COPY").FirstOrDefault().LineNumber);
            Assert.True(df.Instructions.Where(inc => inc.InstructionName == "CMD").Any());
            Assert.Equal(9, df.Instructions.Where(inc => inc.InstructionName == "CMD").FirstOrDefault().LineNumber);
        }
    }
}
