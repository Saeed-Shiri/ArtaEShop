using Discount.Application.Mappers;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using ErrorOr;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Queries.GetDiscount;
public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
{
    private readonly IDiscountRepository _repository;

    public GetDiscountQueryHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = await _repository
            .GetDiscount(request.ProductName);

        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount with the product name = {request.ProductName} not found"));
        }
        var response = DiscountMapper.Mapper.Map<CouponModel>(coupon);
        return response;
    }
}
