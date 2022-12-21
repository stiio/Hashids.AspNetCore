using HashidsNet.AspNetCore.Attributes;

namespace Sample.Models;

public class SampleModel
{
    public long HashidLong { get; set; }

    public long? NullableHashidLong { get; set; }

    public long NonHashidsLong { get; set; }
}