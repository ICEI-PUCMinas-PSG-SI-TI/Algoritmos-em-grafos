using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhGrafos
{
    internal class Celula
    {
        public int no;
        public int peso;
        public Celula Next;

        public Celula(int no, int peso)
        {
            this.no = no;
            Next = null;
            this.peso = peso;
        }
    }
}
