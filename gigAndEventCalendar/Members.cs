using gigAndEventCalendar;


namespace gigAndEventCalendar
{
    class Members : CollectionUi
    {
        private Dictionary<int, Member> memberDict;

        public Members(int dictLength)
        {
            memberDict = new Dictionary<int, Member>(dictLength);
        }


        public void AddMember(string name, string age, string joinDate, string instrument)
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
                IEnumerable<int> tempId = memberDict.TakeLast(1).Select(kvp => kvp.Value.GetMemberId());
                memberId = tempId.First() + 1;
            }

            //Checks if date is valid if not asks to re-enter
            while (DateOnly.TryParse(Truncate(joinDate, 10), out joinDateConv) == false)
            {
                Console.Clear();
                Console.WriteLine("ERROR: Enter With Format MM/YYYY");
                Console.Write($"Date: ");
                joinDate = Console.ReadLine();
            }

            //Checks if Price is valid if not asks to re-enter
            while (int.TryParse(Truncate(age, 5), out ageConv) == false)
            {
                Console.Clear();
                Console.WriteLine("ERROR: Age must be an Integer");
                Console.Write($"Price: ");
                age = Console.ReadLine();
            }

            try
            {
                Console.Clear();
                memberDict.Add(memberId, new Member(memberId, Truncate(name, 20), ageConv, joinDateConv, Truncate(instrument, 20)));
            }
            catch
            {
                throw;
            }
        }


        public bool RemoveGig(int id)
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
        public Dictionary<int, Member> GetMembers()
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

        public int GetCount()
        {
            return memberDict.Count;
        }

        public override List<int> GetPadding()
        {
            List<string> titles = GetHeaderItems();

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
                padding.Add(memberDict.Max(kvp => Convert.ToString(kvp.Value.GetMemberId()).Length) + 1);
                padding.Add(memberDict.Max(kvp => kvp.Value.GetName().Length));
                padding.Add(memberDict.Max(kvp => Convert.ToString(kvp.Value.GetAge()).Length));
                padding.Add(memberDict.Max(kvp => kvp.Value.GetJoinDateString().Length));
                padding.Add(memberDict.Max(kvp => kvp.Value.GetInstrument().Length));
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
            "Age",
            "Join Date",
            "Instrument"
        };
            return headerItems;

        }

        //misc functions


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