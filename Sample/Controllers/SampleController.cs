using HashidsNet;
using HashidsNet.AspNetCore.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sample.Models;

namespace Sample.Controllers;

[ApiController]
[Route("sample")]
public class SampleController : ControllerBase
{
    private readonly IHashids hashids;
    private readonly MvcOptions mvcOptions;

    public SampleController(IHashids hashids, IOptions<MvcOptions> mvcOptions)
    {
        this.hashids = hashids;
        this.mvcOptions = mvcOptions.Value;
    }

    [HttpGet("encode")]
    public IActionResult Encode(long id)
    {
        return this.Ok(this.hashids.EncodeLong(id));
    }

    [HttpGet("decode/{id}")]
    public IActionResult Decode2([Hashids] long id)
    {
        return this.Ok(id);
    }

    [HttpGet("decode")]
    public IActionResult Decode([Hashids] long id)
    {
        return this.Ok(id);
    }

    [HttpPost("model")]
    public IActionResult DecodeModel(SampleModel request)
    {
        return this.Ok(request);
    }
}