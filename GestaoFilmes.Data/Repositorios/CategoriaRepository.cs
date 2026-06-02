using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GestaoFilme.Data
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly List<Categoria> _categorias = new List<Categoria>();
        private int _nextId = 1;

        public void Adicionar(Categoria categoria)
        {
            categoria.Id = _nextId++;
            _categorias.Add(categoria);
        }

        public List<Categoria> Listar()
        {
            return _categorias;
        }
        public Categoria ObterPorNome(string nome)
        {
            return _categorias.FirstOrDefault(c => c.Nome.ToLower() == nome.ToLower());
        }

        public Categoria ObterPorId(int id)
        {
            return _categorias.FirstOrDefault(c => c.Id == id());
        }
     

        public void Remover(string nome)
        {
            var cat = ObterPorNome(nome);
            if (cat != null)
                _categorias.Remove(cat);
        }
    }
}

