const { test, expect } = require('@playwright/test');

test.describe('Smoke Test Suite', () => {
  test('Critical Path: Register, Login, Use Main Feature', async ({ page }) => {
    // Registration
    await page.goto('/register');
    await page.fill('#username', 'smokeuser');
    await page.fill('#email', 'smoke@example.com');
    await page.fill('#password', 'smokepass123');
    await page.click('#register-button');
    await expect(page).toHaveURL('/dashboard');

    // Login (after logout)
    await page.click('#logout-button');
    await page.goto('/login');
    await page.fill('#username', 'smokeuser');
    await page.fill('#password', 'smokepass123');
    await page.click('#login-button');
    await expect(page).toHaveURL('/dashboard');

    // Use Main Feature
    await page.click('#create-new-item');
    await page.fill('#item-name', 'Smoke Test Item');
    await page.click('#save-item');
    await expect(page.locator('#item-list')).toContainText('Smoke Test Item');
  });

  // Additional smoke tests for other critical paths
});