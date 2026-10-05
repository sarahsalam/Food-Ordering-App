using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Security;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Data.Seeders;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roles = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var users = services.GetRequiredService<UserManager<ApplicationUser>>();
        var context = services.GetRequiredService<AppDbContext>();
        var configuration = services.GetRequiredService<IConfiguration>();

        var initialization = await context.RbacInitializations.SingleOrDefaultAsync();
        if (initialization != null)
        {
            if (initialization.DefaultRegistrationRoleId == null)
            {
                var defaultRole = await roles.FindByNameAsync(RoleNames.Customer);
                if (defaultRole != null)
                {
                    initialization.DefaultRegistrationRoleId = defaultRole.Id;
                    await context.SaveChangesAsync();
                }
            }
            return;
        }

        var newRoles = new HashSet<string>();
        var newPermissions = new HashSet<string>();

        foreach (var name in RoleNames.All)
        {
            if (!await roles.RoleExistsAsync(name))
            {
                Check(await roles.CreateAsync(new IdentityRole<Guid>(name) { Id = Guid.NewGuid() }));
                newRoles.Add(name);
            }
        }

        foreach (var name in PermissionNames.All)
        {
            if (!await context.Permissions.AnyAsync(permission => permission.Name == name))
            {
                context.Permissions.Add(new Permission { Id = Guid.NewGuid(), Name = name });
                newPermissions.Add(name);
            }
        }
        await context.SaveChangesAsync();

        var grants = RolePermissions.Defaults;

        foreach (var grant in grants)
        {
            var role = await roles.FindByNameAsync(grant.Key);
            foreach (var permissionName in grant.Value)
            {
                if (!newRoles.Contains(grant.Key) && !newPermissions.Contains(permissionName))
                {
                    continue;
                }
                var permission = await context.Permissions.SingleAsync(item => item.Name == permissionName);
                if (!await context.RolePermissions.AnyAsync(item =>
                    item.RoleId == role!.Id && item.PermissionId == permission.Id))
                {
                    context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = role!.Id,
                        PermissionId = permission.Id
                    });
                }
            }
        }
        await context.SaveChangesAsync();

        var registrationRole = await roles.FindByNameAsync(RoleNames.Customer);
        var email = configuration["SeedAdmin:Email"];
        var password = configuration["SeedAdmin:Password"];
        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password))
        {
            context.RbacInitializations.Add(new RbacInitialization
            {
                Id = 1, DefaultRegistrationRoleId = registrationRole!.Id
            });
            await context.SaveChangesAsync();
            return;
        }
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException("Set both SeedAdmin:Email and SeedAdmin:Password.");
        }

        var admin = await users.FindByEmailAsync(email);
        if (admin == null)
        {
            admin = new ApplicationUser { UserName = email, Email = email, FullName = "Administrator" };
            Check(await users.CreateAsync(admin, password));
        }
        if (!await users.IsInRoleAsync(admin, RoleNames.Admin))
        {
            Check(await users.AddToRoleAsync(admin, RoleNames.Admin));
        }
        context.RbacInitializations.Add(new RbacInitialization
        {
            Id = 1, DefaultRegistrationRoleId = registrationRole!.Id
        });
        await context.SaveChangesAsync();
    }

    private static void Check(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(", ", result.Errors.Select(error => error.Description)));
        }
    }
}
