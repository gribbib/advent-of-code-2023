public abstract class AbstractDays
{
    public string FileName { get; set; }
    public long Result { get; internal set; } = 0;
    public long Run()
    {
        var reader = new StreamReader(FileName);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            try
            {
                DoLoopThings(line);
            }
            catch (ContinueException)
            {
                continue;
            }
        }
        DoFinalThings();
        return Result;
    }

    public abstract void DoLoopThings(string line);
    public abstract void DoFinalThings();
}