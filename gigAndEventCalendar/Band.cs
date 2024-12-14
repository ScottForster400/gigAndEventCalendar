
using gigAndEventCalendar;

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
            bandMembers = new Members();
            bandGigs = new Gigs();

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
        public Members getMembers()
        {
            return bandMembers;
        }
        public Gigs GetGigs()
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
    }
}