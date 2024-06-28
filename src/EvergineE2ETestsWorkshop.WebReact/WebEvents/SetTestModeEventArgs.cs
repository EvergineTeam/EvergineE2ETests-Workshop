namespace EvergineE2ETestsWorkshop.WebReact.WebEvents;

public class SetTestModeEventArgs
{
    public bool Activated { get; }

    public SetTestModeEventArgs(bool activated)
    {
        this.Activated = activated;
    }
}
