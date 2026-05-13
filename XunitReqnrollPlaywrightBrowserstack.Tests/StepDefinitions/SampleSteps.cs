using Microsoft.Playwright;
using Reqnroll;

namespace XunitReqnrollPlaywrightBrowserstack.Tests.StepDefinitions;

[Binding]
public class SampleSteps
{
    private readonly ScenarioContext _scenario;
    private IPage Page => _scenario.Get<IPage>("page");
    private string _productTitle = string.Empty;

    public SampleSteps(ScenarioContext scenario) => _scenario = scenario;

    [Given(@"I open the bstackdemo home page")]
    public async Task OpenHome()
    {
        await Page.GotoAsync("https://bstackdemo.com/");
    }

    [When(@"I add the first product to the cart")]
    public async Task AddFirstProduct()
    {
        var firstProduct = Page.Locator("[id=\"\\31 \"]");
        var titles = await firstProduct.Locator(".shelf-item__title").AllInnerTextsAsync();
        _productTitle = titles[0];
        await firstProduct.GetByText("Add to Cart").ClickAsync();
    }

    [Then(@"the cart shows 1 item that matches the product I added")]
    public async Task CartHasOneMatchingItem()
    {
        var quantity = await Page.Locator(".bag__quantity").InnerTextAsync();
        Assert.Equal("1", quantity);

        var cartTitle = await Page.Locator(".shelf-item__details").Locator(".title").InnerTextAsync();
        Assert.Equal(_productTitle, cartTitle);
    }
}
