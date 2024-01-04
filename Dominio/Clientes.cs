using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Clientes
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail {  get; set; }
        public long Dni {  get; set; }  
        public long Telefono { get; set; }
        public bool Estado {  get; set; }
    }
}
