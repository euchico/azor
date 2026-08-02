using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class AzorDbContext(DbContextOptions<AzorDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
}
