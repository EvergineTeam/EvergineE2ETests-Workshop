/* eslint-disable testing-library/prefer-screen-queries */
import { test, expect } from '@playwright/test';

test.describe('Teapot tests', () => {
  test('change color', async ({ page }) => {
    await page.goto('http://localhost:3000/');
    await page.getByRole('button', { name: 'From React to Wasm' }).click();
    await expect(page.getByRole('textbox', { name: 'result' })).toHaveValue(/Changing the color of the teapot: #[0-9A-F]{6}/);
  });

  test('get current rotation', async ({ page }) => {
    await page.goto('http://localhost:3000/');
    await page.locator('#evergine-canvas').click({ position: { x: 112, y: 79 } });
    await expect(page.getByRole('textbox', { name: 'result' })).toHaveValue(/Teapot current rotation: \d+º/);
  });
});

