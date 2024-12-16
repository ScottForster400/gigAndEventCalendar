
using gigAndEventCalendar;
using System.Collections.Concurrent;

namespace gigAndEventCalendar
{
    internal class Band : FileManagment
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
        public void setName(string bandName)
        {
            this.bandName = bandName;
        }

        public void setGigs(Gigs gigs)
        {
            bandGigs = gigs;
        }

        public void setMembers(Members members)
        {
            bandMembers = members;
        }


        //get commands
        public string getName()
        {
            return bandName;
        }
        public Members getMemberCollection()
        {
            return bandMembers;
        }
        public Gigs GetGigCollection()
        {
            return bandGigs;
        }


        //file management

        public override void writeBinary(BinaryWriter bw)
        {
            bw.Write(bandName);
            int memberCount= bandMembers.getCount();
            bw.Write(memberCount);
            foreach(Member member in bandMembers.GetMembers().Values)
            {
                member.writeBinary(bw);
            }
            int gigCount = bandGigs.getCount();
            bw.Write(gigCount);
            foreach(Gig gig in bandGigs.GetGigs().Values)
            {
                gig.writeBinary(bw);
            }
        }

        public  void writeBinaryCon(BinaryWriter bw)
        {
            bw.Write(bandName);
            int memberCount = bandMembers.getCount();
            bw.Write(memberCount);
            ConcurrentDictionary<int, Member> conMems = new ConcurrentDictionary<int, Member>(bandMembers.GetMembers());
            
            Parallel.ForEach(conMems.Values, member =>
            {
                member.writeBinary(bw);
            });
            int gigCount = bandGigs.getCount();
            ConcurrentDictionary<int, Gig> conBand = new ConcurrentDictionary<int, Gig>(bandGigs.GetGigs());
            bw.Write(gigCount);
            Parallel.ForEach(conBand.Values, gig =>
            {
                gig.writeBinary(bw);
            });
        }
    }
}