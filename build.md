# Build Instructions for Project AAMM

## Introduction
This document guides you through the process of building the Project AAMM.

## Prerequisites
- .NET SDK (Version: 7.0.100) as specified in global.json.
- Ensure the .NET Entity Framework CLI tool is installed (Version: 7.0.13).

## Environment Setup
1. Clone the repository: `git clone https://github.com/MirZamanHayat/Final-Project-AAMM`.
2. Install the .NET EF tool: `dotnet tool install --global dotnet-ef --version 7.0.13`.

## Building the Project
1. Navigate to your project's root directory.
2. Run `dotnet restore` to restore the project dependencies.
3. Run `dotnet build` to build the project.
4. Note: Resolve any build errors in the test files (e.g., syntax errors in `UserServiceTests.cs`) before a successful build.

