namespace Task04.Models;

public abstract class LightingFixture : ISwitchable, IBreakable
{
    public event EventHandler? StateChanged;
    public event EventHandler? Broken;

    private bool _isOn;
    protected bool IsBroken;
    public bool IsOn
    {
        get => _isOn;
        protected set
        {
            if (IsBroken && value)
            {
                return;
            }
            _isOn = value;
            OnStateChanged();
        }
    }
    
    public bool IsSelfBroken => IsBroken;

    public abstract void TurnOn();
    public abstract void TurnOff();

    public virtual void Break()
    {
        IsBroken = true;
        TurnOff();
        OnBroken();
    }

    protected void OnStateChanged() => StateChanged?.Invoke(this, EventArgs.Empty);
    
    protected void OnBroken() => Broken?.Invoke(this, EventArgs.Empty);
}