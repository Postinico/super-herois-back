namespace API.Models
{
    public class Heroi
    {
        protected Heroi() { }

        public Heroi
        (
            string nome,
            string nomeHeroi,
            DateTime dataNascimento,
            decimal altura,
            decimal peso
        )
        {
            Nome = nome;
            NomeHeroi = nomeHeroi;
            DataNascimento = dataNascimento;
            Altura = altura;
            Peso = peso;
        }

        public int Id { get; set; }

        public string Nome { get; private set; }

        public string NomeHeroi { get; private set; }

        public DateTime DataNascimento { get; private set; }

        public decimal Altura { get; private set; }

        public decimal Peso { get; private set; }

        public void Update
        (
            string nome,
            string nomeHeroi,
            DateTime dataNascimento,
            decimal altura,
            decimal peso
        )
        {
            Nome = nome;
            NomeHeroi = nomeHeroi;
            DataNascimento = dataNascimento;
            Altura = altura;
            Peso = peso;
        }
    }
}
