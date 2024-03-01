using System.ComponentModel.DataAnnotations;

namespace MiniPloomes.Domain.Dto;

public record ClienteDto(
    [Required]
    string Nome, 
    [Required]
    string UrlAvatar
){}