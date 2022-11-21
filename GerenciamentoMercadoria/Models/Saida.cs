﻿using System.ComponentModel.DataAnnotations;

namespace GerenciamentoMercadoria.Models
{
    public class Saida
    {
        [Key]
        public int Id { get; set; }
        public string? Tipo { get; set; }
        [Required]
        public int Quantidade { get; set; }
        public DateTime? DataHora { get; set; }
        [Required]
        public string Rua { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public int MercadoriaId { get; set; }
        public Mercadoria? mercadoria { get; set; }
    }
}

