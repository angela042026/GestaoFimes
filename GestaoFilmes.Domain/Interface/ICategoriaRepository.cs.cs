using GestaoFilmes.Domain.Entities;
using System.Collections.Generic;

namespace GestaoFilmes.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        public void Adicionar(Categoria categoria);
        public List<Categoria> ObterTodas();
        public Categoria ObterPorNome(string nome);
        public Categoria ObterPorId(int id);
        public void Remover(int id);
    }
}

