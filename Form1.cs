using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SopaDeLetras
{
    public partial class Form1 : Form
    {
        private char[,] sopaDeLetras;
        private string[] palabras = { "CASA", "PERRO", "GATO", "SOL", "LUNA" };
        private int tamaño = 10;
        private List<string> palabrasEncontradas = new List<string>();
        private ListBox listBox;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Sopa de Letras";
            GenerarSopaDeLetras();
            DibujarTitulo();
            DibujarSopaDeLetras();
            InicializarTablaPalabras();
        }

        private void GenerarSopaDeLetras()
        {
            sopaDeLetras = new char[tamaño, tamaño];
            RellenarConLetrasAleatorias(sopaDeLetras);
            InsertarPalabras(sopaDeLetras, palabras);
        }

        private void RellenarConLetrasAleatorias(char[,] sopa)
        {
            Random rand = new Random();
            for (int i = 0; i < sopa.GetLength(0); i++)
            {
                for (int j = 0; j < sopa.GetLength(1); j++)
                {
                    sopa[i, j] = (char)('A' + rand.Next(0, 26));
                }
            }
        }

        private void InsertarPalabras(char[,] sopa, string[] palabras)
        {
            foreach (var palabra in palabras)
            {
                int fila = new Random().Next(0, sopa.GetLength(0));
                int columna = new Random().Next(0, sopa.GetLength(1) - palabra.Length);
                for (int i = 0; i < palabra.Length; i++)
                {
                    sopa[fila, columna + i] = palabra[i];
                }
            }
        }

        private void DibujarTitulo()
        {
            Label titulo = new Label
            {
                Text = "Encuentra las Palabras",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };
            this.Controls.Add(titulo);
        }

        private void DibujarSopaDeLetras()
        {
            int offset = 40; // Espacio para el título
            this.ClientSize = new Size(tamaño * 40, tamaño * 40 + offset + 50);
            for (int i = 0; i < tamaño; i++)
            {
                for (int j = 0; j < tamaño; j++)
                {
                    TextBox tb = new TextBox
                    {
                        Multiline = true,
                        Width = 40,
                        Height = 40,
                        Text = sopaDeLetras[i, j].ToString().ToUpper(),
                        Location = new Point(j * 40, i * 40 + offset),
                        TextAlign = HorizontalAlignment.Center,
                        Font = new Font("Arial", 14),
                        ReadOnly = true
                    };
                    tb.Click += new EventHandler(TextBox_Click);
                    this.Controls.Add(tb);
                }
            }
        }

        private void TextBox_Click(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && !tb.BackColor.Equals(Color.Yellow))
            {
                tb.BackColor = Color.Yellow;
                VerificarPalabra(tb.Text);
            }
        }

        private void VerificarPalabra(string letra)
        {
            palabrasEncontradas.Add(letra);
            ActualizarTablaPalabras();
        }

        private void InicializarTablaPalabras()
        {
            listBox = new ListBox
            {
                Location = new Point(0, tamaño * 40 + 40),
                Size = new Size(this.ClientSize.Width, 50),
                Font = new Font("Arial", 14)
            };
            this.Controls.Add(listBox);
        }

        private void ActualizarTablaPalabras()
        {
            if (listBox != null)
            {
                listBox.DataSource = null;
                listBox.DataSource = palabrasEncontradas.Distinct().ToList();
            }
        }
    }
}
