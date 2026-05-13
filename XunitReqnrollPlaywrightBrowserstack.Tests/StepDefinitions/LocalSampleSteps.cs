using Microsoft.Playwright;
using Reqnroll;

namespace XunitReqnrollPlaywrightBrowserstack.Tests.StepDefinitions;

// Mirrors browserstack/csharp-playwright-browserstack -> SampleLocalTest.cs:
//   page.GotoAsync("http://bs-local.com:45454/")  +  title.Contains("BrowserStack Local")
[Binding]
public class LocalSampleSteps
{
    private readonly ScenarioContext _scenario;
    private IPage Page => _scenario.Get<IPage>("page");

    public LocalSampleSteps(ScenarioContext scenario) => _scenario = scenario;

    [Given(@"I open the local sample page on bs-local")]
    public async Task OpenLocalSamplePage()
    {
        await Page.GotoAsync("http://bs-local.com:45454/");
    }

    [Then(@"the local sample page title contains ""(.*)""")]
    public async Task LocalSamplePageTitleContains(string expected)
    {
        var actual = await Page.TitleAsync();
        Assert.Contains(expected, actual);
    }
}
