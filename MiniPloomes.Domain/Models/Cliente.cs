namespace MiniPloomes.Domain;

public record Cliente(
    string Nome,
    DateTime CreatedAt,
    string EmailConta,
    string UrlAvatar
){}