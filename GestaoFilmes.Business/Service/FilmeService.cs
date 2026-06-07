using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using GestaoFilmes.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace GestaoFilmes.Business.Service
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepo;
        private readonly ICategoriaRepository _categoriaRepo;
        private readonly IRealizadorRepository _realizadorRepo;

        public FilmeService(IFilmeRepository filmeRepo, ICategoriaRepository categoriaRepo, IRealizadorRepository realizadorRepo)
        {
            _filmeRepo = filmeRepo;
            _categoriaRepo = categoriaRepo;
            _realizadorRepo = realizadorRepo;
        }

        public void RegistarFilme(Filme filme)
        {
            if (string.IsNullOrWhiteSpace(filme.Titulo))
                throw new Exception("O título do filme é obrigatório.");

            // CORREÇÃO AQUI: Mudado de ProcurarPorId para ObterPorId
            var categoria = _categoriaRepo.ObterPorId(filme.CategoriaId);
            if (categoria == null)
                throw new Exception("A categoria selecionada não existe.");

            var realizador = _realizadorRepo.ProcurarPorId(filme.RealizadorId);
            if (realizador == null)
                throw new Exception("O realizador selecionado não existe.");

            if (_filmeRepo.ProcurarPorTitulo(filme.Titulo) != null)
                throw new Exception("Já existe um filme registado com este título.");

            _filmeRepo.Adicionar(filme);
        }

        public List<Filme> ListarFilmes()
        {
            return _filmeRepo.ObterTodos();
        }

        public Filme BuscarFilmePorTitulo(string titulo)
        {
            return _filmeRepo.ProcurarPorTitulo(titulo);
        }

        public bool EliminarFilme(int id)
        {
            return _filmeRepo.Remover(id);
        }
    }
}
