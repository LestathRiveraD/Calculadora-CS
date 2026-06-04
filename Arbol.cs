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
            TreeNode root = new TreeNode("Expresion Matematica");
            foreach (string nivel in niveles)
            {
                root.Nodes.Add(nivel);
            }
            treeView.Nodes.Add(root);
            treeView.ExpandAll();
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