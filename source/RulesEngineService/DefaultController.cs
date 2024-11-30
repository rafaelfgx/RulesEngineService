namespace RulesEngineService;

[Route("")]
[ApiController]
public class DefaultController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var json = await System.IO.File.ReadAllTextAsync("AppRules.json");

        var workflows = JsonSerializer.Deserialize<Workflow[]>(json);

        var rulesEngine = new RulesEngine.RulesEngine(workflows);

        var parameters = new RuleParameter[]
        {
            new("PaymentType", "Cash"),
            new("Value", 1000)
        };

        return Ok(await rulesEngine.ExecuteAllRulesAsync(workflows.First().WorkflowName, parameters));
    }
}
