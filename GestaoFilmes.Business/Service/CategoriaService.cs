using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using GestaoFilmes.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repo;

        public CategoriaService(ICategoriaRepository repo)
        {
            _repo = repo;
        }

        public void RegistarCategoria(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
                throw new Exception("O nome da categoria é obrigatório.");

            if (_repo.ObterPorNome(categoria.Nome) != null)
                throw new Exception("Já existe uma categoria com esse nome.");

            _repo.Adicionar(categoria);
        }

        public List<Categoria> ListarCategorias()
        {
            return _repo.ObterTodas();
        }

        public Categoria ProcurarCategoriaPorNome(string nome)
        {
            return _repo.ObterPorNome(nome);
        }

        public bool EliminarCategoria(int id)
        {
            return _repo.Remover(id);
        }
    }
}