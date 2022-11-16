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
        public void gramatica(){
            cabecera();
            listaProducciones();
        }
        private void cabecera(){
            match("Gramatica");
            match(":");
            match(Tipos.SNT);
            match(Tipos.FinProduccion);
        }

        private void listaProducciones(){
            match(Tipos.SNT);
            match(Tipos.Produce);
            match(Tipos.FinProduccion);
            if(!FinArchivo()){
                listaProducciones();
            }
        }
    }


}