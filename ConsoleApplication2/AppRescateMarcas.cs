using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ConsoleApplication2
{
    class AppRescateMarcas
    {
        public static Timer RescataMarcas = new Timer();
        public static ManejoArchivo myarchivo = new ManejoArchivo();

        static void Main(string[] args)
        {
            //RescataMarcas.Interval = 300000;
            RescataMarcas.Interval = Convert.ToInt32(myarchivo.Datos[1].ToString());
            RescataMarcas.Elapsed += new ElapsedEventHandler(TimerRescateMarca);
            RescataMarcas.Start();
            
            Console.WriteLine("------------ Sistema de Rescate de Marcas Off Line Karthus ------------");
            Console.WriteLine();
            Console.WriteLine("Automaticamente el sistema  comenzara la recoleccion de Marcas cada 2 minutos.");
            while (true)
            {
                Console.ReadLine();   
            }
        }

        private static void TimerRescateMarca(object sender, ElapsedEventArgs e)
        {
            RescataMarcas.Stop();
            FTP myftp = new FTP("ftp://192.168.100.212", myarchivo.Datos[2].ToString(), myarchivo.Datos[3].ToString());
            string resul = myftp.DescargaArchivo("MarcaFueraLinea.txt", "MarcaFueraLinea.txt");
            if (resul == "Error")
            {
                RescataMarcas.Start();
            }
            else
            {
                IngresaBaseDato ingreBBDD = new IngresaBaseDato("MarcaFueraLinea.txt");
                ingreBBDD.IngresoMarca();
                RescataMarcas.Start();
            }            
        }
    }
}
