using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
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

    public GetAllTypesHandler(ITypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typeList = await _repository.GetAllTypes();
        var resonse = ProductMapper.Mapper.Map<IList<TypeResponse>>(typeList);

        return resonse;
    }
}
