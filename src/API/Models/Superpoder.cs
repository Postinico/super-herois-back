using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Superpoderes")]
    public class Superpoder
    {
        protected Superpoder() { }

        public Superpoder(string superpoder, string descricao)
        {
            SuperPoder = superpoder;
            Descricao = descricao;
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("SuperPoder")]
        public string SuperPoder { get; private set; }

        [Column("Descricao")]
        public string Descricao { get; private set; }

        public void Update(string superPoder, string descricao)
        {
            SuperPoder = superPoder;
            Descricao = descricao;
        }
    }
}
