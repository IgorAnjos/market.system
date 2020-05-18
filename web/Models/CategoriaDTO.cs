using System.ComponentModel.DataAnnotations;

namespace mktSystem.Models
{
    public class CategoriaDTO
    {
        [Required]
        public int Id { get; set; }
         
        [Required]
        [StringLength(100, ErrorMessage="Nome da categoria não pode ter mais de 100 caracteres")]
        [MinLength(2,ErrorMessage="Nome da categoria precisa ter mais de 2 caracteres")]
        public string Nome { get; set; }
        public bool Status { get; set; }
    }
}