using GestaoFilmes.Domain.Interface;

using System;

namespace MovieManagement.Business
{
    public class RealizadorService
    {
        private readonly IRealizadorRepository _repo;

        public RealizadorService(IRealizadorRepository repo)
        {
            _repo = repo;
        }

        public void RegistrarRealizador(Realizador realizador)
        {
            if (string.IsNullOrWhiteSpace(realizador.Nome))
                throw new Exception("O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(realizador.Pais))
                throw new Exception("O país é obrigatório.");

            _repo.Adicionar(realizador);
        }


        public void EliminarRealizador(string nome)
        {
            _repo.Remover(nome);
        }

        public Realizador ProcurarRealizadorPorId(int id)
        {
            return _repo.Procurar(id);
        }

        public void ListarRealizador()
        {
            var realizadores = _repo.Listar();

            foreach (var r in realizadores)
                Console.WriteLine($"{r.Id} - {r.Nome} ({r.Pais})");
        }
    }
}
