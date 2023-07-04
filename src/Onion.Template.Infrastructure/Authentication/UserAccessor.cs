using Microsoft.AspNetCore.Http;
using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Domain.Commom;

namespace Onion.Template.Infrastructure.Authentication;

[Injection(DI.Scoped)]
public class UserAccessor : IUserAccessor
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UserAccessor(IHttpContextAccessor httpContextAccessor)
		=> _httpContextAccessor = httpContextAccessor;

	public Guid UserId => CurrentUser.UserId;

	public CurrentUser CurrentUser => new CurrentUser(_httpContextAccessor.HttpContext.User.Claims);
}
