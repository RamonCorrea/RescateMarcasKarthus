using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApplication2
{
    class IngresaBaseDato
    {
        private string nombreArchivo;
        private int MarcaProcesadas = 0;
        ComponeCadena cadenaBBDD;
        ManejoArchivo archivo = new ManejoArchivo();
        string ServiBBDD; 
        string BD;
        string USER;
        string PASSW;

        public IngresaBaseDato(string archivo)
        {
            nombreArchivo = archivo;
        }
        
        /* FUNCION QUE SE ENCARGA DE PROCESAR E INGRESAR LAS MARCAS FUERA DE LINEA
         * RESCATADAS A LA BASE DE DATOS */ 
        public void IngresoMarca()
        {
            StreamReader fichero = File.OpenText(nombreArchivo);
            string linea;
            string ArchivoProcesado = "Procesado"+nombreArchivo;
            int LargoT = Convert.ToInt32(archivo.Datos[0].ToString());
            
            try
            {
                while (true)
                {
                    linea = fichero.ReadLine();
                    if (linea == "" || linea == null)
                    {
                        Console.WriteLine("Se procesaron {0} Marcas.", MarcaProcesadas);
                        File.Move(nombreArchivo, ArchivoProcesado);
                        fichero.Close();
                        break;    
                    }
                    else
                    {
                        cadenaBBDD = new ComponeCadena(linea,LargoT);
                        string Resul = ConexioSQL(cadenaBBDD.DevuelveCodTarjeta(), cadenaBBDD.DevuelveFecha(), cadenaBBDD.DevuelveHora(), cadenaBBDD.DevuelveIP(), cadenaBBDD.DevuelveEvento(), cadenaBBDD.DevuelveFechaHora());
                        Console.WriteLine(Resul);
                        MarcaProcesadas += 1;
                    }
                }
            }
            catch (IOException)
            {
                fichero.Close();
            }

        }

        /* FUNCION ENCARGADA DE LA CONEXION A LA BASE DE DATOS Y EL INGRESO DE LOS DATOS DE LA MARCA AL SP PROC_CONTROLES_GENERALES */
        public string ConexioSQL(string nroTarjeta, string FechaMarca, string HoraMarca, string codLector, string evento, string FechaHora)
        {
            SqlConnection cone;
            ServiBBDD = archivo.Datos[4].ToString();
            BD = archivo.Datos[5].ToString();
            USER = archivo.Datos[6].ToString();
            PASSW = archivo.Datos[7].ToString();

            try
            {
                string Resultado;
                string cadena = (@"data source =" + ServiBBDD + "; initial catalog =" + BD + "; user id =" + USER + "; password =" + PASSW + "");
                cone = new SqlConnection(cadena);
                SqlCommand ProcedureStore = new SqlCommand();


                cone.Open();

                ProcedureStore.Connection = cone;
                ProcedureStore.CommandType = CommandType.StoredProcedure;
                ProcedureStore.CommandText = "Proc_JAX_Controles_Generales";
                ProcedureStore.CommandTimeout = 10;

                ProcedureStore.Parameters.Add(new SqlParameter("@Nro_Tarjeta", SqlDbType.Char));
                ProcedureStore.Parameters.Add(new SqlParameter("@FechaMarca1", SqlDbType.Char));
                ProcedureStore.Parameters.Add(new SqlParameter("@HoraMarca", SqlDbType.Char));
                ProcedureStore.Parameters.Add(new SqlParameter("@Cod_Lector", SqlDbType.Char));
                ProcedureStore.Parameters.Add(new SqlParameter("@Evento", SqlDbType.Int));
                ProcedureStore.Parameters.Add(new SqlParameter("@Fecha_con_Hora1", SqlDbType.Char));
                ProcedureStore.Parameters.Add(new SqlParameter("@Cod_Error", SqlDbType.Char));
                ProcedureStore.Parameters.Add(new SqlParameter("@Cod_InternoTrab", SqlDbType.Char));
                ProcedureStore.Parameters.Add(new SqlParameter("@Cod_ServCasino", SqlDbType.Char));
                ProcedureStore.Parameters.Add(new SqlParameter("@Cod_Empresa", SqlDbType.Char));


                ProcedureStore.Parameters["@Nro_Tarjeta"].Value = nroTarjeta;
                ProcedureStore.Parameters["@FechaMarca1"].Value = FechaMarca;
                ProcedureStore.Parameters["@HoraMarca"].Value = HoraMarca;
                ProcedureStore.Parameters["@Cod_Lector"].Value = codLector;
                ProcedureStore.Parameters["@Evento"].Value = evento;
                ProcedureStore.Parameters["@Fecha_con_Hora1"].Value = FechaHora;

                ProcedureStore.Parameters["@Cod_Error"].Direction = ParameterDirection.Output;
                ProcedureStore.Parameters["@Cod_Error"].Size = 100;
                ProcedureStore.Parameters["@Cod_InternoTrab"].Direction = ParameterDirection.Output;
                ProcedureStore.Parameters["@Cod_InternoTrab"].Size = 13;
                ProcedureStore.Parameters["@Cod_ServCasino"].Direction = ParameterDirection.Output;
                ProcedureStore.Parameters["@Cod_ServCasino"].Size = 3;
                ProcedureStore.Parameters["@Cod_Empresa"].Direction = ParameterDirection.Output;
                ProcedureStore.Parameters["@Cod_Empresa"].Size = 4;

                ProcedureStore.ExecuteScalar();

                Resultado = Convert.ToString(ProcedureStore.Parameters["@Cod_Error"].Value);
                cone.Close();
                return Resultado;
            }
            catch (Exception)
            {
                Console.WriteLine("Hay un problema en la conexion hacia el servidor");
                return "Servidor Fuera de Linea";
            }
        }
    }
}
