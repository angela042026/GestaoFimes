using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace GestaoFilmes.Data
{
    public class FilmeRepository : IFilmeRepository
    {
        // 1. "Base de Dados" temporária
        private static readonly List<Filme> _filmes = new List<Filme>();

        // 2. Controlador para gerar IDs automáticos
        private static int _proximoId = 1;

        public void Adicionar(Filme filme)
        {
            // Atribui um ID único e incrementa para o próximo filme
            filme.Id = _proximoId++;
            _filmes.Add(filme);
        }

        public List<Filme> ObterTodos()
        {
            // Devolve a lista completa
            return _filmes;
        }

        public Filme ObterPorTitulo(string titulo)
        {
            // Procura o primeiro filme com o título igual (ignora maiúsculas/minúsculas e espaços)
            return _filmes.FirstOrDefault(f => f.Titulo.Equals(titulo?.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public bool Remover(int id)
        {
            var filme = _filmes.FirstOrDefault(f => f.Id == id);
            if (filme != null)
            {
                _filmes.Remove(filme);
                return true; // Encontrou e removeu
            }
            return false; // Não encontrou nenhum filme com esse ID
        }
    }
}

