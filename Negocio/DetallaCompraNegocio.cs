using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using Dominio; 

public class DetalleCompraNegocio
{   public void InsertarDetalleCompra( DetalleCompra detalleCompra)
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

    public List<DetalleCompra> ObtenerDetallesPorIdCompra(int idCompra, string pro)
    {
        try
        {
            List<DetalleCompra> detallesCompra = new List<DetalleCompra>();

            using (AccesoDatos Datos = new AccesoDatos())
            {
                Datos.SetearQuery("SELECT dc.DetalleCompraID, dc.CompraID, dc.ProductoID, dc.Cantidad, dc.PrecioCompra, dc.Subtotal, p.Nombre AS NombreProducto, pxp.ProveedorID " +
                                  "FROM DetalleCompra dc " +
                                  "INNER JOIN Productos p ON dc.ProductoID = p.ProductoID " +
                                  "INNER JOIN Producto_x_Proveedor pxp ON dc.ProductoID = pxp.ProductoID " +
                                  "WHERE dc.CompraID = @IdCompra");
                Datos.setearParametros("@IdCompra", idCompra);
                Datos.EjecutarLectura();

                while (Datos.lector.Read())
                {
                    DetalleCompra detalle = new DetalleCompra();
                    detalle.IdDetalleCompra = Datos.lector.GetInt32(0);
                    detalle.IdCompra = Datos.lector.GetInt32(1);
                    detalle.IdProducto = Datos.lector.GetInt32(2);
                    detalle.Cantidad = Datos.lector.GetInt32(3);
                    detalle.PrecioCompra = Datos.lector.GetDecimal(4);
                    detalle.Subtotal = Datos.lector.GetDecimal(5);
                    detalle.NombreProducto = Datos.lector.GetString(6);

                    // Obtener el nombre del proveedor
                    int proveedorID = Datos.lector.GetInt32(7);

                    ProveedoresNegocio negocio = new ProveedoresNegocio();
                    detalle.NombreProveedor = pro;                 
                    detallesCompra.Add(detalle);
                    detallesCompra = EliminarDuplicados(detallesCompra);
                }
            }

            return detallesCompra;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private List<DetalleCompra> EliminarDuplicados(List<DetalleCompra> detallesCompra)
    {
        List<DetalleCompra> detallesFiltrados = new List<DetalleCompra>();
        HashSet<int> idsAgregados = new HashSet<int>();

        foreach (var detalle in detallesCompra)
        {
            if (!idsAgregados.Contains(detalle.IdProducto))
            {
                detallesFiltrados.Add(detalle);
                idsAgregados.Add(detalle.IdProducto);
            }
        }

        return detallesFiltrados;
    }


}



