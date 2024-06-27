namespace RulesEngineService;

[Route("")]
[ApiController]
public class DefaultController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var json = System.IO.File.ReadAllText("AppRules.json");

        var workflows = JsonSerializer.Deserialize<Workflow[]>(json);

        var rulesEngine = new RulesEngine.RulesEngine(workflows);

        var parameters = new RuleParameter[]
        {
            new RuleParameter("PaymentType", "Cash"),
            new RuleParameter("Value", 1000)
        };

        var result = await rulesEngine.ExecuteAllRulesAsync(workflows.First().WorkflowName, parameters);

        var successRuleResult = result.SuccessRuleResult();

        return Ok(result);
    }
}
