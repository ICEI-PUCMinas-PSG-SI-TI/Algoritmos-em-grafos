using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoGrafos
{
    public class GrafoPonderado
    {
        public int NumVertices { get; set; }
        public int NumArestas { get; set; }
        public List<Tuple<int, int, int>> Arestas { get; set; }

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


        /// <summary>
        /// Troca os vértices especificados.
        /// </summary>
        public void TrocarVertices(int v1, int v2)
        {
            Arestas = Arestas.Select(aresta =>
            {
                int novoVertice1 = aresta.Item1 == v1 ? v2 : (aresta.Item1 == v2 ? v1 : aresta.Item1);
                int novoVertice2 = aresta.Item2 == v1 ? v2 : (aresta.Item2 == v2 ? v1 : aresta.Item2);
                return new Tuple<int, int, int>(novoVertice1, novoVertice2, aresta.Item3);
            }).ToList();
        }


        /// <summary>
        /// Realiza a busca em largura no grafo.
        /// </summary>
        public (Dictionary<int, int> Nivel, Dictionary<int, int?> Predecessores, List<Tuple<int, int>> ArvoreBusca) BuscaEmLargura(int verticeInicial)
        {
            var nivel = new Dictionary<int, int>();
            var predecessores = new Dictionary<int, int?>();
            var arvoreBusca = new List<Tuple<int, int>>();
            var visitados = new HashSet<int>();
            var fila = new Queue<int>();

            // Inicializa estruturas
            for (int i = 1; i <= NumVertices; i++)
            {
                nivel[i] = -1; // -1 significa "não visitado"
                predecessores[i] = null;
            }

            // Configuração inicial
            nivel[verticeInicial] = 0;
            visitados.Add(verticeInicial);
            fila.Enqueue(verticeInicial);

            // Mapeia as adjacências
            var adjacencias = new Dictionary<int, List<int>>();
            foreach (var aresta in Arestas)
            {
                if (!adjacencias.ContainsKey(aresta.Item1))
                    adjacencias[aresta.Item1] = new List<int>();
                if (!adjacencias.ContainsKey(aresta.Item2))
                    adjacencias[aresta.Item2] = new List<int>();

                adjacencias[aresta.Item1].Add(aresta.Item2);
                adjacencias[aresta.Item2].Add(aresta.Item1);
            }

            // BFS
            while (fila.Count > 0)
            {
                int atual = fila.Dequeue();
                if (!adjacencias.ContainsKey(atual)) continue;

                // Ordena os vértices adjacentes em ordem numérica crescente
                foreach (var vizinho in adjacencias[atual].OrderBy(v => v))
                {
                    if (!visitados.Contains(vizinho))
                    {
                        visitados.Add(vizinho);
                        fila.Enqueue(vizinho);
                        nivel[vizinho] = nivel[atual] + 1;
                        predecessores[vizinho] = atual;
                        arvoreBusca.Add(new Tuple<int, int>(atual, vizinho));
                    }
                }
            }

            return (nivel, predecessores, arvoreBusca);
        }


        /// <summary>
        /// Realiza a busca em profundidade no grafo.
        /// </summary>
        public (Dictionary<int, int> Descoberta, Dictionary<int, int> Finalizacao, List<Tuple<int, int>> ArvoreBusca) BuscaEmProfundidade(int verticeInicial)
        {
            var descoberta = new Dictionary<int, int>();
            var finalizacao = new Dictionary<int, int>();
            var arvoreBusca = new List<Tuple<int, int>>();
            var visitados = new HashSet<int>();
            int tempo = 0;

            // Mapeia as adjacências
            var adjacencias = new Dictionary<int, List<int>>();
            foreach (var aresta in Arestas)
            {
                if (!adjacencias.ContainsKey(aresta.Item1))
                    adjacencias[aresta.Item1] = new List<int>();
                if (!adjacencias.ContainsKey(aresta.Item2))
                    adjacencias[aresta.Item2] = new List<int>();

                adjacencias[aresta.Item1].Add(aresta.Item2);
                adjacencias[aresta.Item2].Add(aresta.Item1);
            }

            // Ordena as adjacências para garantir a ordem numérica
            foreach (var chave in adjacencias.Keys.ToList())
            {
                adjacencias[chave] = adjacencias[chave].OrderBy(v => v).ToList();
            }

            // Função recursiva para DFS
            void DFSVisit(int vertice)
            {
                visitados.Add(vertice);
                descoberta[vertice] = ++tempo;

                if (adjacencias.ContainsKey(vertice))
                {
                    foreach (var vizinho in adjacencias[vertice])
                    {
                        if (!visitados.Contains(vizinho))
                        {
                            arvoreBusca.Add(new Tuple<int, int>(vertice, vizinho));
                            DFSVisit(vizinho);
                        }
                    }
                }

                finalizacao[vertice] = ++tempo;
            }

            // Inicializa os tempos
            for (int i = 1; i <= NumVertices; i++)
            {
                descoberta[i] = -1;
                finalizacao[i] = -1;
            }

            // Inicia a DFS a partir do vértice inicial
            DFSVisit(verticeInicial);

            return (descoberta, finalizacao, arvoreBusca);
        }


        /// <summary>
        /// Obtém o peso de uma aresta entre dois vértices.
        /// </summary>
        public int ObterPesoAresta(int v1, int v2)
        {
            var aresta = Arestas.FirstOrDefault(a =>
                (a.Item1 == v1 && a.Item2 == v2) ||
                (a.Item1 == v2 && a.Item2 == v1));
            return aresta?.Item3 ?? int.MaxValue;
        }

        /// <summary>
        /// Executa o algoritmo de Dijkstra para encontrar o caminho mínimo entre dois vértices.
        /// </summary>
        public (int Distancia, List<int> Caminho) Dijkstra(int origem, int destino)
        {
            var dist = new Dictionary<int, int>();
            var prev = new Dictionary<int, int?>();
            var visitados = new HashSet<int>();
            var fila = new SortedSet<(int Distancia, int Vertice)>();

            // Inicializa as estruturas
            for (int i = 1; i <= NumVertices; i++)
            {
                dist[i] = int.MaxValue;
                prev[i] = null;
            }
            dist[origem] = 0;
            fila.Add((0, origem));

            // Mapeia as adjacências
            var adjacencias = new Dictionary<int, List<(int Vizinho, int Peso)>>();
            foreach (var aresta in Arestas)
            {
                if (!adjacencias.ContainsKey(aresta.Item1))
                    adjacencias[aresta.Item1] = new List<(int, int)>();
                if (!adjacencias.ContainsKey(aresta.Item2))
                    adjacencias[aresta.Item2] = new List<(int, int)>();

                adjacencias[aresta.Item1].Add((aresta.Item2, aresta.Item3));
                adjacencias[aresta.Item2].Add((aresta.Item1, aresta.Item3));
            }

            // Executa o algoritmo de Dijkstra
            while (fila.Count > 0)
            {
                var (distAtual, verticeAtual) = fila.Min;
                fila.Remove((distAtual, verticeAtual));
                if (visitados.Contains(verticeAtual)) continue;
                visitados.Add(verticeAtual);

                if (!adjacencias.ContainsKey(verticeAtual)) continue;

                foreach (var (vizinho, peso) in adjacencias[verticeAtual])
                {
                    if (visitados.Contains(vizinho)) continue;

                    int novaDistancia = distAtual + peso;
                    if (novaDistancia < dist[vizinho])
                    {
                        fila.Remove((dist[vizinho], vizinho)); // Remove se existir
                        dist[vizinho] = novaDistancia;
                        prev[vizinho] = verticeAtual;
                        fila.Add((novaDistancia, vizinho));
                    }
                }
            }

            // Reconstrói o caminho
            var caminho = new List<int>();
            for (int? at = destino; at.HasValue; at = prev[at.Value])
            {
                caminho.Add(at.Value);
            }
            caminho.Reverse();

            return dist[destino] == int.MaxValue ? (int.MaxValue, new List<int>()) : (dist[destino], caminho);
        }


        /// <summary>
        /// Implementa o algoritmo de Floyd-Warshall para calcular caminhos mínimos entre todos os pares de vértices.
        /// </summary>
        public (int[,] Dist, int?[,] Next) FloydWarshall()
        {
            int[,] dist = new int[NumVertices + 1, NumVertices + 1];
            int?[,] next = new int?[NumVertices + 1, NumVertices + 1];

            // Inicializa as matrizes de distância e predecessores
            for (int i = 1; i <= NumVertices; i++)
            {
                for (int j = 1; j <= NumVertices; j++)
                {
                    dist[i, j] = i == j ? 0 : int.MaxValue;
                    next[i, j] = null;
                }
            }

            foreach (var aresta in Arestas)
            {
                int u = aresta.Item1;
                int v = aresta.Item2;
                int peso = aresta.Item3;

                dist[u, v] = peso;
                next[u, v] = v;
            }

            // Executa o algoritmo de Floyd-Warshall
            for (int k = 1; k <= NumVertices; k++)
            {
                for (int i = 1; i <= NumVertices; i++)
                {
                    for (int j = 1; j <= NumVertices; j++)
                    {
                        if (dist[i, k] != int.MaxValue && dist[k, j] != int.MaxValue &&
                            dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                            next[i, j] = next[i, k];
                        }
                    }
                }
            }

            return (dist, next);
        }

        /// <summary>
        /// Reconstrói o caminho mínimo entre dois vértices usando a matriz de predecessores.
        /// </summary>
        public List<int> ReconstruirCaminho(int origem, int destino, int?[,] next)
        {
            var caminho = new List<int>();
            if (next[origem, destino] == null)
                return caminho;

            int atual = origem;
            while (atual != destino)
            {
                caminho.Add(atual);
                atual = next[atual, destino].Value;
            }
            caminho.Add(destino);
            return caminho;
        }



    }
}