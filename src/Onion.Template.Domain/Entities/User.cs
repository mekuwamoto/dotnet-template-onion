namespace Onion.Template.Domain.Entities;

public class User : BaseEntity
{
	public string FirstName { get; private set; } = null!;
	public string LastName { get; private set; } = null!;
	public string Email { get; private set; } = null!;
	public string Username { get; private set; } = null!;
	public string PasswordSalt { get; private set; } = null!;
	public string PasswordHash { get; private set; } = null!;
	public List<Todo> TodoList { get; private set; } = new();

	public User(string firstName, string lastName, string email, string username, string passwordSalt, string passwordHash)
	{
		FirstName = firstName;
		LastName = lastName;
		Email = email;
		Username = username;
		PasswordSalt = passwordSalt;
		PasswordHash = passwordHash;
	}
}
