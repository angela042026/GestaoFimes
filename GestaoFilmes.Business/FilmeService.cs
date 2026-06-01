using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;

using System;

namespace GestaoFilmes.Business
{
    public class FilmeService: IFilmeService
    {
        private readonly IFilmeRepository _repositorio;

        // Construtor: serve para receber o repositório quando o serviço é criado
        public FilmeService(IFilmeRepository repositorio)
        {
            _repositorio = repositorio;
        }
       
            public void RegistarFilme(Filme filme)
        {
            // 1. REGRA: Título obrigatório
            if (string.IsNullOrWhiteSpace(filme.Titulo))
                throw new Exception("O título do filme não pode estar vazio!");

            // 2. REGRA: Verificar se o título já existe
            var filmeExistente = _repositorio.ProcurarPorTitulo(filme.Titulo);

            if (filmeExistente != null)
            {
                throw new InvalidOperationException($"Já existe um filme registado com o título '{filme.Titulo}'!");
            }

            // 3. REGRA: Ano válido
            if (filme.Ano < 1888 || filme.Ano > DateTime.Now.Year)
                throw new Exception("O ano do filme é inválido!");

            // 4. Gravação segura
            _repositorio.Adicionar(filme);
        }
        public List<Filme> ListarFilmes()
        {
            // 1. O Serviço pede os dados puros ao repositório e devolve-os para a interface
            return _repositorio.ObterTodos();
        }

        public Filme BuscarFilmePorTitulo(string titulo)
        {
            // 2. REGRA DE NEGÓCIO: Não permite buscas com textos vazios
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("O título de pesquisa não pode estar vazio!");
            return _repositorio.ProcurarPorTitulo(titulo);
        }

        public bool EliminarFilme(int id)
        {
            // 3. REGRA DE NEGÓCIO: Segurança de dados
            if (id <= 0)
                throw new ArgumentException("O ID do filme a remover deve ser maior que zero!");

            // 4. Executa a remoção e devolve true (se apagou) ou false (se não encontrou)
            return _repositorio.Remover(id);
        }

    }
}


