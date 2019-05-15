namespace Feeders
{
    public interface IDataFeeder
    {
        int CurrentAmountOfRows { get; set; }
        string CurrentTableName { get; set; }
        string Next();
        bool End { get; }
        DbCounter ApmCounter { get; }
        DbCounter AppCounter { get; }
    }
}