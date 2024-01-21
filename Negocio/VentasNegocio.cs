﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    class VentasNegocio
    {
        public List<Ventas> ListarVentas(string id = "")
        {
            List<Ventas> Lista = new List<Ventas>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("select VentaID,ClienteID,FechaVenta,TotalVenta,Estado from Ventas where Estado=0");
                if (!string.IsNullOrEmpty(id))
                {
                    datos.Comando.CommandText += " and VentaID = @Id";
                    datos.setearParametros("@Id", Convert.ToInt32(id));
                }

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Ventas aux = new Ventas();
                    aux.IdVenta = (int)datos.lector["VentaID"];
                    aux.IdCliente = (int)datos.lector["CLienteID"];
                    aux.FechaVenta = (DateTime)datos.lector["FechaVenta"];
                    aux.TotalVenta = (decimal)datos.lector["TotalVenta"];
                    aux.Estado = (bool)datos.lector["Estado"]; 
                    
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

        public long AgregarVenta(Ventas nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("INSERT INTO Ventas(ClienteID,FechaVenta,TotalVenta,Estado) VALUES (@ClienteID,@FechaVenta,@TotalVenta,@Estado); SELECT SCOPE_IDENTITY();");

                    Datos.setearParametros("@ClienteID", nuevo.IdCliente);
                    Datos.setearParametros("@FechaVenta", nuevo.FechaVenta);
                    Datos.setearParametros("@TotalVendido", nuevo.TotalVenta);
                    Datos.setearParametros("@Estado", 0);

                    long idVenta = Convert.ToInt64(Datos.ejecutarScalar());

                    // Asignar el ID generado al objeto Medico
                    nuevo.IdVenta = (int)idVenta;

                    return idVenta;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void ModificarVenta(Ventas nuevo)
        {
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetearQuery("UPDATE Ventas SET ClienteID=@ClienteID,FechaVenta=@FechaVenta,TotalVenta=@TotalVenta where VentaID=@VentaID");

                Datos.setearParametros("@ClienteID", nuevo.IdCliente);
                Datos.setearParametros("@FechaVenta", nuevo.FechaVenta);
                Datos.setearParametros("@TotalVenta", nuevo.TotalVenta);
                Datos.setearParametros("@Estado", 0);
                Datos.setearParametros("@VentaID", nuevo.IdVenta);


                Datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Datos.CerrarConexion();
            }
        }

        public void EliminarVenta(int VentaID)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("update Ventas set estado=1 where VentaID =@VentaID");
                datos.setearParametros("@VentaID", VentaID);
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