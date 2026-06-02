using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;

using System;

namespace MovieManagement.Business
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repo;

        public CategoriaService(ICategoriaRepository repo)
        {
            _repo = repo;
        }

        public void RegistrarCategoria(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
                throw new Exception("O nome da categoria é obrigatório.");

            if (_repo.Procurar(categoria.Nome) != null)
                throw new Exception("Já existe uma categoria com esse nome.");

            _repo.Adicionar(categoria);
        }

        public bool EliminarCategoria(string nome)
        {
            _repo.Remover(nome);
        }

        public Categoria ProcurarCategoriaPorNome(string nome)
        {
            return _repo.Procurar(nome);
        }

        public void ListarCategorias()
        {
            var categorias = _repo.Listar();

            foreach (var c in categorias)
                Console.WriteLine($"{c.Id} - {c.Nome}");
        }
    }
}
public void RegistrarCategoria(Categoria categoria);
public List<Categoria> ListarCategorias();
public Categoria ProcurarCategoriaPorId(int id);
public bool EliminarCategoria(int id);