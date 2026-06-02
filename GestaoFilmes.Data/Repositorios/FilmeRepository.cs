using GestaoFilmes.Domain.Entities;   // Para encontrar a classe 'Filme'
using GestaoFilmes.Domain.Interface; // Para encontrar a interface 'IFilmeRepository'
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace GestaoFilmes.Data.Repositorios
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly List<Filme> _filmes = new List<Filme>();
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

        public Filme ProcurarPorTitulo(string titulo)
        {
            return _filmes.FirstOrDefault(
                f => f.Titulo.Contains(titulo,
                    StringComparison.OrdinalIgnoreCase));
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

        

