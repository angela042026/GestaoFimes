using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoFilmes.Data.Repositorios
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly List<Filme> _filmes = new();
        private static int _proximoId = 1;

        public void Adicionar(Filme filme)
        {
            filme.Id = _proximoId++;
            _filmes.Add(filme);
        }

        public List<Filme> ObterTodos()
        {
            return _filmes
                .OrderBy(f => f.Titulo, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public Filme ProcurarPorTitulo(string titulo)
        {
            return _filmes.FirstOrDefault(f =>
                string.Equals(f.Titulo, titulo, StringComparison.OrdinalIgnoreCase));
        }

        public bool Remover(int id)
        {
            var filme = _filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null) return false;

            _filmes.Remove(filme);
            return true;
        }
    }
}
