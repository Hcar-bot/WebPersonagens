using System.ComponentModel.DataAnnotations;

namespace WebPersonagens.Models
{
    public class PersonagemDetalheViewModel
    {
        public Personagem Personagem { get; set; }

        [Display(Name = "Valor Total dos Itens")]
        [DataType(DataType.Currency)]
        public decimal TotalValorItens { get; set; }

        [Display(Name = "Poder Total Calculado")]
        public decimal PoderTotal { get; set; }
    }
}