# dotnet core dockerfile parser

This is a simple set of classes for parsing and generating Dockerfiles

# usage

## generation
```csharp
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
File.WriteAllText(dir + "/Dockerfile", df.Content);
```

## parsing
```csharp
var df = Dockerfile.Parse(new TextReader("Dockerfile"));
...
```

# bugs
Probably. [File issues.](https://github.com/brendandburns/dotnet-dockerfile/issues)

# nuget
```sh
dotnet add package Dockerfile --version 1.0.0
```

[Dockerfile-1.0.0](https://www.nuget.org/packages/Dockerfile/1.0.0)


