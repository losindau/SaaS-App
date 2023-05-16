using InventoryManagementApp.Data.Enum;
using InventoryManagementApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                context.Database.EnsureCreated();

                //Companies
                var companies = new List<Company>()
                    {
                        new Company()
                        {
                            Name = "UEHA",
                            Address = "56 Nguyen Dinh Chieu",
                            PhoneNumber = "123456789",
                            Email = "ueha@gmail.com",
                            isDeleted = false
                        },
                        new Company()
                        {
                            Name = "UEHB",
                            Address = "56 Nguyen Dinh Chieu",
                            PhoneNumber = "123456789",
                            Email = "uehb@gmail.com",
                            isDeleted = false
                        }
                    };
                if (!context.Companies.Any())
                {
                    context.Companies.AddRange(companies);
                    context.SaveChanges();
                }

                //StockItems
                var stockitems = new List<StockItem>()
                {
                    new StockItem()
                    {
                        Name = "Cable",
                        Type = StockItemType.Wiring,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 1,
                        isDeleted = false
                    },
                    new StockItem()
                    {
                        Name = "Light Bulb",
                        Type = StockItemType.Lighting,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 1,
                        isDeleted = false
                    },
                    new StockItem()
                    {
                        Name = "Switch",
                        Type = StockItemType.Switches,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 1,
                        isDeleted = false
                    },
                    new StockItem()
                    {
                        Name = "Circuit Breaker",
                        Type = StockItemType.Electrical,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 1,
                        isDeleted = false
                    },
                    new StockItem()
                    {
                        Name = "Extension Cord",
                        Type = StockItemType.Electrical,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 1,
                        isDeleted = false
                    },
                    new StockItem()
                    {
                        Name = "Cable",
                        Type = StockItemType.Wiring,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 2,
                        isDeleted = false
                    },
                    new StockItem()
                    {
                        Name = "Light Bulb",
                        Type = StockItemType.Lighting,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 2,
                        isDeleted = false
                    },
                    new StockItem()
                    {
                        Name = "Switch",
                        Type = StockItemType.Switches,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 2,
                        isDeleted = false
                    },
                    new StockItem()
                    {
                        Name = "Circuit Breaker",
                        Type = StockItemType.Electrical,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 2,
                        isDeleted = false
                    },
                    new StockItem()
                    {
                        Name = "Extension Cord",
                        Type = StockItemType.Electrical,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 2,
                        isDeleted = false
                    }
                };
                if (!context.StockItems.Any())
                {
                    context.StockItems.AddRange(stockitems);
                    context.SaveChanges();
                }

                //Equipment
                var equipment = new List<Equipment>()
                    {
                        new Equipment()
                        {
                            Name = "Screwdriver",
                            Quantity = 100,
                            Type = EquipmentType.HandTool,
                            QuantityState = QuantityState.High,
                            CompanyID = 1,
                            isDeleted = false
                        },
                        new Equipment()
                        {
                            Name = "Pliers",
                            Quantity = 100,
                            Type = EquipmentType.HandTool,
                            QuantityState = QuantityState.High,
                            CompanyID = 1,
                            isDeleted = false
                        },
                        new Equipment()
                        {
                            Name = "Voltage Tester",
                            Quantity = 100,
                            Type = EquipmentType.ElectricalTool,
                            QuantityState = QuantityState.High,
                            CompanyID = 1,
                            isDeleted = false
                        },
                        new Equipment()
                        {
                            Name = "Drill",
                            Quantity = 100,
                            Type = EquipmentType.PowerTool,
                            QuantityState = QuantityState.High,
                            CompanyID = 1,
                            isDeleted = false
                        },
                        new Equipment()
                        {
                            Name = "Tape Measure",
                            Quantity = 100,
                            Type = EquipmentType.MeasuringTool,
                            QuantityState = QuantityState.High,
                            CompanyID = 1,
                            isDeleted = false
                        },
                        new Equipment()
                        {
                            Name = "Screwdriver",
                            Quantity = 100,
                            Type = EquipmentType.HandTool,
                            QuantityState = QuantityState.High,
                            CompanyID = 2,
                            isDeleted = false
                        },
                        new Equipment()
                        {
                            Name = "Pliers",
                            Quantity = 100,
                            Type = EquipmentType.HandTool,
                            QuantityState = QuantityState.High,
                            CompanyID = 2,
                            isDeleted = false
                        },
                        new Equipment()
                        {
                            Name = "Voltage Tester",
                            Quantity = 100,
                            Type = EquipmentType.ElectricalTool,
                            QuantityState = QuantityState.High,
                            CompanyID = 2,
                            isDeleted = false
                        },
                        new Equipment()
                        {
                            Name = "Drill",
                            Quantity = 100,
                            Type = EquipmentType.PowerTool,
                            QuantityState = QuantityState.High,
                            CompanyID = 2,
                            isDeleted = false
                        },
                        new Equipment()
                        {
                            Name = "Tape Measure",
                            Quantity = 100,
                            Type = EquipmentType.MeasuringTool,
                            QuantityState = QuantityState.High,
                            CompanyID = 2,
                            isDeleted = false
                        },
                    };
                if (!context.Equipment.Any())
                {
                    context.Equipment.AddRange(equipment);
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
                if (!await roleManager.RoleExistsAsync(UserRoles.Staff))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Staff));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                string uehAEmail = "ueha@gmail.com";
                var uehAUser = await userManager.FindByEmailAsync(uehAEmail);
                if (uehAUser == null)
                {
                    var newCompanyUser = new AppUser()
                    {
                        UserName = uehAEmail,
                        Email = uehAEmail,
                        EmailConfirmed = true,
                        CompanyID = 1,
                        isDeleted = false
                    };
                    await userManager.CreateAsync(newCompanyUser, "uehA@1234?");
                    await userManager.AddToRoleAsync(newCompanyUser, UserRoles.Admin);
                }

                string uehBEmail = "uehb@gmail.com";
                var uehBUser = await userManager.FindByEmailAsync(uehBEmail);
                if (uehBUser == null)
                {
                    var newCompanyUser = new AppUser()
                    {
                        UserName = uehBEmail,
                        Email = uehBEmail,
                        EmailConfirmed = true,
                        CompanyID = 2,
                        isDeleted = false
                    };
                    await userManager.CreateAsync(newCompanyUser, "uehB@1234?");
                    await userManager.AddToRoleAsync(newCompanyUser, UserRoles.Admin);
                }

                string managerAEmail = "managera@gmail.com";
                var managerAUser = await userManager.FindByEmailAsync(managerAEmail);
                if (managerAUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = managerAEmail,
                        FirstName = "Steven",
                        LastName = "Gerrard",
                        PhoneNumber = "0898237581",
                        PhoneNumberConfirmed = true,
                        Email = managerAEmail,
                        EmailConfirmed = true,
                        CompanyID = 1,
                        isDeleted = false
                    };
                    await userManager.CreateAsync(newAppUser, "managerA@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Manager);
                }

                string managerBEmail = "managerb@gmail.com";
                var managerBUser = await userManager.FindByEmailAsync(managerBEmail);
                if (managerBUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = managerBEmail,
                        FirstName = "Nunez",
                        LastName = "Darwin",
                        PhoneNumber = "0898237581",
                        PhoneNumberConfirmed = true,
                        Email = managerBEmail,
                        EmailConfirmed = true,
                        CompanyID = 2,
                        isDeleted = false
                    };
                    await userManager.CreateAsync(newAppUser, "managerB@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Manager);
                }

                string staffAEmail = "staffa@gmail.com";
                var staffAUser = await userManager.FindByEmailAsync(staffAEmail);
                if (staffAUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = staffAEmail,
                        FirstName = "Salah",
                        LastName = "Mohamed",
                        PhoneNumber = "0946777827",
                        PhoneNumberConfirmed = true,
                        Email = staffAEmail,
                        EmailConfirmed = true,
                        CompanyID = 1,
                        isDeleted = false
                    };
                    await userManager.CreateAsync(newAppUser, "staffA@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Staff);
                }

                string staffBEmail = "staffb@gmail.com";
                var staffBUser = await userManager.FindByEmailAsync(staffBEmail);
                if (staffBUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = staffBEmail,
                        FirstName = "Gakpo",
                        LastName = "Cody",
                        PhoneNumber = "0946777827",
                        PhoneNumberConfirmed = true,
                        Email = staffBEmail,
                        EmailConfirmed = true,
                        CompanyID = 2,
                        isDeleted = false
                    };
                    await userManager.CreateAsync(newAppUser, "staffB@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Staff);
                }

                string staffCEmail = "staffc@gmail.com";
                var staffCUser = await userManager.FindByEmailAsync(staffCEmail);
                if (staffCUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = staffCEmail,
                        FirstName = "Boris",
                        LastName = "Nguyen",
                        PhoneNumber = "0946777827",
                        PhoneNumberConfirmed = true,
                        Email = staffCEmail,
                        EmailConfirmed = true,
                        CompanyID = 1,
                        isDeleted = false
                    };
                    await userManager.CreateAsync(newAppUser, "staffC@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Staff);
                }

                string staffDEmail = "staffd@gmail.com";
                var staffDUser = await userManager.FindByEmailAsync(staffDEmail);
                if (staffDUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = staffDEmail,
                        FirstName = "Bao",
                        LastName = "Nguyen",
                        PhoneNumber = "0946777827",
                        PhoneNumberConfirmed = true,
                        Email = staffDEmail,
                        EmailConfirmed = true,
                        CompanyID = 1,
                        isDeleted = false
                    };
                    await userManager.CreateAsync(newAppUser, "staffD@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Staff);
                }

                string staffEEmail = "staffe@gmail.com";
                var staffEUser = await userManager.FindByEmailAsync(staffEEmail);
                if (staffEUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = staffEEmail,
                        FirstName = "Dau",
                        LastName = "Losin",
                        PhoneNumber = "0946777827",
                        PhoneNumberConfirmed = true,
                        Email = staffEEmail,
                        EmailConfirmed = true,
                        CompanyID = 1,
                        isDeleted = false
                    };
                    await userManager.CreateAsync(newAppUser, "staffE@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Staff);
                }

                string staffFEmail = "stafff@gmail.com";
                var staffFUser = await userManager.FindByEmailAsync(staffFEmail);
                if (staffFUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = staffFEmail,
                        FirstName = "Dat",
                        LastName = "Doan",
                        PhoneNumber = "0946777827",
                        PhoneNumberConfirmed = true,
                        Email = staffFEmail,
                        EmailConfirmed = true,
                        CompanyID = 1,
                        isDeleted = false
                    };
                    await userManager.CreateAsync(newAppUser, "staffF@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Staff);
                }
            }
        }
    }
}
