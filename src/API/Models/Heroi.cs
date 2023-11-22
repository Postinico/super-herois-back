using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Heroi
    {
        protected Heroi() { }

        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string NomeHeroi { get; set; }

        public DateTime DataNascimento { get; set; }

        public decimal Altura { get; set; }

        public decimal Peso { get; set; }
    }
}
