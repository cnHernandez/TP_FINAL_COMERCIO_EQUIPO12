using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Proveedores
    {
        public int IdProveedor {  get; set; }
        public string Nombre {  get; set; }
        public string Categoria { get; set; }
        public bool Estado { get; set; }
        public string UrlImagen { get; set; }
    }
}
