using System.ComponentModel.DataAnnotations;

namespace mktSystem.Models
{
    public class FornecedorDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required(ErrorMessage="Nome do fornecedor é obrigatório")]
        [StringLength(100, ErrorMessage="Nome da fornecedor não pode ter mais de 100 caracteres")]
        [MinLength(2,ErrorMessage="Nome da fornecedor precisa ter mais de 2 caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage="E-mail é obrigatório")]
        [EmailAddress(ErrorMessage="E-mail inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage="Telefone é obrigatório")]
        [Phone(ErrorMessage="Número de telefone inválido")]
        public string Telefone { get; set; }
    }
}