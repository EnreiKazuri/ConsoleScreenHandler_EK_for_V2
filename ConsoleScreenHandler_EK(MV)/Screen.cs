public class Screen
{
    public string ID {get; set;}
    public bool EntryPoint {get; set;}
    public string Title {get; set;}
    public List<Listing>? Listing {get; set;}
    public string? Message {get; set;}
    public Field[]? Fields {get; set;}
    public Action[] Actions {get; set;}
}