namespace GestaoFilmes.Domain.Entities
{
    public class Filmes
    
        {
            //modelo de dados do sistema

            public int Id { get; set; }
            public string Título { get; set; }
            public DateTime Ano { get; set; }
            public string Língua { get; set; }
                       
            public Filmes()
            {
                Título = string.Empty;//defenir como vazio para evitar null
            }
        
    }
}

