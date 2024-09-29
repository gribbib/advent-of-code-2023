public abstract class AbstractDays
{
    public string FileName { get; set; }
    public int Result { get; internal set; } = 0;
    public int Run()
    {
        var reader = new StreamReader(FileName);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            DoThings(line);
        }
        return Result;
    }

    public abstract void DoThings(string line);
}