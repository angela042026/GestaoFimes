using GestaoFilmes.Domain.Entities;
using System.Collections.Generic;

namespace GestaoFilmes.Domain.Interface
{
    public interface ICategoriaRepository
    {
        public void Adicionar(Categoria categoria);
        public List<Categoria> ObterTodas();
        public Categoria ObterPorNome(string nome);
       public Categoria ObterPorId(int id);
        public bool Remover(int id);
    }
}
//Porquê adicionar o ObterPorId? No futuro, quando fores associar uma categoria a um filme, o utilizador vai escolher o ID da categoria
//(Ex: "Escolha o género: 1 - Ação, 2 - Drama"). O teu código vai precisar de procurar a categoria pelo ID para garantir que ela existe.