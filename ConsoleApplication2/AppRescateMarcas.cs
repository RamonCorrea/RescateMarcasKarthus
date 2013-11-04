using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ConsoleApplication2
{
    class AppRescateMarcas
    {
        public static Timer RescataMarcas = new Timer();
        private static int contador = 0;

        static void Main(string[] args)
        {

            RescataMarcas.Interval = 300000;
            RescataMarcas.Elapsed += new ElapsedEventHandler(TimerRescateMarca);
            RescataMarcas.Start();
            
            Console.WriteLine("------------ Sistema de Rescate de Marcas Off Line Karthus ------------");
            Console.WriteLine();
            Console.WriteLine("Automaticamente el sistema  comenzara la recoleccion de Marcas cada 2 minutos.");
            Console.ReadLine();
        }

        private static void TimerRescateMarca(object sender, ElapsedEventArgs e)
        {
            RescataMarcas.Stop();
            FTP myftp = new FTP("ftp://192.168.100.212", "adm", "qwerty");
            myftp.DescargaArchivo("MarcaFueraLinea.txt", "MarcaFueraLinea.txt");
            IngresaBaseDato ingreBBDD = new IngresaBaseDato("MarcaFueraLinea.txt");
            ingreBBDD.IngresoMarca();
            RescataMarcas.Start();
            contador += 1;       
        }
    }
}
