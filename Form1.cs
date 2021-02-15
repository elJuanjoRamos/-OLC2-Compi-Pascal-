using CompiPascal.analizer;
using CompiPascal.controller;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompiPascal
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Console.WriteLine("Archivo Nuevo");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Abrir Archivo");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Guardar Archivo");
        }

        private void analizar_Click(object sender, EventArgs e)
        {
           
        }

        private void ejecutar_Click(object sender, EventArgs e)
        {
            //LIMPIA LA CONSOLA
            ConsolaController.Instance.clean();
            var texto_analizar = textAnalizar.Text;
            if (texto_analizar != "")
            {
            
                consola.Text = "Programa Encendido\nEn ejecucion ...\n";
            
                Syntactic syntactic = new Syntactic();
                syntactic.analizer(texto_analizar.ToLower());

                consola.Text = consola.Text + "\n" + ConsolaController.Instance.getText() + "\n" + "\nFinalizado.";

            } else
            {
                consola.Text = "Debe escribir texto en el editor";
            }


        }
    }
}
