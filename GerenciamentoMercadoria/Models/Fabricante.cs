using System.ComponentModel.DataAnnotations;

namespace GerenciamentoMercadoria.Models
{
    public class Fabricante
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [MaxLength(50, ErrorMessage = "Limite de 50 caracteres excedido.")]
        public string? Nome { get; set; }
    }
}
