using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.Sessions;

public sealed class DeviceId : ValueObject<DeviceId>
{
    public string Value { get; }

    private DeviceId(string value)
    {
        Value = value;
    }

    public static DeviceId Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        return new DeviceId(value.Trim());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}