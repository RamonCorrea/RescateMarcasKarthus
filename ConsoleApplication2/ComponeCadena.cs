using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication2
{
    class ComponeCadena
    {
        private string[] cadenaDividida;
        private string cadenaCompleta;

        public ComponeCadena(string cadena)
        {
            if (cadena == "" || cadena == null)
            {
                return;
            }
            else
            {
                cadenaCompleta = cadena;
                cadenaDividida = cadenaCompleta.Split(',');
            }
        }

        /* ESTA FUNCION SE LE DEBE ESPECIFICAR EL LARGO DE CODIGO DEL TRABAJADOR */
        public string DevuelveCodTarjeta()
        {
            string cadena = cadenaDividida[0];
            string CodTarjeta = cadena.Remove(21);
            CodTarjeta = CodTarjeta.Remove(0, 8);
            return CodTarjeta;
        }

        public string DevuelveFecha()
        {
            string cadena = cadenaDividida[0];
            string Fecha = cadena.Remove(0, 20);
            Fecha = Fecha.Remove(8);
            return Fecha;
        }

        /* METODO EL CUAL PERMITE OBTENER LA HORA DEL STRING ENVIADO DESDE EL CLIENTE */
        public string DevuelveHora()
        {
            string cadena = cadenaDividida[0];
            string Hora = cadena.Remove(0, 28);
            Hora = Hora.Remove(6);
            Hora = Hora.Insert(2, ":");
            Hora = Hora.Insert(5, ":");
            return Hora; 
        }

        /* METODO EL CUAL PERMITE OBTENER EL EVENTO ENVIADO DESDE EL CLIENTE */
        public string DevuelveEvento()
        {

            string cadena = cadenaDividida[0];
            string evento = cadena.Remove(0, 34);
            return evento;
      
        }

        /* METODOD EL CUAL PERMITE OBTENER LA FECHA Y LA HORA ENVIADA DESDE EL CLIENTE */
        public string DevuelveFechaHora()
        {
            
            string cadena = cadenaDividida[0];
            string FechaHora = cadena.Remove(0, 20);
            FechaHora = FechaHora.Remove(14);
            FechaHora = FechaHora.Insert(4, "/");
            FechaHora = FechaHora.Insert(7, "/");
            FechaHora = FechaHora.Insert(10, " ");
            FechaHora = FechaHora.Insert(13, ":");
            FechaHora = FechaHora.Insert(16, ":");
            return FechaHora;
          
        }

        public string DevuelveIP()
        {
            string[] Desarme = cadenaCompleta.Split(',');
            string cadenaIP = Desarme[1];
            return cadenaIP;
        }
    }
}
