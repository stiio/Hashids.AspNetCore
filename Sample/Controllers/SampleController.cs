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
    public IActionResult DecodeFromRoute([HashidsBinder] long id)
    {
        return this.Ok(id);
    }

    [HttpGet("decode")]
    public IActionResult DecodeFromQuery([HashidsBinder] long id)
    {
        return this.Ok(id);
    }

    [HttpGet("decode/nullable_query")]
    public IActionResult DecodeNullableQuery([HashidsBinder] int? id)
    {
        return this.Ok(id);
    }

    [HttpPost("decode/form")]
    public IActionResult DecodeFromForm([FromForm, HashidsBinder] long id)
    {
        return this.Ok(id);
    }

    [HttpPost("decode/form_model")]
    public IActionResult DecodeFromFormModel([FromForm] SampleFormModel request)
    {
        return this.Ok(request.Id);
    }

    [HttpPost("model")]
    public ActionResult<SampleModel> DecodeFromBody(SampleModel request)
    {
        return this.Ok(request);
    }
}