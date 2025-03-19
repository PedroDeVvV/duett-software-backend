using ApiDuettSoftware.Model.Enum;

namespace ApiDuettSoftware.Dto;

public record PersonDto(string Name, string Email, string Cpf, string Password, PersonType Type);



