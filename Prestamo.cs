using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreria
{
    public class Prestamo
    {
        public int? noPrestamo { get; set; }

        public int? noLector { get; set; }

        public long? isbn { get; set; }

        public string? Fecha { get; set;  }
    }
}
