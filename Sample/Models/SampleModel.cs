using HashidsNet.AspNetCore.Attributes;

namespace Sample.Models;

public class SampleModel
{
    [Hashids]
    public long HashidLong { get; set; }

    [Hashids]
    public long? NullableHashidLong { get; set; }

    public long NonHashidsLong { get; set; }
}