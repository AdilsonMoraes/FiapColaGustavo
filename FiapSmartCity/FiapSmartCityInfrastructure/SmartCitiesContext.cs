using FiapSmartCityDomain.Authentication;
using FiapSmartCityDomain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FiapSmartCityInfrastructure
{
    public class SmartCitiesContext : IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SmartCitiesContext(DbContextOptions<SmartCitiesContext> options, IHttpContextAccessor httpContextAccessor) 
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        //public DbSet<EntityName> EntityName { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            if (_httpContextAccessor?.HttpContext?.User != null)
            {
                var userName = Users.FirstOrDefault(u => u.Id == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)?.UserName;
                ChangeTracker.Entries().ToList().ForEach(entity =>
                {
                    if (entity.Entity is AuditableEntity auditableEntity)
                    {
                        if (entity.State == EntityState.Added)
                        {
                            auditableEntity.CreatedBy = userName;
                            auditableEntity.CreatedDateTime = DateTime.Now;
                        }
                        else if (entity.State == EntityState.Modified)
                        {
                            auditableEntity.UpdatedBy = userName;
                            auditableEntity.UpdatedDateTime = DateTime.Now;
                        }
                    }
                });
            }
            return base.SaveChanges();
        }

    }
}
