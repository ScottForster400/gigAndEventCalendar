
using gigAndEventCalendar;
using System.Collections.Concurrent;

namespace gigAndEventCalendar
{
    internal class Band : IFileManagment
    {

        private string bandName;
        private Members bandMembers;
        private Gigs bandGigs;


        public Band(string bandName)
        {
            this.bandName = bandName;

        }

        //Initialise Collections - this is done so i can correctly pass in the length needed for each collection

        public void InitialiseMemCollection(int dictLength)
        {
            bandMembers = new Members(dictLength);
        }
        public void InitialiseGigCollection(int dictLength)
        {
            bandGigs = new Gigs(dictLength);
        }


        //set commands
        public void SetName(string bandName)
        {
            this.bandName = bandName;
        }

        public void SetGigs(Gigs gigs)
        {
            bandGigs = gigs;
        }

        public void SetMembers(Members members)
        {
            bandMembers = members;
        }


        //get commands
        public string GetName()
        {
            return bandName;
        }
        public Members GetMemberCollection()
        {
            return bandMembers;
        }
        public Gigs GetGigCollection()
        {
            return bandGigs;
        }


        //file management

        public void WriteBinary(BinaryWriter bw)
        {
            bw.Write(bandName);
            int memberCount= bandMembers.GetCount();
            bw.Write(memberCount);
            foreach(Member member in bandMembers.GetMembers().Values)
            {
                member.WriteBinary(bw);
            }
            int gigCount = bandGigs.GetCount();
            bw.Write(gigCount);
            foreach(Gig gig in bandGigs.GetGigs().Values)
            {
                gig.WriteBinary(bw);
            }
        }
    }
}