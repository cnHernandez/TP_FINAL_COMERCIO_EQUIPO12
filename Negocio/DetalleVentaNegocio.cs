using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
     public class DetalleVentaNegocio
    {
        public void insertarDetalleVenta(DetalleVenta detalleVenta)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetearQuery("insert into DetalleVenta (VentaID,ProductoID,Cantidad,PrecioVenta,Subtotal) values (@idVenta, @productoId, @cantidad, @precioVenta, @subtotal)");
                datos.setearParametros("@idVenta",detalleVenta.IdVenta);
                datos.setearParametros("@productoId",detalleVenta.IdProducto);
                datos.setearParametros("@cantidad",detalleVenta.Cantidad);
                datos.setearParametros("@precioVenta",detalleVenta.PrecioVenta);
                datos.setearParametros("@subtotal",detalleVenta.Subtotal);

                datos.ejecutarAccion();
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }
    }
}
