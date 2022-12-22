[![https://www.nuget.org/packages/Stio.HashidsNet.AspNetCore](https://img.shields.io/nuget/v/Stio.HashidsNet.AspNetCore)](https://www.nuget.org/packages/Stio.HashidsNet.AspNetCore/)

# Hashids.AspNetCore
Extension library for [hashids.net](https://github.com/ullmark/hashids.net).  
Auto decode and encode hash ids for your api.

## Getting started
1. Install package:  

From PM console:
```
Install-Package Stio.Hashids.AspNetCore
```
or .net core cli:
```
dotnet add package Stio.Hashids.AspNetCore
```
2. Register service:
```csharp
builder.Services.AddHashids("this is my salt");
```
3. Decorate hash properties:  
In your DTOs, decorate the properties that you want to be hash with one of the:
- HashidsIntJsonConverterAttribute
- HashidsNullableIntJsonConverterAttribute
- HashidsLongJsonConverterAttribute
- HashidsNullableLongJsonConverterAttribute
> Only for System.Text.Json

```csharp
public class SampleModel
{
    [HashidsLongJsonConverter]
    public long HashidsLong { get; set; }

    [HashidsNullableLongJsonConverter]
    public long? NullableHashidsLong { get; set; }

    public long NonHashidsLong { get; set; }

    [HashidsIntJsonConverter]
    public int HashidsInt { get; set; }

    [HashidsNullableIntJsonConverter]
    public int? HashidsNullableInt { get; set; }

    public int NonHashidsInt { get; set; }
}
```

Use decorated model in your api for request or response
```csharp
[HttpPost]
public ActionResult<SampleModel> DecodeFromBody(SampleModel request)
{
    return this.Ok(request);
}
```
For hash the parameters of a route, query, or form-data, use the `HashidsBinderAttribute`
```csharp
 [HttpGet("decode/{id}")]
 public IActionResult DecodeFromRoute([HashidsBinder] long id)
 {
     return this.Ok(id);
 }

 [HttpGet("decode")]
 public IActionResult DecodeFromQuery([HashidsBinder] long id)
 {
     return this.Ok(id);
 }

 [HttpPost("decode")]
 public IActionResult DecodeFromForm([FromForm, HashidsBinder] long id)
 {
     return this.Ok(id);
 }
```

See [full example](https://github.com/stiio/Hashids.AspNetCore/tree/master/Sample).
