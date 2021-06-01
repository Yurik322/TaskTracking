using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    /// <summary>
    /// User entity model.
    /// </summary>
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Employee Employee { get; set; }
    }
}