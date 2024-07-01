import { expect, test } from '@playwright/test';
test.describe('Configuration', () => {
  test('GPU hardware acceleration', async ({ page, browserName }) => {
    if (browserName !== 'chromium') {
      console.info(`       Skipping GPU hardware acceleration test in ${browserName}`);
      return;
    }

    await page.goto('chrome://gpu');
    await expect(page.locator('#content')).toContainText('* WebGL: Hardware accelerated');
  });
});
