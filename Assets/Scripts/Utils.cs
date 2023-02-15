public static class Utils
{
    // Number of options per question
    public const int NUMBER_OF_OPTIONS = 3;

    // Formatting styles for numbers in a table
    public const string TABLE_NUMBER_PREFIX = "";
    public const string TABLE_NUMBER_SUFFIX = ".";

    // Formatting styles for questions
    public const string QUESTION_PROGRESS_PREFIX = "Question ";
    public const string QUESTION_NUMBER_PREFIX = "(";
    public const string QUESTION_NUMBER_SEPARATOR = " / ";
    public const string QUESTION_NUMBER_SUFFIX = ")";

    // Pretty prints an index, making the first item 1 instead of 0
    public static string FormatTableNumber(int tableIndex)
    {
        return TABLE_NUMBER_PREFIX + (tableIndex + 1).ToString() + TABLE_NUMBER_SUFFIX;
    }

    // Pretty prints an index for a question number
    public static string FormatQuestionNumber(int questionIndex, int totalNumOfQuestions)
    {
        return QUESTION_PROGRESS_PREFIX + QUESTION_NUMBER_PREFIX +
               (questionIndex + 1).ToString() + QUESTION_NUMBER_SEPARATOR +
               totalNumOfQuestions.ToString() + QUESTION_NUMBER_SUFFIX;
    }
}
