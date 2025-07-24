// busca em profundidade com backtracking

class Estradas
{
    public string origem { get; set; }
    public string destino { get; set; }
    public int distancia { get; set; }

    public Estradas(string origem, string destino, int distancia)
    {
        this.origem = origem;
        this.destino = destino;
        this.distancia = distancia;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(origem, destino, distancia);
    }

}

class EncontraCaminhos
{
    // Função recursiva reversa, partindo do destino e "voltando" para a origem
    private void EncontraCaminho(Estradas[] estradas, string origem, string destino, List<Estradas> caminhos, int distanciaTotal, List<int> distancias)
    {
        if (origem == destino) // Para quando o que eu passei (el.origen) é igual ao destino (A)
        {
            distancias.Add(distanciaTotal);
            return;
        }

        foreach (var el in estradas)
        {
            if (el.destino == destino && !caminhos.Contains(el))
            {
                caminhos.Add(el);
                EncontraCaminho(estradas, origem, el.origem, caminhos, distanciaTotal + el.distancia, distancias);
                caminhos.RemoveAt(caminhos.Count - 1); // backtrack
                // É executado uma vez para cada chamada recursiva, logo após a recursão correspondente retornar.
            }
        }
    }

    public List<int> EncontraTodosOsCaminhos(Estradas[] estradas, string origem, string destino)
    {
        List<int> distancias = new List<int>();
        EncontraCaminho(estradas, origem, destino, new List<Estradas>(), 0, distancias);
        return distancias;
    }
}

class ExtraiEstradas
{
    public List<Estradas> extraiEstradas()
    {
        string raw = File.ReadAllText("listaEstradas.txt");
        string[] listaEstradas = raw.Split(',');

        List<Estradas> estradas = new List<Estradas>();

        foreach (var el in listaEstradas)
        {
            string[] partes = el.Split(':');
            string origem = partes[0].Split("->")[0].Trim();
            string destino = partes[0].Split("->")[1].Trim();
            int distancia = int.Parse(partes[1]);
            estradas.Add(new Estradas(origem, destino, distancia));
        }

        return estradas;
    }
}

class Program
{
    static void Main()
    {
        ExtraiEstradas extrator = new ExtraiEstradas();
        List<Estradas> estradas = extrator.extraiEstradas();
        EncontraCaminhos EncontraCaminhos = new EncontraCaminhos();

        var distancias = EncontraCaminhos.EncontraTodosOsCaminhos(estradas.ToArray(), "A", "D");

        Console.WriteLine($"Caminhos encontrados: {distancias.Count}");
        foreach (var d in distancias)
        {
            Console.WriteLine($"Distância total: {d}");
        }
    }
}
