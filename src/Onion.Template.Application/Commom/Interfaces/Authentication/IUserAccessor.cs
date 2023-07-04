using Onion.Template.Domain.Commom;

namespace Onion.Template.Application.Commom.Interfaces.Authentication;

public interface IUserAccessor
{
	Guid UserId { get; }
	CurrentUser CurrentUser { get; }
}
