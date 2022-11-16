/*
    Melendez Chavez Ivan
*/
using System;
using System.Collections.Generic;


namespace generador
{
    public class Lenguaje : Sintaxis
    {
        public Lenguaje(){

        }
        public Lenguaje(string nombre) : base(nombre)
        {
            //log.WriteLine("Segundo constructor");
        }

        ~Lenguaje()
        {
            //log.WriteLine("Destructor");
        }

    }


}