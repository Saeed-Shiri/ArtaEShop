using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Ordering.Commands.DeleteOrder;
public sealed record DeleteOrderCommand(int Id) : IRequest;
