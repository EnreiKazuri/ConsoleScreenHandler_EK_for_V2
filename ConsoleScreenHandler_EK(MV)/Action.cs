public class Action
{
    public string? Name {get; set;}
    public string? Option {get; set;}
    public string Handler {get; set;}
    public string? NextScreen {get; set;}
    public Action() {}
    public Action(string nextscreen)
    {
        this.Handler = "nextscreen";
        this.NextScreen = nextscreen;
    }
}