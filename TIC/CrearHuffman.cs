using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_namespace
{
    class CrearHuffman
    {
        public List<String> crearHuffman(string linea)
        {
            var huffman = new Huffman<char>(linea);
            List<String> lista = new List<string>();
            String aux;
            //Codificamos
            List<int> codificar = huffman.listaCodificada(linea);
            //Decodificamos
            //List<char> decodificar = huffman.listaDecodificada(codificar);
            //var outString = new string(decodificar.ToArray());
            //Console.WriteLine(outString == linea ? "Funciono" : "Fallo");
            //Console.WriteLine(outString);
            
            var chars = new HashSet<char>(linea);
            
            foreach (char c in chars)
            {
                codificar = huffman.Codificador(c);
                //Console.Write("{0}:  ", c);
                aux = c.ToString();
                foreach (int bit in codificar)
                {
                    aux = aux + bit.ToString();
                    //Console.Write("{0}", bit);
                }
                //Console.WriteLine();
                lista.Add(aux);
            }
            //Console.ReadKey();
            return lista;
        }
    }
}
