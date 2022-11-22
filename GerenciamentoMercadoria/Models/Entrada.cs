using System.ComponentModel.DataAnnotations;

namespace GerenciamentoMercadoria.Models    
{
    public class Entrada
    {
 
        [Key]
        public int Id { get; set; }
        public string? Tipo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int Quantidade { get; set; }
        public DateTime? DataHora { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [MaxLength(150, ErrorMessage = "Limite de 150 caracteres excedido.")]
        public string? Rua { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [MaxLength(100, ErrorMessage = "Limite de 100 caracteres excedido.")]
        public string? Bairro { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string? Estado { get; set; }
        [Required]
        public int MercadoriaId { get; set; }
        public Mercadoria? mercadoria { get; set; }
    }
}
