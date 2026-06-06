using GestaoFilmes.Domain.Interfaces;

using GestaoFilmes.Domain.Entities;
using System.Collections.Generic;

namespace GestaoFilmes.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        void Adicionar(Categoria categoria);
        List<Categoria> ObterTodas();
        Categoria ObterPorNome(string nome);
        Categoria ObterPorId(int id);
        bool Remover(int id);
    }
}