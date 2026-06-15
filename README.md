# 🧩 Quick.AutoInject

Simplifies dependency injection registration in ASP.NET Core using attributes — no boilerplate in `Program.cs` required.

[![NuGet](https://img.shields.io/nuget/v/Quick.AutoInject.svg)](https://www.nuget.org/packages/Quick.AutoInject)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Quick.AutoInject.svg)](https://www.nuget.org/packages/Quick.AutoInject)

---

## 📦 Installation

```bash
dotnet add package Quick.AutoInject
```

---

## ⚙️ Configure in Program.cs

```csharp
using Quick.AutoInject;

builder.Services.AddAutoInject();
```

---

## 🚀 Usage

Add one of the following attributes to your class depending on the lifetime you need:

| Attribute | Lifetime |
|---|---|
| `[ScopedService]` | Scoped |
| `[TransientService]` | Transient |
| `[SingletonService]` | Singleton |

```csharp
using Quick.AutoInject;

[ScopedService]
public class MyClass : IMyClass { }

[TransientService]
public class MyClass { }

[SingletonService]
public class MyClass { }
```

Then inject and use them the traditional way:

```csharp
public class YourController : ControllerBase
{
    private readonly MyClass _myClass;
    private readonly IMyClass _iMyClass;

    public YourController(MyClass myClass, IMyClass iMyClass)
    {
        _myClass = myClass;
        _iMyClass = iMyClass;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MyCommand command)
    {
        var response = await _myClass.Create(command);
        return Ok(response);
    }
}
```

---

## 🤝 Contributing

Have a suggestion or found a bug? Feel free to:

- 📧 Contact me at [gfmendoza.27@outlook.com](mailto:gfmendoza.27@outlook.com)
- 🐛 Open an issue on [GitHub](https://github.com/gfmendozad/Quick.AutoInject)

---

## 👤 Author

**German Mendoza** — [LinkedIn](https://linkedin.com/in/gfmendozad) · [Portfolio](https://gmendoza.syscore.app)