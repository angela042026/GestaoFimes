using System;
using System.Collections.Generic;
using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using GestaoFilmes.Business;
using GestaoFilmes.Data;
using GestaoFilmes.Domain;

namespace GestaoFilmes.ConsoleUI
{
    //Antes de escrever o código do menu, é preciso fazer a ligação entre os projetos
    // botão direito em Dependencies do projeto GestaoFilmes.ConsoleUI-> Add Project Reference...
    // Selecionar os três projetos: Domain, Business e Data
    //Porquê: O menu vai precisar de criar o objeto Filme(Domain), chamar o FilmeService(Business) e instanciar o FilmeRepository(Data).
    class Program
    {
        // CORREÇÃO TÉCNICA: Especificamos o caminho exato de cada classe para evitar conflitos no compilador
        private static readonly GestaoFilmes.Domain.Interface.IFilmeRepository _filmeRepository = new GestaoFilmes.Data.FilmeRepository();
        private static readonly GestaoFilmes.Business.FilmeService _filmeService = new GestaoFilmes.Business.FilmeService(_filmeRepository);


        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== GESTÃO DE FILMES ===");
                Console.WriteLine("1. Adicionar Filme");
                Console.WriteLine("2. Listar Filmes");
                Console.WriteLine("3. Procurar Filme");
                Console.WriteLine("4. Remover Filme");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": MenuAdicionarFilme(); break;
                    case "2": MenuListarFilmes(); break;
                    case "3": MenuProcurarFilme(); break;
                    case "4": MenuRemoverFilme(); break;
                    case "0": continuar = false; break;
                    default:
                        Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void MenuAdicionarFilme()
        {
            Console.Clear();
            Console.WriteLine("=== ADICIONAR NOVO FILME ===\n");

            Filme novoFilme = new Filme();

            Console.Write("Título: ");
            novoFilme.Titulo = Console.ReadLine();

            Console.Write("Ano de Lançamento: ");
            if (int.TryParse(Console.ReadLine(), out int ano))
            {
                novoFilme.Ano = ano;
            }

            Console.Write("Língua: ");
            novoFilme.Lingua = Console.ReadLine();

            Console.WriteLine("\nClassificação:");
            Console.WriteLine("0-Péssimo | 1-Mau | 2-Médio | 3-Bom | 4-Muito Bom | 5-Excelente");
            Console.Write("Escolha (0-5): ");
            if (int.TryParse(Console.ReadLine(), out int nota) && nota >= 0 && nota <= 5)
            {
                novoFilme.Classificacao = (Classificacao)nota;
            }

            try
            {
                _filmeService.RegistarFilme(novoFilme);
                Console.WriteLine("\nFilme adicionado com sucesso com o ID: " + novoFilme.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Erro: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void MenuListarFilmes()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE FILMES REGISTADOS ===\n");

            var lista = _filmeService.ListarFilmes();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum filme registado no sistema.");
            }
            else
            {
                foreach (var f in lista)
                {
                    Console.WriteLine($"[ID: {f.Id}] {f.Titulo} ({f.Ano}) - Língua: {f.Lingua} | Nota: {(int)f.Classificacao} *");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void MenuProcurarFilme()
        {
            Console.Clear();
            Console.WriteLine("=== PROCURAR FILME POR TÍTULO ===\n");

            Console.Write("Digite o título exato que procura: ");
            string busca = Console.ReadLine();

            try
            {
                var filme = _filmeService.BuscarFilmePorTitulo(busca);

                if (filme != null)
                {
                    Console.WriteLine($"\nFilme Encontrado:");
                    Console.WriteLine($"-> ID: {filme.Id}");
                    Console.WriteLine($"-> Título: {filme.Titulo}");
                    Console.WriteLine($"-> Ano: {filme.Ano}");
                    Console.WriteLine($"-> Língua: {filme.Lingua}");
                    Console.WriteLine($"-> Nota: {(int)filme.Classificacao} *");
                }
                else
                {
                    Console.WriteLine("\nNenhum filme encontrado com esse título.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Erro: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void MenuRemoverFilme()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVER FILME ===\n");

            Console.Write("Digite o ID do filme a remover: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    bool apagou = _filmeService.EliminarFilme(id);
                    if (apagou)
                        Console.WriteLine("\nFilme removido com sucesso!");
                    else
                        Console.WriteLine("\nNão foi encontrado nenhum filme com esse ID.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido! Deve digitar um número.");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}
