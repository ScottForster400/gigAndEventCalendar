using gigAndEventCalendar;


namespace gigAndEventCalendar
{
    class Gigs : CollectionUi
    {
        private Dictionary<int, Gig> gigDict;
        private string filePath = "gigs.txt";

        public Gigs(int dictLength)
        {
            gigDict = new Dictionary<int, Gig>();
        }



        public void AddGig(string name, string gigDate, string gigTime, string price, string capacity, string address, string postcode)
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
                IEnumerable<int> tempId = gigDict.TakeLast(1).Select(kvp => kvp.Value.GetGigId());
                gigId = tempId.First() + 1;
            }

            //Checks if date is valid if not asks to re-enter
            while (DateOnly.TryParse(Truncate(gigDate, 10), out gigDateConv) == false)
            {
                Console.Clear();
                Console.WriteLine("ERROR: Enter With Format DD/MM/YYYY");
                Console.Write($"Date: ");
                gigDate = Console.ReadLine();
            }

            //Checks if Time is valid if not asks to re-enter
            while (TimeOnly.TryParse(Truncate(gigTime, 5), out gigTimeConv) == false)
            {
                Console.Clear();
                Console.WriteLine("ERROR: Enter With Format HH:MM");
                Console.Write($"Time: ");
                gigTime = Console.ReadLine();
            }

            //Checks if Price is valid if not asks to re-enter
            while (int.TryParse(Truncate(price, 8), out gigPriceConv) == false)
            {
                Console.Clear();
                Console.WriteLine("ERROR: Price must be an Integer");
                Console.Write($"Price: ");
                price = Console.ReadLine();
            }

            //Checks if Capcity is valid if not asks to re-enter
            while (int.TryParse(Truncate(capacity, 8), out gigCapacityConv) == false)
            {
                Console.Clear();
                Console.WriteLine("ERROR: Capacity must be an Integer");
                Console.Write($"Capacity: ");
                capacity = Console.ReadLine();
            }
            try
            {
                Console.Clear();
                gigDict.Add(gigId, new Gig(gigId, Truncate(name, 20), gigDateConv, gigTimeConv, gigPriceConv, gigCapacityConv, Truncate(address, 40), Truncate(postcode, 8)));
            }
            catch
            {
                throw;
            }
        }

        public bool RemoveGig(int id)
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

        public Gig GetGig(int id)
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

        public int GetCount()
        {
            return gigDict.Count;
        }

        public override List<int> GetPadding()
        {
            List<string> titles = GetHeaderItems();

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
                // +1 is included in the id padding as when written each id will have "|" added to the front
                padding.Add(gigDict.Max(kvp => Convert.ToString(kvp.Value.GetGigId()).Length) + 1);
                padding.Add(gigDict.Max(kvp => kvp.Value.GetName().Length));
                padding.Add(gigDict.Max(kvp => kvp.Value.getDate().ToString().Length));
                padding.Add(gigDict.Max(kvp => kvp.Value.GetTime().Length));
                padding.Add(gigDict.Max(kvp => Convert.ToString(kvp.Value.GetPrice()).Length));
                padding.Add(gigDict.Max(kvp => Convert.ToString(kvp.Value.GetCapacity()).Length));
                padding.Add(gigDict.Max(kvp => kvp.Value.GetAddress().Length));
                padding.Add(gigDict.Max(kvp => kvp.Value.GetPostcode().Length));
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

        public override List<string> GetHeaderItems()
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
        public override string Truncate(string inputtedString, int maxLength)
        {
            if (inputtedString.Length > maxLength)
            {
                Console.WriteLine($"{inputtedString} is above {maxLength}");
                inputtedString = inputtedString.Substring(0, maxLength);
                Console.WriteLine($"It has been shortened to {inputtedString}");
                Console.WriteLine();

            }

            return inputtedString;
        }

    }
}
