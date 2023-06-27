using CommandLine;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;

namespace dotnet_ai
{
    public class Options
    {
        [Option('q', "query", Required = true, HelpText = "Query to request the program to run.")]
        public string Query { get; set; }
    }

    internal class Program
    {
        static async Task Handle(string query)
        {
            IKernel kernel = KernelBuilder.Create();
            string model = "gpt-3.5-turbo-16k";
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("An OPENAI API Key must be added as an environment variable of name: OPENAI_API_KEY");
            }

            string orgId = "";
            kernel.Config.AddOpenAIChatCompletionService(model, apiKey, orgId);

            var chatGPT = kernel.GetService<IChatCompletion>();
            string skPrompt = @"""
=======
Documentation 
=======

Usage: dotnet [runtime-options] [path-to-application] [arguments]

Execute a .NET application.

runtime-options:
  --additionalprobingpath <path>   Path containing probing policy and assemblies to probe for.
  --additional-deps <path>         Path to additional deps.json file.
  --depsfile                       Path to <application>.deps.json file.
  --fx-version <version>           Version of the installed Shared Framework to use to run the application.
  --roll-forward <setting>         Roll forward to framework version  (LatestPatch, Minor, LatestMinor, Major, LatestMajor, Disable).
  --runtimeconfig                  Path to <application>.runtimeconfig.json file.

path-to-application:
  The path to an application .dll file to execute.

Usage: dotnet [sdk-options] [command] [command-options] [arguments]

Execute a .NET SDK command.

sdk-options:
  -d|--diagnostics  Enable diagnostic output.
  -h|--help         Show command line help.
  --info            Display .NET information.
  --list-runtimes   Display the installed runtimes.
  --list-sdks       Display the installed SDKs.
  --version         Display .NET SDK version in use.

SDK commands:
  add               Add a package or reference to a .NET project.
  build             Build a .NET project.
  build-server      Interact with servers started by a build.
  clean             Clean build outputs of a .NET project.
  format            Apply style preferences to a project or solution.
  help              Show command line help.
  list              List project references of a .NET project.
  msbuild           Run Microsoft Build Engine (MSBuild) commands.
  new               Create a new .NET project or file.
  nuget             Provides additional NuGet commands.
  pack              Create a NuGet package.
  publish           Publish a .NET project for deployment.
  remove            Remove a package or reference from a .NET project.
  restore           Restore dependencies specified in a .NET project.
  run               Build and run a .NET project output.
  sdk               Manage .NET SDK installation.
  sln               Modify Visual Studio solution files.
  store             Store the specified assemblies in the runtime package store.
  test              Run unit tests using the test runner specified in a .NET project.
  tool              Install or manage tools that extend the .NET experience.
  vstest            Run Microsoft Test Engine (VSTest) commands.
  workload          Manage optional workloads.

Additional commands from bundled tools:
  dev-certs         Create and manage development certificates.
  fsi               Start F# Interactive / execute F# scripts.
  user-jwts         Manage JSON Web Tokens in development.
  user-secrets      Manage development user secrets.
  watch             Start a file watcher that runs a command when files change.

 `dotnet add package` - Adds or updates a package reference in a project file.    ```dotnet add [<PROJECT>] package <PACKAGE_NAME>     [-f|--framework <FRAMEWORK>] [--interactive]     [-n|--no-restore] [--package-directory <PACKAGE_DIRECTORY>]     [--prerelease] [-s|--source <SOURCE>] [-v|--version <VERSION>]  dotnet add package -h|--help ``` 
 `dotnet add reference` - Adds project-to-project (P2P) references.    ```dotnet add [<PROJECT>] reference [-f|--framework <FRAMEWORK>]      [--interactive] <PROJECT_REFERENCES>  dotnet add reference -h|--help ``` 
 `dotnet build-server` - Interacts with servers started by a build.    ```dotnet build-server shutdown [--msbuild] [--razor] [--vbcscompiler]  dotnet build-server shutdown -h|--help  dotnet build-server -h|--help ```  ## Commands  - **`shutdown`**    Shuts down build servers that are started from dotnet. By default, all servers are shut down.  ## Options  [!INCLUDE [help](../../../includes/cli-help.md)]  - **`--msbuild`**    Shuts down the MSBuild build server.  - **`--razor`**    Shuts down the Razor build server.  - **`--vbcscompiler`**    Shuts down the VB/C# compiler build server.
 `dotnet build` - Builds a project and all of its dependencies.    ```dotnet build [<PROJECT>|<SOLUTION>] [-a|--arch <ARCHITECTURE>]     [-c|--configuration <CONFIGURATION>] [-f|--framework <FRAMEWORK>]     [--force] [--interactive] [--no-dependencies] [--no-incremental]     [--no-restore] [--nologo] [--no-self-contained] [--os <OS>]     [-o|--output <OUTPUT_DIRECTORY>] [-r|--runtime <RUNTIME_IDENTIFIER>]     [--self-contained [true|false]] [--source <SOURCE>] [--tl [auto|on|off]]     [--use-current-runtime, --ucr [true|false]] [-v|--verbosity <LEVEL>]     [--version-suffix <VERSION_SUFFIX>]  dotnet build -h|--help ``` 
 `dotnet clean` - Cleans the output of a project.    ```dotnet clean [<PROJECT>|<SOLUTION>] [-c|--configuration <CONFIGURATION>]     [-f|--framework <FRAMEWORK>] [--interactive]     [--nologo] [-o|--output <OUTPUT_DIRECTORY>]     [-r|--runtime <RUNTIME_IDENTIFIER>] [-v|--verbosity <LEVEL>]  dotnet clean -h|--help ``` 
 `dotnet dev-certs` - Generates a self-signed certificate to enable HTTPS use in development.    ```dotnet dev-certs https    [-c|--check] [--clean] [-ep|--export-path <PATH>]   [--format] [-i|--import] [-np|--no-password]   [-p|--password] [-q|--quiet] [-t|--trust]   [-v|--verbose] [--version]  dotnet dev-certs https -h|--help ``` 
 `dotnet format` - Formats code to match `editorconfig` settings.    ```dotnet format [options] [<PROJECT | SOLUTION>]  dotnet format -h|--help ``` 
 `dotnet help` - Shows more detailed documentation online for the specified command.    ```dotnet help <COMMAND_NAME> [-h|--help] ``` 
 `dotnet list package` - Lists the package references for a project or solution.    ```dotnet list [<PROJECT>|<SOLUTION>] package [--config <SOURCE>]     [--deprecated]     [--framework <FRAMEWORK>] [--highest-minor] [--highest-patch]     [--include-prerelease] [--include-transitive] [--interactive]     [--outdated] [--source <SOURCE>] [-v|--verbosity <LEVEL>]     [--vulnerable]     [--format <console|json>]     [--output-version <VERSION>]  dotnet list package -h|--help ``` 
 `dotnet list reference` - Lists project-to-project references.    ```dotnet list [<PROJECT>] reference  dotnet list -h|--help ``` 
 `dotnet migrate` - Migrates a Preview 2 .NET Core project to a .NET Core SDK-style project.    ```dotnet migrate [<SOLUTION_FILE|PROJECT_DIR>] [--format-report-file-json <REPORT_FILE>]     [-r|--report-file <REPORT_FILE>] [-s|--skip-project-references [Debug|Release]]     [--skip-backup] [-t|--template-file <TEMPLATE_FILE>] [-v|--sdk-package-version]     [-x|--xproj-file]  dotnet migrate -h|--help ``` 
 `dotnet msbuild` - Builds a project and all of its dependencies. Note: A solution or project file may need to be specified if there are multiple.    ```dotnet msbuild <MSBUILD_ARGUMENTS>  dotnet msbuild -h ``` 
 `dotnet new install` - installs a template package.    ```dotnet new install <PATH|NUGET_ID>  [--interactive] [--add-source|--nuget-source <SOURCE>] [--force]      [-d|--diagnostics] [--verbosity <LEVEL>] [-h|--help] ``` 
 `dotnet new list` -  Lists available templates to be run using `dotnet new`.    ```dotnet new list [<TEMPLATE_NAME>] [--author <AUTHOR>] [-lang|--language {""C#""|""F#""|VB}]     [--tag <TAG>] [--type <TYPE>] [--columns <COLUMNS>] [--columns-all]     [-o|--output <output>] [--project <project>] [--ignore-constraints]     [-d|--diagnostics] [--verbosity <LEVEL>] [-h|--help] ``` 
 `dotnet new search` - searches for the templates supported by `dotnet new` on NuGet.org.    ```dotnet new search <TEMPLATE_NAME>  dotnet new search [<TEMPLATE_NAME>] [--author <AUTHOR>] [-lang|--language {""C#""|""F#""|VB}]     [--package <PACKAGE>] [--tag <TAG>] [--type <TYPE>]     [--columns <COLUMNS>] [--columns-all]     [-d|--diagnostics] [--verbosity <LEVEL>] [-h|--help] ``` 
 `dotnet new uninstall` - uninstalls a template package.    ```dotnet new uninstall <PATH|NUGET_ID>      [-d|--diagnostics] [--verbosity <LEVEL>] [-h|--help] ``` 
 `dotnet new update` - updates installed template packages.    ```dotnet new update [--interactive] [--add-source|--nuget-source <SOURCE>]      [-d|--diagnostics] [--verbosity <LEVEL>] [-h|--help]  dotnet new update --check-only|--dry-run [--interactive] [--add-source|--nuget-source <SOURCE>]      [-d|--diagnostics] [--verbosity <LEVEL>] [-h|--help] ``` 
 `dotnet new` - Creates a new project, configuration file, or solution based on the specified template.    ```dotnet new <TEMPLATE> [--dry-run] [--force] [-lang|--language {""C#""|""F#""|VB}]     [-n|--name <OUTPUT_NAME>] [-f|--framework <FRAMEWORK>] [--no-update-check]     [-o|--output <OUTPUT_DIRECTORY>] [--project <PROJECT_PATH>]     [-d|--diagnostics] [--verbosity <LEVEL>] [Template options]  dotnet new -h|--help ``` 
 `dotnet nuget add source` - Add a NuGet source.    ```dotnet nuget add source <PACKAGE_SOURCE_PATH> [--name <SOURCE_NAME>] [--username <USER>]     [--password <PASSWORD>] [--store-password-in-clear-text]     [--valid-authentication-types <TYPES>] [--configfile <FILE>]  dotnet nuget add source -h|--help ```  
 `dotnet nuget delete` - Deletes or unlists a package from the server.    ```dotnet nuget delete [<PACKAGE_NAME> <PACKAGE_VERSION>] [--force-english-output]     [--interactive] [-k|--api-key <API_KEY>] [--no-service-endpoint]     [--non-interactive] [-s|--source <SOURCE>]  dotnet nuget delete -h|--help ``` 
 `dotnet nuget disable source` - Disable a NuGet source.    ```dotnet nuget disable source <NAME> [--configfile <FILE>]  dotnet nuget disable source -h|--help ``` 
 `dotnet nuget enable source` - Enable a NuGet source.    ```dotnet nuget enable source <NAME> [--configfile <FILE>]  dotnet nuget enable source -h|--help ``` 
 `dotnet nuget list source` - Lists all configured NuGet sources.    ```dotnet nuget list source [--format [Detailed|Short]] [--configfile <FILE>]  dotnet nuget list source -h|--help ``` 
 `dotnet nuget locals` - Clears or lists local NuGet resources.    ```dotnet nuget locals <CACHE_LOCATION> [(-c|--clear)|(-l|--list)] [--force-english-output]  dotnet nuget locals -h|--help ``` 
 `dotnet nuget push` - Pushes a package to the server and publishes it.    ```dotnet nuget push [<ROOT>] [-d|--disable-buffering] [--force-english-output]     [--interactive] [-k|--api-key <API_KEY>] [-n|--no-symbols]     [--no-service-endpoint] [-s|--source <SOURCE>] [--skip-duplicate]     [-sk|--symbol-api-key <API_KEY>] [-ss|--symbol-source <SOURCE>]     [-t|--timeout <TIMEOUT>]  dotnet nuget push -h|--help ``` 
 `dotnet nuget remove source` - Remove a NuGet source.    ```dotnet nuget remove source <NAME> [--configfile <FILE>]  dotnet nuget remove source -h|--help ``` 
 `dotnet nuget sign` - Signs all the NuGet packages matching the first argument with a certificate.    ```dotnet nuget sign [<package-path(s)>]     [--certificate-path <PATH>]     [--certificate-store-name <STORENAME>]     [--certificate-store-location <STORELOCATION>]     [--certificate-subject-name <SUBJECTNAME>]     [--certificate-fingerprint <FINGERPRINT>]     [--certificate-password <PASSWORD>]     [--hash-algorithm <HASHALGORITHM>]     [-o|--output <OUTPUT DIRECTORY>]     [--overwrite]     [--timestamp-hash-algorithm <HASHALGORITHM>]     [--timestamper <TIMESTAMPINGSERVER>]     [-v|--verbosity <LEVEL>]  dotnet nuget sign -h|--help ``` 
 `dotnet nuget trust` - Gets or sets trusted signers to the NuGet configuration.    ```dotnet nuget trust [command] [Options]  dotnet nuget trust -h|--help ``` 
 `dotnet nuget update source` - Update a NuGet source.    ```dotnet nuget update source <NAME> [--source <SOURCE>] [--username <USER>]     [--password <PASSWORD>] [--store-password-in-clear-text]     [--valid-authentication-types <TYPES>] [--configfile <FILE>]  dotnet nuget update source -h|--help ``` 
 `dotnet nuget verify` - Verifies a signed NuGet package.    ```dotnet nuget verify [<package-path(s)>]     [--all]     [--certificate-fingerprint <FINGERPRINT>]     [-v|--verbosity <LEVEL>]     [--configfile <FILE>]  dotnet nuget verify -h|--help ``` 
 `dotnet pack` - Packs the code into a NuGet package.    ```dotnet pack [<PROJECT>|<SOLUTION>] [-c|--configuration <CONFIGURATION>]     [--force] [--include-source] [--include-symbols] [--interactive]     [--no-build] [--no-dependencies] [--no-restore] [--nologo]     [-o|--output <OUTPUT_DIRECTORY>] [--runtime <RUNTIME_IDENTIFIER>]     [-s|--serviceable] [-v|--verbosity <LEVEL>]     [--version-suffix <VERSION_SUFFIX>]  dotnet pack -h|--help ``` 
 `dotnet publish` - Publishes the application and its dependencies to a folder for deployment to a hosting system.    ```dotnet publish [<PROJECT>|<SOLUTION>] [-a|--arch <ARCHITECTURE>]     [-c|--configuration <CONFIGURATION>]     [-f|--framework <FRAMEWORK>] [--force] [--interactive]     [--manifest <PATH_TO_MANIFEST_FILE>] [--no-build] [--no-dependencies]     [--no-restore] [--nologo] [-o|--output <OUTPUT_DIRECTORY>]     [--os <OS>] [-r|--runtime <RUNTIME_IDENTIFIER>]     [--sc|--self-contained [true|false]] [--no-self-contained]     [-s|--source <SOURCE>] [--use-current-runtime, --ucr [true|false]]     [-v|--verbosity <LEVEL>] [--version-suffix <VERSION_SUFFIX>]  dotnet publish -h|--help ``` 
 `dotnet remove package` - Removes package reference from a project file.    ```dotnet remove [<PROJECT>] package <PACKAGE_NAME>  dotnet remove package -h|--help ``` 
 `dotnet remove reference` - Removes project-to-project (P2P) references.    ```dotnet remove [<PROJECT>] reference [-f|--framework <FRAMEWORK>]      <PROJECT_REFERENCES>  dotnet remove reference -h|--help ``` 
 `dotnet restore` - Restores the dependencies and tools of a project.    ```dotnet restore [<ROOT>] [--configfile <FILE>] [--disable-parallel]     [-f|--force] [--force-evaluate] [--ignore-failed-sources]     [--interactive] [--lock-file-path <LOCK_FILE_PATH>] [--locked-mode]     [--no-cache] [--no-dependencies] [--packages <PACKAGES_DIRECTORY>]     [-r|--runtime <RUNTIME_IDENTIFIER>] [-s|--source <SOURCE>]     [--use-current-runtime, --ucr [true|false]] [--use-lock-file]     [-v|--verbosity <LEVEL>]  dotnet restore -h|--help ``` 
 `dotnet run` - Runs source code without any explicit compile or launch commands.    ```dotnet run [-a|--arch <ARCHITECTURE>] [-c|--configuration <CONFIGURATION>]     [-f|--framework <FRAMEWORK>] [--force] [--interactive]     [--launch-profile <NAME>] [--no-build]     [--no-dependencies] [--no-launch-profile] [--no-restore]     [--os <OS>] [--project <PATH>] [-r|--runtime <RUNTIME_IDENTIFIER>]     [-v|--verbosity <LEVEL>] [[--] [application arguments]]  dotnet run -h|--help ``` 
 `dotnet sdk check` - Lists the latest available version of the .NET SDK and .NET Runtime, for each feature band.    ```dotnet sdk check  dotnet sdk check -h|--help ``` 
 `dotnet sln` - Lists or modifies the projects in a .NET solution file.    ```dotnet sln [<SOLUTION_FILE>] [command]  dotnet sln [command] -h|--help ``` 
 `dotnet store` - Stores the specified assemblies in the [runtime package store](../deploying/runtime-store.md).    ```dotnet store -m|--manifest <PATH_TO_MANIFEST_FILE>     -f|--framework <FRAMEWORK_VERSION> -r|--runtime <RUNTIME_IDENTIFIER>     [--framework-version <FRAMEWORK_VERSION>] [--output <OUTPUT_DIRECTORY>]     [--skip-optimization] [--skip-symbols] [-v|--verbosity <LEVEL>]     [--working-dir <WORKING_DIRECTORY>]  dotnet store -h|--help ``` 
 `dotnet test` - .NET test driver used to execute unit tests.    ```dotnet test [<PROJECT> | <SOLUTION> | <DIRECTORY> | <DLL> | <EXE>]     [--test-adapter-path <ADAPTER_PATH>]      [-a|--arch <ARCHITECTURE>]     [--blame]     [--blame-crash]     [--blame-crash-dump-type <DUMP_TYPE>]     [--blame-crash-collect-always]     [--blame-hang]     [--blame-hang-dump-type <DUMP_TYPE>]     [--blame-hang-timeout <TIMESPAN>]     [-c|--configuration <CONFIGURATION>]     [--collect <DATA_COLLECTOR_NAME>]     [-d|--diag <LOG_FILE>]     [-f|--framework <FRAMEWORK>]     [-e|--environment <NAME=""VALUE"">]     [--filter <EXPRESSION>]     [--interactive]     [-l|--logger <LOGGER>]     [--no-build]     [--nologo]     [--no-restore]     [-o|--output <OUTPUT_DIRECTORY>]     [--os <OS>]     [--results-directory <RESULTS_DIR>]     [-r|--runtime <RUNTIME_IDENTIFIER>]     [-s|--settings <SETTINGS_FILE>]     [-t|--list-tests]     [-v|--verbosity <LEVEL>]     [<args>...]     [[--] <RunSettings arguments>]  dotnet test -h|--help ``` 
 `dotnet tool install` - Installs the specified [.NET tool](global-tools.md) on your machine.    ```dotnet tool install <PACKAGE_NAME> -g|--global     [--add-source <SOURCE>] [--configfile <FILE>] [--disable-parallel]     [--framework <FRAMEWORK>] [--ignore-failed-sources] [--interactive]     [--no-cache] [--prerelease]     [--tool-manifest <PATH>] [-v|--verbosity <LEVEL>]     [--version <VERSION_NUMBER>]  dotnet tool install <PACKAGE_NAME> --tool-path <PATH>     [--add-source <SOURCE>] [--configfile <FILE>] [--disable-parallel]     [--framework <FRAMEWORK>] [--ignore-failed-sources] [--interactive]     [--no-cache] [--prerelease]     [--tool-manifest <PATH>] [-v|--verbosity <LEVEL>]     [--version <VERSION_NUMBER>]  dotnet tool install <PACKAGE_NAME> [--local]     [--add-source <SOURCE>] [--configfile <FILE>]     [--create-manifest-if-needed] [--disable-parallel]     [--framework <FRAMEWORK>] [--ignore-failed-sources] [--interactive]     [--no-cache] [--prerelease]     [--tool-manifest <PATH>] [-v|--verbosity <LEVEL>]     [--version <VERSION_NUMBER>]  dotnet tool install -h|--help ``` 
 `dotnet tool list` - Lists all [.NET tools](global-tools.md) of the specified type currently installed on your machine.    ```dotnet tool list -g|--global  dotnet tool list --tool-path <PATH>  dotnet tool list --local  dotnet tool list  dotnet tool list -h|--help ``` 
 `dotnet tool restore` - Installs the .NET local tools that are in scope for the current directory.    ```dotnet tool restore     [--configfile <FILE>] [--add-source <SOURCE>]     [--tool-manifest <PATH_TO_MANIFEST_FILE>] [--disable-parallel]     [--ignore-failed-sources] [--no-cache] [--interactive]     [-v|--verbosity <LEVEL>]  dotnet tool restore -h|--help ``` 
 `dotnet tool run` - Invokes a local tool.    ```dotnet tool run <COMMAND NAME>  dotnet tool run -h|--help ``` 
 `dotnet tool search` - Searches all [.NET tools](global-tools.md) that are published to NuGet.    ```dotnet tool search [--detail]  [--prerelease]     [--skip <NUMBER>] [--take <NUMBER>] <SEARCH TERM>  dotnet tool search -h|--help ``` 
 `dotnet tool uninstall` - Uninstalls the specified [.NET tool](global-tools.md) from your machine.    ```dotnet tool uninstall <PACKAGE_NAME> -g|--global  dotnet tool uninstall <PACKAGE_NAME> --tool-path <PATH>  dotnet tool uninstall <PACKAGE_NAME>  dotnet tool uninstall -h|--help ``` 
 `dotnet tool update` - Updates the specified [.NET tool](global-tools.md) on your machine.    ```dotnet tool update <PACKAGE_ID> -g|--global     [--add-source <SOURCE>] [--configfile <FILE>]     [--disable-parallel] [--framework <FRAMEWORK>]     [--ignore-failed-sources] [--interactive]     [--no-cache] [--prerelease]     [-v|--verbosity <LEVEL>] [--version <VERSION>]  dotnet tool update <PACKAGE_ID> --tool-path <PATH>     [--add-source <SOURCE>] [--configfile <FILE>]     [--disable-parallel] [--framework <FRAMEWORK>]     [--ignore-failed-sources] [--interactive]      [--no-cache] [--prerelease]     [-v|--verbosity <LEVEL>] [--version <VERSION>]  dotnet tool update <PACKAGE_ID> --local     [--add-source <SOURCE>] [--configfile <FILE>]     [--disable-parallel] [--framework <FRAMEWORK>]     [--ignore-failed-sources] [--interactive]     [--no-cache] [--prerelease]     [--tool-manifest <PATH>]     [-v|--verbosity <LEVEL>] [--version <VERSION>]  dotnet tool update -h|--help ``` 
 `dotnet watch` - Restarts or [hot reloads](#hot-reload) the specified application, or runs a specified dotnet command, when changes in source code are detected.    ```dotnet watch [<command>]   [--list]   [--no-hot-reload] [--non-interactive]   [--project <PROJECT>]   [-q|--quiet] [-v|--verbose]   [--version]   [--] <forwarded arguments>   dotnet watch -?|-h|--help ``` 
 `dotnet workload install` - Installs optional workloads.    ```dotnet workload install <WORKLOAD_ID>...     [--configfile <FILE>] [--disable-parallel]     [--ignore-failed-sources] [--include-previews] [--interactive]     [--no-cache] [--skip-manifest-update]     [--source <SOURCE>] [--temp-dir <PATH>] [-v|--verbosity <LEVEL>]  dotnet workload install -?|-h|--help ``` 
 `dotnet workload list` - Lists installed workloads.    ```dotnet workload list [-v|--verbosity <LEVEL>]  dotnet workload list [-?|-h|--help] ``` 
 `dotnet workload repair` - Repairs workloads installations.    ```dotnet workload repair     [--configfile] [--disable-parallel] [--ignore-failed-sources]     [--interactive] [--no-cache]     [-s|--source <SOURCE>] [--temp-dir <PATH>]     [-v|--verbosity <LEVEL>]  dotnet workload repair -?|-h|--help ``` 
 `dotnet workload restore` - Installs workloads needed for a project or a solution.    ```dotnet workload restore [<PROJECT | SOLUTION>]     [--configfile <FILE>] [--disable-parallel]     [--ignore-failed-sources] [--include-previews] [--interactive]     [--no-cache] [--skip-manifest-update]     [-s|--source <SOURCE>] [--temp-dir <PATH>] [-v|--verbosity <LEVEL>]  dotnet workload restore -?|-h|--help ``` 
 `dotnet workload search` - Searches for optional workloads.    ```dotnet workload search [<SEARCH_STRING>] [-v|--verbosity <LEVEL>]  dotnet workload search -?|-h|--help ``` 
 `dotnet workload uninstall` - Uninstalls a specified workload.    ```dotnet workload uninstall <WORKLOAD_ID...>  dotnet workload uninstall -?|-h|--help ``` 
 `dotnet workload update` - Updates installed workloads.    ```dotnet workload update     [--advertising-manifests-only]     [--configfile <FILE>] [--disable-parallel]     [--from-previous-sdk] [--ignore-failed-sources]     [--include-previews] [--interactive] [--no-cache]     [-s|--source <SOURCE>] [--temp-dir <PATH>]     [-v|--verbosity <LEVEL>]  dotnet workload update -?|-h|--help ``` 
 `dotnet workload` - Provides information about the available workload commands and installed workloads.    ```dotnet workload [--info]  dotnet workload -?|-h|--help ``` 
""";

            var systemMessage = $"You are a bot that generates a correctly formatted JSON list with dotnet sdk commands and code based on the following documentation: {skPrompt}.";
            var chat = (OpenAIChatHistory)chatGPT.CreateNewChat(systemMessage);
            chat.AddUserMessage("Generate a list of all steps for the following query using the information above - the output should be descriptive, concise and contain the correct commands from the aforementioned documentation: " +
                $"Query: {query}");

            string assistantReply = await chatGPT.GenerateMessageAsync(chat, new ChatRequestSettings() { MaxTokens = 10000, Temperature = 0.0 });
            chat.AddAssistantMessage(assistantReply);
            Console.WriteLine(assistantReply);
        }

        static async Task Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            await result.MapResult(async o => { await Handle(o.Query); }, errors => Task.FromResult(errors));
        }
    }
}