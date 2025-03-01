namespace Task04.Models;

public enum ChandelierMode
{
    TurnedOff = 0,
    TurnedFirstHalf = 1,
    TurnedSecondHalf = 2,
    TurnedFull = 3
}

public static class ChandelierModeExtensions
{
    public static String GetDescription(this ChandelierMode mode)
    {
        switch (mode)
        {
            case ChandelierMode.TurnedOff:
                return "Полностью выключено";
            case ChandelierMode.TurnedFirstHalf:
                return "Включена только первая половина";
            case ChandelierMode.TurnedSecondHalf:
                return "Включена только вторая половина";
            case ChandelierMode.TurnedFull:
                return "Полностью включено";
        }

        return "";
    }
}