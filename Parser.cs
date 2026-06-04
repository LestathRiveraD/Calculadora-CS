namespace MyWindowApp;
using System.Text.RegularExpressions;
partial class Parser
{
    private List<string> ListaDeToken;
    private int pos;
    private string TokenActual => ListaDeToken[pos];
    public void SetParser(List<string> tokens)
    {
        ListaDeToken = tokens;
        pos = 0;
    }
    public double Parse()
    {
        double resultado = Expresion();
        if (TokenActual != "FIN")
            throw new Exception("Token inesperado después de la expresión.");
        return resultado;
    }
    private double Expresion()
    {
        double resultado = Termino();
        while (TokenActual == "+")
        {
            string operador = TokenActual;
            SiguienteToken(operador);
            if (operador == "+")
            resultado += Termino();
        }
       while (TokenActual == "-")
        {
            string operador = TokenActual;
            SiguienteToken(operador);
            if (operador == "-")
            resultado -= Termino();
        }
        return resultado;
    }
    
    private void SiguienteToken(string token)
    {
        if (TokenActual == token)
            pos++;
        else
        MessageBox.Show("Se esperaba " + token + ", pero se encontró "+TokenActual);
    }

    private double Termino()
    {
        double resultado = Potencia();
        while (TokenActual == "*")
        {
            string operador = TokenActual;
            SiguienteToken(operador);
            if (operador == "*")
            resultado *= Potencia();
        }
        while (TokenActual == "/")
        {
            string operador = TokenActual;
            SiguienteToken(operador);
            if (operador == "/")
            resultado /= Potencia();
        }
        return resultado;
    }

    private double Potencia()
    {
        double resultado = Factor();
        while (TokenActual == "^")
        {
            string operador = TokenActual;
            SiguienteToken(operador);
            if (operador == "^")
            resultado = Math.Pow(resultado, Factor());
        }
        return resultado;
    }

    private double Factor()
    {
        if (TokenActual == "(")
        {
            SiguienteToken("(");
            double resultado = Expresion();
            SiguienteToken(")");
            return resultado;
        }
        else if (Regex.IsMatch(TokenActual, @"^\d*\.?\d*$"))
        {
            double value = double.Parse(TokenActual);
            SiguienteToken(TokenActual);
            return value;
        }
        
        else
        {
            throw new Exception($"Token inesperado: '{TokenActual}'");
        }
    }
}
