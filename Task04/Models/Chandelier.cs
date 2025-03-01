namespace Task04.Models;

public class Chandelier : LightingFixture, ISwitchable
{
    private int _mode;
    public ChandelierMode Mode => Enum.Parse<ChandelierMode>(_mode.ToString());

    public override void TurnOn()
    {
        if (IsBroken)
        {
            return;
        }
        if ((_mode + 1) % 4 == 0)
        {
            return;
        }
        _mode = (_mode + 1) % 4;
        if (IsOn)
        {
            OnStateChanged();
            return;
        }
        IsOn = true;
        OnStateChanged();
    }

    public override void TurnOff()
    {
        if (!IsOn) return;
        _mode = (_mode - 1 + 4) % 4;
        if (_mode == 0)
        {
            IsOn = false;
        }
        OnStateChanged();
    }

    public override void Break()
    {
        IsOn = false;
        _mode = 0;
        IsBroken = true;
        OnStateChanged();
        OnBroken();
    }
}