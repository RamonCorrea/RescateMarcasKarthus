using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace ConsoleApplication2
{
    class ManejoArchivo
    {
        private string Ruta = null;
        public ArrayList Datos = new ArrayList();

        /* CONSTRUCTOR DE LA CLASE */
        public ManejoArchivo()
        {
            Ruta = @"Conf\ConfiguracionKARTHUS.txt";
            LecturaParametros();
        }

        /* FUNCION ENCARGA DE REALIZAR LA LECTURA DE PARAMETROS */
        public void LecturaParametros()
        {
            try
            {
                StreamReader fichero = File.OpenText(Ruta);
                string linea;

                while (true)
                {
                    linea = fichero.ReadLine();
                    if (linea == "" || linea == null)
                    {
                        fichero.Close();
                        break;
                    }
                    else
                    {
                        string[] Dato = linea.Split('=');
                        Datos.Add(Dato[1]);
                    }
                }
                fichero.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Problemas en la lectura del archivo");
            }
        }
    }
}
