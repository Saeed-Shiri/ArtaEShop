
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands.UpdateDiscount;
public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
{
    private readonly IDiscountRepository _repository;

    public UpdateDiscountCommandHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = DiscountMapper.Mapper.Map<Coupon>(request);

        await _repository
            .UpdateDiscount(coupon);

        var response = DiscountMapper.Mapper.Map<CouponModel>(coupon);
        return response;
    }
}
