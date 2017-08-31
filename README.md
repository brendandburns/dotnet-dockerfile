# dotnet core dockerfile parser

This is a simple set of classes for parsing and generating Dockerfiles

# usage

## generation
```csharp
var instructions = new List<Instruction>();
instructions.Add(new Instruction("FROM", "debian:9"));
instructions.Add(new Instruction("RUN", " apt-get update && apt-get install libunwind8 libicu57"));
instructions.Add(new Instruction("COPY", string.Format("* /exe/", dir)));
instructions.Add(new Instruction("CMD", string.Format("/exe/{0} {1}", exe, getArgs(args))));

var df = new Dockerfile(instructions.ToArray(), new Comment[0]);
File.WriteAllText(dir + "/Dockerfile", df.Contents());
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


