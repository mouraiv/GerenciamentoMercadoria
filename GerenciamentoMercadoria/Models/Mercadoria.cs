using System.ComponentModel.DataAnnotations;

namespace GerenciamentoMercadoria.Models
{
    public class Mercadoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(50, ErrorMessage = "Limite de 50 caracteres excedido.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(150, ErrorMessage = "Limite de 150 caracteres excedido.")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int FabricanteId { get; set; }
        public virtual Fabricante? fabricante { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int CategoriaId { get; set; }
        public virtual Categoria? categoria { get; set; }
    }
}
