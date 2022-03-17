using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }

        [Display(Name = "Local")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Local { get; set; }

        [Display(Name = "Data evento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo {0} deve ter no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo 50 caracteres")]
        public string Tema { get; set; }

        [Display(Name = "Qtd Pessoas")]
        [Range(1, 120000, ErrorMessage = "O campo {0} deve ter valores entre 1 a 120.000")]
        public int QtdPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)", ErrorMessage = "O arquivo enviado não é uma imagem válida. (gif, jpg, joeg, bmp ou png)")]
        public string ImageUrl { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Phone(ErrorMessage = "O {0} está número inválido")]
        public string Telefone { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um {0} válido")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedeSociais { get; set; }
        public IEnumerable<PalestranteDto> PalestranteEventos { get; set; }
    }
}