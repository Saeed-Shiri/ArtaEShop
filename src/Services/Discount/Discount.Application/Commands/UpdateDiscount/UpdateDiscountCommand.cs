
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands.UpdateDiscount;
public sealed record UpdateDiscountCommand(int Id, string ProductName, string Description, int Amount) : IRequest<CouponModel>;
