using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("HeroisSuperpoderes")]
    public class HeroiSuperpoder
    {
        protected HeroiSuperpoder() { }

        [Column("HeroiId")]
        public int HeroiId { get; set; }

        [Column("SuperpoderId")]
        public int SuperpoderId { get; set; }
    }
}
