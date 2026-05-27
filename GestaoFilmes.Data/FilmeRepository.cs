using GestaoFilmes.Domain.Entities;   // Para encontrar a classe 'Filme'
using GestaoFilmes.Domain.Interface;
using GestaoFilmes.Domain.Interfaces; // Para encontrar a interface 'IFilmeRepository'
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoFilmes.Data
{
    public class FilmeRepository : IFilmeRepository
    {
        // O resto do teu código está 100% PERFEITO e não mexe em nada!
        private static readonly List<Filme> _filmes = new List<Filme>();
        private static int _proximoId = 1;

        public void Adicionar(Filme filme)
        {
            filme.Id = _proximoId++;
            _filmes.Add(filme);
        }

        public List<Filme> ObterTodos()
        {
            return _filmes;
        }

        public Filme ObterPorTitulo(string titulo)
        {
            return _filmes.FirstOrDefault(f => f.Titulo.Equals(titulo?.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public bool Remover(int id)
        {
            var filme = _filmes.FirstOrDefault(f => f.Id == id);
            if (filme != null)
            {
                _filmes.Remove(filme);
                return true;
            }
            return false;
        }
    }
}


