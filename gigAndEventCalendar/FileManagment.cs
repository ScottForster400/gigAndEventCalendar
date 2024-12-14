using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gigAndEventCalendar
{
    internal abstract class FileManagment
    {
        public abstract void writeBinary(BinaryWriter bw);

        //public abstract void readBinary(BinaryReader br);
    }
}
