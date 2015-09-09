using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_namespace
{
    public class Huffman<T> where T : IComparable
    {
        private readonly Dictionary<T, Nodo<T>> _DiccionarioHojas = new Dictionary<T, Nodo<T>>();
        private readonly Nodo<T> _raiz;

        //Creacion de codigo
        public Huffman(IEnumerable<T> valores)
        {
            var contador = new Dictionary<T, int>();
            var PrioridadCola = new PrioridadCola<Nodo<T>>();
            int valorContador = 0;

            foreach (T valor in valores)
            {
                if (!contador.ContainsKey(valor))
                {
                    contador[valor] = 0;
                }
                contador[valor]++;
                valorContador++;
            }

            foreach (T valor in contador.Keys)
            {
                var nodo = new Nodo<T>((double)contador[valor] / valorContador, valor);
                PrioridadCola.Add(nodo);
                _DiccionarioHojas[valor] = nodo;
            }

            while (PrioridadCola.Count > 1)
            {
                Nodo<T> hijoIzq = PrioridadCola.Pop();
                Nodo<T> hijoDer = PrioridadCola.Pop();
                var padre = new Nodo<T>(hijoIzq, hijoDer);
                PrioridadCola.Add(padre);
            }

            _raiz = PrioridadCola.Pop();
            _raiz.IsZero = false;
        }

        public List<int> Codificador(T valor)
        {
            var returnValue = new List<int>();
            metodoCodificador(valor, returnValue);
            return returnValue;
        }

        public void metodoCodificador(T valor, List<int> codificando)
        {
            if (!_DiccionarioHojas.ContainsKey(valor))
            {
                throw new ArgumentException("Valor invalido");
            }
            Nodo<T> nodoCur = _DiccionarioHojas[valor];
            var codificandovolteado = new List<int>();
            while (!nodoCur.IsRoot)
            {
                codificandovolteado.Add(nodoCur.Bit);
                nodoCur = nodoCur.Padre;
            }

            codificandovolteado.Reverse();
            codificando.AddRange(codificandovolteado);
        }

        public List<int> listaCodificada(IEnumerable<T> valores)
        {
            var returnValue = new List<int>();

            foreach (T valor in valores)
            {
                metodoCodificador(valor, returnValue);
            }
            return returnValue;
        }

        public T Decodificar(List<int> bitArreglo, ref int posicion)
        {
            Nodo<T> nodoCur = _raiz;
            while (!nodoCur.IsLeaf)
            {
                if (posicion > bitArreglo.Count)
                {
                    throw new ArgumentException("Valor invalido");
                }
                nodoCur = bitArreglo[posicion++] == 0 ? nodoCur.HijoIzq : nodoCur.HijoDer;
            }
            return nodoCur.Valor;
        }

        public List<T> listaDecodificada(List<int> bitArreglo)
        {
            int posicion = 0;
            var returnValue = new List<T>();

            while (posicion != bitArreglo.Count)
            {
                returnValue.Add(Decodificar(bitArreglo, ref posicion));
            }
            return returnValue;
        }
    }
}
