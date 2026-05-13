using Microsoft.Playwright;
using Reqnroll;

namespace XunitReqnrollPlaywrightBrowserstack.Tests.Hooks;

[Binding]
public class PlaywrightHooks
{
    private readonly ScenarioContext _scenario;
    private IPlaywright? _pw;
    private IBrowser? _browser;

    public PlaywrightHooks(ScenarioContext scenario) => _scenario = scenario;

    [BeforeScenario]
    public async Task SetUp()
    {
        _pw = await Playwright.CreateAsync();
        _browser = await _pw.Chromium.LaunchAsync();
        var context = await _browser.NewContextAsync();
        var page = await context.NewPageAsync();
        _scenario.Set(page, "page");
    }

    [AfterScenario]
    public async Task TearDown()
    {
        if (_browser is not null) await _browser.CloseAsync();
        _pw?.Dispose();
    }
}
