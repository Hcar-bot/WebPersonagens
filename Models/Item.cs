using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPersonagens.Models 
{
    [Table("Itens")]
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Valor { get; set; }

        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }

        public int? PersonagemId { get; set; }

        [ForeignKey("PersonagemId")]
        public virtual Personagem? Personagem { get; set; }
    }
}