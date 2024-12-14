using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using gigAndEventCalendar;

namespace gigAndEventCalendar
{
    internal class Gig : FileManagment
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

        public void setName(string name)
        {
            this.name = name;
        }

        public bool setDate(string selectedGigDate)
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

        public bool setTime(string selectedTime)
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

        public bool setPrice(string selectedPrice)
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

        public bool setCapacity(string selectedCapacity)
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

        public void setPostcode(string postcode)
        {
            this.postcode = postcode;
        }

        public void editAddress(string address)
        {
            this.address = address;
        }





        // Get Commands

        public List<string> getInfo()
        {
            List<string> info = new List<string>()
            {
                $"|{Convert.ToString(gigId)}",
                $"{name}",
                $"{getDate()}",
                $"{getTime()}",
                $"{Convert.ToString(price)}",
                $"{Convert.ToString(capacity)}",
                $"{address}",
                $"{postcode}"
            };
            return info;
        }

        public string getFormatted(List<int> pad)
        {
            return $"|{Convert.ToString(gigId).PadRight(pad[0] - 1)}|{name.PadRight(pad[1])}|{getDateString().PadRight(pad[2])}|{getTime().PadRight(pad[3])}|{Convert.ToString(price).PadRight(pad[4])}|{Convert.ToString(capacity).PadRight(pad[5])}|{address.PadRight(pad[6])}|{postcode.PadRight(pad[7])}|";

        }

        public string getFormattedFile()
        {
            return $"{Convert.ToString(gigId)}|{name}|{getDateString()}|{getTime()}|{Convert.ToString(price)}|{Convert.ToString(capacity)}|{address}|{postcode}";
        }

        public int getGigId()
        {
            return gigId;
        }

        public string getName()
        {
            return name;
        }
        public string getDateString()
        {
            return gigDate.ToString("dd/MM/yyyy");

        }
        public DateOnly getDate()
        {
            return gigDate;
        }
        public string getTime()
        {
            return gigTime.ToString();
        }
        public int getPrice()
        {
            return price;
        }
        public int getCapacity()
        {
            return capacity;
        }
        public string getAddress()
        {
            return address;
        }
        public string getPostcode()
        {
            return postcode;
        }

        //File Management
        public override void writeBinary(BinaryWriter bw)
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