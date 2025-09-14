using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Users.InActiveUser
{
    public class InActiveUserCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<InActiveUserCommand>
    {
        public async Task<Result> Handle(InActiveUserCommand request, CancellationToken cancellationToken)
        {
            // Lấy admin từ database
            var admin = await context.Users.FirstOrDefaultAsync(u => u.Id == userContext.UserId, cancellationToken);

            // Kiểm tra admin có tồn tại không
            if (admin == null || admin.Role != UserRole.Admin)
            {
                return Result.Failure(UserErrors.NotAdmin);
            }

            // Lấy user cần vô hiệu hóa
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            // Kiểm tra xem người dùng có tồn tại không
            if (user == null)
            {
                return Result.Failure(UserErrors.NotFoundByEmail); // Tài khoản không tồn tại
            }

            // Đánh dấu người dùng là không hoạt động
            user.Status = UserStatus.InActive;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(); // Trả về kết quả thành công
        }

    }
}
