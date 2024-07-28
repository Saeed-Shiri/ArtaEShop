using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.GetAllTypes;
public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponse>>
{
    private readonly ITypeRepository _repository;
    private readonly IMapper _mapper;

    public GetAllTypesHandler(ITypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typeList = await _repository.GetAllTypes();
        var resonse = _mapper.Map<IList<TypeResponse>>(typeList);

        return resonse;
    }
}
