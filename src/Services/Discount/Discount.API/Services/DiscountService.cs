using Discount.Application.Commands.CreateDiscount;
using Discount.Application.Commands.DeleteDiscount;
using Discount.Application.Commands.UpdateDiscount;
using Discount.Application.Queries.GetDiscount;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly ISender _sender;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(ISender sender, ILogger<DiscountService> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var query = new GetDiscountQuery(request.ProductName);
        var response = await _sender
            .Send(query);

        _logger.LogInformation($"Discount is retrieved for thr product name: {response.ProductName} and amount: {response.Amount}");

        return response;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var command = new CreateDiscountCommand(
            request.Coupon.ProductName,
            request.Coupon.Description,
            request.Coupon.Amount
        );

        var response = await _sender
            .Send(command);

        _logger.LogInformation($"Discount is successfully created for thr product name: {response?.ProductName} and amount: {response?.Amount}");

        return response!;

    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var command = new UpdateDiscountCommand
        (
            request.Coupon.Id,
            request.Coupon.ProductName,
            request.Coupon.Description,
            request.Coupon.Amount
        );

        var response = await _sender.Send(command);

        _logger.LogInformation($"Discount is successfully updated Product Name: {response.ProductName}");

        return response;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var command = new DeleteDiscountCommand(request.ProductName);

        var deleted = await _sender.Send(command);

        var response = new DeleteDiscountResponse
        {
            Success = deleted
        };

        return response;
    }
}
