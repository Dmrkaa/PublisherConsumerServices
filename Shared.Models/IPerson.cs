namespace Shared.Models
{
    public interface IUser
    {
        string Email { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        string Name { get; set; }
    }
}