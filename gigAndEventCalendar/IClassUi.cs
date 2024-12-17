using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gigAndEventCalendar
{
    internal interface IClassUi
    {
        public List<string> GetInfo();

        public String GetFormatted(List<int> pad);
    }
}
