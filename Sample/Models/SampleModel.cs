using HashidsNet.AspNetCore.Attributes;

namespace Sample.Models;

public class SampleModel
{
    [HashidsLongJsonConverter]
    public long HashidsLong { get; set; }

    [HashidsLongJsonConverter]
    public long? NullableHashidsLong { get; set; }

    public long NonHashidsLong { get; set; }

    [HashidsIntJsonConverter]
    public int HashidsInt { get; set; }

    [HashidsIntJsonConverter]
    public int? HashidsNullableInt { get; set; }

    public int NonHashidsInt { get; set; }
}