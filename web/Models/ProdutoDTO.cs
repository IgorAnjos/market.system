using System.ComponentModel.DataAnnotations;
using mktSystem.Models;

namespace mktSystem.Models
{
    public class ProdutoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage="Nome do produto é obrigatório")]
        [StringLength(100, ErrorMessage="Nome do produto não pode ter mais de 100 caracteres")]
        [MinLength(2,ErrorMessage="Nome do produto precisa ter mais de 2 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Categoria do produto é obrigatório")]
        public int CategoriaID { get; set; }

        [Required(ErrorMessage="Fornecedor do produto é obrigatório")]
        public int FornecedorID { get; set; }

        [Required(ErrorMessage="Preço custo do produto é obrigatório")]
        public float PrecoCusto { get; set; }

        [Required(ErrorMessage="Preço venda do produto é obrigatório")]
        public float PrecoVenda { get; set; }

        [Required(ErrorMessage="Medição do produto é obrigatório")]
        [Range(0,2,ErrorMessage="Medição inválida")]
        public int Medicao { get; set; }
    }
}