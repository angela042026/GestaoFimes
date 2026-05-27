using System.Collections.Generic;
using GestaoFilmes.Domain.Entities;

namespace GestaoFilmes.Domain.Interfaces
{
    public interface ICategoriaService
    {
       public void RegistarCategoria(Categoria categoria);
        public List<Categoria> ListarCategorias();
        public Categoria BuscarCategoriaPorId(int id);
        public bool EliminarCategoria(int id);
    }
}
