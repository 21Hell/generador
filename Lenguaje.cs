/*
    Melendez Chavez Ivan
*/
using System;
using System.Collections.Generic;

//Requerimiento 1: [x] construir un metodo que permita indentar el codigo generado con las llaves de apertura y cierre 

//Requerimiento 2: Declarar un atributo primera produccion de tipo string y actualizarlo con la primera produccion de la gramatica

//Requerimiento 3: [x] Primera publica y el resto privadas

//Requerimiento 4:  El constructor Lexito parametrizado debe valida que
//                  Que la extencion del archivo a compilar sea .gen 
//                  y si no lo es debe lanzar una excepcion

//Requerimiento 5:  Resolver la ambiguedad de ST y SNT 

//Requerimiento 6:  Agregar el parecis derecho y el parecis izquierdo y derecho escapado en la matriz de transiciones

//Requerimiento 6:  Match de un Or y Cerradura Epsilon



namespace generador
{
    public class Lenguaje : Sintaxis
    {
        List<string> listaSNT = new List<string>();

        int nivel = 0;

        int numerodeproduccion = 0;

        string primeraProduccion = "";
        public Lenguaje(string nombre) : base(nombre)
        {
            
        }
        public Lenguaje()
        {
        }
        public void Dispose()
        {
            cerrar();
        }
        private bool esSNT(string token)
        {
            return listaSNT.Contains(token);
        }
        private void agregarSNT(string token)
        {
            
        //Requerimiento 6:  
            listaSNT.Add(token);
        }

        //metodo imprimir que recibe un string y verifica si es llave de apertura o cierre
        //si es llave de apertura aumenta el nivel
        //si es llave de cierre disminuye el nivel
        //si no es llave de apertura o cierre imprime el string
        private void imprimir(string token, string documento)
        {
            if (token == "{")
            {
                nivel++;
                for (int i = 0; i < nivel-1; i++)
                {
                    if (documento == "lenguaje")
                    {
                        lenguaje.Write("\t");
                    }
                    else if (documento == "programa")
                    {
                        programa.Write("\t");
                    }
                }
                if (documento == "lenguaje")
                {
                    lenguaje.WriteLine(token);
                }
                else if (documento == "programa")
                {
                    programa.WriteLine(token);
                }
            }
            else if (token == "}")
            {
                nivel--;
                for (int i = 0; i < nivel; i++)
                {
                    if (documento == "lenguaje")
                    {
                        lenguaje.Write("\t");
                    }
                    else if (documento == "programa")
                    {
                        programa.Write("\t");
                    }
                }
                if (documento == "lenguaje")
                {
                    lenguaje.WriteLine(token);
                }
                else if (documento == "programa")
                {
                    programa.WriteLine(token);
                }

            }
            else
            {
                for (int i = 0; i < nivel; i++)
                {
                    if (documento == "lenguaje")
                    {
                        lenguaje.Write("\t");
                    }
                    else if (documento == "programa")
                    {
                        programa.Write("\t");
                    }
                }
                if (documento == "lenguaje")
                {
                    lenguaje.WriteLine(token);
                }
                else if (documento == "programa")
                {
                    programa.WriteLine(token);
                }
            }
        }

        private void Programa(string produccionPrincipal)
        {
            agregarSNT("ListaIdentificadores");
            imprimir("using System;", "programa");
            imprimir("using System.IO;", "programa");
            imprimir("using System.Collections.Generic;", "programa");
            programa.WriteLine();
            imprimir("namespace Generico", "programa");
            imprimir("{", "programa");
            imprimir("public class Program", "programa");
            imprimir("{", "programa");
            imprimir("public static void Main(string[] args)", "programa");
            imprimir("{", "programa");
            imprimir("try", "programa");
            imprimir("{", "programa");
            imprimir("Lenguaje lenguaje = new Lenguaje();", "programa");
            imprimir("lenguaje." +primeraProduccion+ "();", "programa");
            imprimir("}", "programa");
            imprimir("catch (Exception e)", "programa");
            imprimir("{", "programa");
            imprimir("Console.WriteLine(e.Message);", "programa");
            imprimir("}", "programa");
            imprimir("}", "programa");
            imprimir("}", "programa");
            imprimir("}", "programa");
        }
        public void gramatica()
        {
            cabecera();
            cabeceraLenguaje();
            listaProducciones();
            Programa(primeraProduccion);
            lenguaje.WriteLine("\t}");
            lenguaje.WriteLine("}");
        }
        private void cabecera()
        {
            match("Gramatica");
            match(":");
            match(Tipos.ST);
            match(Tipos.FinProduccion);
        }

        private void cabeceraLenguaje()
        {
            imprimir("using System;", "lenguaje");
            imprimir("using System.IO;", "lenguaje");
            imprimir("using System.Collections.Generic;", "lenguaje");
            lenguaje.WriteLine();
            imprimir("namespace generador", "lenguaje");
            imprimir("{", "lenguaje");
            imprimir("public class Lenguaje : Sintaxis", "lenguaje");
            imprimir("{", "lenguaje");
            imprimir("string nombreProyecto;", "lenguaje");
            imprimir("public Lenguaje(string nombre) : base(nombre)", "lenguaje");
            imprimir("{", "lenguaje");
            imprimir("}", "lenguaje");
            imprimir("public Lenguaje()", "lenguaje");
            imprimir("{", "lenguaje");
            imprimir("}", "lenguaje");
            imprimir("public void Dispose()", "lenguaje");
            imprimir("{", "lenguaje");
            imprimir("cerrar();", "lenguaje");
            imprimir("}", "lenguaje");
        }
        private void listaProducciones()
        {
            string tipo = "";
            if(numerodeproduccion == 0){
                tipo = "public";
                primeraProduccion = getContenido();
            }else{
                tipo = "private";
            }
            imprimir(tipo+" void "+ getContenido() + "()", "lenguaje");
            imprimir("{", "lenguaje");
            match(Tipos.ST);
            match(Tipos.Produce);
            simbolos();
            match(Tipos.FinProduccion);
            agregarSNT(getContenido());
            imprimir("}", "lenguaje");
            if (!FinArchivo())
            {
                numerodeproduccion++;
                listaProducciones();
            }

        }
        

        private void simbolos()
        {
            if (esTipo(getContenido()))
            {
                imprimir("match(Tipos." + getContenido() + ");", "lenguaje");
                match(Tipos.ST);
            }
            else if (esSNT(getContenido()))
            {
                imprimir(getContenido() + "();", "lenguaje");
                match(Tipos.ST);
            }else if (getClasificacion() == Tipos.ST)
            {
                imprimir("match(\"" + getContenido() + "\");", "lenguaje");
                match(Tipos.ST);
            }
            if (getClasificacion() != Tipos.FinProduccion)
            {
                simbolos();
            }
        }
        private bool esTipo(string clasificacion)
        {
            
            switch (clasificacion)
            {
                case "Identificador":
                case "Numero":
                case "Caracter":
                case "Asignacion":
                case "Inicializacion":
                case "OperadorLogico":
                case "OperadorRelacional":
                case "OperadorTernario":
                case "OperadorTermino":
                case "OperadorFactor":
                case "IncrementoTermino":
                case "IncrementoFactor":
                case "FinSentencia":
                case "Cadena":
                case "TipoDato":
                case "Zona":
                case "Condicion":
                case "Ciclo":
                    return true;
            }
            return false;
        }




    }
}