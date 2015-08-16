namespace BusinessLogic.Interfaces
{
    internal interface IConvertFloatToString
    {
        string AddAnd(string inputString);
        string GetCurrentInputValue(string inputString, int loopCounter);
    }
}