using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compras
    {
        public int IdCompras {  get; set; }
        public int IdProveedor { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal TotalCompra { get; set; }
        public bool  Estado { get; set; }
    }
}
