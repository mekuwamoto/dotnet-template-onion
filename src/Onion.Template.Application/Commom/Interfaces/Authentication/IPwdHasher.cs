namespace Onion.Template.Application.Commom.Interfaces.Authentication;

public interface IPwdHasher
{
	string ComputeHash(string password, string salt, string pepper, int iteration);
	string GenerateSalt();
}
