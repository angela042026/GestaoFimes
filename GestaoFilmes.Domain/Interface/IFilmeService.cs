using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestaoFilmes.Domain.Interface
{
    //O Service responde à pergunta: "Que regras de negócio devo aplicar antes de mexer nos dados?"
    public interface IFilmeService
    {
        public void RegistarFilme(Filme filme);
        //void: Significa "vazio". Indica que esta função faz uma ação(regista o filme), mas não devolve nenhuma resposta no final.
        //(Filme filme): É o parâmetro de entrada.Significa que, para esta função funcionar, a interface gráfica tem de lhe passar
        //um objeto completo do tipo Filme(com título, ano, língua, etc.).
        public List<Filme> ListarFilmes();
        //List<Filme>: É o tipo de retorno.Significa que, quando esta função terminar de correr, vai devolver uma lista cheia de objetos Filme
        public Filme BuscarFilmePorTitulo(string titulo);
        //Filme: É o tipo de retorno.Significa que esta função vai devolver apenas um único filme no final(ou null se não encontrar nenhum).
        public bool EliminarFilme(int id);
    }
}

// a interface define o que o sistema é obrigado a fazer, mas não explica como é feito

//UI->IFilmeService-> (valida) IFilmeRepository -> (guarda) List<Filme>