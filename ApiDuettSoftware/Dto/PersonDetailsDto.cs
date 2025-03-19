using ApiDuettSoftware.Model.Enum;

namespace ApiDuettSoftware.Dto;

public record PersonDetailsDto(int Id, string Name, string Email, string Cpf, PersonType Type);
