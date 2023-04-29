[![https://www.nuget.org/packages/Stio.HashidsNet.AspNetCore](https://img.shields.io/nuget/v/Stio.HashidsNet.AspNetCore)](https://www.nuget.org/packages/Stio.HashidsNet.AspNetCore/)

# Hashids.AspNetCore
Extension library for [hashids.net](https://github.com/ullmark/hashids.net).  
Auto decode and encode hash ids for your api.

## [Release notes](/CHANGELOG.md)

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
In your DTOs, decorate the properties that you want to be hash with HashidsJsonConverterAttribute  
> Only for System.Text.Json

```csharp
public class SampleModel
{
    [HashidsJsonConverter]
    public long HashidsLong { get; set; }

    [HashidsJsonConverter]
    public long? NullableHashidsLong { get; set; }

    public long NonHashidsLong { get; set; }

    [HashidsJsonConverter]
    public int HashidsInt { get; set; }

    [HashidsJsonConverter]
    public int? HashidsNullableInt { get; set; }

    public int NonHashidsInt { get; set; }

    [HashidsJsonConverter]
    public long[] HashidsLongArray { get; set; } = null!;

    [HashidsJsonConverter]
    public long?[] HashidsNullableLongArray { get; set; } = null!;

    [HashidsJsonConverter]
    public long?[]? HashidsNullableLongNullableArray { get; set; } = null!;

    public long[] NonHashidsLongArray { get; set; } = null!;

    [HashidsJsonConverter]
    public List<long> HashidsLongList { get; set; } = null!;

    [HashidsJsonConverter]
    public ICollection<long> HashidsLongCollection { get; set; } = null!;

    [HashidsJsonConverter]
    public ISet<long> HashidsLongSet { get; set; } = null!;
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
