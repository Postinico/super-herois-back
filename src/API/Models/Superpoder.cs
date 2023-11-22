using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Superpoderes")]
    public class Superpoder
    {
        protected Superpoder() { }

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("SuperPoder")]
        public string SuperPoder { get; set; }

        [Column("Descricao")]
        public string Descricao { get; set; }
    }
}
