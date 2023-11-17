using Microsoft.AspNetCore.Identity;

namespace Coffee.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegisteredIn { get; set; } = DateTime.UtcNow;
    }
}
