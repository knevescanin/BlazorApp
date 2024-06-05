using BlazorApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BlazorApp.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ICollection<ImageFile> ImageFiles { get; set; }
    }
}