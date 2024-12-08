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
        /// <summary>
        /// Encontra as arestas adjacentes à aresta (u, v).
        /// </summary>
        public List<Tuple<int, int, int>> EncontrarArestasAdjacentes(int u, int v)
        {
            return Arestas
                .Where(aresta =>
                    (aresta.Item1 == u || aresta.Item2 == u || aresta.Item1 == v || aresta.Item2 == v) &&
                    !(aresta.Item1 == u && aresta.Item2 == v)) // Exclui a própria aresta
                .ToList();
        }


    }
}