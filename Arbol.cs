using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MyWindowApp
{
    public partial class Arbol : UserControl
    {
        private List<string> tokens;
        private TreeView treeView = new TreeView();
        public void setDisplay(String text)
        {
            display.Text = text;
            tokens =  SepararTokens(text);


            treeView.Nodes.Clear();
            ParserArbol parser = new ParserArbol();
            parser.SetParser(tokens);
            List<string> niveles = parser.Parse();
            TreeNode raiz = treeView.Nodes.Add("Raiz");
            AgregarNiveles(raiz, niveles, 0);
            treeView.ExpandAll();
        }

            private void AgregarNiveles(TreeNode root, List<string> niveles, int indice)
    {
        if (indice >= niveles.Count)
            return;

        string nivel = niveles[indice];

        if (nivel != "Expresion" &&
            nivel != "Termino" &&
            nivel != "Factor" &&
            nivel != "Potencia")
        {
            TreeNode nuevoNodo = root.Nodes.Add(nivel);

            // Continúa agregando los siguientes elementos debajo del nuevo nodo
            AgregarNiveles(nuevoNodo, niveles, indice + 1);
        }
        else
        {
            TreeNode nuevoNodo = root.Nodes.Add(nivel);

            // Continúa agregando los siguientes elementos debajo de este nodo
            AgregarNiveles(nuevoNodo, niveles, indice + 1);
        }
    }
        private List<String> SepararTokens(string expr)
        {
            var partirEnTokens = Regex.Matches(expr, @"(?:\d+\.\d+|\d+|\.\d+)|[+\-/*^()]");
            var ListaDeToken = new List<string>();
            foreach (Match m in partirEnTokens)
                ListaDeToken.Add(m.Value);
            ListaDeToken.Add("FIN");
            return ListaDeToken;
        }
        TextBox display;

        public Arbol()
        {
            Dock = DockStyle.Fill;
            Parser parser = new Parser();

            TableLayoutPanel panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.Black;


            Controls.Add(panel);
            
            display = new TextBox();
            display.Text = "";
            display.PlaceholderText = "0";
            display.Font = new Font("Segoe UI", 28);
            display.ForeColor = Color.White;
            display.BackColor = Color.Black;
            display.BorderStyle = BorderStyle.None;
            display.TextAlign = HorizontalAlignment.Right;
            display.Dock = DockStyle.Top;
            display.Height = 80;
            display.ReadOnly = true;



            Controls.Add(display);
            treeView.Dock = DockStyle.Fill;
            panel.Controls.Add(treeView);



            
        }
        
    }
}