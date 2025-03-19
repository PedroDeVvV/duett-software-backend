using ApiDuettSoftware.Model.Enum;

namespace ApiDuettSoftware.Dto;

public record UpdateDataPerson(
    int Id,
    string CurrentPassword, 
    string NewPassword
    );
