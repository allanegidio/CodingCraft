namespace Lojinha.MVC.Models
{
    public class CompraClienteProduto
    {
        public int Quantidade { get; set; }
        public decimal Total => ProdutoCliente.Preco * Quantidade;


        public int ProdutoClienteId { get; set; }
        public ProdutoCliente ProdutoCliente { get; set; }
    }
}