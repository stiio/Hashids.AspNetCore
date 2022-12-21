using HashidsNet.AspNetCore.Attributes;

namespace Sample.Models;

public class SampleFormModel
{
    [HashidsBinder]
    public long Id { get; set; }
}