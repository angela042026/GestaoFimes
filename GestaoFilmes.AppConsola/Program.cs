using GestaoFilmes.Business.Service;
using GestaoFilmes.Data.Repositorios;
using GestaoFilmes.Domain;
using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using GestaoFilmes.Domain.Interfaces;
using MovieManagement.Business;
using System;

namespace GestaoFilmes.ConsoleUI
{
    class Program
    {
        // ---------------------- REPOSITÓRIOS SQLITE ----------------------
        static readonly IFilmeRepository _filmeRepository = new FilmeRepositorySQLite();
        static readonly ICategoriaRepository _categoriaRepository = new CategoriaRepositorySQLite();
        static readonly IRealizadorRepository _realizadorRepository = new RealizadorRepositorySQLite();

        // ---------------------- SERVIÇOS ----------------------
        private static readonly ICategoriaService _categoriaService =
            new CategoriaService(_categoriaRepository);

        static readonly IFilmeService _filmeService =
            new FilmeService(_filmeRepository, _categoriaRepository, _realizadorRepository);

        private static readonly IRealizadorService _realizadorService =
            new RealizadorService(_realizadorRepository);

        // ---------------------- FUNÇÕES AUXILIARES ----------------------
        private static string ConverterParaEstrelas(int nota)
        {
            return new string('★', nota) + new string('☆', 5 - nota);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║           GESTÃO DE FILMES           ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();

                // MELHORIA: Ordem lógica do fluxo de negócio
                Console.WriteLine("1. 🏷️ Categorias");
                Console.WriteLine("2. 🎥 Realizadores");
                Console.WriteLine("3. 🎬 Filmes");
                Console.WriteLine("0. ❌ Sair");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1": MenuCategorias(); break;
                    case "2": MenuRealizadores(); break;
                    case "3": MenuFilmes(); break;
                    case "0": continuar = false; break;
                    default: MostrarErro("Opção inválida!"); break;
                }
            }
        }

        // ---------------------- ESTILO VISUAL ----------------------
        private static void Titulo(string texto)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine($"║   {texto.PadLeft((38 + texto.Length) / 2).PadRight(38)}   ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");
            Console.ResetColor();
        }

        private static void MostrarErro(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + msg);
            Console.ResetColor();
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void MostrarSucesso(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + msg);
            Console.ResetColor();
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void Pausa()
        {
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        // ---------------------- FILMES ----------------------
        private static void MenuFilmes()
        {
            bool voltar = false;

            while (!voltar)
            {
                Titulo("FILMES");

                Console.WriteLine("1. ➕ Adicionar filme");
                Console.WriteLine("2. 📄 Listar filmes");
                Console.WriteLine("3. 🔍 Procurar filme");
                Console.WriteLine("4. 🗑️ Remover filme");
                Console.WriteLine("0. 🔙 Voltar");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1": MenuAdicionarFilme(); break;
                    case "2": MenuListarFilmes(); break;
                    case "3": MenuProcurarFilme(); break;
                    case "4": MenuRemoverFilme(); break;
                    case "0": voltar = true; break;
                    default: MostrarErro("Opção inválida!"); break;
                }
            }
        }

        private static void MenuAdicionarFilme()
        {
            Titulo("ADICIONAR FILME");

            Filme novoFilme = new Filme();

            Console.Write("Título: ");
            novoFilme.Titulo = Console.ReadLine() ?? string.Empty;

            Console.Write("Ano de Lançamento: ");
            if (int.TryParse(Console.ReadLine(), out int ano))
                novoFilme.Ano = ano;

            Console.Write("Língua: ");
            novoFilme.Lingua = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("\n---------------------------------");
            // MELHORIA: Listar categorias automáticas para ajudar o utilizador
            Console.WriteLine("🏷️ Categorias Disponíveis:");
            var categorias = _categoriaService.ListarCategorias();
            if (categorias.Count == 0)
            {
                MostrarErro("Não pode adicionar filmes sem criar categorias primeiro!");
                return;
            }
            foreach (var c in categorias)
            {
                Console.WriteLine($"  [{c.Id}] - {c.Nome}");
            }
            Console.WriteLine("---------------------------------");

            Console.Write("Escolha o ID da categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int catId) || _categoriaRepository.ObterPorId(catId) == null)
            {
                MostrarErro("ID de categoria inválido ou inexistente na base de dados!");
                return;
            }
            novoFilme.CategoriaId = catId;

            Console.WriteLine("\n---------------------------------");
            // MELHORIA: Listar realizadores automáticos para ajudar o utilizador
            Console.WriteLine("🎥 Realizadores Disponíveis:");
            var realizadores = _realizadorService.ListarRealizadores();
            if (realizadores.Count == 0)
            {
                MostrarErro("Não pode adicionar filmes sem criar realizadores primeiro!");
                return;
            }
            foreach (var r in realizadores)
            {
                Console.WriteLine($"  [{r.Id}] - {r.Nome} ({r.Pais})");
            }
            Console.WriteLine("---------------------------------");

            Console.Write("Escolha o ID do realizador: ");
            if (!int.TryParse(Console.ReadLine(), out int realId) || _realizadorRepository.ProcurarPorId(realId) == null)
            {
                MostrarErro("ID de realizador inválido ou inexistente na base de dados!");
                return;
            }
            novoFilme.RealizadorId = realId;

            Console.WriteLine("\nClassificação:");
            Console.WriteLine("0-Péssimo | 1-Mau | 2-Médio | 3-Bom | 4-Muito Bom | 5-Excelente");
            Console.Write("Escolha (0-5): ");

            if (int.TryParse(Console.ReadLine(), out int nota) && nota >= 0 && nota <= 5)
            {
                novoFilme.Classificacao = (ClassificacaoFilme)nota;
            }
            else
            {
                MostrarErro("Classificação inválida. Só são permitidos valores entre 0 e 5.");
                return;
            }

            try
            {
                _filmeService.RegistarFilme(novoFilme);
                MostrarSucesso("Filme adicionado com sucesso! ID: " + novoFilme.Id);
            }
            catch (Exception ex)
            {
                MostrarErro(ex.Message);
            }
        }

        private static void MenuListarFilmes()
        {
            Titulo("LISTA DE FILMES");

            var lista = _filmeService.ListarFilmes();

            if (lista.Count == 0)
            {
                MostrarErro("Nenhum filme registado.");
                return;
            }

            foreach (var f in lista)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("---------------------------------");
                Console.WriteLine($"ID: {f.Id}");
                Console.WriteLine($"Título: {f.Titulo}");
                Console.WriteLine($"Ano: {f.Ano}");
                Console.WriteLine($"Língua: {f.Lingua}");
                Console.WriteLine($"Classificação: {(int)f.Classificacao} {ConverterParaEstrelas((int)f.Classificacao)}");
                Console.ResetColor();
                Console.WriteLine();
            }

            Pausa();
        }

        private static void MenuProcurarFilme()
        {
            Titulo("PROCURAR FILME");

            Console.Write("Digite o título exato: ");
            string busca = Console.ReadLine() ?? string.Empty;

            try
            {
                var filme = _filmeService.BuscarFilmePorTitulo(busca);

                if (filme != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nFilme Encontrado:");
                    Console.ResetColor();
                    Console.WriteLine($"-> ID: {filme.Id}");
                    Console.WriteLine($"-> Título: {filme.Titulo}");
                    Console.WriteLine($"-> Ano: {filme.Ano}");
                    Console.WriteLine($"-> Língua: {filme.Lingua}");
                    Console.WriteLine($"Classificação: {(int)filme.Classificacao} {ConverterParaEstrelas((int)filme.Classificacao)}");
                }
                else
                {
                    MostrarErro("Nenhum filme encontrado com esse título.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MostrarErro(ex.Message);
                return;
            }

            Pausa();
        }

        private static void MenuRemoverFilme()
        {
            Titulo("REMOVER FILME");

            Console.Write("Digite o ID do filme: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    bool apagou = _filmeService.EliminarFilme(id);

                    if (apagou)
                        MostrarSucesso("Filme removido com sucesso!");
                    else
                        MostrarErro("Nenhum filme encontrado com esse ID.");
                }
                catch (Exception ex)
                {
                    MostrarErro(ex.Message);
                }
            }
            else
            {
                MostrarErro("ID inválido!");
            }
        }

        // ---------------------- CATEGORIAS ----------------------
        private static void MenuCategorias()
        {
            bool voltar = false;

            while (!voltar)
            {
                Titulo("CATEGORIAS");

                Console.WriteLine("1. ➕ Adicionar categoria");
                Console.WriteLine("2. 📄 Listar categorias");
                Console.WriteLine("3. 🔍 Procurar categoria");
                Console.WriteLine("4. 🗑️ Remover categoria");
                Console.WriteLine("0. 🔙 Voltar");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1": MenuAdicionarCategoria(); break;
                    case "2": MenuListarCategorias(); break;
                    case "3": MenuProcurarCategoria(); break;
                    case "4": MenuRemoverCategoria(); break;
                    case "0": voltar = true; break;
                    default: MostrarErro("Opção inválida!"); break;
                }
            }
        }

        private static void MenuAdicionarCategoria()
        {
            Titulo("ADICIONAR CATEGORIA");

            Categoria categoria = new Categoria();

            Console.Write("Nome: ");
            categoria.Nome = Console.ReadLine() ?? string.Empty;

            try
            {
                _categoriaService.RegistarCategoria(categoria);
                MostrarSucesso("Categoria adicionada com sucesso.");
            }
            catch (Exception ex)
            {
                MostrarErro(ex.Message);
            }
        }

        private static void MenuListarCategorias()
        {
            Titulo("LISTA DE CATEGORIAS");

            var categorias = _categoriaService.ListarCategorias();

            if (categorias.Count == 0)
            {
                MostrarErro("Nenhuma categoria registada.");
                return;
            }

            foreach (var c in categorias)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{c.Id} - {c.Nome}");
                Console.ResetColor();
            }

            Pausa();
        }

        private static void MenuProcurarCategoria()
        {
            Titulo("PROCURAR CATEGORIA");

            Console.Write("Nome da categoria: ");
            string nome = Console.ReadLine() ?? string.Empty;

            var categoria = _categoriaService.ProcurarCategoriaPorNome(nome);

            if (categoria != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{categoria.Id} - {categoria.Nome}");
                Console.ResetColor();
            }
            else
            {
                MostrarErro("Categoria não encontrada.");
                return;
            }

            Pausa();
        }

        private static void MenuRemoverCategoria()
        {
            Titulo("REMOVER CATEGORIA");

            Console.Write("Digite o ID da categoria: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool apagou = _categoriaService.EliminarCategoria(id);

                if (apagou)
                    MostrarSucesso("Categoria removida com sucesso.");
                else
                    MostrarErro("Categoria não encontrada.");
            }
            else
            {
                MostrarErro("ID inválido!");
            }
        }

        // ---------------------- REALIZADORES ----------------------
        private static void MenuRealizadores()
        {
            bool voltar = false;

            while (!voltar)
            {
                Titulo("REALIZADORES");

                Console.WriteLine("1. ➕ Adicionar realizador");
                Console.WriteLine("2. 📄 Listar realizadores");
                Console.WriteLine("3. 🔍 Procurar realizador");
                Console.WriteLine("4. 🗑️ Remover realizador");
                Console.WriteLine("0. 🔙 Voltar");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1": MenuAdicionarRealizador(); break;
                    case "2": MenuListarRealizadores(); break;
                    case "3": MenuProcurarRealizador(); break;
                    case "4": MenuRemoverRealizador(); break;
                    case "0": voltar = true; break;
                    default: MostrarErro("Opção inválida!"); break;
                }
            }
        }

        private static void MenuAdicionarRealizador()
        {
            Titulo("ADICIONAR REALIZADOR");

            Realizador realizador = new Realizador();

            Console.Write("Nome: ");
            realizador.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("País: ");
            realizador.Pais = Console.ReadLine() ?? string.Empty;

            try
            {
                _realizadorService.RegistarRealizador(realizador);
                MostrarSucesso("Realizador adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                MostrarErro(ex.Message);
            }
        }

        private static void MenuListarRealizadores()
        {
            Titulo("LISTA DE REALIZADORES");

            var realizadores = _realizadorService.ListarRealizadores();

            if (realizadores.Count == 0)
            {
                MostrarErro("Nenhum realizador registado.");
                return;
            }

            foreach (var r in realizadores)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{r.Id} - {r.Nome} ({r.Pais})");
                Console.ResetColor();
            }

            Pausa();
        }

        private static void MenuProcurarRealizador()
        {
            Titulo("PROCURAR REALIZADOR");

            Console.Write("Digite o ID do realizador: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var realizador = _realizadorService.ProcurarRealizadorPorId(id);

                if (realizador != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{realizador.Id} - {realizador.Nome} ({realizador.Pais})");
                    Console.ResetColor();
                }
                else
                {
                    MostrarErro("Realizador não encontrado.");
                    return;
                }

                Pausa();
            }
            else
            {
                MostrarErro("ID inválido!");
            }
        }

        private static void MenuRemoverRealizador()
        {
            Titulo("REMOVER REALIZADOR");

            Console.Write("Digite o ID do realizador: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool apagou = _realizadorService.EliminarRealizador(id);

                if (apagou)
                    MostrarSucesso("Realizador removido com sucesso.");
                else
                    MostrarErro("Realizador não encontrado.");
            }
            else
            {
                MostrarErro("ID inválido!");
            }
        }
    }
}