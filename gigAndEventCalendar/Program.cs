// See https://aka.ms/new-console-template for more information

using Microsoft.VisualBasic;
using System.Data.SqlTypes;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using gigAndEventCalendar;
using System.Collections.Concurrent;

//TEsting
//Bands bandDict = new Bands();
//Gigs gigDict = new Gigs();
//Members memberDict = new Members();
//Band testBand = new Band("test");

//memberDict.addMember("bruh", "12", "12/11/2023", "guitar");
//memberDict.addMember("luh", "13", "02/03/2024", "bass");
//memberDict.addMember("duhh", "10", "07/01/2020", "face");


//gigDict.readFile();

//testBand.setMembers(memberDict);
//testBand.setGigs(gigDict);

//bandDict.storeBand(testBand);

//Gigs gigDict2 = new Gigs();
//Members memberDict2 = new Members();
//Band testBand2 = new Band("test2");

//memberDict2.addMember("bruh2", "12", "12/11/2023", "guitar");
//memberDict2.addMember("luh2", "13", "02/03/2024", "bass");
//memberDict2.addMember("duhh2", "10", "07/01/2020", "face");

//testBand2.setMembers(memberDict2);
//testBand2.setGigs(gigDict);

//Dictionary<string, Band> test1 = bandDict.getBands();
//bandDict.storeBand(testBand2);


//Console.WriteLine("not file read");

//foreach (Band band in test1.Values)
//{
//    Console.WriteLine(band.getFormattedFile());
//    Console.WriteLine();
//}


//bandDict.saveToBinaryFile();

//Bands bandDict2 = new Bands();

//bandDict2.readBinaryFile();
//Console.WriteLine();
//Console.WriteLine("file read");
//Dictionary<string, Band> test2 = bandDict.getBands();
//foreach (Band band in test2.Values)
//{
//    Console.WriteLine(band.getFormattedFile());
//    Console.WriteLine();
//}

//multiProcessTest();


//programme start-up
if (args.Length > 0)
{
    if (args[0] == "--show")
    {
        int bandCount = GetBandCountFromFile();
        
        Bands bandDict = new Bands(bandCount);
        bandDict.ReadBinaryFile();


        if (bandDict.GetBands().ContainsKey(args[1]))
        {
            while (true)
            {
                homeMenuDisplay(bandDict.GetBand(args[1]),bandDict);
            }
        }
        else
        {
            Console.WriteLine("Band does not exist you can add this band by using '--add' command");

            Console.WriteLine("Band List:");
            foreach (String name in bandDict.GetBands().Keys)
            {
                Console.WriteLine($"- {name}");
            }
            Console.WriteLine();
        }

    }
    else if (args[0] == "--add")
    {
        int bandCount = GetBandCountFromFile();

        Bands bandDict = new Bands(bandCount);
        bandDict.ReadBinaryFile();
        bandDict.AddBand(args[1]);
        bandDict.SaveToBinaryFile();
        Console.WriteLine();
    }
    else if (args[0] == "--help")
    {
        int bandCount = GetBandCountFromFile();

        Bands bandDict = new Bands(bandCount);
        bandDict.ReadBinaryFile();

        Console.WriteLine("Commands:");
        Console.WriteLine("'--add bandName' - Add specified band to the program");
        Console.WriteLine("'--show bandName' - Launches application displaying the specified bands info");
        Console.WriteLine("");

        Console.WriteLine("Band List:");
        foreach (String name in bandDict.GetBands().Keys)
        {
            Console.WriteLine($"- {name}");
        }
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("Unknown Command enter '--help' for a list of commands");
        Console.WriteLine();
    }
}
else
{
    Console.WriteLine("Welcome to the Gig Calendar enter '--help' for a list of commands");
    Console.WriteLine();
}


//gigs
void ViewGigs(Gigs gigDict, int gigStartAt, Bands bandDict, Dictionary<int,Gig> orderedGigs)
{
    //has to be one instead of zero to allow it to work with for statement
    List<int> padding = new List<int>(8);
    int borderLength = 0;
    List<int> navPadding = new List<int>(6);
    int pageNumber = (gigDict.GetGigs().Count() / 10);
    int pageRemainder = gigDict.GetGigs().Count % 10;

    //checks if your on the last page
    bool lastPage = false;
    if (gigStartAt / 10 == pageNumber && pageRemainder != 0)
    {
        lastPage = true;
    }


    //If number is a multiple of ten prevents creating an extra page that is blank
    if (pageRemainder == 0 && pageNumber>0)
    {
        pageNumber = pageNumber - 1;
    }


    // creates nav items to pass into nav functions
    List<string> navItems = GetNavItems("Gigs",pageNumber);

    int navItemNums = navItems.Count();

  
    //Adds the largest length of each attribute of gigs for display formatting
    padding = gigDict.getPadding();

    //gets border length for table
    borderLength = 0;
    foreach (int i in padding)
    {
        //needs plus one to account for the | splitting the values
        borderLength = borderLength + i+1;
    }

    //gets padding for options needs to be a list due to HorizontalListNav parameters
    for (int i = 0; i < navItemNums; i++)
    {
        navPadding.Add(borderLength / navItemNums + navItemNums);
    }

    //displays title 
    Console.Clear();
    Console.WriteLine("\r\n  _______ ___ _______     _______ _______ ___     _______ ______  ______   _______ _______ \r\n |   _   |   |   _   |   |   _   |   _   |   |   |   _   |   _  \\|   _  \\ |   _   |   _   \\\r\n |.  |___|.  |.  |___|   |.  1___|.  1   |.  |   |.  1___|.  |   |.  |   \\|.  1   |.  l   /\r\n |.  |   |.  |.  |   |   |.  |___|.  _   |.  |___|.  __)_|.  |   |.  |    |.  _   |.  _   1\r\n |:  1   |:  |:  1   |   |:  1   |:  |   |:  1   |:  1   |:  |   |:  1    |:  |   |:  |   |\r\n |::.. . |::.|::.. . |   |::.. . |::.|:. |::.. . |::.. . |::.|   |::.. . /|::.|:. |::.|:. |\r\n `-------`---`-------'   `-------`--- ---`-------`-------`--- ---`------' `--- ---`--- ---'\r\n                                                                                           \r\n");

    Console.WriteLine($"<{Convert.ToString(gigStartAt)[0]}/{pageNumber}>");

    //Writes each gig into a table
    TableHeadersWrite(padding, borderLength,gigDict.getHeaderItems());


    if (pageNumber > 0)
    {
        if(lastPage == true)
        {
            for (int i = gigStartAt; i < (pageRemainder+gigStartAt); i++)
            {
                Console.WriteLine($"{orderedGigs.Values.ElementAt(i).getFormatted(padding)}");

            }
            
        }
        else
        {
            for (int i = gigStartAt; i < gigStartAt + 10; i++)
            {
                Console.WriteLine($"{orderedGigs.Values.ElementAt(i).getFormatted(padding)}");

            }
        }

    }
    else
    {
        for (int i = gigStartAt; i < (pageRemainder + gigStartAt); i++)
        {
            Console.WriteLine($"{orderedGigs.Values.ElementAt(i).getFormatted(padding)}");

        }
    }


    //foreach (Gig gig in orderedGigs.Values)
    //{
    //    Console.WriteLine(gig.getFormatted(padding));
    //}
    for (int i = 0; i < borderLength; i++)
    {
        Console.Write("-");
    }
    for(int i=0; i< 2; i++)
    {
        Console.WriteLine();
    }


    int option = HorizontalListNav(navItems, navPadding,false,borderLength);


    if(pageNumber<=0)
    //Goes to certain function depending on selected option
    switch (option)
    {
        case 1:
            Console.Clear();
            AddGig(gigDict,bandDict);
            break;
        case 2:
            Console.Clear();
            GigSelect(orderedGigs, padding, borderLength,gigDict, gigStartAt, pageRemainder, lastPage, bandDict,"edit");
            break;
        case 3:
            Console.Clear();
            GigSelect(orderedGigs, padding, borderLength,gigDict, gigStartAt, pageRemainder, lastPage, bandDict, "remove");
            break;
        case 4:
            Console.Clear();
            return;
        case 999:
                return;
        default:
            Console.Clear();
            return;
        
    }
    else
    {
        switch (option)
        {
            case 1:
                if(gigStartAt/10 == pageNumber)
                {
                    ViewGigs(gigDict, gigStartAt,bandDict,orderedGigs);
                }
                else
                {
                    gigStartAt = gigStartAt + 10;
                    ViewGigs(gigDict, gigStartAt, bandDict,orderedGigs);
                }
                break;
            case 2:
                if (gigStartAt == 0)
                {
                    ViewGigs(gigDict, gigStartAt, bandDict,orderedGigs);
                }
                else
                {
                    gigStartAt = gigStartAt - 10;
                    ViewGigs(gigDict, gigStartAt, bandDict,orderedGigs);
                }
                break;
            case 3:
                Console.Clear();
                AddGig(gigDict, bandDict);
                break;
            case 4:
                Console.Clear();
                GigSelect(orderedGigs, padding, borderLength, gigDict, gigStartAt, pageRemainder, lastPage, bandDict, "edit");
                break;
            case 5:
                Console.Clear();
                GigSelect(orderedGigs, padding, borderLength, gigDict, gigStartAt, pageRemainder, lastPage, bandDict, "remove");
                break;
            case 6:
                Console.Clear();
                return;

            default:
                Console.Clear();
                return;

        }
    }
    Console.Clear();
   
}

void GigSelect(Dictionary<int,Gig> orderedGigs, List<int> padding, int borderLength, Gigs gigDict, int gigStartAt, int pageRemainder, bool lastPage, Bands bandDict, string selectedFunction)
{
    // need to cretae a dictiorny of int and strings to allow it to get passed into VerticalDictNav 
     Dictionary<int, string> orderdGigsString = new Dictionary<int, string>(10);
   
    //foreach (Gig gig in orderedGigs.Values)
    //{
    //    orderdGigsString.Add(gig.getGigId(), $"{gig.getFormatted(padding)}\u001b[0m");
        
    //};

   
    
    if (lastPage == true)
    {
        for (int i = gigStartAt; i < (pageRemainder + gigStartAt); i++)
        {
            orderdGigsString.Add(orderedGigs.Values.ElementAt(i).getGigId(), $"{orderedGigs.Values.ElementAt(i).getFormatted(padding)}\u001b[0m");

        }

    }
    else
    {
        for (int i = gigStartAt; i < gigStartAt + 10; i++)
        {
            orderdGigsString.Add(orderedGigs.Values.ElementAt(i).getGigId(), $"{orderedGigs.Values.ElementAt(i).getFormatted(padding)}\u001b[0m");

        }
    }


        //Main Title
    Console.WriteLine("\r\n  _______ ___ _______     _______ _______ ___     _______ ______  ______   _______ _______ \r\n |   _   |   |   _   |   |   _   |   _   |   |   |   _   |   _  \\|   _  \\ |   _   |   _   \\\r\n |.  |___|.  |.  |___|   |.  1___|.  1   |.  |   |.  1___|.  |   |.  |   \\|.  1   |.  l   /\r\n |.  |   |.  |.  |   |   |.  |___|.  _   |.  |___|.  __)_|.  |   |.  |    |.  _   |.  _   1\r\n |:  1   |:  |:  1   |   |:  1   |:  |   |:  1   |:  1   |:  |   |:  1    |:  |   |:  |   |\r\n |::.. . |::.|::.. . |   |::.. . |::.|:. |::.. . |::.. . |::.|   |::.. . /|::.|:. |::.|:. |\r\n `-------`---`-------'   `-------`--- ---`-------`-------`--- ---`------' `--- ---`--- ---'\r\n                                                                                           \r\n");
    Console.WriteLine();
    

    TableHeadersWrite(padding, borderLength, gigDict.getHeaderItems());

    int option = VerticalDictNav(orderdGigsString, padding,true,borderLength,selectedFunction);


    //Goes to certain function depending on selected option
    if(option<= orderedGigs.Count() && option >= 0)
    {
        int selectedGigId = orderedGigs.ElementAt(option - 1).Value.getGigId();
        if (selectedFunction == "edit")
        {
            EditGigInfo(orderedGigs[selectedGigId], padding, borderLength, gigDict, bandDict);
        }
        else
        {
            // Removes selected gig
            bool sucesssfull = gigDict.removeGig(selectedGigId);
            if (sucesssfull == true)
            {
                bandDict.SaveToBinaryFile();
                sucess("Removed");
            }
        };
    }
    else if (option == 999)
    {
        return;
    }
}

void AddGig(Gigs gigDict, Bands bandDict)
{
    Console.CursorVisible = true;

    //Main Title
    Console.WriteLine("\r\n  _______ ___ _______     _______ _______ ___     _______ ______  ______   _______ _______ \r\n |   _   |   |   _   |   |   _   |   _   |   |   |   _   |   _  \\|   _  \\ |   _   |   _   \\\r\n |.  |___|.  |.  |___|   |.  1___|.  1   |.  |   |.  1___|.  |   |.  |   \\|.  1   |.  l   /\r\n |.  |   |.  |.  |   |   |.  |___|.  _   |.  |___|.  __)_|.  |   |.  |    |.  _   |.  _   1\r\n |:  1   |:  |:  1   |   |:  1   |:  |   |:  1   |:  1   |:  |   |:  1    |:  |   |:  |   |\r\n |::.. . |::.|::.. . |   |::.. . |::.|:. |::.. . |::.. . |::.|   |::.. . /|::.|:. |::.|:. |\r\n `-------`---`-------'   `-------`--- ---`-------`-------`--- ---`------' `--- ---`--- ---'\r\n                                                                                           \r\n");

    Console.Write("Name Of Gig: ");
    string gigName = Console.ReadLine();

    Console.Write("Date Of Gig (DD/MM/YYYY): ");
    string gigDate = Console.ReadLine();

    Console.Write("Time Of Gig (HH:MM): ");
    string gigTime = Console.ReadLine();

    Console.Write("Price of Gig: £");
    string gigPrice = Console.ReadLine();

    Console.Write("Capacity Of Gig: ");
    string gigCapacity = Console.ReadLine();

    Console.Write("Address Of Gig: ");
    string gigAdress = Console.ReadLine();

    Console.Write("Postcode Of Gig: ");
    string gigPostcode = Console.ReadLine();

    gigDict.addGig(gigName, gigDate, gigTime, gigPrice, gigCapacity, gigAdress, gigPostcode);
    bandDict.SaveToBinaryFile();
    sucess("Added");
}

void EditGigInfo(Gig selectedGig, List<int> padding, int borderLength, Gigs gigDict, Bands bandDict)
{
    bool valid = false;
    Console.Clear();
    //Main Title
    Console.WriteLine("\r\n  _______ ___ _______     _______ _______ ___     _______ ______  ______   _______ _______ \r\n |   _   |   |   _   |   |   _   |   _   |   |   |   _   |   _  \\|   _  \\ |   _   |   _   \\\r\n |.  |___|.  |.  |___|   |.  1___|.  1   |.  |   |.  1___|.  |   |.  |   \\|.  1   |.  l   /\r\n |.  |   |.  |.  |   |   |.  |___|.  _   |.  |___|.  __)_|.  |   |.  |    |.  _   |.  _   1\r\n |:  1   |:  |:  1   |   |:  1   |:  |   |:  1   |:  1   |:  |   |:  1    |:  |   |:  |   |\r\n |::.. . |::.|::.. . |   |::.. . |::.|:. |::.. . |::.. . |::.|   |::.. . /|::.|:. |::.|:. |\r\n `-------`---`-------'   `-------`--- ---`-------`-------`--- ---`------' `--- ---`--- ---'\r\n                                                                                           \r\n");

    TableHeadersWrite(padding,borderLength, gigDict.getHeaderItems());

    List<string> gigInf = selectedGig.getInfo();

    int option = HorizontalListNav(gigInf,padding,true,borderLength);


    //Allows input of new info depending on selection
    Console.CursorVisible = true;
    switch (option)
    {
        case 1:
            Console.Write("Id Cannot Be Edited");
            Console.WriteLine("");
            Console.Write("Click Enter to Reselect...");
            Console.ReadLine();
            EditGigInfo(selectedGig,padding,borderLength, gigDict,bandDict);
            break;
        case 2:
            Console.WriteLine($"Current Name: {gigInf[option - 1]}");
            Console.Write("New Name: ");
            selectedGig.setName(Console.ReadLine());
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;
        case 3:
            while (valid == false)
            {
                Console.WriteLine("Enter With Format DD/MM/YYYY");
                Console.WriteLine($"Current Date: {gigInf[option - 1]}");
                Console.Write("New Date: ");
                valid = selectedGig.setDate(Console.ReadLine());
    
            }
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;
        case 4:
            while (valid == false)
            {
                Console.WriteLine("Enter With Format HH:mm");
                Console.WriteLine($"Current Time: {gigInf[option - 1]}");
                Console.Write("New Time: ");
                valid = selectedGig.setTime(Console.ReadLine());

            }
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;

        case 5:
            while (valid == false)
            {
                Console.WriteLine($"Current Price: £{gigInf[option - 1]}");
                Console.Write("New Price: ");
                valid = selectedGig.setPrice(Console.ReadLine());

            }
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;
        case 6:
            while (valid == false)
            {
                Console.WriteLine($"Current Capacity: {gigInf[option - 1]}");
                Console.Write("New Capacity: ");
                valid = selectedGig.setCapacity(Console.ReadLine());
            }
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;
        case 7:
            Console.WriteLine($"Current Address: {gigInf[option - 1]}");
            Console.Write("New Address: ");
            selectedGig.editAddress(Console.ReadLine());
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;
        case 8:
            Console.WriteLine($"Current Postcode: {gigInf[option - 1]}");
            Console.Write("New Postcode: ");
            selectedGig.setPostcode(Console.ReadLine());
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;
        default:
            Console.Clear();
            return;

    }

}


//members
void ViewMembers(Members memDict, int memStartAt, Bands bandDict, Dictionary<int, Member> orderedMembers)
{
    //has to be one instead of zero to allow it to work with for statement

    List<int> padding = new List<int>(5);
    int borderLength = 0;
    List<int> navPadding = new List<int>(6);
    bool hasEntries = memDict.GetMembers().Count() != 0;

    //
    int pageNumber = (memDict.GetMembers().Count() / 10);
    int pageRemainder = memDict.GetMembers().Count % 10;

    //checks if your on the last page
    bool lastPage = false;
    if (memStartAt / 10 == pageNumber && pageRemainder != 0)
    {
        lastPage = true;
    }


    //If number is a multiple of ten prevents creating an extra page that is blank
    if (pageRemainder == 0 && pageNumber > 0)
    {
        pageNumber = pageNumber - 1;
    }


    // creates nav items to pass into nav functions
    List<string> navItems = GetNavItems("Members", pageNumber);

    int navItemNums = navItems.Count();
   
    //Adds the largest length of each attribute of gigs for display formatting
    padding = memDict.getPadding();

    //gets border length for table
    borderLength = 0;
    foreach (int i in padding)
    {
        //needs plus one to account for the | splitting the values
        borderLength = borderLength + i + 1;
    }

    //gets padding for options needs to be a list due to HorizontalListNav parameters
    for (int i = 0; i < navItemNums; i++)
    {
        navPadding.Add(borderLength / navItemNums + 8);
    }

   
    //displays title 
    Console.Clear();
    Console.WriteLine("\r\n  _______ ___ _______     _______ _______ ___     _______ ______  ______   _______ _______ \r\n |   _   |   |   _   |   |   _   |   _   |   |   |   _   |   _  \\|   _  \\ |   _   |   _   \\\r\n |.  |___|.  |.  |___|   |.  1___|.  1   |.  |   |.  1___|.  |   |.  |   \\|.  1   |.  l   /\r\n |.  |   |.  |.  |   |   |.  |___|.  _   |.  |___|.  __)_|.  |   |.  |    |.  _   |.  _   1\r\n |:  1   |:  |:  1   |   |:  1   |:  |   |:  1   |:  1   |:  |   |:  1    |:  |   |:  |   |\r\n |::.. . |::.|::.. . |   |::.. . |::.|:. |::.. . |::.. . |::.|   |::.. . /|::.|:. |::.|:. |\r\n `-------`---`-------'   `-------`--- ---`-------`-------`--- ---`------' `--- ---`--- ---'\r\n                                                                                           \r\n");

    //Displays pagenumber
    Console.WriteLine($"<{Convert.ToString(memStartAt)[0]}/{pageNumber}>");

    //Writes each gig into a table
    TableHeadersWrite(padding, borderLength,memDict.getHeaderItems());


    if (pageNumber > 0)
    {
        if (lastPage == true)
        {
            for (int i = memStartAt; i < (pageRemainder + memStartAt); i++)
            {
                Console.WriteLine($"{orderedMembers.Values.ElementAt(i).getFormatted(padding)}");

            }

        }
        else
        {
            for (int i = memStartAt; i < memStartAt + 10; i++)
            {
                Console.WriteLine($"{orderedMembers.Values.ElementAt(i).getFormatted(padding)}");

            }
        }

    }
    else
    {
        for (int i = memStartAt; i < (pageRemainder + memStartAt); i++)
        {
            Console.WriteLine($"{orderedMembers.Values.ElementAt(i).getFormatted(padding)}");

        }
    }


    //foreach (Gig gig in orderedGigs.Values)
    //{
    //    Console.WriteLine(gig.getFormatted(padding));
    //}
    for (int i = 0; i < borderLength; i++)
    {
        Console.Write("-");
    }
    for (int i = 0; i < 2; i++)
    {
        Console.WriteLine();
    }


    int option = HorizontalListNav(navItems, navPadding, false, borderLength);


    if (pageNumber <= 0)
        //Goes to certain function depending on selected option
        switch (option)
        {
            case 1:
                Console.Clear();
                AddMember(memDict, bandDict);
                break;
            case 2:
                Console.Clear();
                MemberSelect(orderedMembers, padding, borderLength, memDict, memStartAt, pageRemainder, lastPage, bandDict,"edit");
                break;
            case 3:
                Console.Clear();
                MemberSelect(orderedMembers, padding, borderLength, memDict, memStartAt, pageRemainder, lastPage, bandDict, "remove");
                break;
            case 4:
                Console.Clear();
                return;
            case 999:
                return;
            default:
                Console.Clear();
                return;

        }
    else
    {
        switch (option)
        {
            //Case 1 and 2 are for navigation through pages
            case 1:
                if (memStartAt / 10 == pageNumber)
                {
                    ViewMembers(memDict, memStartAt, bandDict,orderedMembers);
                }
                else
                {
                    memStartAt = memStartAt + 10;
                    ViewMembers(memDict, memStartAt, bandDict, orderedMembers);
                }
                break;
            case 2:
                if (memStartAt == 0)
                {
                    ViewMembers(memDict, memStartAt, bandDict, orderedMembers);
                }
                else
                {
                    memStartAt = memStartAt - 10;
                    ViewMembers(memDict, memStartAt, bandDict,orderedMembers);
                }
                break;
            case 3:
                Console.Clear();
                //AddGig(gigDict, bandDict);
                break;
            case 4:
                Console.Clear();
                //EditGigSelect(orderedGigs, padding, borderLength, gigDict, gigStartAt, pageRemainder, lastPage, bandDict);
                break;
            case 5:
                Console.Clear();
                //RemoveGigSelect(gigDict, orderedGigs, padding, borderLength, gigStartAt, pageRemainder, lastPage, bandDict);
                break;
            case 6:
                Console.Clear();
                return;

            default:
                Console.Clear();
                return;

        }
    }
    Console.Clear();

}

void MemberSelect(Dictionary<int, Member> orderedMembers, List<int> padding, int borderLength, Members memDict, int memStartAt, int pageRemainder, bool lastPage, Bands bandDict, string selectedFunction)
{
    // need to cretae a dictiorny of int and strings to allow it to get passed into VerticalDictNav
    Dictionary<int, string> orderedMemString = new Dictionary<int, string>(10);
    //foreach (Gig gig in orderedGigs.Values)
    //{
    //    orderedMemString.Add(gig.getMemberId(), $"{gig.getFormatted(padding)}\u001b[0m");

    //};



    if (lastPage == true)
    {
        for (int i = memStartAt; i < (pageRemainder + memStartAt); i++)
        {
            orderedMemString.Add(orderedMembers.Values.ElementAt(i).getMemberId(), $"{orderedMembers.Values.ElementAt(i).getFormatted(padding)}\u001b[0m");

        }

    }
    else
    {
        for (int i = memStartAt; i < memStartAt + 10; i++)
        {
            orderedMemString.Add(orderedMembers.Values.ElementAt(i).getMemberId(), $"{orderedMembers.Values.ElementAt(i).getFormatted(padding)}\u001b[0m");

        }
    }


    //Main Title
    Console.WriteLine("\r\n  _______ ___ _______     _______ _______ ___     _______ ______  ______   _______ _______ \r\n |   _   |   |   _   |   |   _   |   _   |   |   |   _   |   _  \\|   _  \\ |   _   |   _   \\\r\n |.  |___|.  |.  |___|   |.  1___|.  1   |.  |   |.  1___|.  |   |.  |   \\|.  1   |.  l   /\r\n |.  |   |.  |.  |   |   |.  |___|.  _   |.  |___|.  __)_|.  |   |.  |    |.  _   |.  _   1\r\n |:  1   |:  |:  1   |   |:  1   |:  |   |:  1   |:  1   |:  |   |:  1    |:  |   |:  |   |\r\n |::.. . |::.|::.. . |   |::.. . |::.|:. |::.. . |::.. . |::.|   |::.. . /|::.|:. |::.|:. |\r\n `-------`---`-------'   `-------`--- ---`-------`-------`--- ---`------' `--- ---`--- ---'\r\n                                                                                           \r\n");
    Console.WriteLine();


    TableHeadersWrite(padding, borderLength, memDict.getHeaderItems());

    int option = VerticalDictNav(orderedMemString, padding, true, borderLength, selectedFunction);


    //Goes to certain function depending on selected option
    if (option <= orderedMembers.Count() && option >= 0)
    {
        int selectedMemId = orderedMembers.ElementAt(option - 1).Value.getMemberId();
        if (selectedFunction == "edit")
        {
            EditMemberInfo(orderedMembers[selectedMemId], padding, borderLength, memDict, bandDict);
        }
        else
        {
            // Removes selected gig
            bool sucesssfull = memDict.removeGig(selectedMemId);
            if (sucesssfull == true)
            {
                bandDict.SaveToBinaryFile();
                sucess("Removed");
            }
        };
    }
    else if (option == 999)
    {
        return;
    }

  
}

void EditMemberInfo( Member selectedMem, List<int> padding, int borderLength, Members memDict, Bands bandDict)
{
    bool valid = false;
    Console.Clear();
    //Main Title
    Console.WriteLine("\r\n  _______ ___ _______     _______ _______ ___     _______ ______  ______   _______ _______ \r\n |   _   |   |   _   |   |   _   |   _   |   |   |   _   |   _  \\|   _  \\ |   _   |   _   \\\r\n |.  |___|.  |.  |___|   |.  1___|.  1   |.  |   |.  1___|.  |   |.  |   \\|.  1   |.  l   /\r\n |.  |   |.  |.  |   |   |.  |___|.  _   |.  |___|.  __)_|.  |   |.  |    |.  _   |.  _   1\r\n |:  1   |:  |:  1   |   |:  1   |:  |   |:  1   |:  1   |:  |   |:  1    |:  |   |:  |   |\r\n |::.. . |::.|::.. . |   |::.. . |::.|:. |::.. . |::.. . |::.|   |::.. . /|::.|:. |::.|:. |\r\n `-------`---`-------'   `-------`--- ---`-------`-------`--- ---`------' `--- ---`--- ---'\r\n                                                                                           \r\n");

    TableHeadersWrite(padding, borderLength, memDict.getHeaderItems());

    List<string> memInf = selectedMem.getInfo();

    int option = HorizontalListNav(memInf, padding, true, borderLength);


    //Allows input of new info depending on selection
    Console.CursorVisible = true;
    switch (option)
    {
        case 1:
            Console.Write("Id Cannot Be Edited");
            Console.WriteLine("");
            Console.Write("Click Enter to Reselect...");
            Console.ReadLine();
            EditMemberInfo(selectedMem, padding, borderLength, memDict, bandDict);
            break;
        case 2:
            Console.WriteLine($"Current Name: {memInf[option - 1]}");
            Console.Write("New Name: ");
            selectedMem.setName(Console.ReadLine());
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;
        case 3:
            while (valid == false)
            {
                Console.WriteLine($"Current Age: {memInf[option - 1]}");
                Console.Write("New Age: ");
                valid = selectedMem.setAge(Console.ReadLine());

            }
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;
        case 4:
            while (valid == false)
            {
                Console.WriteLine("Enter With Format MM/YYYY");
                Console.WriteLine($"Current Join Date: {memInf[option - 1]}");
                Console.Write("New Join Date: ");
                valid = selectedMem.setJoinDate(Console.ReadLine());

            }
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;

        case 5:
            Console.WriteLine($"Current Instrument: £{memInf[option - 1]}");
            Console.Write("New Instrument: ");
            selectedMem.setInstrument(Console.ReadLine());
            bandDict.SaveToBinaryFile();
            sucess("Updated");
            break;

        default:
            Console.Clear();
            return;

    }
}

void AddMember(Members memDict, Bands bandDict)
{
    Console.CursorVisible = true;

    //Main Title
    Console.WriteLine("\r\n  _______ ___ _______     _______ _______ ___     _______ ______  ______   _______ _______ \r\n |   _   |   |   _   |   |   _   |   _   |   |   |   _   |   _  \\|   _  \\ |   _   |   _   \\\r\n |.  |___|.  |.  |___|   |.  1___|.  1   |.  |   |.  1___|.  |   |.  |   \\|.  1   |.  l   /\r\n |.  |   |.  |.  |   |   |.  |___|.  _   |.  |___|.  __)_|.  |   |.  |    |.  _   |.  _   1\r\n |:  1   |:  |:  1   |   |:  1   |:  |   |:  1   |:  1   |:  |   |:  1    |:  |   |:  |   |\r\n |::.. . |::.|::.. . |   |::.. . |::.|:. |::.. . |::.. . |::.|   |::.. . /|::.|:. |::.|:. |\r\n `-------`---`-------'   `-------`--- ---`-------`-------`--- ---`------' `--- ---`--- ---'\r\n                                                                                           \r\n");

    Console.Write("Name Of Member: ");
    string memName = Console.ReadLine();

    Console.Write("Age Of Member: ");
    string memAge = Console.ReadLine();

    Console.Write("Join Date Of Member: ");
    string memJoinDate = Console.ReadLine();

    Console.Write("Instrument Of Member: ");
    string memInstrument = Console.ReadLine();

    memDict.addMember(memName, memAge, memJoinDate, memInstrument);
    bandDict.SaveToBinaryFile();
    sucess("Added");
}



//Navigation
int VerticalListNav(List<string> selectedNavItems)
{
    // Used https://www.youtube.com/watch?v=YyD1MRJY0qI to implement arrow key navigation

    
    ConsoleKeyInfo key;
    int option = 1;
    bool selected = false;
    string selector = "\u001b[32m";

    Console.WriteLine($"Use v or ^ to navigate and Enter/Select to choose");
    Console.WriteLine();

    (int left, int top) = Console.GetCursorPosition();
    Console.CursorVisible = false;

    while (selected == false)
    {
        int count = 1;
        Console.SetCursorPosition(left, top);

        foreach (string item in selectedNavItems)
        {
            Console.WriteLine($"{(option == count ? selector : "")}{item}\u001b[0m");
            Console.WriteLine();
            count = count +1;
        }

        key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.DownArrow:
                option = (option == count - 1 ? 1 : option + 1);
                break;

            case ConsoleKey.UpArrow:
                option = (option == 1 ? count - 1 : option - 1);
                break;

            case ConsoleKey.Enter:
                selected = true;
                break;
            //option 999 is the escape clause
            case ConsoleKey.Escape:
                return option=999;
                break;
        }

    }
    return option;
}

int HorizontalListNav(List<string> selectedNavItems, List<int> padding, bool isTable,int borderLength)
{
    // Used https://www.youtube.com/watch?v=YyD1MRJY0qI to implement arrow key navigation


    ConsoleKeyInfo key;
    int option = 1;
    bool selected = false;
    string selector = "\u001b[32m";




    (int left, int top) = Console.GetCursorPosition();
    Console.CursorVisible = false;

    while (selected == false)
    {
        int count = 1;
        Console.SetCursorPosition(left, top);

        foreach (string item in selectedNavItems)
        {
            //all need to be separate write statements so padding is applied correctly
            Console.Write($"{(option == count ? selector : null)}");
            Console.Write($"{item}".PadRight(padding[count - 1]));
            Console.Write("\u001b[0m");

            //if its a table will add side borders
            if (isTable == true)
            {
                Console.Write("|");
            }
            count = count + 1;
        }
        
        //adds bottom of the table if isTable==true
        if (isTable == true)
        {
            Console.WriteLine();
            for (int i = 0; i < borderLength; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.RightArrow:
                option = (option == count - 1 ? 1 : option + 1);
                break;

            case ConsoleKey.LeftArrow:
                option = (option == 1 ? count - 1 : option - 1);
                break;

            case ConsoleKey.Enter:
                selected = true;
                break;
            case ConsoleKey.Escape:
                return option=999;
                break;
        }

    }
    return option;
}

int VerticalDictNav(Dictionary<int,string> selectedNavItems, List<int> padding, bool isTable, int borderLength, string selectedFunction)
{
    //Console.Clear();
    // Used https://www.youtube.com/watch?v=YyD1MRJY0qI to implement arrow key navigation


    // option select
  
    ConsoleKeyInfo key;
    int option = 1;
    bool selected = false;
    string selector = "\u001b[32m";
    (int left, int top) = Console.GetCursorPosition();
    Console.CursorVisible = false;

    while (selected == false)
    {
        
        Console.SetCursorPosition(left, top);
        //Used to assign each gig a number so it can be tracked when selecting options
        int count = 1;



        ////Writes selected items in selected page and assigns option checker to see if it is the one that is being selected
        foreach(string item in selectedNavItems.Values)
        {
            Console.WriteLine($"{(option == count ? selector : "")}{item}");
            count = count + 1;
        }

        if (isTable ==  true)
        {
            //border bottom
            for (int i = 0; i < borderLength; i++)
            {
                Console.Write("-");
            }
        }
       

        for (int i = 0; i < 2; i++)
        {
            Console.WriteLine();
        }

        Console.WriteLine($"Use Enter/Select to choose the Item you wish to {selectedFunction} ...");

        //checks key reading allows nav through options
        key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.DownArrow:
                option = (option == count - 1 ? 1 : option + 1);
                break;

            case ConsoleKey.UpArrow:
                option = (option == 1 ? count - 1 : option - 1);
                break;

            case ConsoleKey.Enter:
                selected = true;
                return option;
                break;
            case ConsoleKey.Escape:
                return option = 999;
                break;
        }
    }
    return option;
}



//misc functions
void homeMenuDisplay(Band selectedBand, Bands bandDict)
{
    Console.Clear();

    List<string> navItems = new List<string>
    {
        "View Gigs",
        "View Members",
        "View Bands",
        "Exit"
    };

    //Main Title
    Console.WriteLine("\r\n  _______ ___ _______     _______ _______ ___     _______ ______  ______   _______ _______ \r\n |   _   |   |   _   |   |   _   |   _   |   |   |   _   |   _  \\|   _  \\ |   _   |   _   \\\r\n |.  |___|.  |.  |___|   |.  1___|.  1   |.  |   |.  1___|.  |   |.  |   \\|.  1   |.  l   /\r\n |.  |   |.  |.  |   |   |.  |___|.  _   |.  |___|.  __)_|.  |   |.  |    |.  _   |.  _   1\r\n |:  1   |:  |:  1   |   |:  1   |:  |   |:  1   |:  1   |:  |   |:  1    |:  |   |:  |   |\r\n |::.. . |::.|::.. . |   |::.. . |::.|:. |::.. . |::.. . |::.|   |::.. . /|::.|:. |::.|:. |\r\n `-------`---`-------'   `-------`--- ---`-------`-------`--- ---`------' `--- ---`--- ---'\r\n                                                                                           \r\n");

    int option = VerticalListNav(navItems);

    //calls specific function depending on selection
    switch (option)
    {
        case 1:
            Dictionary<int, Gig> orderedGigs = new Dictionary<int, Gig>();
            orderedGigs = selectedBand.GetGigCollection().GetGigs().OrderBy(gigInf => gigInf.Value.getDate()).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            ViewGigs(selectedBand.GetGigCollection(),0, bandDict,orderedGigs);
            Console.WriteLine();
            break;
        case 2:
            Dictionary<int, Member> orderedMembers = new Dictionary<int, Member>();
            orderedMembers = selectedBand.getMemberCollection().GetMembers().OrderBy(memInf => memInf.Value.getName()).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            ViewMembers(selectedBand.getMemberCollection(),0, bandDict,orderedMembers);
            break;
        case 3:
            Console.WriteLine("lemon");
            Console.WriteLine();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
            break;
        case 4:
            Console.Clear();
            Environment.Exit(0);
            break;
        case 999:
            Console.Clear();
            Environment.Exit(0);
            break;

        default:
            Console.WriteLine("Selection not in list");
            Console.WriteLine();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
            break;
    }
    return;
};

void sucess(string action)
{
    Console.WriteLine($"Information {action}");
    Console.WriteLine("Please click enter to continue...");
    Console.ReadLine();
}

void TableHeadersWrite(List<int> padding, int borderLength, List<string> headerItems)
{
    //length for top and bottom of table

    for (int i = 0; i < borderLength; i++)
    {
        Console.Write("-");
    }
    Console.WriteLine();

    //Writes table header with correct padding

    int counter = 0;
    foreach (string item in headerItems) 
    {
        Console.Write(item.PadRight(padding[counter]));
        Console.Write('|');
        counter++;
    }
    Console.WriteLine();
}

List<string> GetNavItems (string item, int pageNumber)
    {
        List<string> navItems = new List<string>(6);

        if (pageNumber > 0)
        {
            navItems = new List<string>()
        {
            "Next Page",
            "Previous",
            $"Add {item}",
            $"Edit {item}",
            $"Remove {item}",
            "Back"
        };
        }
        else
        {
            navItems = new List<string>
        {
            $"Add {item}",
            $"Edit {item}",
            $"Remove {item}",
            "Back"
        };
        }
        return navItems;
    }

int GetBandCountFromFile()
{
    int bandCount = 0;
    try
    {
        //Reads the number of bands stored in file so the Bands dictionary can be created with a correct sized length
        FileStream file = File.Open("bandInfo.dat", FileMode.Open);
        BinaryReader br = new BinaryReader(file);
        bandCount = br.ReadInt32();
        br.Close();
        file.Close();
    }
    catch (FileNotFoundException e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("Creating File");
        try
        {
            File.Create("bandInfo.dat").Close();
            Console.WriteLine("input '--help' for a list of commands");
            Console.WriteLine();
            Console.Write("Press Enter to Continue...");
            Console.ReadLine();
            Environment.Exit(1);
        }
        catch (Exception e2)
        {
            Console.WriteLine(e2.Message);
            Environment.Exit(1);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Environment.Exit(1);
    }

    return bandCount;
}



//Testing
void multiProcessTest()
{
    

    Bands bandDict = new Bands(0);
    Gigs gigDict = new Gigs(0);
    Members memberDict = new Members(0);
    

    for (int i = 0; i<100; i++)
    {
        //memberDict.addMember("bruh", "12", "12/11/2023", "guitar");
        gigDict.addGig("test gig", "12/12/2012", "9:00", "12", "300", "Test Adress", "TEST");
        //Console.WriteLine(i);
    }

    for (int i = 0; i < 100; i++)
    {
        Band testBand = new Band($"test{Convert.ToString(i)}");
        testBand.setMembers(memberDict);
        testBand.setGigs(gigDict);
        bandDict.storeBand(testBand);
    }

    Console.WriteLine("Write");

    var timer = Stopwatch.StartNew();
    bandDict.SaveToBinaryFile();
    timer.Stop();
    Console.WriteLine($"Non-parallel ordering: {timer.ElapsedMilliseconds} ms ");

    ConcurrentDictionary<string, Band> conBands = new ConcurrentDictionary<string, Band>(bandDict.GetBands());


    var timer2 = Stopwatch.StartNew();
    bandDict.SaveToBinaryFileParallel(conBands);
    timer2.Stop();
    Console.WriteLine($"parallel ordering: {timer2.ElapsedMilliseconds} ms ");



    //Console.WriteLine("Sort Test");
    //var timer = Stopwatch.StartNew();
    //Dictionary<int,Gig> orderedGigs1 = gigDict.GetGigs().OrderBy(gigInf => gigInf.Value.getDate()).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    //timer.Stop();
    //Console.WriteLine($"Non-parallel ordering: {timer.ElapsedMilliseconds} ms ");

    //var timer2 = Stopwatch.StartNew();
    //Dictionary<int, Gig> orderedGigs2 = gigDict.GetGigs().AsParallel().OrderBy(gigInf => gigInf.Value.getDate()).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    //timer2.Stop();
    //Console.WriteLine($"parallel ordering: {timer2.ElapsedMilliseconds} ms");

    int stopper = 32623;

}

DateOnly RandomDay()
{
    // used https://stackoverflow.com/questions/194863/random-date-in-c-sharp to get random day function

    Random rnd = new Random();
    DateTime start = new DateTime(1995, 1, 1);
    int range = (DateTime.Today - start).Days;
    var randDate = start.AddDays(rnd.Next(range));
    return DateOnly.FromDateTime(randDate);
}