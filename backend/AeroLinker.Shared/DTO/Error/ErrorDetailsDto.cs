using AeroLinker.Shared.Enums;

namespace AeroLinker.Shared.DTO.Error;

public record ErrorDetailsDto(string Message, ErrorType ErrorType);
