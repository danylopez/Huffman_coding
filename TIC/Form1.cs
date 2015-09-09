using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIC
{
    public partial class Form1 : Form
    {
        static string linea = ""; //Valor que se lee
        static List<Valor> valores = new List<Valor>(); //Crea lista de valores

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {    
            Practica1 obj = new Practica1(); //Crea objeto de practica
            double hx, n, miu;
            int i, j, aux;
            bool x = true; //Auxiliar para impresion
            string codex;

            /*
             *
             * LECTURA ARCHIVO
             * 
             */
            //Lectura Archivo si no se a elegido ningun archivo
            aux = linea.Length;//Auxiliar si ya se leyo un archivo
            if (aux == 0)
            {
                //Mientras no se abra ningun archivo
                while (true)
                {
                    //Crea una ventana para abrir archivo
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        System.IO.StreamReader archivo = new System.IO.StreamReader(openFileDialog1.FileName);
                        //Lectura de la linea
                        linea = archivo.ReadLine();
                        //Si lee algo sale del ciclo infinito
                        if (linea.Length != 0)
                        {
                            break;
                        }

                        archivo.Close();
                    }
                }
            }

            //Llama al metodo leer
            valores = obj.llenarvector(linea);
            
            /*
             * 
             * MOSTRAR DATOS LEIDOS
             * 
             */
            //Muestra los datos en el Form
            richTextBox1.Text = "";
            for (i = 0; i < linea.Length; i++)
            {
                if (x == true)
                {
                    AnexarTexto(this.richTextBox1, Color.Blue, linea[i].ToString());
                    x = false;
                }
                else
                {
                    AnexarTexto(this.richTextBox1, Color.Red, linea[i].ToString());
                    x = true;
                }
            }

            /*
             * 
             * METODO HUFFMAN
             * 
             */
            //Se crea un objeto tipo Huffman
            Huffman_namespace.CrearHuffman objt = new Huffman_namespace.CrearHuffman();
            //Lista con caracter
            List<String> listaHuffmanTodo = new List<string>();
            //Lista sin caracter
            List<String> listaHuffman = new List<string>();
            //Se llama metodo Huffman
            listaHuffmanTodo = objt.crearHuffman(linea);
            //Se agrega a la lista de vectores
            for (i = 0; i < listaHuffmanTodo.Count; i++)
            {
                listaHuffman.Add(listaHuffmanTodo[i].Remove(0, 1));
                valores[i].codigohuffman = listaHuffman[i];
            }
            
            /*
             * 
             * MOSTRAR DATOS CODIFICADOS
             * 
             */
            x = true;
            richTextBox2.Text = "";
            for (i = 0; i < linea.Length; i++)
            {
                if (x == true)
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (String.Equals(valores[j].nombre.ToString(), linea[i].ToString(), StringComparison.Ordinal))
                        {
                            AnexarTexto(this.richTextBox2, Color.Blue, valores[j].codigohuffman);
                            break;
                        }
                    }
                    x = false;
                }
                else
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (String.Equals(valores[j].nombre.ToString(), linea[i].ToString(), StringComparison.Ordinal))
                        {
                            AnexarTexto(this.richTextBox2, Color.Red, valores[j].codigohuffman);
                            break;
                        }
                    }
                    x = true;
                }
            }
            //Guardamos el codigo
            codex = richTextBox2.Text+"x";

            /*
             * 
             * MOSTRAR TRADUCCION
             * 
             */
            x = true;
            richTextBox3.Text = "";
            while (codex != "x")
            {
                if (x == true)
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (codex.StartsWith(valores[j].codigohuffman))
                        {
                            AnexarTexto(this.richTextBox3, Color.Blue, valores[j].nombre.ToString());
                            codex = codex.Substring(valores[j].codigohuffman.ToString().Length);
                            break;
                        }
                    }
                    x = false;
                }
                else
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (codex.StartsWith(valores[j].codigohuffman))
                        {
                            AnexarTexto(this.richTextBox3, Color.Red, valores[j].nombre.ToString());
                            codex = codex.Substring(valores[j].codigohuffman.ToString().Length);
                            break;
                        }
                    }
                    x = true;
                }
            }
            
            /*
             * 
             * CALCULA LA EFICIENCIA
             * 
             */
            //Calcula la Entropia
            hx = obj.calcularEntropia(valores);
            //Calcula la Media
            n = obj.calcularMedia(valores);
            miu = hx / n;
            label5.Text = miu.ToString();
        }

        //Metodo Para dar color a RichBox
        void AnexarTexto(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;
            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear
        }

        //Submenu para elegir archivo
        private void elegirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Crea una ventana para abrir archivo
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader archivo = new System.IO.StreamReader(openFileDialog1.FileName);
                //Lectura de la linea
                linea = archivo.ReadLine();
                archivo.Close();
            }
        }

    }
}
