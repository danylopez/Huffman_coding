using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_namespace
{
    internal class Nodo<T> : IComparable
    {
        internal Nodo(double probabilidad, T valor)
        {
            Probabilidad = probabilidad;
            HijoIzq = HijoDer = Padre = null;
            Valor = valor;
            IsLeaf = true;
        }

        internal Nodo(Nodo<T> leftSon, Nodo<T> rightSon)
        {
            HijoIzq = leftSon;
            HijoDer = rightSon;
            Probabilidad = leftSon.Probabilidad + rightSon.Probabilidad;
            leftSon.IsZero = true;
            rightSon.IsZero = false;
            leftSon.Padre = rightSon.Padre = this;
            IsLeaf = false;
        }

        internal Nodo<T> HijoIzq { get; set; }
        internal Nodo<T> HijoDer { get; set; }
        internal Nodo<T> Padre { get; set; }
        internal T Valor { get; set; }
        //La función IsLeaf devuelve true si el miembro especificado es un miembro hoja.
        internal bool IsLeaf { get; set; }
        //La función devuelve el valor de la rama.
        internal bool IsZero { get; set; }

        //Da el valor del Bit
        internal int Bit
        {
            get { return IsZero ? 0 : 1; }
        }

        //Retorn el padre del arbol
        internal bool IsRoot
        {
            get { return Padre == null; }
        }

        //La probabilidad
        internal double Probabilidad { get; set; }

        //Comparacion de Objetos
        public int CompareTo(object obj)
        {
            return -Probabilidad.CompareTo(((Nodo<T>)obj).Probabilidad);
        }
    }
}
