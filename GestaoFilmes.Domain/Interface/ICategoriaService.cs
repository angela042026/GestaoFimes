

using System.Collections.Generic;
using GestaoFilmes.Domain.Entities;

namespace GestaoFilmes.Domain.Interfaces
{
    public interface ICategoriaService
    {
        public void RegistrarCategoria(Categoria categoria);
        public List<Categoria> ListarCategorias();
        public Categoria ProcurarCategoriaPorNome(string nome);
        public bool EliminarCategoria(string nome);
    }
}
