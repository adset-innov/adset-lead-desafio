namespace AdSet.Lead.Domain.VOs;

public sealed class VehicleOptions
{
    public bool AirConditioning { get; }
    public bool Alarm { get; }
    public bool Airbag { get; }
    public bool AbsBrakes { get; }

    private VehicleOptions() { }

    public VehicleOptions(
        bool airConditioning = false,
        bool alarm = false,
        bool airbag = false,
        bool absBrakes = false
    )
    {
        AirConditioning = airConditioning;
        Alarm = alarm;
        Airbag = airbag;
        AbsBrakes = absBrakes;
    }

    public VehicleOptions With(
        bool? airConditioning = null,
        bool? alarm = null,
        bool? airbag = null,
        bool? absBrakes = null
    )
    {
        return new VehicleOptions(
            airConditioning ?? AirConditioning,
            alarm ?? Alarm,
            airbag ?? Airbag,
            absBrakes ?? AbsBrakes
        );
    }

    private bool Equals(VehicleOptions other) =>
        AirConditioning == other.AirConditioning &&
        Alarm == other.Alarm &&
        Airbag == other.Airbag &&
        AbsBrakes == other.AbsBrakes;

    public override bool Equals(object? obj) =>
        obj is VehicleOptions other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(AirConditioning, Alarm, Airbag, AbsBrakes);
} 