# dotnet-ai

![Nuget](https://img.shields.io/nuget/v/dotnet-ai?style=flat-square)

This repo is responsible for listing steps to achieve the query requested by the user.

## Prerequisites

1. Ensure you add the OpenAI API KEY to your environment such as the following if you are running in powershell:

```
$env:OPENAI_API_KEY = ''
```

or the following if you are running in cmd:

```
set OPENAI_API_KEY = ''
```

## Usage

After building the project, you can invoke it by passing in the desired query with the --query argument:

```
.\dotnet-ai.exe --query "Create an application that prints the planets of the solar system"
```

You can execute the query, as well by passing in the --execute flag:

```
.\dotnet-ai.exe --query "Create an application that prints the planets of the solar system" --execute
```

## Example Response

```
.\dotnet-ai.exe --query "create a program that'll generate the first 30 prime numbers and also add the newtonsoft json nuget package"
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

## Next Steps

- Create a dotnet tool with this project.