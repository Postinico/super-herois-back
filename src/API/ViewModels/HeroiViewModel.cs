namespace API.ViewModels
{
    public class HeroiViewModel
    {
        public HeroiViewModel
        (
            int id, 
            string nome,
            string nomeHeroi,
            DateTime dataNascimento,
            decimal altura,
            decimal peso
        )
        {
            Id = id;
            Nome = nome;
            NomeHeroi = nomeHeroi;
            DataNascimento = dataNascimento;
            Altura = altura;
            Peso = peso;
        }

        public int Id { get; private set; }

        public string Nome { get; private set; }

        public string NomeHeroi { get; private set; }

        public DateTime DataNascimento { get; private set; }

        public decimal Altura { get; private set; }

        public decimal Peso { get; private set; }
    }
}
