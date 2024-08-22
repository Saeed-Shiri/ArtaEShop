

using MediatR;

namespace Discount.Application.Commands.DeleteDiscount;
public sealed record DeleteDiscountCommand(string ProductName) : IRequest<bool>;