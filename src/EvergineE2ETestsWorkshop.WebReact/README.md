# WebReact Template

## Introduction

This document explains the template structure for building web applications using Evergine + React.

## Requirements

- Visual Studio 2022 or later.
- npm (<https://www.npmjs.com>), which is included with Node.js.
- .Net 6 SDK (<https://dotnet.microsoft.com/en-us/download/dotnet/6.0>).
- Wasm tools v6:
  - Check with `dotnet workload list`.
  - If .Net 7 SDK is not available, install with `dotnet workload install wasm-tools`, else install with `dotnet workload install wasm-tools-net6`.

## Structure of Projects

- EvergineE2ETestsWorkshop: common Evergine project for all platforms. Contains Evergine exclusive code like scenes, components, behaviors,...
- everginee2etestsworkshop.react.spa: SPA (Single Page Application) web project that uses the Visual Studio template for Javascript/Typescript standalone projects ([.esproj](https://learn.microsoft.com/en-us /visualstudio/javascript/javascript-in-visual-studio?view=vs-2022)) + Create React App (CRA). Slightly modified to add a small demo of Evergine in React. It is the web application that the browser will execute.
- EvergineE2ETestsWorkshop.Host: ASP.NET project that acts as a server for the Web application. It is used to deploy to production. Includes optimization to serve compressed assets.
- EvergineE2ETestsWorkshop.WebReact: BlazorWebAssembly type project that will run very fast in the browser thanks to [WebAssembly](https://webassembly.org) embedded within the SPA.

## Build and Run in Development Mode

It is recommended using Visual Studio to run the solution and modify the projects that use C#, and Visual Studio Code to modify the project that uses Typescript (everginee2etestsworkshop.react.spa). But it is also possible to build and run the solution from the command line and use any other IDE to modify the files.

### From Visual Studio

- Open EvergineE2ETestsWorkshop.WebReact.sln with Visual Studio.
- Build the whole solution.
- Select everginee2etestsworkshop.react.spa as startaup project, if necessary, and click on run button (or press F5). Note: to set the project as startup project, use the mouse right click on the project and click on the option _Set as Startup Project_.

Hot reload in React is working, so you can change something in .tsx files and see the results as soon as you save the changes. On the other hand, for the changes in C# files to take effect, it is necessary to stop, and run the solution again. The first time Visual Studio ran the solution it opened a console to start the React Development Server, so subsequent times it will start faster, as long as you do not close that console.

### From Command Line

Open your favourite terminal application and run the following commands:

```pwsh
cd Path to the solution folder\EvergineE2ETestsWorkshop
dotnet build .\EvergineE2ETestsWorkshop.WebReact\EvergineE2ETestsWorkshop.WebReact.csproj
cd everginee2etestsworkshop.react.spa
dotnet build
npm start
```

## Run Frontend Tests

Execute `npm test` in everginee2etestsworkshop.react.spa folder.

## How to Debug the Code with Visual Studio

The TypeScript code in everginee2etestsworkshop.react.spa project can be debugged at any time, just setting a breakpoint in the .tsx files. But, to debug the C# code that run in the browser thanks to WebAssembly is necessary to change the startup project to EvergineE2ETestsWorkshop.WebReact, the process is the following:

- Select everginee2etestsworkshop.react.spa as startaup project and click on run button (or press F5). This will build the code, if necessary, and start the React Development Server in a console. Do not close that console.
- Stop debugging in Visual Studio clicking on stop button (or shift+F5).
- Select EvergineE2ETestsWorkshop.WebReact as startaup project and click on run button (or press F5).
- Visual Studio will open a web browser with the URL: <https://localhost:5001>. Change the URL to: <http://localhost:3000>.
- Set some breakpoint in the C# or TypeScript code and enjoy your debugging session!

Currently, hot reload is not available in C# code.

## Build and Run in Production Mode

### From Visual Studio

- Right click on EvergineE2ETestsWorkshop project and click on Publish... to create a folder profile.
- Select Folder and click on Finish.
- Click on Publish button.
- When the process finish, click on Open folder link.
- Execute EvergineE2ETestsWorkshop.Host.exe.
- Click on <https://localhost:5001>, or copy the link and paste in a browser.

### From Command Line

Open your favourite terminal application and run the following commands:

```pwsh
cd Path to the solution folder\EvergineE2ETestsWorkshop
dotnet build .\EvergineE2ETestsWorkshop.WebReact\EvergineE2ETestsWorkshop.WebReact.csproj --configuration Release
cd EvergineE2ETestsWorkshop\Host
dotnet publish --configuration Release
cd bin\Release\net6.0\publish
.\EvergineE2ETestsWorkshop.Host.exe
```

Click on <https://localhost:5001>, or copy the link and paste in a browser.
