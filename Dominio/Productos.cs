using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Productos
    {
        public int IdProductos {  get; set; }
        public string Nombre {  get; set; }
       
        public decimal PorcentajeGanancia { get; set; }
        public int StockActual {  get; set; }
        public int StockMinimo { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }
        public bool Estado {  get; set; }
        public string UrlImagen { get; set; }
        public List<Producto_x_Proveedor> ProductosXProveedores { get; set; }
    }
}
