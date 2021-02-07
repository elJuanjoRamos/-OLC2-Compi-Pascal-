using CompiPascal.analizer;
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
    public partial class Form1 : Form
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
            var a = textAnalizar.Text;
            Syntactic syntactic = new Syntactic();
            syntactic.analizer(a);
        }
    }
}
