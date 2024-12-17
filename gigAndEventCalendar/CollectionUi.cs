using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace gigAndEventCalendar
{
    internal abstract class CollectionUi
    {
        public abstract List<int> GetPadding();

        public abstract  List<string> GetHeaderItems();
    
        public abstract string Truncate(string selectedText, int maxLength);
    }
}
