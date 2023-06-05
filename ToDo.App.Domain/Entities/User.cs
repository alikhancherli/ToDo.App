using Microsoft.AspNetCore.Identity;

namespace ToDo.App.Domain.Entities
{
    public sealed class User : IdentityUser<int>
    {
        public string FullName { get; private set; } = default!;
        public bool IsActive { get; private set; }

        public static User Create(string firstname, string lastname, string username, string email)
        {
            return new User()
            {
                Email = email,
                FullName = $"{firstname}, {lastname}",
                UserName = username,
                IsActive = true
            };
        }

        public void Edit(string firstname, string lastname, string email)
        {
            FullName = $"{firstname}, {lastname}";
            Email = email;
        }
    }
}
