using System.ComponentModel.DataAnnotations;

namespace mktSystem.Models
{
    public class PromocaoDTO
    {
        [Required]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage="Nome da promoção não pode ter mais de 100 caracteres")]
        [MinLength(2,ErrorMessage="Nome da promoção precisa ter mais de 2 caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage="Produto obrigatório")]
        public int ProdutoID { get; set; }
        
        [Required]
        [Range(0,100)]
        public float Porcentagem { get; set; }
        public bool Status { get; set; }
    }
}