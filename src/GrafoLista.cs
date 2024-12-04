using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TrabalhGrafos
{
    internal class GrafoComListaAD
    {
        private int numVertices;
        private int numArestas;
        private Celula[] adjLists;

        // Construtor para criar um grafo com número de vértices fornecido
        public GrafoComListaAD(int vertices, int numArestas)
        {
            numVertices = vertices;
            this.numArestas = numArestas;
            adjLists = new Celula[vertices];

            // Inicializa as listas de adjacência
            for (int i = 0; i < vertices; i++)
            {
                adjLists[i] = null;
            }
        }

        public void PerguntarD()
        {
            int numArestasColocadas = 0;
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    if (i != j && numArestasColocadas != numArestas)
                    {
                        Console.WriteLine($"deseja coloca peso na aresta entre os vertices: {i} e {j}? (1 para sim 2 para nao)");
                        int desejo = int.Parse(Console.ReadLine());
                        if (desejo == 1)
                        {
                            Console.WriteLine("Qual peso?");
                            int peso = int.Parse(Console.ReadLine());
                            AdicionarPesoeArestaDirecionado(i, j, peso);
                            numArestasColocadas++;
                        }
                    }
                }
            }
        }

        static int[,] Matriz(int numVertice)
        {
            int[,] mat = new int[numVertice, numVertice];
            for (int i = 0; i < numVertice; i++)
            {
                for (int j = 0; j < numVertice; j++)
                {
                    mat[i, j] = 0;
                }
                Console.WriteLine("");
            }
            return mat;
        }

        //nao ta certo
        public void PerguntarND()
        {
            int[,] m = Matriz(numVertices);
            int numArestasColocadas = 0;
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    if (i != j && numArestasColocadas != numArestas && m[j, i] == 0)
                    {
                        Console.WriteLine($"deseja coloca peso na aresta entre os vertices: {i} e {j}? (1 para sim 2 para nao)");
                        int desejo = int.Parse(Console.ReadLine());
                        if (desejo == 1)
                        {
                            Console.WriteLine("Qual peso?");
                            int peso = int.Parse(Console.ReadLine());
                            m[i, j] = peso;
                            m[j, i] = m[i, j];
                            AdicionarPesoeAresta(i, j, peso);
                            numArestasColocadas++;
                        }
                    }
                }
            }
        }

        public void AdicionarPesoeArestaDirecionado(int s, int d, int peso)
        {
            // Adiciona aresta de s para d
            Celula newCelula = new Celula(d, peso);
            newCelula.Next = adjLists[s];
            adjLists[s] = newCelula;
        }

        // Método para adicionar uma aresta ao grafo
        public void AdicionarPesoeAresta(int s, int d, int peso)
        {
            // Adiciona aresta de s para d
            Celula newCelula = new Celula(d, peso);
            newCelula.Next = adjLists[s];
            adjLists[s] = newCelula;

            // Adiciona aresta de d para s (grafo não direcionado)
            newCelula = new Celula(s, peso);
            newCelula.Next = adjLists[d];
            adjLists[d] = newCelula;
        }

        // Método para imprimir o grafo
        public void Imprimir()
        {
            for (int v = 0; v < numVertices; v++)
            {
                Celula temp = adjLists[v];
                Console.Write("\n no " + v + ": ");
                while (temp != null)
                {
                    Console.Write("No: " + temp.no + " (valor do peso: " + temp.peso + ") -> ");
                    temp = temp.Next;
                }
                Console.WriteLine();
            }
        }
    }
}
