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


        /// <summary>
        /// Encontra os vértices adjacentes a um vértice especificado.
        /// </summary>
        public List<int> EncontrarVerticesAdjacentes(int v)
        {
            return Arestas
                .Where(aresta => aresta.Item1 == v || aresta.Item2 == v) // Verifica adjacência
                .Select(aresta => aresta.Item1 == v ? aresta.Item2 : aresta.Item1) // Retorna o outro vértice
                .Distinct() // Remove duplicatas
                .ToList();
        }


        /// <summary>
        /// Encontra as arestas incidentes a um vértice especificado.
        /// </summary>
        public List<Tuple<int, int, int>> EncontrarArestasIncidentes(int v)
        {
            return Arestas
                .Where(aresta => aresta.Item1 == v || aresta.Item2 == v) // Verifica se o vértice está presente
                .ToList();
        }

        /// <summary>
        /// Encontra a aresta que conecta dois vértices.
        /// </summary>
        public Tuple<int, int, int> EncontrarAresta(int v1, int v2)
        {
            return Arestas.FirstOrDefault(aresta =>
                (aresta.Item1 == v1 && aresta.Item2 == v2) || (aresta.Item1 == v2 && aresta.Item2 == v1));
        }

        /// <summary>
        /// Calcula o grau de um vértice especificado.
        /// </summary>
        public int CalcularGrau(int v)
        {
            return Arestas.Count(aresta => aresta.Item1 == v || aresta.Item2 == v);
        }

        /// <summary>
        /// Verifica se dois vértices são adjacentes.
        /// </summary>
        public bool SaoAdjacentes(int v1, int v2)
        {
            return Arestas.Any(aresta =>
                (aresta.Item1 == v1 && aresta.Item2 == v2) || (aresta.Item1 == v2 && aresta.Item2 == v1));
        }


        /// <summary>
        /// Atualiza o peso de uma aresta especificada.
        /// </summary>
        public bool AtualizarPesoAresta(int v1, int v2, int novoPeso)
        {
            for (int i = 0; i < Arestas.Count; i++)
            {
                var aresta = Arestas[i];
                if ((aresta.Item1 == v1 && aresta.Item2 == v2) || (aresta.Item1 == v2 && aresta.Item2 == v1))
                {
                    Arestas[i] = new Tuple<int, int, int>(aresta.Item1, aresta.Item2, novoPeso);
                    return true;
                }
            }
            return false;
        }


    }
}