using System.ComponentModel.DataAnnotations;

namespace WebPersonagens.Models
{
    public class ContagemPorUniversoViewModel
    {
        [Display(Name = "Universo")]
        public string NomeUniverso { get; set; }

        [Display(Name = "Total de Personagens")]
        public int ContagemPersonagens { get; set; }
    }
}