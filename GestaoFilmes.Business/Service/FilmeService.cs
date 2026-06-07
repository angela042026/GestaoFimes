using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using GestaoFilmes.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace GestaoFilmes.Business.Service
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _repositorio;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IRealizadorRepository _realizadorRepository;

        public FilmeService(
            IFilmeRepository repositorio,
            ICategoriaRepository categoriaRepository,
            IRealizadorRepository realizadorRepository)
        {
            _repositorio = repositorio;
            _categoriaRepository = categoriaRepository;
            _realizadorRepository = realizadorRepository;
        }

        public void RegistarFilme(Filme filme)
        {
            if (string.IsNullOrWhiteSpace(filme.Titulo))
                throw new Exception("O título do filme não pode estar vazio!");

            // Validar categoria
            var categoria = _categoriaRepository.ProcurarPorId(filme.CategoriaId);
            if (categoria == null)
                throw new Exception("A categoria indicada não existe.");

            // Validar realizador
            var realizador = _realizadorRepository.ProcurarPorId(filme.RealizadorId);
            if (realizador == null)
                throw new Exception("O realizador indicado não existe.");

            // Validar ano
            if (filme.Ano < 1888 || filme.Ano > DateTime.Now.Year)
                throw new Exception("O ano do filme é inválido!");

            // Verificar duplicado
            var filmeExistente = _repositorio.ProcurarPorTitulo(filme.Titulo.Trim().ToLower());
            if (filmeExistente != null)
                throw new InvalidOperationException($"Já existe um filme registado com o título '{filme.Titulo}'!");

            _repositorio.Adicionar(filme);
        }

        public List<Filme> ListarFilmes()
        {
            return _repositorio.ObterTodos();
        }

        public Filme BuscarFilmePorTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("O título não pode estar vazio.");

            titulo = titulo.Trim().ToLower();

            return _repositorio.ProcurarPorTitulo(titulo);
        }

        public bool EliminarFilme(int id)
        {
            if (id <= 0)
                throw new ArgumentException("O ID do filme a remover deve ser maior que zero!");

            return _repositorio.Remover(id);
        }
    }
}
