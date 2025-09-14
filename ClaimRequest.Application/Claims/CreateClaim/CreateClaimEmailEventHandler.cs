using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Common.DTO;
using DocumentFormat.OpenXml.InkML;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Application.Claims.Redis;

namespace ClaimRequest.Application.Claims.CreateClaim
{
    public class CreateClaimEmailEventHander(
        ClaimEventPublisher publisher) : INotificationHandler<ClaimCreatedDomainEvent>
    {
        public async Task Handle(ClaimCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await publisher.PublishClaimCreatedEventAsync(notification);
        }
    }
}
