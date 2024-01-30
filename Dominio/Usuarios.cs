using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Usuarios
    {
        public enum TipoUsuarios
        {
            administrador = 1,
            vendedor = 2   
        }
        public int IdUsuario {  get; set; }
        public string Nombre {  get; set; }
        public string Contrasena {  get; set; }
        public TipoUsuarios TipoUsuario { get; set; }

        public Usuarios(string user, string pass, bool esAdmin, bool esVendedor)
        {
            Nombre = user;
            Contrasena = pass;

            TipoUsuario = esAdmin ? TipoUsuarios.administrador : TipoUsuarios.vendedor;
        }

        
        }
    }

