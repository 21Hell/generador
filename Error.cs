using System;
using System.IO;

namespace generador
{
    public class Error : Exception
    {
        public Error(string mensaje, StreamWriter log)
        {
            Console.WriteLine(mensaje);
        }
    }
}