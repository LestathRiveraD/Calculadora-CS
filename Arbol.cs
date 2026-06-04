using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MyWindowApp
{
    public partial class Arbol : UserControl
    {
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
            Parser parser = new Parser();

            TableLayoutPanel panel = new TableLayoutPanel();
            panel.RowCount = 5;
            panel.ColumnCount = 4;
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.Black;

            for (int i = 0; i < 4; i++)
            {
                panel.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 25f));
            }

            for (int i = 0; i < 5; i++)
            {
                panel.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 20f));
            }

            string[,] buttons =
            {
                { "AC", "(", ")", "/" },
                { "7", "8", "9", "*" },
                { "4", "5", "6", "-" },
                { "1", "2", "3", "+" },
                { "0", ".", "^", "=" }
            };

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    string text = buttons[row, col];

                    if (text == "")
                        continue;

                    Button btn = new Button();

                    btn.Text = text;
                    btn.Dock = DockStyle.Fill;
                    btn.Font = new Font("Segoe UI", 18);

                    if ("/*-+=^".Contains(text))
                    {
                        btn.BackColor = Color.Orange;
                        btn.ForeColor = Color.White;
                    }
                    else if (text == "AC")
                    {
                        btn.BackColor = Color.Gray;
                        btn.ForeColor = Color.Black;
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(50, 50, 50);
                        btn.ForeColor = Color.White;
                    }

                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    if (btn.Text == "AC")
                    {
                        btn.Click += (sender, e) =>
                        {
                            display.Text = String.Empty; // Clear the display
                        };
                    }
                    else if (btn.Text == "=")
                    {
                        btn.Click += (sender, e) =>
                        {
                            List<string> tokens = SepararTokens(display.Text);
                            
                            string resultado = "";
                            foreach(string s in tokens)
                                resultado += "[" + s + "], ";
                            
                            //MessageBox.Show("Tokens encontrados: " + resultado); // Placeholder for calculation logic
                            parser.SetParser(tokens);
                            try
                            {
                                double res = parser.Parse();
                                display.Text = res.ToString();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        };

                    }
                    else
                    {
                        btn.Click += (sender, e) =>
                        {
                        // Button click logic
                        display.Text += text; 
                        };
                    };
                    panel.Controls.Add(btn, col, row);
                }
            }

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



            
        }
        
    }
}