# dotnet-ai: Leveraging LLMs to Create and Run dotnet Projects Faster and Easier.

![Nuget](https://img.shields.io/nuget/v/dotnet-ai?style=flat-square)

This tool leverages Large Language Models (LLMs) to convert a user query into a series of steps for the dotnet SDK that can be optionally executed.  To be put simply: The tool takes a user query of an intended action, an optional argument to execute the steps and responds with a series of steps. 

The main purpose is to streamline and simplify the process of using the dotnet SDK to create projects by simply specifying instructions and optionally invoking them.

## Prerequisites

1. Acquire an OpenAI API KEY. [Here](https://www.howtogeek.com/885918/how-to-get-an-openai-api-key/) is a tutorial on how to do so.
2. Install the .NET6 SDK from [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). 

## Getting Started 

1. Install the dotnet-ai tool by invoking the following on your terminal: ``dotnet tool install dotnet-ai -g``.
2. Set the OpenAI API Key as an environment variable.
   1. If you are using Powershell, use: ``$env:OPENAI_API_KEY='Add Your OpenAI API Key Here'``
   2. If you are using Command Prompt, use: ``set OPENAI_API_KEY=Add Your OPEN API KEY Without Quotations.``
3. Invoke the tool: 
   1. `dotnet-ai --query "Add your query here"`: This will list the steps required.
   2. `dotnet-ai --query "Add your query here" --execute`: This will list the steps required and then execute them.
4. Examples:
   1. Handling some basic SDK operations both in the form of questions and commands:
      1. `dotnet-ai --query "Display the sdks installed" --execute`.
      2. `dotnet-ai --query "How do I add a nuget package?"`.
      3. `dotnet-ai --query "How do I uninstall a dotnet tool"`.
   2. Generating projects from simple to more complex in C#, F# or Visual Basic:
      1. `dotnet-ai --query "Create and run an application that'll print all the planets of the solar system."`.
      2. `dotnet-ai --query "Create and run an application that'll generate the first 20 Fibonacci numbers in F# in a project called 'Fib'" --execute`.
      3. `dotnet-ai --query "Create an implementation of Priority Queues in C#" --execute`.
      4. `dotnet-ai --query "Implement of Bubblesort in VB in a project called Bubblesort" --execute`.

NOTE: Some projects are too complex for the current LLM to generate code for.
    
## Getting Started With Development and Running Locally

1. Clone the repo: `git clone https://github.com/MokoSan/dotnet-ai.git`.
2. cd into the folder: `cd dotnet-ai`.
3. Build the repo: 
   1. `cd dotnet-ai`.
   2. `dotnet build`. Use `dotnet build -c Release` to build in Release mode.
4. Run the code:
   1. Running using the dotnet SDK:
      1. `dotnet run -- --query "Create an application that prints the planets of the solar system."`.
   2. Running from the binary:
      1. `cd bin`.
      2. `cd Debug` or `cd Release`.
      3. `cd net6.0`.
      4. `.\dotnet-ai.exe --query "Create an application that prints the planets of the solar system."`.

## Example Response

```
dotnet-ai --query "create a program that'll generate the first 30 prime numbers and also add the newtonsoft json nuget package"
```

**Response**:

To create a program that generates the first 30 prime numbers and add the Newtonsoft.Json NuGet package, you can follow these steps:

1. Create a new .NET project:
   ```dotnet new console -n PrimeNumberGenerator```

2. Change to the project directory:
   ```cd PrimeNumberGenerator```

3. Add the Newtonsoft.Json NuGet package:
   ```dotnet add package Newtonsoft.Json```

4. Open the Program.cs file in a text editor and replace the existing code with the following code:
   ```csharp
   using System;
   using Newtonsoft.Json;

   namespace PrimeNumberGenerator
   {
       class Program
       {
           static void Main(string[] args)
           {
               int count = 0;
               int number = 2;

               while (count < 30)
               {
                   if (IsPrime(number))
                   {
                       Console.WriteLine(number);
                       count++;
                   }

                   number++;
               }
           }

           static bool IsPrime(int number)
           {
               if (number < 2)
                   return false;

               for (int i = 2; i <= Math.Sqrt(number); i++)
               {
                   if (number % i == 0)
                       return false;
               }

               return true;
           }
       }
   }
   ```

5. Save the changes to the Program.cs file.

6. Build the project:
   ```dotnet build```

7. Run the program:
   ```dotnet run```

This will generate and display the first 30 prime numbers. The Newtonsoft.Json NuGet package has been added to the project to enable JSON serialization and deserialization capabilities.

## License

This project is licensed under the [MIT License](https://github.com/git/git-scm.com/blob/main/MIT-LICENSE.txt).

## Contributions and Issues

Contributions are most welcome! If you encounter any issues or have suggestions for improvements, please feel free to open an issue or submit a pull request.
