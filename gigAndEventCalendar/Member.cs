using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using gigAndEventCalendar;

namespace gigAndEventCalendar
{
    internal class Member : FileManagment
    {

        private int memberId;
        private string name;
        private int age;
        private DateOnly joinDate;
        private string instrument;

        public Member(int memberId, string name, int age, DateOnly joinDate, string instrument)
        {
            this.memberId = memberId;
            this.name = name;
            this.age = age;
            this.joinDate = joinDate;
            this.instrument = instrument;
        }

        //set commands

        public void setName(string name)
        {
            this.name = name;
        }
        public bool setAge(string selectedAge)
        {
            int ageConv;
            if (int.TryParse(selectedAge, out ageConv) == false)
            {
                Console.WriteLine("ERROR: Invalid Input");
                Console.WriteLine();
                return false;
            }
            else
            {
                age = ageConv;
                return true;
            }
        }
        public bool setJoinDate(string selectedJoinDate)
        {

            DateOnly joinDateConv;
            if (DateOnly.TryParse(selectedJoinDate, out joinDateConv) == false)
            {
                Console.WriteLine("ERROR: Invalid Input");
                Console.WriteLine();
                return false;
            }
            else
            {
                joinDate = joinDateConv;
                return true;
            }
        }
        public void setInstrument(string instrument)
        {
            this.instrument = instrument;
        }


        //get commands
        public List<string> getInfo()
        {
            List<string> info = new List<string>()
            {
                $"|{Convert.ToString(memberId)}",
                $"{name}",
                $"{Convert.ToString(age)}",
                $"{getJoinDateString()}",
                $"{instrument}"
            };
            return info;
        }
        public string getFormatted(List<int> pad)
        {
            return $"|{Convert.ToString(memberId).PadRight(pad[0] - 1)}|{name.PadRight(pad[1])}|{Convert.ToString(age).PadRight(pad[2])}|{getJoinDateString().PadRight(pad[3])}|{instrument.PadRight(pad[4])}|";

        }
        
        public int getMemberId()
        {
            return memberId;
        }
        public string getName()
        {
            return name;
        }
        public int getAge()
        {
            return age;
        }
        public DateOnly getJoinDate()
        {
            return joinDate;
        }
        public string getJoinDateString()
        {
            return joinDate.ToString("MM/yyyy");
        }
        public string getInstrument()
        {
            return instrument;
        }

        //file writing

        public override void writeBinary(BinaryWriter bw)
        {
            bw.Write(name);
            bw.Write(age);
            bw.Write(Convert.ToString(joinDate));
            bw.Write(instrument);
            
        }
    }
}