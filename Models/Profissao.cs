using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPersonagens.Models 
{
    [Table("Profissoes")]
    public class Profissao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50)]
        [Display(Name = "Nome da Profissão")]
        public string Nome { get; set; }

        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }

        public virtual ICollection<Personagem> Personagens { get; set; }
    }
}