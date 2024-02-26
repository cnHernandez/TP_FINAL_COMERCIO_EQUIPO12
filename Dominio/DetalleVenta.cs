using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string NombreProveedor { get; set; }
        public string NombreProducto { get; set; }
        public string NombreCliente { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Subtotal { get; set; }


    }
}