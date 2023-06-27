# dotnet-ai

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
dotnet run -- --query "Create an application that prints Hello World"
```