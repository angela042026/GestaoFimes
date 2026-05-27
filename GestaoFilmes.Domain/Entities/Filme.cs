namespace GestaoFilmes.Domain.Entities
{
    //Esta classe é uma representação (um molde) de um filme no mundo real para o teu programa.
    //contém apenas as propriedades (dados) que definem o que é um filme, sem qualquer lógica de gravação ou exibição.
    
    public class Filme 
    {
        public int Id { get; set; }
        public string Titulo { get; set; } 
        public int Ano { get; set; }       
        public string Lingua { get; set; } 
        
        public Classificacao ClassificacaoFilme { get; set; }

        public Filme()
        {
            Titulo = string.Empty;
            Lingua = string.Empty;
            // definir uma classificação padrão ao criar o filme
            ClassificacaoFilme = Classificacao.Medio;
        }
       
    }
}


