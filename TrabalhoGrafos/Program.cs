using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhGrafos
{
    internal class Program
    {
        static int ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("1 - Construir Grafo com matriz adjacente (direcionado)");
            Console.WriteLine("2 - Construir Grafo com matriz adjacente (nao direcionado)");
            Console.WriteLine("3 - Construir Grafo com lista adjacente (direcionado)");
            Console.WriteLine("4 - Construir Grafo com matriz adjacente (nao direcionado)");

            return int.Parse(Console.ReadLine());
        }

        static void CriandoGrafo(int x)
        {
            int numVertice, numAresta;
            Console.WriteLine("Quantos vertices voce deseja?");
            numVertice = int.Parse(Console.ReadLine());
            Console.WriteLine("Quantos arestas voce deseja?");
            numAresta = int.Parse(Console.ReadLine());
            if (numAresta > numVertice)
                numAresta = numVertice;
            if (x == 1)
            {
                int[,] matriz = ConstruindoMatrizDirecionada(numVertice, numAresta);
                Console.WriteLine("Matriz final: \n");
                ImprimindoMatriz(matriz, numVertice);
            }
            else if (x == 2)
            {
                int[,] matriz = ConstruindoMatrizNaoDirecionada(numVertice, numAresta);
                Console.WriteLine("Matriz final: \n");
                ImprimindoMatriz(matriz, numVertice);
            }
            else if (x == 3)
            {
                GrafoComListaAD grafo = new GrafoComListaAD(numVertice, numAresta);
                AdicionarArestaGrafoDirecionado(grafo);
            }
            else if (x == 4)
            {
                GrafoComListaAD grafo = new GrafoComListaAD(numVertice, numAresta);
                AdicionarArestaGrafoNaoDirecionado(grafo);
            }
        }

        static void ImprimindoMatriz(int[,] matrizM, int numeroVertice)
        {
            for (int i = 0; i < numeroVertice; i++)
            {
                for (int j = 0; j < numeroVertice; j++)
                {
                    Console.Write($"{matrizM[i, j]} ");
                }
                Console.Write("\n");
            }
        }

        static int[,] Matrizz(int numVertice)
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

        static int[,] ConstruindoMatrizDirecionada(int numeroVertice, int numeroArestas)
        {
            Console.WriteLine("Criando matriz: ");
            int numArestasColocadas = 0;
            int[,] Matriz = Matrizz(numeroVertice);
            for (int i = 0; i < numeroVertice; i++)
            {
                for (int j = 0; j < numeroVertice; j++)
                {
                    if (i != j && numArestasColocadas != numeroArestas)
                    {
                        Console.WriteLine($"deseja coloca peso na aresta entre os vertices: {i} para {j}? (1 para sim 2 para nao)");
                        int desejo = int.Parse(Console.ReadLine());
                        if (desejo == 1)
                        {
                            Console.WriteLine("Qual peso?");
                            Matriz[i, j] = int.Parse(Console.ReadLine());
                            numArestasColocadas++;
                        }
                    }
                }
            }
            return Matriz;
        }

        static int[,] ConstruindoMatrizNaoDirecionada(int numeroVertice, int numeroArestas)
        {
            Console.WriteLine("Criando matriz: ");
            int numArestasColocadas = 0;
            int[,] Matriz = Matrizz(numeroVertice);
            for (int i = 0; i < numeroVertice; i++)
            {
                for (int j = 0; j < numeroVertice; j++)
                {
                    if (i != j && numArestasColocadas != numeroArestas && Matriz[j, i] == 0)
                    {
                        Console.WriteLine($"deseja coloca peso na aresta entre os vertices: {i} e {j}? (1 para sim 2 para nao)");
                        int desejo = int.Parse(Console.ReadLine());
                        if (desejo == 1)
                        {
                            Console.WriteLine("Qual peso?");
                            Matriz[i, j] = int.Parse(Console.ReadLine());
                            Matriz[j, i] = Matriz[i, j];
                            numArestasColocadas++;
                        }
                    }
                }
            }
            return Matriz;
        }

        static void AdicionarArestaGrafoDirecionado(GrafoComListaAD grafo)
        {
            grafo.PerguntarD();
            ImprimirGrafo(grafo);
        }

        static void ImprimirGrafo(GrafoComListaAD grafo)
        {
            grafo.Imprimir();
        }

        static void AdicionarArestaGrafoNaoDirecionado(GrafoComListaAD grafo)
        {
            grafo.PerguntarND();
            ImprimirGrafo(grafo);
        }

        static void Main(string[] args)
        {
            int x = ExibirMenu();
            CriandoGrafo(x);
        }
    }
}
