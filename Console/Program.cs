using System.Text;

// Configurações de cenario
int numeroDeCenarios = 1000;
bool deveTrocarDeAlternativa = true;

// Lista com todas alternativas
List<string> alternativas = new(){ "A", "B", "C" };

// Resultados
List<string[]> resultados = new()
{
    new string[] { "PORTA-CORRETA", "PRIMEIRA-ESCOLHA", "PORTA-ERRADA", "SEGUNDA-ESCOLHA", "RESULTADO" },
};

// Loop que gera os cenarios
for(int i = 0; i < numeroDeCenarios; i++)
{
    string[] resultado = new string[5];
    var random = new Random();
    List<string> alternativasDisponiveis = new(alternativas);
    // Gera uma alternativa correta
    string alternativaCorreta = alternativasDisponiveis[random.Next(alternativasDisponiveis.Count)];
    resultado[0] = alternativaCorreta;

    // Seleciona uma alternativa
    string alternativaEscolhida = alternativasDisponiveis[random.Next(alternativasDisponiveis.Count)];
    resultado[1] = alternativaEscolhida;

    // Remove a alternativa que não é a certa e nem a escolhida
    string alternativaRemovida = alternativasDisponiveis.First(alternativa => alternativa != alternativaEscolhida && alternativa != alternativaCorreta);
    alternativasDisponiveis.Remove(alternativaRemovida);
    resultado[2] = alternativaRemovida;

    // Troca a alternativa selecionada
    alternativaEscolhida = alternativasDisponiveis.First(alternativa => alternativa != alternativaEscolhida);
    resultado[3] = alternativaEscolhida;

    // Determina se escolheu a porta certa ou errada
    if (alternativaEscolhida == alternativaCorreta)
        resultado[4] = "GANHOU";
    else
        resultado[4] = "PERDEU";

    resultados.Add(resultado);
}

int numeroDeResultadosCorretos = resultados.Where(x => x[4] == "GANHOU").Count();
int porcentagemDeAcertos = (numeroDeResultadosCorretos * 100) / numeroDeCenarios;

System.Console.WriteLine($"Número de cenarios testados: {numeroDeCenarios}");
System.Console.WriteLine($"Acertos: {numeroDeResultadosCorretos}\nErros: {numeroDeCenarios - numeroDeResultadosCorretos}");
System.Console.WriteLine($"Porcentagem de acertos de quem trocou a resposta: {porcentagemDeAcertos}%");
System.Console.WriteLine($"Porcentagem de acertos de quem não trocou a resposta: {100 - porcentagemDeAcertos}%\n\n");

foreach (var resultado in resultados)
{
    var stringBuilder = new StringBuilder();
    for (int i = 0; i < resultado.Length; i++)
    {
        var item = resultado[i];
        var itemAlinhado = item.PadSides(resultados[0][i].Length);
        stringBuilder.Append(itemAlinhado);
        stringBuilder.Append('|');
    }

    System.Console.WriteLine(stringBuilder.ToString());
}


public static class StringExtensions
{
    public static string PadSides(this string str, int totalWidth, char paddingChar = ' ')
    {
        int padding = totalWidth - str.Length;
        int padLeft = padding / 2 + str.Length;
        return str.PadLeft(padLeft, paddingChar).PadRight(totalWidth, paddingChar);
    }
}