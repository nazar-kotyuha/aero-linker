﻿using AeroLinker.Core.Common.DTO.Auth;
using AeroLinker.Core.DAL.Entities;
using AutoMapper;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace AeroLinker.Core.BLL.MappingProfiles;

public sealed class UserRegisterProfile : Profile
{
    public UserRegisterProfile()
    {
        CreateMap<User, UserRegisterDto>()!.ReverseMap();

        CreateMap<Payload, UserRegisterDto>()!
            .ForMember(m => m.FirstName, s => s.MapFrom(f => ReplaceSpaces(f.GivenName)))
            .ForMember(m => m.LastName, s => s.MapFrom(f => ReplaceSpaces(f.FamilyName)))
            .ForMember(m => m.Username, s => s.MapFrom(f => ReplaceSpaces(f.Name)));
    }

    private string ReplaceSpaces(string value)
    {
        return value == null ? "" : value.Replace(' ', '-');
    }
}