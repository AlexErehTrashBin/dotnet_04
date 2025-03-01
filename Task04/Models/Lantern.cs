namespace Task04.Models;

public class Lantern : LightingFixture, ISwitchable
{
    public override void TurnOn()
    {
        if (IsOn) return;
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