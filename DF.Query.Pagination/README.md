# DF.Query.Pagination

DF.Query.Pagination is a library that provides easy pagination handling in .NET applications.

## Installation

You can install the library using the NuGet Package Manager. 

Package Manager:
```
NuGet\Install-Package DF.Query.Pagination -Version 2.1.0
```

.NET CLI:
```
dotnet add package DF.Query.Pagination --version 2.1.0
```

Package Reference:
```
<PackageReference Include="DF.Query.Pagination" Version="2.1.0" />
```

## How to Use

* Add pagination to request model

```
public class CarListRequest
{
    public Pagination Pagination { get; set; }
}
```

* Use the method GetPaginatedList

```
public async Task<PaginatedList<Car>> GetCars(CarListRequest request)
{
    IQueryable<Car> query = _dbContext.Cars;

    return await query.GetPaginatedListAsync(request.Pagination);
}
```

* Return paginated list from endpoint

```
[HttpPost]
[Route("[action]")]
public async Task<PaginatedList<Car>> List([FromBody] CarListRequest request)
{
    return await _carService.GetCars(request);
}
```