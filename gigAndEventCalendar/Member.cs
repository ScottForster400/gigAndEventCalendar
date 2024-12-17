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
    internal class Member : IFileManagment,IClassUi
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

        public void SetName(string name)
        {
            this.name = name;
        }
        public bool SetAge(string selectedAge)
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
        public bool SetJoinDate(string selectedJoinDate)
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
        public void SetInstrument(string instrument)
        {
            this.instrument = instrument;
        }


        //get commands
        public List<string> GetInfo()
        {
            List<string> info = new List<string>()
            {
                $"|{Convert.ToString(memberId)}",
                $"{name}",
                $"{Convert.ToString(age)}",
                $"{GetJoinDateString()}",
                $"{instrument}"
            };
            return info;
        }
        public string GetFormatted(List<int> pad)
        {
            return $"|{Convert.ToString(memberId).PadRight(pad[0] - 1)}|{name.PadRight(pad[1])}|{Convert.ToString(age).PadRight(pad[2])}|{GetJoinDateString().PadRight(pad[3])}|{instrument.PadRight(pad[4])}|";

        }
        
        public int GetMemberId()
        {
            return memberId;
        }
        public string GetName()
        {
            return name;
        }
        public int GetAge()
        {
            return age;
        }
        public DateOnly GetJoinDate()
        {
            return joinDate;
        }
        public string GetJoinDateString()
        {
            return joinDate.ToString("MM/yyyy");
        }
        public string GetInstrument()
        {
            return instrument;
        }

        //file writing

        public void WriteBinary(BinaryWriter bw)
        {
            bw.Write(name);
            bw.Write(age);
            bw.Write(Convert.ToString(joinDate));
            bw.Write(instrument);
            
        }
    }
}