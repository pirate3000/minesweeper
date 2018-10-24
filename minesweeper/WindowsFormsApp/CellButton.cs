using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    class CellButton : Button
    {
        public uint Row { get; set; }
        public uint Column { get; set; }
    }
}
