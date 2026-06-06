using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using GestaoFilmes.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GestaoFilmes.Data.Repositorios
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly List<Categoria> _categorias = new();
        private int _nextId = 1;

        public void Adicionar(Categoria categoria)
        {
            categoria.Id = _nextId++;
            _categorias.Add(categoria);
        }

        public List<Categoria> ObterTodas()
        {
            return _categorias;
        }

        public Categoria ObterPorNome(string nome)
        {
            return _categorias.FirstOrDefault(c =>
                string.Equals(c.Nome, nome, System.StringComparison.OrdinalIgnoreCase));
        }

        public Categoria ObterPorId(int id)
        {
            return _categorias.FirstOrDefault(c => c.Id == id);
        }

        public bool Remover(int id)
        {
            var cat = ObterPorId(id);
            if (cat == null)
                return false;

            _categorias.Remove(cat);
            return true;
        }
    }
}