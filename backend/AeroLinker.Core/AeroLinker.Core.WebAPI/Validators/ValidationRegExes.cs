﻿namespace AeroLinker.Core.WebAPI.Validators;

public static class ValidationRegExes
{
    public const string NoCyrillic = @"^(?![\u0410-\u044F\u0400-\u04FF]).*$";
}
