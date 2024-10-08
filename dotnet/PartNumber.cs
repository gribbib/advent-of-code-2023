// See https://aka.ms/new-console-template for more information
public class PartNumber
{
    public int Index { get; set; } = -1;
    public bool LastOnLine { get; set; }
    public int SearchLength { get { return ValueString == null ? 0 : ValueString.Length + (LastOnLine || Index == 0 ? 1 : 2); } }
    public int SearchStartIndex { get { return Index == 0 ? 0 : Index - 1; } }
    public string? ValueString { get; set; }
    public int Value
    {
        get
        {
            if (String.IsNullOrEmpty(ValueString))
            {
                return 0;
            }
            else
            {
                return int.Parse(ValueString);
            }
        }
    }
    public bool Added { get; set; } = false;

    internal bool IndexInRange(int requestedIndex)
    {
        return ValueString == null ? false : requestedIndex >= Index - 1 && requestedIndex <= Index + ValueString.Length;
    }
}