# CSE 325 


# W01 Assignment

## 1. ASP.NET Core Web API

For the "Create a web API with ASP.NET Core controllers" module, the original `Pizzas` list contained two records:

```csharp
new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
```

I added an additional record:

```csharp
new Pizza { Id = 3, Name = "Pepperoni", IsGlutenFree = false }
```

I also updated `nextId` to `4` so that new pizzas created through the API receive a unique ID.

The resulting pizza list is:

```csharp
Pizzas = new List<Pizza>
{
    new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
    new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true },
    new Pizza { Id = 3, Name = "Pepperoni", IsGlutenFree = false }
};
```

### API Verification

I tested the CRUD operations successfully:

- GET: `200 OK`
- POST: `201 Created`
- PUT: `204 No Content`
- DELETE: `204 No Content`

A GET request returned the existing pizza records including the additional Pepperoni record.

## 2. Sales Summary Function

The following function generates a sales summary file containing the total sales and a detailed total for each JSON sales file:

```csharp
void GenerateSalesSummary(
    IEnumerable<string> salesFiles,
    string storesDirectory,
    string outputFile)
{
    double totalSales = 0;
    StringBuilder details = new StringBuilder();

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);

        SalesData? data =
            JsonConvert.DeserializeObject<SalesData?>(salesJson);

        double fileTotal = data?.Total ?? 0;

        totalSales += fileTotal;

        string fileName =
            Path.GetRelativePath(storesDirectory, file);

        details.AppendLine($" {fileName}: {fileTotal:C}");
    }

    StringBuilder report = new StringBuilder();

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {totalSales:C}");
    report.AppendLine();
    report.AppendLine("Details:");
    report.Append(details);

    File.WriteAllText(outputFile, report.ToString());
}
```

The generated report showed an actual total sales value of:

```text
Sales Summary
----------------------------
Total Sales: $ 2.012,20
```