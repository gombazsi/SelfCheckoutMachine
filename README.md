# SelfCheckoutMachine

The repository contains a .NET Web API project, with the given specifications.

- The state of the machine is stored on a local Microsoft SQL Server.
- Euro payment is supported, with a database sheme supporting further currencies to be implemented, possibility to update conversion rate without restarting the application.
- Entity Framework is used for db connection, ORM.
- Database migrations are run automatically when starting app.

DotNet Version used: 6.0.6
Used packages:
- Microsoft.EntityFrameworkCore 6.0.6
- Swashbuckle.AspNetCore 6.2.3
- System.Linq 4.3

About currency support:
- Optional query parameter "currencyCode" is added to StocksController endpoints. If no parameter is given, "HUF" is used.
- Valid currencyCodes are: "HUF", "EUR". Default conversion rates are seeded for both.
- Exception is thrown if currencyCode is not supported by the machine.
- Property "currencyCode" is added to object used in CheckoutController. If no code is given, "HUF" is used.
- Stock POST returns money stored in the database with the given currency, or "HUF"
- Checkout POST returns change in "HUF". The machine returns as much change as it can. 
  No Exception is thrown, if the change is less then the "HUF" value of inserted money minus the price of the purcase.

