using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace ConsoleApplication2
{
    class FTP
    {
        private string Direccion;
        private string Usuario;
        private string Password;
        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;
        private Stream ftpStream = null;

        public FTP(string Direccion, string usuario, string pass)
        {
            this.Direccion = Direccion;
            this.Usuario = usuario;
            this.Password = pass;
        }

        public void DescargaArchivo(string ArchivoFTP, string ArchivoLocal)
        {
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(Direccion + "/" + ArchivoFTP);
                ftpRequest.Credentials = new NetworkCredential(Usuario, Password);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;

                Console.WriteLine("Iniciando proceso de Descarga de Archivo.");
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpStream = ftpResponse.GetResponseStream();
                FileStream ArchivoDescargado = new FileStream(ArchivoLocal, FileMode.Create);
                byte[] byteBuffer = new byte[2048];
                int bytesRead = ftpStream.Read(byteBuffer, 0, 2048);
              
                while (bytesRead > 0)
                {
                    ArchivoDescargado.Write(byteBuffer, 0, bytesRead);
                    bytesRead = ftpStream.Read(byteBuffer, 0, 2048);
                }
                
                Console.WriteLine("Proceso de Descarga Finalizado.");
                EliminarArchivo(ArchivoFTP);
                ArchivoDescargado.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception)
            {
                Console.WriteLine("No se encuentra el archivo para descargar");
            }

            return;
        }

        public void EliminarArchivo(string ArchivoEliminar)
        {
            try
            {
                Console.WriteLine("Iniciando proceso de eliminacion de archivo iniciado.");
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(Direccion + "/" + ArchivoEliminar);
                ftpRequest.Credentials = new NetworkCredential(Usuario, Password);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                Console.WriteLine("Archivo eliminado correctamente.");
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception)
            {
                Console.WriteLine("No se encuentra el archivo a eliminar");
            }
        }
    }
}

