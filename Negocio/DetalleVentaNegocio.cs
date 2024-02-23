﻿using System;
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
                datos.setearParametros("@idVenta", detalleVenta.IdVenta);
                datos.setearParametros("@productoId", detalleVenta.IdProducto);
                datos.setearParametros("@cantidad", detalleVenta.Cantidad);
                datos.setearParametros("@precioVenta", detalleVenta.PrecioVenta);
                datos.setearParametros("@subtotal", detalleVenta.Subtotal);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public List<DetalleVenta> ObtenerDetallesPorIdVenta(int idVenta, string cliente)
        {
            try
            {
                List<DetalleVenta> detallesVenta = new List<DetalleVenta>();

                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("select dv.DetalleVentaID, dv.VentaID, dv.ProductoID, c.Nombre, p.Nombre,  dv.Cantidad, dv.PrecioVenta, dv.Subtotal from DetalleVenta dv inner join Ventas v on dv.VentaID = v.VentaID inner join Clientes c on c.ClienteID = v.ClienteID inner join Productos p on p.ProductoID = dv.ProductoID");
                    Datos.setearParametros("@IdCompra", idVenta);
                    Datos.EjecutarLectura();

                    while (Datos.lector.Read())
                    {
                        DetalleVenta detalle = new DetalleVenta();
                        detalle.IdDetalleVenta = Datos.lector.GetInt32(0);
                        detalle.IdVenta = Datos.lector.GetInt32(1);
                        detalle.IdProducto = Datos.lector.GetInt32(2);
                        detalle.NombreCliente = Datos.lector.GetString(3);
                        detalle.NombreProducto = Datos.lector.GetString(4);
                        detalle.Cantidad = Datos.lector.GetInt32(5);
                        detalle.PrecioVenta = Datos.lector.GetDecimal(6);
                        detalle.Subtotal = Datos.lector.GetDecimal(7);

                        // Obtener el nombre del proveedor
                        //int clienteID = Datos.lector.GetInt32(7);

                        //ClientesNegocio negocio = new ClientesNegocio();
                        //detalle.NombreCliente = cliente;
                        //detallesVenta.Add(detalle);
                        //detallesCompra = EliminarDuplicados(detallesCompra);
                    }
                }

                return detallesVenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

