using GestaoFilmes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestaoFilmes.Domain.Interface
{
    //O Repository responde à pergunta: "Como acedo aos dados?"
        public interface IFilmeRepository
        {
           public void Adicionar(Filme filme);
            public List<Filme> ObterTodos();
            public Filme ProcurarPorTitulo(string titulo);
            public bool Remover(int id);
        }
}
//funciona como um contrato obrigatório.  
//   "Qualquer classe que queira gravar filmes (seja em ficheiro de texto, JSON ou SQL)" 
//    " tem de implementar obrigatoriamente estas 4 funções

//IFilmeService está no Domain porque: Define as ações de negócio a aplicação promete fazer para o utilizador.

//IFilmeRepository está no Domain porque: O negócio está a dizer: "Eu não sei onde os dados vão ser guardados (se num ficheiro ou numa base de dados),
//mas quem tratar disso na camada Data vai ter de cumprir este contrato".