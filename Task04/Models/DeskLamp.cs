namespace Task04.Models;

public class DeskLamp : LightingFixture, ISwitchable, IPluggable
{
    public bool IsPluggedIn { get; private set; }

    public void PlugIn()
    {
        IsPluggedIn = true;
        OnStateChanged();
    }

    public void Unplug()
    {
        IsPluggedIn = false;
        TurnOff();
    }

    public override void TurnOn()
    {
        if (!IsPluggedIn || IsOn) return;
        IsOn = true;
        OnStateChanged();
    }

    public override void TurnOff()
    {
        if (!IsOn) return;
        IsOn = false;
        OnStateChanged();
    }
}