using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductosNegocio
    {
        public List<Productos> ListarProductos(string id = "")
        {
            List<Productos> Lista = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("select p.Nombre, p.PrecioCompra, p.PorcentajeGanancia, p.StockActual, p.StockMinimo, p.TipoID, p.MarcaID from Productos p where Estado = 0");
                if (!string.IsNullOrEmpty(id))
                {
                    datos.Comando.CommandText += " and ProductoID = @Id";
                    datos.setearParametros("@Id", Convert.ToInt32(id));
                }

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Productos aux = new Productos();

                    aux.IdProductos = (int)datos.lector["ProductoID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.PrecioCompra = (decimal)datos.lector["PrecioCompra"];
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.IdMarca = (int)datos.lector["MarcaID"];
                    aux.Estado = (bool)datos.lector["Estado"];
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


        public long AgregarProducto(Productos nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("insert into Productos (Nombre, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, TipoID,MarcaID, UrlImagen, Estado) values (@Nombre, @PrecioCompra, @PorcentajeGanancia, @StockActual,@StockMinimo, @TipoID, @MarcaID,@UrlImagen, @Estado); SELECT SCOPE_IDENTITY();");

                    Datos.setearParametros("@Nombre", nuevo.Nombre);
                    Datos.setearParametros("@PrecioCompra", nuevo.PrecioCompra);
                    Datos.setearParametros("@PorcentajeGanancia", nuevo.PorcentajeGanancia);
                    Datos.setearParametros("@StockActual", nuevo.StockActual);
                    Datos.setearParametros("@StockMinimo", 50);
                    Datos.setearParametros("@TipoID", nuevo.IdCategoria);
                    Datos.setearParametros("@MarcaID", nuevo.IdMarca);
                    Datos.setearParametros("@UrlImagen", nuevo.UrlImagen);
                    Datos.setearParametros("@Estado", 0);

                    long ID = Convert.ToInt64(Datos.ejecutarScalar());

                    // Asignar el ID generado al objeto 
                    nuevo.IdProductos = (int)ID;

                    return ID;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void EliminarProducto(int IdProducto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("update Productos set estado=1 where ProductoID =@IdProducto");
                datos.setearParametros("@IdProducto", IdProducto);
                datos.ejecutarAccion();

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
    }
}

