using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPersonagens.Models
{
    [Table("Universos")]
    public class Universo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do universo é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
        [Display(Name = "Nome do Universo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição deve ser detalhada.")]
        [Display(Name = "Descrição do Universo")]
        public string Descricao { get; set; }

        public virtual ICollection<Personagem> Personagens { get; set; }
    }
}