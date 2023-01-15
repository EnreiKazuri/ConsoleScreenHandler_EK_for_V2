using Newtonsoft.Json;

public class ScreenHandler
{
    public Screen[] Screens {get; set;}
    ActionHandler ActionHandler = new ActionHandler();
    public string _PathToRead {get; set;}
    public ScreenHandler(string pathToRead)
    {
        _PathToRead = pathToRead;
        Deserialize(pathToRead);
        Initialize();
    }
    public void Deserialize(string path)
    {
        using (var reader = new StreamReader(path))
        {
            string readingsFromJson = reader.ReadToEnd();
            Screen[] screensFromJson = JsonConvert.DeserializeObject<Screen[]>(readingsFromJson);
            this.Screens = screensFromJson;
        }
        for (int i = 0; i < Screens.Length; i++)
        {
            foreach (var item in Screens[i].Actions)
            {
                item.Option = item.Option.ToLower();
            }
        }
    }
    public void Initialize()
    {
        LoadScreen(Screens.Where(s => s.EntryPoint == true).First());
    }
    public void LoadScreen(Screen screen)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(screen.Title + Environment.NewLine);
            if (screen.EntryPoint)
            {
                if (screen.Listing != null)
                {
                    Console.WriteLine("Current Listing");
                    foreach (var item in screen.Listing)
                    {
                        Console.WriteLine("{0}: {1}", item.ListingCode, item.ListingMessage);
                    }
                    Console.WriteLine(Environment.NewLine);
                }
            }
            if (screen.Message != null)
            {
                Console.WriteLine(screen.Message);
                string choice = Console.ReadLine()[0].ToString().ToLower();
                if (string.Equals(choice, screen.Actions.Where(s => s.Option.ToString() == choice).First().Option.ToString()))
                {
                    ActionHandler.Execute(this, screen.Actions.Where(s => s.Option.ToString() == choice).First());
                }
            }
            else if (screen.Fields != null)
            {
                Console.WriteLine("Actions: ");
                foreach(var action in screen.Actions)
                {
                    Console.WriteLine("({0}) {1}", action.Option, action.Name);
                }
                string choice = Console.ReadLine()[0].ToString();
                if (string.Equals(choice, screen.Actions.Where(s => s.Option.ToString() == choice).First().Option.ToString()))
                {
                    ActionHandler.Execute(this, screen.Actions.Where(s => s.Option.ToString() == choice).First());
                }
            }
        }
    }
    public void ChangeScreens(string NextScreen)
    {
        ActionHandler.NextScreen(NextScreen, this);
    }
}