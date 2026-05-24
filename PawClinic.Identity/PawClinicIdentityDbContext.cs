using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PawClinic.Identity.Models;

namespace PawClinic.Identity
{
    public class PawClinicIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public PawClinicIdentityDbContext(DbContextOptions<PawClinicIdentityDbContext> options)
            : base(options)
        {
        }
    }
}
