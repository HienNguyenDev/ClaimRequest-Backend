using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.EmailTemplates.UpdateEmailTemplate;

public class UpdateEmailTemplateCommandHandler(IDbContext context) : ICommandHandler<UpdateEmailTemplateCommand, UpdateEmailTemplateResponse>
{
    public async Task<Result<UpdateEmailTemplateResponse>> Handle(UpdateEmailTemplateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var email = await context.EmailTemplates.FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

            if (email == null)
            {
                return Result.Failure<UpdateEmailTemplateResponse>(new Error
                (
                    "400",
                    "Email template not found",
                    ErrorType.NotFound
                ));
            }

            email.Content = command.Content ?? email.Content;
            email.MainContent = command.MainContent ?? email.MainContent;
            email.Header = command.Header ?? email.Header;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(new UpdateEmailTemplateResponse
            {
                Id = email.Id,
                Content = email.Content,
                MainContent = email.MainContent,
                Header = email.Header,
            });
        }
        catch (Exception ex)
        {
            return Result.Failure<UpdateEmailTemplateResponse>(new Error
            (
                "400",
                "Unexpected exception",
                ErrorType.Failure
            ));
            
        }
    }
}