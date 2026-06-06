using GestaoFilmes.Business.Service;
using GestaoFilmes.Data.Repositorios;
using GestaoFilmes.Domain;
using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Entities.GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using GestaoFilmes.Domain.Interfaces;
using MovieManagement.Business;
using System;
using System.Collections.Generic;
using GestaoFilmes.Data.Repositorios;

namespace GestaoFilmes.ConsoleUI
{
    class Program
    {
        private static readonly IFilmeRepository _filmeRepository = new FilmeRepository();
        private static readonly FilmeService _filmeService = new FilmeService(_filmeRepository);

        private static readonly ICategoriaRepository _categoriaRepository = new CategoriaRepository();
        private static readonly ICategoriaService _categoriaService = new CategoriaService(_categoriaRepository);

        private static readonly IRealizadorRepository _realizadorRepository = new RealizadorRepository();
        private static readonly IRealizadorService _realizadorService = new RealizadorService(_realizadorRepository);

        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("          GESTÃO DE FILMES       ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Filmes");
                Console.WriteLine("2. Categorias");
                Console.WriteLine("3. Realizadores");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        MenuFilmes();
                        break;
                    case "2":
                        MenuCategorias();
                        break;
                    case "3":
                        MenuRealizadores();
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void MenuFilmes()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("            FILMES               ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Adicionar filme");
                Console.WriteLine("2. Listar filmes");
                Console.WriteLine("3. Procurar filme");
                Console.WriteLine("4. Remover filme");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        MenuAdicionarFilme();
                        break;
                    case "2":
                        MenuListarFilmes();
                        break;
                    case "3":
                        MenuProcurarFilme();
                        break;
                    case "4":
                        MenuRemoverFilme();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void MenuCategorias()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("            CATEGORIAS           ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Adicionar categoria");
                Console.WriteLine("2. Listar categorias");
                Console.WriteLine("3. Procurar categoria por nome");
                Console.WriteLine("4. Remover categoria por ID");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        MenuAdicionarCategoria();
                        break;
                    case "2":
                        MenuListarCategorias();
                        break;
                    case "3":
                        MenuProcurarCategoria();
                        break;
                    case "4":
                        MenuRemoverCategoria();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void MenuRealizadores()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("           REALIZADORES          ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Adicionar realizador");
                Console.WriteLine("2. Listar realizadores");
                Console.WriteLine("3. Procurar realizador por nome");
                Console.WriteLine("4. Remover realizador por ID");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        MenuAdicionarRealizador();
                        break;
                    case "2":
                        MenuListarRealizadores();
                        break;
                    case "3":
                        MenuProcurarRealizador();
                        break;
                    case "4":
                        MenuRemoverRealizador();
                        break;
                    case "0":
                        voltar = true;
                        break;
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
            novoFilme.Titulo = Console.ReadLine() ?? string.Empty;

            Console.Write("Ano de Lançamento: ");
            if (int.TryParse(Console.ReadLine(), out int ano))
                novoFilme.Ano = ano;

            Console.Write("Língua: ");
            novoFilme.Lingua = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("\nClassificação:");
            Console.WriteLine("0-Péssimo | 1-Mau | 2-Médio | 3-Bom | 4-Muito Bom | 5-Excelente");
            Console.Write("Escolha (0-5): ");
            if (int.TryParse(Console.ReadLine(), out int nota) && nota >= 0 && nota <= 5)
            {
                novoFilme.Classificacao = (ClassificacaoFilme)nota;
            }
            else
            {
                Console.WriteLine("Classificação inválida. Só são permitidos valores entre 0 e 5.");
                Console.ReadKey();
                return;
            }

            try
            {
                _filmeService.RegistarFilme(novoFilme);
                Console.WriteLine("\nFilme adicionado com sucesso com o ID: " + novoFilme.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
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
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine($"ID: {f.Id}");
                    Console.WriteLine($"Título: {f.Titulo}");
                    Console.WriteLine($"Ano: {f.Ano}");
                    Console.WriteLine($"Língua: {f.Lingua}");
                    Console.WriteLine($"Classificação: {(int)f.Classificacao}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void MenuProcurarFilme()
        {
            Console.Clear();
            Console.WriteLine("=== PROCURAR FILME POR TÍTULO ===\n");

            Console.Write("Digite o título exato que procura: ");
            string busca = Console.ReadLine() ?? string.Empty;

            try
            {
                var filme = _filmeService.BuscarFilmePorTitulo(busca);

                if (filme != null)
                {
                    Console.WriteLine("\nFilme Encontrado:");
                    Console.WriteLine($"-> ID: {filme.Id}");
                    Console.WriteLine($"-> Título: {filme.Titulo}");
                    Console.WriteLine($"-> Ano: {filme.Ano}");
                    Console.WriteLine($"-> Língua: {filme.Lingua}");
                    Console.WriteLine($"-> Nota: {(int)filme.Classificacao}");
                }
                else
                {
                    Console.WriteLine("\nNenhum filme encontrado com esse título.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
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

        private static void MenuAdicionarCategoria()
        {
            Console.Clear();
            Console.WriteLine("=== ADICIONAR CATEGORIA ===\n");

            Categoria categoria = new Categoria();

            Console.Write("Nome: ");
            categoria.Nome = Console.ReadLine() ?? string.Empty;

            try
            {
                _categoriaService.RegistarCategoria(categoria);
                Console.WriteLine("\nCategoria adicionada com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void MenuListarCategorias()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE CATEGORIAS ===\n");

            var categorias = _categoriaService.ListarCategorias();

            if (categorias.Count == 0)
            {
                Console.WriteLine("Nenhuma categoria registada.");
            }
            else
            {
                foreach (var c in categorias)
                {
                    Console.WriteLine($"{c.Id} - {c.Nome}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void MenuProcurarCategoria()
        {
            Console.Clear();
            Console.WriteLine("=== PROCURAR CATEGORIA ===\n");

            Console.Write("Nome da categoria: ");
            string nome = Console.ReadLine() ?? string.Empty;

            var categoria = _categoriaService.ProcurarCategoriaPorNome(nome);

            if (categoria != null)
                Console.WriteLine($"\n{categoria.Id} - {categoria.Nome}");
            else
                Console.WriteLine("\nCategoria não encontrada.");

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void MenuRemoverCategoria()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVER CATEGORIA ===\n");

            Console.Write("Digite o ID da categoria: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool apagou = _categoriaService.EliminarCategoria(id);

                if (apagou)
                    Console.WriteLine("\nCategoria removida com sucesso.");
                else
                    Console.WriteLine("\nCategoria não encontrada.");
            }
            else
            {
                Console.WriteLine("\nID inválido!");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void MenuAdicionarRealizador()
        {
            Console.Clear();
            Console.WriteLine("=== ADICIONAR REALIZADOR ===\n");

            Realizador realizador = new Realizador();

            Console.Write("Nome: ");
            realizador.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("País: ");
            realizador.Pais = Console.ReadLine() ?? string.Empty;

            try
            {
                _realizadorService.RegistarRealizador(realizador);
                Console.WriteLine("\nRealizador adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void MenuListarRealizadores()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE REALIZADORES ===\n");

            var realizadores = _realizadorService.ListarRealizadores();

            if (realizadores.Count == 0)
            {
                Console.WriteLine("Nenhum realizador registado.");
            }
            else
            {
                foreach (var r in realizadores)
                {
                    Console.WriteLine($"{r.Id} - {r.Nome} ({r.Pais})");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void MenuProcurarRealizador()
        {
            Console.Clear();
            Console.WriteLine("=== PROCURAR REALIZADOR ===\n");

            Console.Write("Nome do realizador: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o ID do realizador: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var realizador = _realizadorService.ProcurarRealizadorPorId(id);

                if (realizador != null)
                    Console.WriteLine($"\n{realizador.Id} - {realizador.Nome} ({realizador.Pais})");
                else
                    Console.WriteLine("\nRealizador não encontrado.");

                Console.WriteLine("\nPressione qualquer tecla para voltar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nID inválido!");
                Console.WriteLine("\nPressione qualquer tecla para voltar...");
                Console.ReadKey();
            }
        }



        private static void MenuRemoverRealizador()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVER REALIZADOR ===\n");

            Console.Write("Digite o ID do realizador: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool apagou = _realizadorService.EliminarRealizador(id);

                if (apagou)
                    Console.WriteLine("\nRealizador removido com sucesso.");
                else
                    Console.WriteLine("\nRealizador não encontrado.");
            }
            else
            {
                Console.WriteLine("\nID inválido!");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}