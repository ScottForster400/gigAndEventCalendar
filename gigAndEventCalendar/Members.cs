using gigAndEventCalendar;

class Members
{
    private Dictionary<int, Member> memberDict;

    public Members(int dictLength)
    {
        memberDict = new Dictionary<int, Member>(dictLength);
    }


    public void addMember(string name, string age, string joinDate, string instrument)
    {
        int memberId;
        int ageConv;
        DateOnly joinDateConv;
        

        //Assigns gig Id depending on id's already stored
        if (memberDict.Count == 0)
        {
            memberId = 1;
        }
        else
        {
            IEnumerable<int> tempId = memberDict.TakeLast(1).Select(kvp => kvp.Value.getMemberId());
            memberId = tempId.First() + 1;
        }

        //Checks if date is valid if not asks to re-enter
        while (DateOnly.TryParse(joinDate, out joinDateConv) == false)
        {
            Console.Clear();
            Console.WriteLine("ERROR: Enter With Format MM/YYYY");
            Console.Write($"Date: ");
            joinDate = Console.ReadLine();
        }

        //Checks if Price is valid if not asks to re-enter
        while (int.TryParse(age, out ageConv) == false)
        {
            Console.Clear();
            Console.WriteLine("ERROR: Age must be an Integer");
            Console.Write($"Price: ");
            age = Console.ReadLine();
        }

        try
        {
            memberDict.Add(memberId, new Member(memberId, name, ageConv, joinDateConv, instrument));
        }
        catch
        {
            throw;
        }
    }


    public bool removeGig(int id)
    {
        if (memberDict.ContainsKey(id))
        {
            memberDict.Remove(id);
            return true;
        }
        else
        {
            Console.WriteLine("ERROR: Member Not Found");
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            return false;
        }

    }






    //get commands
    public Dictionary<int,Member> GetMembers()
    {
        return memberDict;
    }
    public Member GetMember(int id)
    {
        if (memberDict.ContainsKey(id))
        {
            return memberDict[id];
        }
        else
        {
            return null;
        }
    }

    public int getCount()
    {
        return memberDict.Count;
    }

    public List<int> getPadding()
    {
        List<string> titles = getHeaderItems();
        
        List<int> padding = new List<int>(5);
        if (memberDict.Count == 0)
        {
            foreach (string title in titles)
            {
                padding.Add(title.Length);
            }
        }

        else
        {
            padding.Add(memberDict.Max(kvp => Convert.ToString(kvp.Value.getMemberId()).Length));
            padding.Add(memberDict.Max(kvp => kvp.Value.getName().Length));
            padding.Add(memberDict.Max(kvp => Convert.ToString(kvp.Value.getAge()).Length));
            padding.Add(memberDict.Max(kvp => kvp.Value.getJoinDateString().Length));
            padding.Add(memberDict.Max(kvp => kvp.Value.getInstrument().Length));
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
            "Age",
            "Join Date",
            "Instrument"
        };
        return headerItems;

    }

}