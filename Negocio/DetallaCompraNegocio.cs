using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using Dominio; 

public class DetalleCompraNegocio
{
    public List<DetalleCompra> ListarCompras(string id = "")
    {
        List<DetalleCompra> Lista = new List<DetalleCompra>();
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.SetearQuery("select dc.CompraID, p.Nombre as NombreProducto, pv.Nombre as NombreProveedor, dc.Cantidad, dc.PrecioCompra, dc.Subtotal, c.FechaCompra from DetalleCompra dc inner join Productos p on p.ProductoID = dc.ProductoID\r\ninner join Producto_x_Proveedor pp on pp.ProductoID = p.ProductoID inner join Proveedores pv on pv.ProveedorID = pp.ProveedorID inner join Compras c on c.CompraID = dc.CompraID");

            if (!string.IsNullOrEmpty(id))
            {
                datos.Comando.CommandText += " AND c.CompraID = @Id";
                datos.setearParametros("@Id", id);
            }

            datos.EjecutarLectura();

            while (datos.lector.Read())
            {
                DetalleCompra aux = new DetalleCompra();
              
                aux.IdCompra = (int)datos.lector["CompraID"];
                aux.NombreProducto = (string)datos.lector["NombreProducto"];
                aux.NombreProveedor = (string)datos.lector["NombreProveedor"];
                aux.Cantidad = (int)datos.lector["Cantidad"];
                aux.PrecioCompra = (decimal)datos.lector["PrecioCompra"];
                aux.Subtotal = (decimal)datos.lector["Subtotal"];
                aux.Fecha = (DateTime)datos.lector["FechaCompra"];

                Lista.Add(aux);
            }

            return Lista;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            datos.CerrarConexion();
        }
    }

    


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



