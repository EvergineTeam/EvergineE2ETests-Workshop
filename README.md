# Evergine.Tests.E2E Workshop

## Introduction

This workshop demonstrates how to use Playwright to write end-to-end tests for web applications using Evergine as the graphics engine.

The workshop is divided into different steps, with each step corresponding to a commit in the repository. This allows you to check out each commit and read this README to see an explanation of the changes made.

## Prerequisites

- Visual Studio 2022 17.6+
- .NET SDK 8.0 with wasm-tools workload
- Evergine Studio 2023.9.28.1837+
- Node.js 16.20.0+

Recommended:

- Visual Studio Code with the Playwright Test for VSCode extension (ms-playwright.playwright).

## 1. Creation of Evergine Project

- Create a new project with Evergine Studio using the React SPA template and call it EvergineE2ETestsWorkshop.

- Close Evergine Studio and rename the newly created EvergineE2ETestsWorkshop folder to src.

- Open the EvergineE2ETestsWorkshop.WebReact.sln solution existing in src.

- Run the solution and verify that the demo works fine by clicking on "From React to Wasm" and clicking on the teapot.

## 2. Add Playwright Tests

- Open the everginee2etestsworkshop.react.spa folder with Visual Studio Code.

- Press F1 to open the command palette and select the "Test: Install playwright" option. In the checkboxes, only select Chromium and Firefox. (Alternatively, if you don't have VSCode and the Playwright extension, you can run it from the command line: `npm init playwright@latest`).

- Test the following features from the Testing tab:
  - Run the example tests.
  - Select the execution browsers.
  - Show browser.
  - Show trace viewer.
  - Debug a test.
  - Pick locators.
  - Tune locators.
  - Record tests.

- Record two new tests for the teapot (teapot.spec.ts) opening this URL <http://localhost:3000>:
  - Change color.
  - Get current rotation.

## 3. Capture Scene State

- Comment out the code of the OnChangeColor event in EvergineDemoEvents.cs and run the tests again. Why are they still passing?

- Improve the tests to capture the actual state of the Evergine scene.
