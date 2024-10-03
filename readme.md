# RulesEngineService

https://microsoft.github.io/RulesEngine

## Run

<details>
<summary>Command Line</summary>

#### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download)

#### Steps

1. Open directory **source\RulesEngineService** in command line and execute **dotnet run**.
3. Open <https://localhost:5000>.

</details>

<details>
<summary>Visual Studio Code</summary>

#### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download)
* [Visual Studio Code](https://code.visualstudio.com)
* [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)

#### Steps

1. Open **source** directory in Visual Studio Code.
2. Press **F5**.

</details>

<details>
<summary>Visual Studio</summary>

#### Prerequisites

* [Visual Studio](https://visualstudio.microsoft.com)

#### Steps

1. Open **source\RulesEngineService.sln** in Visual Studio.
2. Set **RulesEngineService** as startup project.
3. Press **F5**.

</details>

## Example

### Rules

```json
[
    {
        "WorkflowName": "DiscountPercentage",
        "GlobalParams": [
            {
                "Name": "CompareValue",
                "Expression": "1000"
            }
        ],
        "Rules": [
            {
                "RuleName": "CreditCard",
                "Expression": "PaymentType == \"CreditCard\"",
                "SuccessEvent": "5"
            },
            {
                "RuleName": "DebitCard",
                "Expression": "PaymentType == \"DebitCard\"",
                "SuccessEvent": "10"
            },
            {
                "RuleName": "CashLessThan",
                "Expression": "PaymentType == \"Cash\" && Value < CompareValue",
                "SuccessEvent": "15"
            },
            {
                "RuleName": "CashGreaterThanOrEqual",
                "Expression": "PaymentType == \"Cash\" && Value >= CompareValue",
                "SuccessEvent": "20"
            }
        ]
    }
]
```

### Execute

```cs
var json = System.IO.File.ReadAllText("Rules.json");

var workflows = JsonSerializer.Deserialize<Workflow[]>(json);

var rulesEngine = new RulesEngine.RulesEngine(workflows);

var parameters = new RuleParameter[]
{
    new RuleParameter("PaymentType", "Cash"),
    new RuleParameter("Value", 1000)
};

var result = await rulesEngine.ExecuteAllRulesAsync(workflows.First().WorkflowName, parameters);
```

### Result

```json
[
    {
        "rule": {
            "ruleName": "CreditCard",
            "expression": "PaymentType == \"CreditCard\"",
            "successEvent": "5"
        },
        "isSuccess": false,
        "inputs": {
            "PaymentType": "Cash",
            "Value": 1000,
            "CompareValue": 1000
        }
    },
    {
        "rule": {
            "ruleName": "DebitCard",
            "expression": "PaymentType == \"DebitCard\"",
            "successEvent": "10"
        },
        "isSuccess": false,
        "inputs": {
            "PaymentType": "Cash",
            "Value": 1000,
            "CompareValue": 1000
        }
    },
    {
        "rule": {
            "ruleName": "CashLessThan",
            "expression": "PaymentType == \"Cash\" && Value < CompareValue",
            "successEvent": "15"
        },
        "isSuccess": false,
        "inputs": {
            "PaymentType": "Cash",
            "Value": 1000,
            "CompareValue": 1000
        }
    },
    {
        "rule": {
            "ruleName": "CashGreaterThanOrEqual",
            "expression": "PaymentType == \"Cash\" && Value >= CompareValue",
            "successEvent": "20"
        },
        "isSuccess": true,
        "inputs": {
            "PaymentType": "Cash",
            "Value": 1000,
            "CompareValue": 1000
        }
    }
]
```

### Value

```cs
var value = result.SingleOrDefault(rule => rule.IsSuccess)?.Rule.SuccessEvent; // 20
```
