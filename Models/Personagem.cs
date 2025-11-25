using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPersonagens.Models 
{
    [Table("Personagens")]
    public class Personagem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        [Required]
        [Range(1, 999)]
        public int Nivel { get; set; }

        [Required]
        [Range(1, 10000)]
        public int VidaMaxima { get; set; }

        [Display(Name = "Profissão")]
        [Required]
        public int ProfissaoId { get; set; }

        [ForeignKey("ProfissaoId")]
        public virtual Profissao Profissao { get; set; }

        [Display(Name = "Universo")]
        [Required]
        public int UniversoId { get; set; }

        [ForeignKey("UniversoId")]
        public virtual Universo Universo { get; set; }

        public virtual ICollection<Item> Itens { get; set; }
    }
}