using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gigAndEventCalendar;

namespace gigAndEventCalendar
{
    internal class Bands
    {
        private Dictionary<string, Band> bandDict;
        private string filePath = "bandInfo.dat";

        public Bands()
        {
            bandDict = new Dictionary<string, Band>();
        }


        public void AddBand(string bandName)
        {
            try
            {
                if (bandDict.ContainsKey(bandName))
                {
                    Console.WriteLine("Band already exists");
                    Console.WriteLine();
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    bandDict.Add(bandName, new Band(bandName));
                    Console.WriteLine("Band successfully added");
                    Console.WriteLine();
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine();
                Console.Write("Press enter to exit...");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public void AddBandFromFile(string bandName)
        {
            bandDict.Add(bandName, new Band(bandName));
        }

        public void storeBand(Band selectedBand)
        {
            bandDict.Add(selectedBand.getName(), selectedBand);
        }



        //Get Bands
        public Dictionary<string,Band> GetBands()
        {

            return bandDict;
        }

        public Band GetBand(string bandName)
        {
            if (bandDict.ContainsKey(bandName))
            {
                return bandDict[bandName];
            }
            else
            {
                return null; ;
            }
        }

        //file management

        public void SaveToBinaryFile()
        {
            try
            {
                FileStream file = File.Open(filePath, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(file);
                int bandCount = bandDict.Count;
                bw.Write(bandCount);
                foreach (Band band in bandDict.Values)
                {
                    band.writeBinary(bw);
                }
                bw.Close();
                file.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Creating File");
                try
                {
                    File.Create(filePath).Close();
                    Console.Write("Press Enter to Continue...");
                    Console.ReadLine();
                    Console.Clear();
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

        }

        public void ReadBinaryFile()
        { 
            int skipped = 0;
            try
            {
                FileStream file = File.Open(filePath, FileMode.Open);
                BinaryReader br = new BinaryReader(file);
                if (file.Length == 0)
                {
                    br.Close();
                    file.Close();
                    return;
                }
                int bandCount = br.ReadInt32();
                for (int j = 0; j < bandCount; j++)
                {
                    string bandName = br.ReadString();
                    AddBandFromFile(bandName);

                    //gets the pointer for number of members and loops through to create that amount of members 
                    int memberCount = br.ReadInt32();
                    for (int i = 0; i < memberCount; i++)
                    {
                        string memName = br.ReadString();
                        int memAge = br.ReadInt32();
                        string memJoinDate = br.ReadString();
                        string memInstrument = br.ReadString();

                        bandDict[bandName].getMembers().addMember(memName, Convert.ToString(memAge), memJoinDate, memInstrument);
                    }

                    //gets the pointer for number of members and loops through to create that amount of members 
                    int gigCount = br.ReadInt32();
                    for (int i = 0; i < gigCount; i++)
                    {
                        string gigName = br.ReadString();
                        string gigDate = br.ReadString();
                        string gigTime = br.ReadString();
                        int gigPrice = br.ReadInt32();
                        int gigCapacity = br.ReadInt32();
                        string gigAdress = br.ReadString();
                        string gigPostcode = br.ReadString();

                        bandDict[bandName].GetGigs().addGig(gigName, gigDate, gigTime, Convert.ToString(gigPrice), Convert.ToString(gigCapacity), gigAdress, gigPostcode);
                    }
                }
                br.Close();
                file.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Creating File");
                try
                {
                    File.Create(filePath).Close();
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


        }
    }


}

