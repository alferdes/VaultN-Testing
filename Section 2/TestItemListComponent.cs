using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

[TestFixture]
public class ItemListComponentTests
{
    private IWebDriver driver;
    private WebDriverWait wait;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        driver.Navigate().GoToUrl("http://localhost:8080"); // Assuming Vue app is running here
    }

    [Test]
    public void ItemList_LoadsCorrectly_DisplaysItems()
    {
        var listButton = wait.Until(d => d.FindElement(By.Id("load-list-button")));
        Assert.IsNotNull(listButton);

        // Test load list button working
        listButton.Click();
        var itemList = wait.Until(d => d.FindElement(By.Id("item-list")));
        Assert.IsNotNull(itemList);

        var items = itemList.FindElements(By.ClassName("item"));
        Assert.IsTrue(items.Count > 0, "No items were loaded");

        // Test item details loading
        items[0].Click();
        var itemDetails = wait.Until(d => d.FindElement(By.Id("item-details")));
        Assert.IsNotNull(itemDetails, "Item details did not load after clicking");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}