using Newtonsoft.Json;
public class ActionHandler
{
    public void Exit()
    {
        Environment.Exit(0);
    }
    public void NextScreen(string NextScreen, ScreenHandler handler)
    {
        handler.LoadScreen(handler.Screens.Where(s => s.ID == NextScreen).First());
    }
    public void CreateListing(string nextscreen, ScreenHandler handler, Screen screen)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Listing's code: ");
            string listingCode = Console.ReadLine();
            if (screen.Listing != null)
            {
                if(screen.Listing.Where(l => l.ListingCode == listingCode).ToList().Count() == 0)
                {
                    Console.WriteLine("Listing's message: ");
                    string listingMessage = Console.ReadLine();
                    try
                    {
                        screen.Listing.Add(new Listing(listingCode, listingMessage));
                        Console.WriteLine("Listing created!");
                        SaveToJson(handler);
                        Execute(handler, new Action(nextscreen));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("This code is already in use, please select another one");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                screen.Listing = new List<Listing>();
                Console.WriteLine("Listing's message: ");
                string listingMessage = Console.ReadLine();
                try
                {
                    screen.Listing.Add(new Listing(listingCode, listingMessage));
                    Console.WriteLine("Listing created!");
                    SaveToJson(handler);
                    Execute(handler, new Action(nextscreen));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(1000);
                }
            }
        }
    }
    public void UpdateListing(string nextscreen, ScreenHandler handler, Screen screen)
    {
        Console.Clear();
        if (screen.Listing != null)
        {
            while (true)
            {
                Console.Clear();
                foreach(var item in screen.Listing)
                {
                     Console.WriteLine("{0}: {1}", item.ListingCode, item.ListingMessage);
                }
                Console.WriteLine("\nWhich listing do you want to modify? (C) to cancel");
                string answerCode = Console.ReadLine().ToLower();
                if (answerCode == "c")
                {
                    Execute(handler, new Action(handler.Screens.Where(s => s.EntryPoint == true).First().ID));
                }
                if (screen.Listing.Where(l => l.ListingCode == answerCode).ToList().Count != 0)
                {
                    Console.WriteLine("New message: ");
                    string answerMessage = Console.ReadLine();
                    try
                    {
                        int index = screen.Listing.FindIndex(s => s.ListingCode == answerCode);
                        screen.Listing.Where(l => l.ListingCode == answerCode).ToList().First().ListingMessage = answerMessage;
                        Console.WriteLine("Listing Modified!");
                        SaveToJson(handler);
                        Execute(handler, new Action(nextscreen));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("Please select a valid code");
                    Thread.Sleep(1000);
                }
            }
        }
        else
        {
            Console.WriteLine("There are no listings to modify");
            Thread.Sleep(1000);
            Execute(handler, new Action(nextscreen));
        }
    }
    public void DeleteListing(string nextscreen, ScreenHandler handler, Screen screen)
    {
        Console.Clear();
        if (screen.Listing != null)
        {
            while (true)
            {
                Console.Clear();
                foreach(var item in screen.Listing)
                {
                     Console.WriteLine("{0}: {1}", item.ListingCode, item.ListingMessage);
                }
                Console.WriteLine("\nWhich listing do you want to delete? (C) to cancel");
                string answerCode = Console.ReadLine().ToLower();
                if (answerCode == "c")
                {
                    Execute(handler, new Action(handler.Screens.Where(s => s.EntryPoint == true).First().ID));
                }
                if (screen.Listing.Where(l => l.ListingCode == answerCode).ToList().Count != 0)
                {
                    try
                    {
                        int index = screen.Listing.FindIndex(s => s.ListingCode == answerCode);
                        screen.Listing.RemoveAt(index);
                        Console.WriteLine("Listing Deleted!");
                        SaveToJson(handler);
                        Execute(handler, new Action(nextscreen));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("Please select a valid code");
                    Thread.Sleep(1000);
                }
            }
        }
        else
        {
            Console.WriteLine("There are no listings to delete");
            Thread.Sleep(1000);
            Execute(handler, new Action(nextscreen));
        }
    }
    public void Execute(ScreenHandler handler, Action action)
    {
        string method = action.Handler;
        switch (method.ToLower())
        {
            case "exit":
                Exit();
                break;
            case "nextscreen":
                NextScreen(action.NextScreen, handler);
                break;
            case "create":
                CreateListing(action.NextScreen, handler, handler.Screens.Where(s => s.EntryPoint == true).First());
                break;
            case "update":
                UpdateListing(action.NextScreen, handler, handler.Screens.Where(s => s.EntryPoint == true).First());
                break;
            case "delete":
                DeleteListing(action.NextScreen, handler, handler.Screens.Where(s => s.EntryPoint == true).First());
                break;
            default:
                break;
        }
    }
    public void SaveToJson(ScreenHandler handler)
    {
        var json = JsonConvert.SerializeObject(handler.Screens.ToArray(), Formatting.Indented);
        File.WriteAllText(handler._PathToRead, json);
        Thread.Sleep(1000);
    }
}