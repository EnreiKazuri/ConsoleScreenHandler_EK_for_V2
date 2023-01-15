public class Listing
{
    public string ListingCode {get; set; }
    public string ListingMessage {get; set;}
    public Listing(string code, string message)
    {
        this.ListingCode = code;
        this.ListingMessage = message;
    }
}