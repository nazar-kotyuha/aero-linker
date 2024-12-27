using AutoMapper;
using AeroLinker.Core.DAL.Context;

namespace AeroLinker.Core.BLL.Services.Abstract;

public abstract class BaseService
{
    internal const string SqlServiceUrlSection = "SqlServiceUrl";
    private protected readonly AeroLinkerCoreContext _context;
    private protected readonly IMapper _mapper;

    public BaseService(AeroLinkerCoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}