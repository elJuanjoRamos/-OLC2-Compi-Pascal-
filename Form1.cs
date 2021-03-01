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
            linenumber.Font = textAnalizar.Font;
            textAnalizar.Select();
            AddLineNumbers();
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
            ErrorController.Instance.Clean();
            var texto_analizar = textAnalizar.Text;
            if (texto_analizar != "")
            {
            
                consola.Text = "$fpc -vw main.pas\nCompi-Pascal Compiler version 3.0.2 ["+ DateTime.Now + "] for x86_64\n";
                consola.Text = consola.Text + "Copyright (c) 2021-2100 by Juan Jose Ramos 201801262\nTarget OS: Windows for x86-64\n";
                consola.Text = consola.Text + "Compiling main.pas\n$main\n\n";


                Syntactic syntactic = new Syntactic();
                syntactic.analizer(texto_analizar, Application.StartupPath);

                consola.Text = consola.Text + ErrorController.Instance.getLexicalError();
                consola.Text = consola.Text + ErrorController.Instance.getSintactycError();
                consola.Text = consola.Text + ErrorController.Instance.getSemantycError();
                consola.Text = consola.Text + ConsolaController.Instance.getText()+ "\nFinalizado.";

               


            } else
            {
                consola.Text = "Debe escribir texto en el editor";
            }
        }


        //ESTO ES TRADUCIR
        private void analizar_Click_1(object sender, EventArgs e)
        {
            //LIMPIA LA CONSOLA
            ConsolaController.Instance.clean();
            ErrorController.Instance.Clean();
            if (traduccion.Text != "")
            {


                Syntactic_Trad syntactic_Trad = new Syntactic_Trad();

                syntactic_Trad.analizer(traduccion.Text, Application.StartupPath);

                consolaTraduccion.Text = "";
                consolaTraduccion.Text = "$fpc -vw main.pas\nCompi-Pascal Compiler version 3.0.2 [" + DateTime.Now + "] for x86_64\n";
                consolaTraduccion.Text = consola.Text + "Copyright (c) 2021-2100 by Juan Jose Ramos 201801262\nTarget OS: Windows for x86-64\n";
                consolaTraduccion.Text = consola.Text + "Compiling main.pas\n$main\n\n";

                consolaTraduccion.Text = consolaTraduccion.Text + ErrorController.Instance.getLexicalError();
                consolaTraduccion.Text = consolaTraduccion.Text + ErrorController.Instance.getSintactycError();
                consolaTraduccion.Text = consolaTraduccion.Text + ErrorController.Instance.getSemantycError();


                consolaTraduccion.Text = consolaTraduccion.Text + syntactic_Trad.get_Traduction();


                consolaTraduccion.Text = consolaTraduccion.Text + ConsolaController.Instance.getText() + "\nFinalizado.";
            }
            else
            {
                consolaTraduccion.Text = "Debe escribir en el editor";
            }
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = textAnalizar.GetCharIndexFromPosition(pt);
            int First_Line = textAnalizar.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = textAnalizar.GetCharIndexFromPosition(pt);
            int Last_Line = textAnalizar.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            linenumber.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            linenumber.Text = "";
            linenumber.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                linenumber.Text += i + 1 + "\n";
            }
        }
        private void textAnalizar_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = textAnalizar.GetPositionFromCharIndex(textAnalizar.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }
        private void textAnalizar_VScroll(object sender, EventArgs e)
        {
            linenumber.Text = "";
            AddLineNumbers();
            linenumber.Invalidate();
        }

        private void textAnalizar_TextChanged(object sender, EventArgs e)
        {
            if (textAnalizar.Text == "")
            {
                AddLineNumbers();
            }
        }
        private void textAnalizar_FontChanged(object sender, EventArgs e)
        {
            linenumber.Font = textAnalizar.Font;
            textAnalizar.Select();
            AddLineNumbers();
        }
        private void linenumber_MouseDown(object sender, MouseEventArgs e)
        {
            linenumber.Select();
            linenumber.DeselectAll();
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = textAnalizar.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)textAnalizar.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)textAnalizar.Font.Size;
            }
            else
            {
                w = 50 + (int)textAnalizar.Font.Size;
            }

            return w;
        }

        private void textAnaslizar_VScroll(object sender, EventArgs e)
        {

        }
        
    }
}
