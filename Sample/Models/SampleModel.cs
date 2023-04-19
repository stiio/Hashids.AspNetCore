using HashidsNet.AspNetCore.Attributes;

namespace Sample.Models;

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
    public HashSet<long> HashidsLongSet { get; set; } = null!;
}