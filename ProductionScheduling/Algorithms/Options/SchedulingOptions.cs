namespace ProductionScheduling.Algorithms.Options;
public struct SchedulingOptions
{
    public OverideOptions OverideOptions { get; set; }

    public SchedulingOptions(OverideOptions overideOptions)
    {
        OverideOptions = overideOptions;
    }
}
