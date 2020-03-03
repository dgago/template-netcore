# Introduction
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project.

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Create a template with "dotnet new" command
1. To add the project to the "dotnet new" command list, run the following command from the solution folder: “dotnet new -i .”
2. Create a new folder where the project will be created.
3. From this new folder, run the following command: "dotnet new Project --projectName <value>".

# Build and Test
TODO: Describe and show how to build your code and run the tests.

# Adding a Migration

Set aspnetcore environment to "Local":

**Using Powershell**
```powershell
$Env:ASPNETCORE_ENVIRONMENT="Local"
```

**Using Terminal**
```powershell
set ASPNETCORE_ENVIRONMENT=Local
```

**Run EF Migration tool**
```
 dotnet ef migrations add <name> --project src\Template.Persistence --startup-project src\Template.API
```

# Contribute
TODO: Explain how other users and developers can contribute to make your code better.

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)