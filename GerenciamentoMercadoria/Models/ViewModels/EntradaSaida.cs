using System.ComponentModel.DataAnnotations;

namespace GerenciamentoMercadoria.Models.ViewModels
{
    public class EntradaSaida
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int Quantidade { get; set; }
        public DateTime? DataHora { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public int MercadoriaId { get; set; }
        public Mercadoria? mercadoria { get; set; }
    }
}
