

using GestaoFilmes.Domain.Entities;
using System.Collections.Generic;

namespace GestaoFilmes.Domain.Interfaces
{
    public interface ICategoriaService
    {
        void RegistarCategoria(Categoria categoria);
        List<Categoria> ListarCategorias();
        Categoria ProcurarCategoriaPorNome(string nome);
        bool EliminarCategoria(int id);
    }
}