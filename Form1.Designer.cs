namespace CompiPascal
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.textAnalizar = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.linenumber = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.consola = new System.Windows.Forms.RichTextBox();
            this.ejecutar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.consolaTraduccion = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.analizar = new System.Windows.Forms.Button();
            this.traduccion = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.errores = new System.Windows.Forms.TabControl();
            this.lexicos = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.errores.SuspendLayout();
            this.lexicos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(122, 28);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(121, 24);
            this.toolStripMenuItem1.Text = "Nuevo";
            // 
            // textAnalizar
            // 
            this.textAnalizar.BackColor = System.Drawing.SystemColors.Menu;
            this.textAnalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textAnalizar.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textAnalizar.Location = new System.Drawing.Point(39, 79);
            this.textAnalizar.Name = "textAnalizar";
            this.textAnalizar.Size = new System.Drawing.Size(644, 689);
            this.textAnalizar.TabIndex = 2;
            this.textAnalizar.Text = "";
            this.textAnalizar.SelectionChanged += new System.EventHandler(this.textAnalizar_SelectionChanged);
            this.textAnalizar.VScroll += new System.EventHandler(this.textAnalizar_VScroll);
            this.textAnalizar.FontChanged += new System.EventHandler(this.textAnalizar_FontChanged);
            this.textAnalizar.TextChanged += new System.EventHandler(this.textAnalizar_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 74);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1410, 884);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.linenumber);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.consola);
            this.tabPage1.Controls.Add(this.ejecutar);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textAnalizar);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1402, 851);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ejecución";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(728, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 27);
            this.label8.TabIndex = 15;
            this.label8.Text = "Salida";
            // 
            // linenumber
            // 
            this.linenumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.linenumber.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.linenumber.Location = new System.Drawing.Point(6, 79);
            this.linenumber.Name = "linenumber";
            this.linenumber.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.linenumber.Size = new System.Drawing.Size(27, 689);
            this.linenumber.TabIndex = 14;
            this.linenumber.Text = "";
            this.linenumber.MouseDown += new System.Windows.Forms.MouseEventHandler(this.linenumber_MouseDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(728, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Consola de ejecución";
            // 
            // consola
            // 
            this.consola.BackColor = System.Drawing.SystemColors.WindowText;
            this.consola.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.consola.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.consola.Location = new System.Drawing.Point(728, 79);
            this.consola.Name = "consola";
            this.consola.Size = new System.Drawing.Size(644, 689);
            this.consola.TabIndex = 12;
            this.consola.Text = "";
            // 
            // ejecutar
            // 
            this.ejecutar.BackColor = System.Drawing.Color.White;
            this.ejecutar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ejecutar.Image = ((System.Drawing.Image)(resources.GetObject("ejecutar.Image")));
            this.ejecutar.Location = new System.Drawing.Point(39, 774);
            this.ejecutar.Name = "ejecutar";
            this.ejecutar.Size = new System.Drawing.Size(644, 35);
            this.ejecutar.TabIndex = 11;
            this.ejecutar.UseVisualStyleBackColor = false;
            this.ejecutar.Click += new System.EventHandler(this.ejecutar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(39, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Editor de texto";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(39, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 27);
            this.label1.TabIndex = 6;
            this.label1.Text = "Entrada";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.consolaTraduccion);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.analizar);
            this.tabPage2.Controls.Add(this.traduccion);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1402, 851);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Traduccion";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // consolaTraduccion
            // 
            this.consolaTraduccion.BackColor = System.Drawing.SystemColors.MenuBar;
            this.consolaTraduccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.consolaTraduccion.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.consolaTraduccion.Location = new System.Drawing.Point(732, 71);
            this.consolaTraduccion.Name = "consolaTraduccion";
            this.consolaTraduccion.Size = new System.Drawing.Size(644, 689);
            this.consolaTraduccion.TabIndex = 18;
            this.consolaTraduccion.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(39, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Editor de texto";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(39, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 27);
            this.label4.TabIndex = 16;
            this.label4.Text = "Entrada";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(732, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Consola de traducción";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(732, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 27);
            this.label6.TabIndex = 14;
            this.label6.Text = "Salida";
            // 
            // analizar
            // 
            this.analizar.BackColor = System.Drawing.Color.White;
            this.analizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.analizar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.analizar.Image = ((System.Drawing.Image)(resources.GetObject("analizar.Image")));
            this.analizar.Location = new System.Drawing.Point(39, 768);
            this.analizar.Name = "analizar";
            this.analizar.Size = new System.Drawing.Size(644, 35);
            this.analizar.TabIndex = 13;
            this.analizar.UseVisualStyleBackColor = false;
            this.analizar.Click += new System.EventHandler(this.analizar_Click_1);
            // 
            // traduccion
            // 
            this.traduccion.BackColor = System.Drawing.SystemColors.MenuBar;
            this.traduccion.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.traduccion.Location = new System.Drawing.Point(39, 73);
            this.traduccion.Name = "traduccion";
            this.traduccion.Size = new System.Drawing.Size(644, 689);
            this.traduccion.TabIndex = 9;
            this.traduccion.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.errores);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1402, 851);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Reportes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // errores
            // 
            this.errores.Controls.Add(this.lexicos);
            this.errores.Controls.Add(this.tabPage5);
            this.errores.Controls.Add(this.tabPage4);
            this.errores.Controls.Add(this.tabPage6);
            this.errores.Controls.Add(this.tabPage7);
            this.errores.Location = new System.Drawing.Point(20, 20);
            this.errores.Name = "errores";
            this.errores.SelectedIndex = 0;
            this.errores.Size = new System.Drawing.Size(1360, 775);
            this.errores.TabIndex = 0;
            // 
            // lexicos
            // 
            this.lexicos.Controls.Add(this.label10);
            this.lexicos.Controls.Add(this.label9);
            this.lexicos.Controls.Add(this.pictureBox1);
            this.lexicos.Location = new System.Drawing.Point(4, 29);
            this.lexicos.Name = "lexicos";
            this.lexicos.Padding = new System.Windows.Forms.Padding(3);
            this.lexicos.Size = new System.Drawing.Size(1352, 742);
            this.lexicos.TabIndex = 0;
            this.lexicos.Text = "Lexicos";
            this.lexicos.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(32, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(599, 27);
            this.label10.TabIndex = 18;
            this.label10.Text = "Listado de errores lexicos encontrados en ejecucion";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(32, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(245, 40);
            this.label9.TabIndex = 17;
            this.label9.Text = "Errores Lexicos";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(32, 115);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1289, 593);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.pictureBox2);
            this.tabPage5.Controls.Add(this.label11);
            this.tabPage5.Controls.Add(this.label12);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1352, 742);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Sintacticos";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(32, 115);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1290, 597);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(32, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(643, 27);
            this.label11.TabIndex = 20;
            this.label11.Text = "Listado de errores sintacticos encontrados en ejecucion";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(32, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(300, 40);
            this.label12.TabIndex = 19;
            this.label12.Text = "Errores Sintacticos";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pictureBox3);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1352, 742);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Semanticos";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(32, 115);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1302, 592);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 23;
            this.pictureBox3.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(32, 74);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(651, 27);
            this.label13.TabIndex = 22;
            this.label13.Text = "Listado de errores semanticos encontrados en ejecucion";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(32, 34);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(313, 40);
            this.label14.TabIndex = 21;
            this.label14.Text = "Errores Semanticos";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.pictureBox4);
            this.tabPage6.Controls.Add(this.label15);
            this.tabPage6.Controls.Add(this.label16);
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1352, 742);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "AST";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(32, 113);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(1300, 595);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 25;
            this.pictureBox4.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(32, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(414, 27);
            this.label15.TabIndex = 24;
            this.label15.Text = "AST generado durante la ejecucion";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label16.Location = new System.Drawing.Point(32, 34);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(441, 40);
            this.label16.TabIndex = 23;
            this.label16.Text = "Arbol de Analisis Sintactico";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.pictureBox5);
            this.tabPage7.Controls.Add(this.label17);
            this.tabPage7.Controls.Add(this.label18);
            this.tabPage7.Location = new System.Drawing.Point(4, 29);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(1352, 742);
            this.tabPage7.TabIndex = 4;
            this.tabPage7.Text = "Tabla de simbolos";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(33, 125);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(1292, 586);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 27;
            this.pictureBox5.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label17.Location = new System.Drawing.Point(33, 74);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(628, 27);
            this.label17.TabIndex = 26;
            this.label17.Text = "Listado de simbolos encontrados durante la ejecucion";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label18.Location = new System.Drawing.Point(33, 34);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(303, 40);
            this.label18.TabIndex = 25;
            this.label18.Text = "Tabla de simbolos";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1444, 982);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.errores.ResumeLayout(false);
            this.lexicos.ResumeLayout(false);
            this.lexicos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.RichTextBox textAnalizar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ejecutar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox consola;
        private System.Windows.Forms.RichTextBox linenumber;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button analizar;
        private System.Windows.Forms.RichTextBox traduccion;
        private System.Windows.Forms.RichTextBox consolaTraduccion;
        private System.Windows.Forms.TabControl errores;
        private System.Windows.Forms.TabPage lexicos;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}

