using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Common.DTO;
using ClaimRequest.Domain.Users;
using DocumentFormat.OpenXml.InkML;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Application.ClaimOverTimes.Redis;

namespace ClaimRequest.Application.EmailEventHandler.ClaimsOTStatusChanged
{
    public class ClaimsOTStatusChangedEventHandler(OvertimeEventPublisher publisher) 
        : INotificationHandler<OverTimeEffortStatusChangedEvent>
    {
        public async Task Handle(OverTimeEffortStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            await publisher.PublishOvertimeEffortStatusChangedEventAsync(notification);
            
        }
    }
}
