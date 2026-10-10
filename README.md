# Animal Spa

## Run on Windows

Requirements:
- Windows 10/11
- Visual Studio 2022 with **.NET desktop development**, or .NET 8 SDK on Windows

### Visual Studio
1. Open `Animal-Spa.csproj`.
2. Let Visual Studio restore the project.
3. Press **F5**.

### Terminal
```powershell
dotnet restore
dotnet run
```


Designer stability fix:
- Dashboard constructors skip runtime-only setup in Visual Studio Designer.
- Image loading in ServiceDetailView is guarded and cached safely.
- AddCustomer/AddPet skip runtime image initialization at design time.
