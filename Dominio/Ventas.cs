using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ventas
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public decimal TotalVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public List<DetalleVenta> DetallesVenta { get; set; }
        public bool Estado { get; set; }
    }
}
