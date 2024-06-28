/* eslint-disable testing-library/prefer-screen-queries */
import { test, expect, ConsoleMessage, Page } from '@playwright/test';

type TestResultDto = {
  teapotColor: string;
  teapotRotation: number;
};

test.describe('Teapot tests', () => {
  let testResult: TestResultDto | null = null;

  test.beforeEach(async ({ page }) => {
    testResult = null;
    page.on("console", captureConsole);
  });

  test.afterEach(async ({ page }) => {
    page.off("console", captureConsole);
  });

  test('change color', async ({ page }) => {
    // Arrange
    await goToPageInTestMode(page);
    
    // Act
    await page.getByRole('button', { name: 'From React to Wasm' }).click();
    
    // Assert
    const inputText = await getInputTextWithRegExp(page, /Changing the color of the teapot: #[0-9A-F]{6}FF/);
    const color = inputText.split(": ")[1];
    expect(testResult?.teapotColor).toEqual(color);
  });

  test('get current rotation', async ({ page }) => {
    // Arrange
    await goToPageInTestMode(page);
    
    // Act
    await page.locator('#evergine-canvas').click({ position: { x: 112, y: 79 } });
    
    // Assert
    const inputText = await getInputTextWithRegExp(page, /Teapot current rotation: \d+º/);
    const degrees = parseFloat(inputText.split(": ")[1].slice(0, -1));
    expect(Math.round(testResult?.teapotRotation ?? -1)).toEqual(degrees);
  });

  function captureConsole(msg: ConsoleMessage) {
    if (msg.type() === "log" && msg.text().startsWith("TestResult")) {
      testResult = JSON.parse(msg.text().replace("TestResult: ", ""));
    }
  }

  async function goToPageInTestMode(page: Page) {
    await page.goto("http://localhost:3000/");
    await page.getByRole("button", { name: "From React to Wasm" }).isEnabled();
    page.evaluate("window.App.webEventsProxy.setTestMode(true)");
  }

  async function getInputTextWithRegExp(page: Page, regExp: RegExp) {
    const inputLocator = page.getByRole("textbox", { name: "result" });
    await expect(inputLocator).toHaveValue(regExp);
    const inputText = await inputLocator.inputValue();
    return inputText;
  }
});

