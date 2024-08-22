using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands.CreateDiscount;
public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
{
    private readonly IDiscountRepository _repository;

    public CreateDiscountCommandHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = DiscountMapper.Mapper.Map<Coupon>(request);
        await _repository
            .CreateDiscount(coupon);

        var response = DiscountMapper.Mapper.Map<CouponModel>(coupon);
        return response;
    }
}
