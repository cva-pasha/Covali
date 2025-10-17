# Covali.Enums
[![NuGet](https://img.shields.io/nuget/v/Covali.Enums)](https://www.nuget.org/packages/Covali.Enums)

## Overview
The `Covali.Enums` library provides a collection of common enumerations used across Covali projects and other .NET applications. The goal is to offer a centralized and standardized set of enums that can be easily shared and reused.

## Features
- **Currency**: A comprehensive enum for currency codes based on the ISO 4217 standard.

### Currency Enum
The `Currency` enum defines a set of currency codes identified by their numeric ISO 4217 codes. This allows for a strongly-typed way to work with currencies in your application.

Each member of the enum corresponds to a specific currency, for example:
- `Currency.USD` (United States Dollar)
- `Currency.EUR` (Euro)
- `Currency.GBP` (Pound Sterling)
- `Currency.JPY` (Japanese Yen)

## Getting Started

### Installation
Add the `Covali.Enums` library to your project via NuGet:
```bash
dotnet add package Covali.Enums
```

### Usage
You can use the `Currency` enum in your classes and methods to ensure type safety when dealing with currency information.

#### Example
Here is a simple example of how to use the `Currency` enum in a C# application:

```csharp
using Covali.Enums;
using System;

public class Transaction
{
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }

    public override string ToString()
    {
        return $"{Amount} {Currency}";
    }
}

public class Program
{
    public static void Main()
    {
        var transaction = new Transaction
        {
            Amount = 100.50m,
            Currency = Currency.USD
        };

        Console.WriteLine(transaction); // Output: 100.50 USD
        
        // You can also cast the enum to its underlying integer value (ISO 4217 code)
        ushort currencyCode = (ushort)transaction.Currency;
        Console.WriteLine(currencyCode); // Output: 840
    }
}
```

## Contributing
Contributions are welcome! If you have ideas for new enums or improvements to existing ones, please open an issue or submit a pull request on the project's GitHub repository.

## License
This project is licensed under the MIT License. See the [LICENSE](https://github.com/cva-pasha/Covali/blob/main/LICENSE.txt) file for more details.
