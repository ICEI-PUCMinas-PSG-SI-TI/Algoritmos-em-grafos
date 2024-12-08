using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TrabalhoGrafos;

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




        //Leitura do grafo ponderado
        static GrafoPonderado LerGrafoDIMACSPonderado(string caminhoArquivo)
        {
            var arestas = new List<Tuple<int, int, int>>();
            int numVertices = 0, numArestas = 0;

            foreach (var linha in File.ReadLines(caminhoArquivo))
            {
                string[] partes = linha.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (partes.Length == 0) continue;

                switch (partes[0])
                {
                    case "c": // Linha de comentário
                              // Ignora linhas de comentário
                        break;

                    case "p": // Linha com as especificações do grafo
                        if (partes.Length >= 4 && partes[1] == "edge")
                        {
                            numVertices = int.Parse(partes[2]);
                            numArestas = int.Parse(partes[3]);
                        }
                        break;

                    case "e": // Linha com uma aresta ponderada
                        if (partes.Length >= 4)
                        {
                            int vertice1 = int.Parse(partes[1]);
                            int vertice2 = int.Parse(partes[2]);
                            int peso = int.Parse(partes[3]);
                            arestas.Add(new Tuple<int, int, int>(vertice1, vertice2, peso));
                        }
                        break;

                    default:
                        throw new FormatException($"Linha não reconhecida: {linha}");
                }
            }

            return new GrafoPonderado(numVertices, numArestas, arestas);
        }



        static void Main(string[] args)
        {

            Console.WriteLine("----------------------------------");
            Console.WriteLine("Digite a opção de execicio no menu");
            Console.WriteLine("1 - Para o execicio 1");
            Console.WriteLine("2 - Para o execicio 2");
            string menu = Console.ReadLine();
            switch (menu)
            {
                case "1":
                    int x = ExibirMenu();
                    CriandoGrafo(x);
                    break;

                case "2":
                    string filePath = "Files/Grafo.txt"; // Substitua pelo caminho do arquivo
                    GrafoPonderado grafo = LerGrafoDIMACSPonderado(filePath);
                    // Exibe os dados do grafo
                    Console.WriteLine($"Número de vértices: {grafo.NumVertices}");
                    Console.WriteLine($"Número de arestas: {grafo.NumArestas}");
                    Console.WriteLine("Arestas:");
                    foreach (var aresta in grafo.Arestas)
                    {
                        Console.WriteLine($"{aresta.Item1} -- {aresta.Item2} [peso: {aresta.Item3}]");
                    }


                    //SubMenu Questão 2


                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Digite a opção do execicio 2");
                    Console.WriteLine(" A - Para a letra 'A' do exercicio 2");
                    Console.WriteLine(" B - Para a letra 'B' do exercicio 2");
                    string submenu = Console.ReadLine();
                    if (submenu == "A")
                    {



                        // Solicita ao usuário a aresta de interesse
                        Console.WriteLine("\nInforme uma aresta no formato 'u v':");
                        string input = Console.ReadLine();
                        string[] partes = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        if (partes.Length == 2 &&
                            int.TryParse(partes[0], out int vertice1) &&
                            int.TryParse(partes[1], out int vertice2))
                        {
                            var adjacentes = grafo.EncontrarArestasAdjacentes(vertice1, vertice2);

                            Console.WriteLine($"\nArestas adjacentes a {vertice1} -- {vertice2}:");
                            if (adjacentes.Any())
                            {
                                foreach (var aresta in adjacentes)
                                {
                                    Console.WriteLine($"{aresta.Item1} -- {aresta.Item2} [peso: {aresta.Item3}]");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nenhuma aresta adjacente encontrada.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Entrada inválida. Certifique-se de informar dois inteiros separados por espaço.");
                        }

                    }
                    else if (submenu == "B")
                    {
                        // Solicita ao usuário o vértice de interesse
                        Console.WriteLine("\nInforme o vértice para encontrar os vértices adjacentes:");
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out int vertice))
                        {
                            var adjacentes = grafo.EncontrarVerticesAdjacentes(vertice);

                            Console.WriteLine($"\nVértices adjacentes ao vértice {vertice}:");
                            if (adjacentes.Any())
                            {
                                Console.WriteLine(string.Join(", ", adjacentes));
                            }
                            else
                            {
                                Console.WriteLine("Nenhum vértice adjacente encontrado.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Entrada inválida. Certifique-se de informar um número inteiro.");
                        }
                    }
            

            break;

            default:
                    break;

        }


    }
}
}
