using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using gigAndEventCalendar;

namespace gigAndEventCalendar
{
    internal class Gig : IFileManagment,IClassUi
    {

        private int gigId;
        private string name;
        private DateOnly gigDate;
        private TimeOnly gigTime;
        private int price;
        private int capacity;
        private string address;
        private string postcode;

        public Gig(int gigId, string name, DateOnly gigDate, TimeOnly gigTime, int price, int capacity, string address, string postcode)
        {
            this.gigId = gigId;
            this.name = name;
            this.gigDate = gigDate;
            this.gigTime = gigTime;
            this.price = price;
            this.capacity = capacity;
            this.address = address;
            this.postcode = postcode;
        }


        //set commands

        public void SetName(string name)
        {
            this.name = name;
        }

        public bool SetDate(string selectedGigDate)
        {

            DateOnly gigDateConv;
            if (DateOnly.TryParse(selectedGigDate, out gigDateConv) == false)
            {
                Console.WriteLine("ERROR: Invalid Input");
                Console.WriteLine();
                return false;
            }
            else
            {
                gigDate = gigDateConv;
                return true;
            }
        }

        public bool SetTime(string selectedTime)
        {
            TimeOnly gigTimeConv;
            if (TimeOnly.TryParse(selectedTime, out gigTimeConv) == false)
            {
                Console.WriteLine("ERROR: Invalid Input");
                Console.WriteLine();
                return false;
            }
            else
            {
                gigTime = gigTimeConv;
                return true;
            }
        }

        public bool SetPrice(string selectedPrice)
        {
            int priceConv;
            if (int.TryParse(selectedPrice, out priceConv) == false)
            {
                Console.WriteLine("ERROR: Invalid Input");
                Console.WriteLine();
                return false;
            }
            else
            {
                price = priceConv;
                return true;
            }
        }

        public bool SetCapacity(string selectedCapacity)
        {
            int capacityConv;
            if (int.TryParse(selectedCapacity, out capacityConv) == false)
            {
                Console.WriteLine("ERROR: Invalid Input");
                Console.WriteLine();
                return false;
            }
            else
            {
                capacity = capacityConv;
                return true;
            }
        }

        public void SetPostcode(string postcode)
        {
            this.postcode = postcode;
        }

        public void EditAddress(string address)
        {
            this.address = address;
        }


        // Get Commands

        public List<string> GetInfo()
        {
            List<string> info = new List<string>(8)
            {
                $"|{Convert.ToString(gigId)}",
                $"{name}",
                $"{getDate()}",
                $"{GetTime()}",
                $"{Convert.ToString(price)}",
                $"{Convert.ToString(capacity)}",
                $"{address}",
                $"{postcode}"
            };
            return info;
        }

        public string GetFormatted(List<int> pad)
        {
            return $"|{Convert.ToString(gigId).PadRight(pad[0] - 1)}|{name.PadRight(pad[1])}|{GetDateString().PadRight(pad[2])}|{GetTime().PadRight(pad[3])}|{Convert.ToString(price).PadRight(pad[4])}|{Convert.ToString(capacity).PadRight(pad[5])}|{address.PadRight(pad[6])}|{postcode.PadRight(pad[7])}|";

        }

        public int GetGigId()
        {
            return gigId;
        }

        public string GetName()
        {
            return name;
        }
        public string GetDateString()
        {
            return gigDate.ToString("dd/MM/yyyy");

        }
        public DateOnly getDate()
        {
            return gigDate;
        }
        public string GetTime()
        {
            return gigTime.ToString();
        }
        public int GetPrice()
        {
            return price;
        }
        public int GetCapacity()
        {
            return capacity;
        }
        public string GetAddress()
        {
            return address;
        }
        public string GetPostcode()
        {
            return postcode;
        }

        //File Management
        public void WriteBinary(BinaryWriter bw)
        {
            bw.Write(name);
            bw.Write(Convert.ToString(gigDate));
            bw.Write(Convert.ToString(gigTime));
            bw.Write(price);
            bw.Write(capacity);
            bw.Write(address);
            bw.Write(postcode);
        }
    }
}