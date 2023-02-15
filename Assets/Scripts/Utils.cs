public static class Utils
{
    public const string TABLE_NUMBER_PREFIX = "";
    public const string TABLE_NUMBER_SUFFIX = ".";

    // Pretty prints an index, making the first item 1 instead of 0
    public static string FormatTableNumber(int tableNumber)
    {
        return TABLE_NUMBER_PREFIX + (tableNumber + 1).ToString() + TABLE_NUMBER_SUFFIX;
    }
}
