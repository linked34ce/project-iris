public static class Converter
{
    private const int DecimalBase = 10;

    public static string ToOrdinal(int num)
    {
        int lastTwoDigits = num % 100;
        if (11 <= lastTwoDigits && lastTwoDigits <= 13)
        {
            return $"{num}th";
        }

        return (num % DecimalBase) switch
        {
            1 => $"{num}st",
            2 => $"{num}nd",
            3 => $"{num}rd",
            _ => $"{num}th",
        };
    }
}
