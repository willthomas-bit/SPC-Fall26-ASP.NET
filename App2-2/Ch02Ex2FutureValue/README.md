# Exercise 2-2 — Future Value app (Empty template)

This is the completed **Murach’s ASP.NET Core MVC** Chapter 2 Exercise 2-2 app.

- **Project name:** `FutureValue`
- **Solution name:** `Ch02Ex2FutureValue`
- Started from the **Empty** template (not the MVC template)

## What is different from Exercise 2-1

The Empty template does **not** create Controllers, Models, or Views. Those folders were added by hand:

```
FutureValue/
  Controllers/
  Models/
  Views/
    Home/
    Shared/
```

After the folders exist, the rest is Exercise 2-1 from step 3: HomeController, Index view, Startup MVC pipeline, model, view imports, GET/POST.

The Empty template also does not register MVC. `Startup.cs` was updated from the empty `app.Run(...)` pipeline to `AddControllersWithViews()` plus the default `{controller=Home}/{action=Index}/{id?}` route.

## How to run in Visual Studio

1. Open `Ch02Ex2FutureValue.sln`.
2. Select the **FutureValue** project as the startup project.
3. Press **Ctrl+F5**.

## How to run from the command line

```bash
cd FutureValue
dotnet run
```

Then browse to `https://localhost:5001`.

## Test values

- Monthly Investment: `100`
- Yearly Interest Rate: `3`
- Number of Years: `3`
- Expected Future Value: **$3,771.46**

**Clear** issues a GET to Index and resets `ViewBag.FV` to 0.

This exercise stops before data-annotation validation, same as 2-1 step 12.
