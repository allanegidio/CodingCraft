using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estamparia.MVC.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public Guid ProdutoId { get; set; }

        public Guid CategoriaId { get; set; }

        public byte TamanhoId { get; set; }

        public byte GolaId { get; set; }

        public int EstampaId { get; set; }

        [Required]
        [Index("IUQ_Produtos_Nome", IsUnique = true)]
        [StringLength(100)]
        public string Nome { get; set; }

        public string Cor { get; set; }

        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        public int Estoque { get; set; }

        public virtual Tamanho Tamanho { get; set; }

        public virtual Gola Gola { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Estampa Estampa { get; set; }
    }
}