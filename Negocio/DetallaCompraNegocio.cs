using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using Dominio; 

public class DetalleCompraNegocio
{
    public void InsertarDetalleCompra( DetalleCompra detalleCompra)
    {
        try
        {
            using (AccesoDatos Datos = new AccesoDatos())
            {
                Datos.SetearQuery("INSERT INTO DetalleCompra (CompraID, ProductoID, Cantidad, PrecioCompra, Subtotal) VALUES (@IdCompra, @IdProducto, @Cantidad, @PrecioCompra, @Subtotal) ");



                Datos.setearParametros("@IdCompra", detalleCompra.IdCompra);
                Datos.setearParametros("@IdProducto", detalleCompra.IdProducto);
                Datos.setearParametros("@Cantidad", detalleCompra.Cantidad);
                Datos.setearParametros("@PrecioCompra", detalleCompra.PrecioCompra);
                Datos.setearParametros("@Subtotal", detalleCompra.Subtotal);

                Datos.ejecutarAccion();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

   
}



