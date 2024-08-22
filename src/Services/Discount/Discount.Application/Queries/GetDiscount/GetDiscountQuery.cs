using Discount.Grpc.Protos;
using ErrorOr;
using MediatR;

namespace Discount.Application.Queries.GetDiscount;
public sealed record GetDiscountQuery(string ProductName) : IRequest<CouponModel>;