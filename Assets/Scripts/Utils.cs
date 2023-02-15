public static class Utils
{
    // Number of options per question
    public const int NUMBER_OF_OPTIONS = 3;

    // Formatting styles for numbers in a table
    public const string TABLE_NUMBER_PREFIX = "";
    public const string TABLE_NUMBER_SUFFIX = ".";

    // Pretty prints an index, making the first item 1 instead of 0
    public static string FormatTableNumber(int tableNumber)
    {
        return TABLE_NUMBER_PREFIX + (tableNumber + 1).ToString() + TABLE_NUMBER_SUFFIX;
    }
}
