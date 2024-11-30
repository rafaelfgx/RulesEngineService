namespace RulesEngineService;

public static class Extensions
{
    public static bool HasSuccessRuleResult(this List<RuleResultTree> result) => result.Any(rule => rule.IsSuccess);

    public static Rule SuccessRuleResult(this List<RuleResultTree> result) => result.SingleOrDefault(rule => rule.IsSuccess)?.Rule;
}
