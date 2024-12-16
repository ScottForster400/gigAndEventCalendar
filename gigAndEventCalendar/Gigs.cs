using gigAndEventCalendar;

class Gigs
{
    private Dictionary<int, Gig> gigDict;
    private string filePath = "gigs.txt";

    public Gigs(int dictLength)
    {
        gigDict = new Dictionary<int, Gig>();
    }



    public void addGig(string name, string gigDate, string gigTime, string price, string capacity, string address, string postcode)
    {
        int gigId;
        DateOnly gigDateConv;
        TimeOnly gigTimeConv;
        int gigPriceConv;
        int gigCapacityConv;

        //Assigns gig Id depending on id's already stored
        if (gigDict.Count == 0)
        {
            gigId = 1;
        }
        else
        {
            IEnumerable<int> tempId = gigDict.TakeLast(1).Select(kvp => kvp.Value.getGigId());
            gigId = tempId.First() + 1;
        }

        //Checks if date is valid if not asks to re-enter
        while (DateOnly.TryParse(gigDate, out gigDateConv) == false)
        {
            Console.Clear();
            Console.WriteLine("ERROR: Enter With Format DD/MM/YYYY");
            Console.Write($"Date: ");
            gigDate = Console.ReadLine();
        }

        //Checks if Time is valid if not asks to re-enter
        while (TimeOnly.TryParse(gigTime, out gigTimeConv) == false)
        {
            Console.Clear();
            Console.WriteLine("ERROR: Enter With Format HH:MM");
            Console.Write($"Time: ");
            gigTime = Console.ReadLine();
        }

        //Checks if Price is valid if not asks to re-enter
        while (int.TryParse(price, out gigPriceConv) == false)
        {
            Console.Clear();
            Console.WriteLine("ERROR: Price must be an Integer");
            Console.Write($"Price: ");
            price = Console.ReadLine();
        }

        //Checks if Capcity is valid if not asks to re-enter
        while (int.TryParse(capacity, out gigCapacityConv) == false)
        {
            Console.Clear();
            Console.WriteLine("ERROR: Capacity must be an Integer");
            Console.Write($"Capacity: ");
            capacity = Console.ReadLine();
        }
        try
        {
            gigDict.Add(gigId, new Gig(gigId, name, gigDateConv, gigTimeConv, gigPriceConv, gigCapacityConv, address, postcode));
        }
        catch
        {
            throw;
        }
    }

    public bool removeGig(int id)
    {
        if (gigDict.ContainsKey(id))
        {
            gigDict.Remove(id);
            return true;
        }
        else
        {
            Console.WriteLine("ERROR: Gig Not Found");
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            return false;
        }

    }


    //Get Commands

    public Gig getGig(int id)
    {
        if (gigDict.ContainsKey(id))
        {
            return gigDict[id];
        }
        else
        {
            return null; ;
        }
    }

    public Dictionary<int, Gig> GetGigs()
    {
        return gigDict;
    }

    public int getCount()
    {
        return gigDict.Count;
    }

    public List<int> getPadding()
    {
        List<string> titles = getHeaderItems();
        
        List<int> padding = new List<int>(8);
        if (gigDict.Count == 0)
        {
            foreach (string title in titles)
            {
                padding.Add(title.Length);
            }
        }

        else
        {
            padding.Add(gigDict.Max(kvp => Convert.ToString(kvp.Value.getGigId()).Length));
            padding.Add(gigDict.Max(kvp => kvp.Value.getName().Length));
            padding.Add(gigDict.Max(kvp => kvp.Value.getDate().ToString().Length));
            padding.Add(gigDict.Max(kvp => kvp.Value.getTime().Length));
            padding.Add(gigDict.Max(kvp => Convert.ToString(kvp.Value.getPrice()).Length));
            padding.Add(gigDict.Max(kvp => Convert.ToString(kvp.Value.getCapacity()).Length));
            padding.Add(gigDict.Max(kvp => kvp.Value.getAddress().Length));
            padding.Add(gigDict.Max(kvp => kvp.Value.getPostcode().Length));
            for (int i = 0; i < padding.Count; i++)
            {
                if (padding[i] < titles[i].Length)
                {
                    padding[i] = titles[i].Length;
                }
            }
        }
        return padding;
    }

    public List<string> getHeaderItems()
    {
        List<string> headerItems = new List<string>()
        {
             "|Id",
            "Name",
            "Date",
            "Time",
            "Price",
            "Capacity",
            "Address",
            "Postcode"
        };
        return headerItems;

    }


}
