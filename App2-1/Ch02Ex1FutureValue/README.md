# Exercise 2-1 — Future Value app (MVC template)

This is the completed **Murach’s ASP.NET Core MVC** Chapter 2 Exercise 2-1 app.

- **Project name:** `FutureValue`
- **Solution name:** `Ch02Ex1FutureValue`

## How to run in Visual Studio

1. Open `Ch02Ex1FutureValue.sln`.
2. Select the **FutureValue** project as the startup project.
3. Press **Ctrl+F5** (Start Without Debugging).
4. The Home/Index view should load.

## How to run from the command line

```bash
cd FutureValue
dotnet run
```

Then browse to `https://localhost:5001` (or the HTTP URL printed in the console).

## What the app does

Enter:

- Monthly Investment (e.g. `100`)
- Yearly Interest Rate (e.g. `3`)
- Number of Years (e.g. `3`)

Click **Calculate**. Future value is compounded monthly:

`FV = (FV + monthlyInvestment) * (1 + yearlyRate / 12 / 100)` for each month.

Example from the book-style walkthrough: **$100 / month at 3% for 3 years ≈ $3,771.46**.

**Clear** issues a GET back to Index and resets the form (`ViewBag.FV = 0`).

## Files that match the exercise figures

| Exercise step | File |
|---|---|
| 3 / 11 — HomeController (fig. 2-4 then 2-12) | `FutureValue/Controllers/HomeController.cs` |
| 4 / 10 — Index view (fig. 2-5 then 2-11) + CSS (fig. 2-14) | `FutureValue/Views/Home/Index.cshtml` |
| 5 — Startup (fig. 2-6) | `FutureValue/Startup.cs` |
| 8 — Model (fig. 2-9) | `FutureValue/Models/FutureValueModel.cs` |
| 9 — View imports (fig. 2-10) | `FutureValue/Views/_ViewImports.cshtml` |

This exercise stops **before** data-annotation validation (that comes later in Chapter 2). Invalid input can produce unexpected results, which is what step 12 describes.

`Program.cs` is the host that loads `Startup` so the project runs on modern .NET SDKs while keeping the book’s `Startup.cs` pipeline.
