

using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands.CreateDiscount;
public sealed record CreateDiscountCommand(string ProductName, string Description, int Amount) : IRequest<CouponModel>;
