using Application.Contract;
using Domain.ConfigModel;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtConfig _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtConfig> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var accountId = jwtUtils.ValidateJwtToken(token);

            if (accountId is not null && accountId > 0)
            {
                var query = "SELECT * FROM " + nameof(People) + " WHERE Id = " + accountId;
                context.Items["Account"] = unitOfWork.PeopleRepository.Get(query, default).Id;
            }

            await _next(context);
        }
    }
}
