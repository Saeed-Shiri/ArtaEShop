//using Discount.Grpc.Protos;

using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Basket.Application.GrpcServices;
public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;
    private readonly ILogger<DiscountGrpcService> _logger;
    private readonly IConfiguration _configuration;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient, ILogger<DiscountGrpcService> logger, IConfiguration configuration)
    {
        _discountProtoServiceClient = discountProtoServiceClient;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest()
        {
            ProductName = productName,
        };
        _logger.LogInformation($"Try to connecto to Discount grpc service:{_configuration["GrpcSettings:DiscountUrl"]} from Basket service");

        return await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
    }
}
