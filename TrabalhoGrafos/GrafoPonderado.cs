using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoGrafos
{
    public class GrafoPonderado
    {
    public int NumVertices { get; }
    public int NumArestas { get; }
    public List<Tuple<int, int, int>> Arestas { get; }

    public GrafoPonderado(int numVertices, int numArestas, List<Tuple<int, int, int>> arestas)
    {
        NumVertices = numVertices;
        NumArestas = numArestas;
        Arestas = arestas;
    }
    }
}