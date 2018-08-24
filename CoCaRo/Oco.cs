using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoCaRo
{
    class Oco:Button
    {
        int dong, cot;

        public int Cot
        {
            get { return cot; }
            set { cot = value; }
        }

        public int Dong
        {
            get { return dong; }
            set { dong = value; }
        }
    }
}
