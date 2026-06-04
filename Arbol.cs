using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MyWindowApp
{
    public partial class Arbol : UserControl
    {
        public void setDisplay(String text)
        {
            display.Text = text;
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

            Controls.Add(display);



            
        }
        
    }
}