namespace MyWindowApp;
using System.Text.RegularExpressions;
partial class ParserArbol
{
    private List<string> ListaDeToken;
    private int pos;
    private string TokenActual => ListaDeToken[pos];
    private List<string> listaNiveles;
    public void SetParser(List<string> tokens)
    {
        ListaDeToken = tokens;
        pos = 0;
    }
    public List<string> Parse()
    {
        listaNiveles = new List<string>();
        Expresion();
        if (TokenActual != "FIN")
            throw new Exception("Token inesperado después de la expresión.");
        return listaNiveles;
    }
    private void Expresion()
    {
        Termino();
        while (TokenActual == "+" || TokenActual == "-")
        {
            string operador = TokenActual;
            SiguienteToken(operador);

            if (operador == "+")
                Termino();
            else
                Termino();
        }
        listaNiveles.Add("Expresion");
    }
    
    private void SiguienteToken(string token)
    {
        if (TokenActual == token)
            pos++;
        else
        MessageBox.Show("Se esperaba " + token + ", pero se encontró "+TokenActual);
    }

    private void Termino()
    {
        Potencia();
        while (TokenActual == "*" || TokenActual == "/")
        {
            string operador = TokenActual;
            SiguienteToken(operador);

            if (operador == "*")
                Potencia();
            else
                Potencia();
        }
        listaNiveles.Add("Termino");
    }

    private void Potencia()
    {
        Factor();
        while (TokenActual == "^")
        {
            string operador = TokenActual;
            SiguienteToken(operador);
            if (operador == "^")
            {
                listaNiveles.Add("Potencia");
            }
        }
    }

    private void Factor()
    {
        if (TokenActual == "(")
        {
            SiguienteToken("(");
            Expresion();
            SiguienteToken(")");
        }
        if (TokenActual == "-")
        {
            SiguienteToken("-");
            Factor(); // Call Factor() recursively
        }
        else if (Regex.IsMatch(TokenActual, @"^\d*\.?\d*$"))
        {
            double value = double.Parse(TokenActual);
            SiguienteToken(TokenActual);
            listaNiveles.Add("Factor");
            listaNiveles.Add(value.ToString());
        }
        
        else
        {
            throw new Exception($"Token inesperado: '{TokenActual}'");
        }
    }
}
