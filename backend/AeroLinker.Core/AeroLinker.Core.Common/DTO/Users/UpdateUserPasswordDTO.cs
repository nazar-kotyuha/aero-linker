﻿namespace AeroLinker.Core.Common.DTO.Users;

public class UpdateUserPasswordDto
{
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
