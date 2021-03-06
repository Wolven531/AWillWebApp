# AWillWebApp

This repository makes use of several modern frontend and backend technologies and can be used as a testing / starting point for those looking to experiment with said technologies.

## Technologies / Frameworks

* dotnet core (`2.1.4xx`)
* Cypress (`3.1.0`)
* NodeJS (`10.10.0`)
* NPM (`6.4.1`)
* React (`16.4.2`) (Typescript variant)
* Redux (`4.0.0`)
* Visual Studio Code (for tasks)

## Current Code Coverage

![Combined Coverage](/AWillWebApp.Tests/coverage-reports/badge_combined.svg) Combined Coverage (SVG that animates between Branch and Line)

![Branch Coverage](/AWillWebApp.Tests/coverage-reports/badge_branchcoverage.svg) Branch Coverage

![Line Coverage](/AWillWebApp.Tests/coverage-reports/badge_linecoverage.svg) Line Coverage

## Setup

* Install Chocolatey (instructions [here](https://chocolatey.org/docs/installation#installing-chocolatey))
* Install NodeJS: `choco install nodejs -y`
* Update NPM: `npm i -g npm@latest`
* Install frontend dependencies: `npm install` from `/AWillWebApp/ClientApp/`
* Install backend dependencies `dotnet restore` from `/`
* Trust local certs for HTTPS: `dotnet dev-certs https --trust`

## First Run

When visiting [https://localhost:5001](https://localhost:5001) for the first time on a machine, you will likely get a security exception.

![localhost HTTPS security exception](/repo_images/invalid_cert.png)

Make sure to add `https://localhost:5001` to the trusted list of exceptions for that machine, otherwise the site will be unusable on that machine (screenshot below is from Firefox).

![add cert exception](/repo_images/add_cert_exception.png)

## Running

The easiest way to run any part of this project is to use the Visual Studio Code editor (available [here](https://code.visualstudio.com/)) and its task runner. The following tasks are defined in `/.vscode/tasks.json`:

* `Build dotnet app` - Use the dotnet core CLI to compile the application
* `Run dotnet app` - Use the dotnet core CLI to run the application (runs insecure HTTP on port `5000`, secure HTTPS on `5001`)
* `Run webpack watcher` - Use the webpack CLI to track file changes for the React app and recompile on change (watches `/AWillWebApp/AWillWebApp/ClientApp/*`)
* `Get C# Code Coverage` - Use the dotnet core CLI (dotnet test) to run tests and collect code coverage (outputs in opencover format to `/AWillWebApp/AWillWebApp.Tests/coverage.opencover.xml`)
* `Run all coverage conversions` - Use the dotnet core CLI and ReportGenerator tool to convert the opencover coverage report to a more human-friendly HTML report
  * **NOTE**: This task requires you have the ReportGenerator tool installed. You can run the `Install dotnet ReportGenerator` command listed in the `Commands` section below or see further instructions [here](https://danielpalme.github.io/ReportGenerator/usage.html)
* `View coverage report in browser` - Launch the system default browser to the local file URL where the HTML report resides
  * **NOTE**: Not all of the report files are tracked in the repository, so make sure to run the `Convert coverage to report` task prior to this task

### Commands

#### Install dotnet ReportGenerator

```DOS
dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.0.0-rc5
```
