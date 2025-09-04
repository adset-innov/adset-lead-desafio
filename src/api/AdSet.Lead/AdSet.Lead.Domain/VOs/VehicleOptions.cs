namespace AdSet.Lead.Domain.VOs;

public sealed class VehicleOptions
{
    public bool AirConditioning { get; }
    public bool Alarm { get; }
    public bool Airbag { get; }
    public bool AbsBrakes { get; }

    private VehicleOptions()
    {
    }

    public VehicleOptions(
        bool airConditioning,
        bool alarm,
        bool airbag,
        bool absBrakes
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
        bool? absBrakes = null)
    {
        return new VehicleOptions(
            airConditioning ?? AirConditioning,
            alarm ?? Alarm,
            airbag ?? Airbag,
            absBrakes ?? AbsBrakes
        );
    }

    private bool Equals(VehicleOptions other)
        => AirConditioning == other.AirConditioning
           && Alarm == other.Alarm
           && Airbag == other.Airbag
           && AbsBrakes == other.AbsBrakes;


    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((VehicleOptions)obj);
    }

    public override int GetHashCode() => HashCode.Combine(AirConditioning, Alarm, Airbag, AbsBrakes);
}