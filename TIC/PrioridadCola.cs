using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_namespace
{
    public class PrioridadCola<T> where T : IComparable
    {
        //Creamos lista de objetos
        protected List<T> ListaObjetos = new List<T>();

        //Sobrecargamos contar
        public virtual int Count
        {
            get { return ListaObjetos.Count; }
        }

        //Sobrecargamos agregar
        public virtual void Add(T val)
        {
            ListaObjetos.Add(val);
            FijarEn(ListaObjetos.Count - 1, val);
            Subiendo(ListaObjetos.Count - 1);
        }

        //Creamos un metodo virtual que solo se ejecute cuando salga del rango
        public virtual T Peek()
        {
            if (ListaObjetos.Count == 0)
            {
                throw new IndexOutOfRangeException("Validando si la lista esta vacia");
            }
            return ListaObjetos[0];
        }

        //Creamos un metodo virtual para introducir datos
        public virtual T Pop()
        {
            if (ListaObjetos.Count == 0)
            {
                throw new IndexOutOfRangeException("Introduciendo datos a una lista vacia");
            }
            T valRet = ListaObjetos[0];
            FijarEn(0, ListaObjetos[ListaObjetos.Count - 1]);
            ListaObjetos.RemoveAt(ListaObjetos.Count - 1);
            Bajando(0);
            return valRet;
        }

        //Asignamos el valor
        protected virtual void FijarEn(int i, T val)
        {
            ListaObjetos[i] = val;
        }

        //Validar si Hijo de la derecha existe
        protected bool HijoDerExiste(int i)
        {
            return IndiceHijoDer(i) < ListaObjetos.Count;
        }

        //Validar si Hijo de la izquierda existe
        protected bool HijoIzqExiste(int i)
        {
            return IndiceHijoIzq(i) < ListaObjetos.Count;
        }

        //Crear Index Padre
        protected int IndicePadre(int i)
        {
            return (i - 1) / 2;
        }

        //Crear Index Hijo Izq
        protected int IndiceHijoIzq(int i)
        {
            return 2 * i + 1;
        }

        //Crear Index Hijo Der
        protected int IndiceHijoDer(int i)
        {
            return 2 * (i + 1);
        }

        //Se asigna el valor a la raiz
        protected T ArrayVal(int i)
        {
            return ListaObjetos[i];
        }

        //Se asigna el valor del padre
        protected T Padre(int i)
        {
            return ListaObjetos[IndicePadre(i)];
        }

        //Se asigna el valor a la Izquierda
        protected T Izquierda(int i)
        {
            return ListaObjetos[IndiceHijoIzq(i)];
        }

        //Se asigna el valor a la Derecha
        protected T Derecha(int i)
        {
            return ListaObjetos[IndiceHijoDer(i)];
        }

        //Crear el siguiente nivel
        protected void Cambiar(int i, int j)
        {
            T valAux = ArrayVal(i);
            FijarEn(i, ListaObjetos[j]);
            FijarEn(j, valAux);
        }

        //Camino hacia arriba
        protected void Subiendo(int i)
        {
            while (i > 0 && ArrayVal(i).CompareTo(Padre(i)) > 0)
            {
                Cambiar(i, IndicePadre(i));
                i = IndicePadre(i);
            }
        }

        //Camino hacia abajo
        protected void Bajando(int i)
        {
            while (i >= 0)
            {
                int iContinua = -1;

                if (HijoDerExiste(i) && Derecha(i).CompareTo(ArrayVal(i)) > 0)
                {
                    iContinua = Izquierda(i).CompareTo(Derecha(i)) < 0 ? IndiceHijoDer(i) : IndiceHijoIzq(i);
                }
                else if (HijoIzqExiste(i) && Izquierda(i).CompareTo(ArrayVal(i)) > 0)
                {
                    iContinua = IndiceHijoIzq(i);
                }

                if (iContinua >= 0 && iContinua < ListaObjetos.Count)
                {
                    Cambiar(i, iContinua);
                }

                i = iContinua;
            }
        }
    }
}
