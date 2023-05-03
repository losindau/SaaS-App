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
                            Phone = 123456789,
                            Email = "ueha@gmail.com",
                        },
                        new Company()
                        {
                            Name = "UEHB",
                            Address = "56 Nguyen Dinh Chieu",
                            Phone = 123456789,
                            Email = "uehb@gmail.com",
                        }
                    };
                if (!context.Companies.Any())
                {
                    context.Companies.AddRange(companies);
                    context.SaveChanges();
                }

                //Toolboxes
                var toolboxes = new List<Toolbox>()
                    {
                        new Toolbox()
                        {
                            CompanyID = 1
                        },
                        new Toolbox()
                        {
                            CompanyID = 1
                        },
                        new Toolbox()
                        {
                            CompanyID = 1
                        },
                        new Toolbox()
                        {
                            CompanyID = 1
                        },
                        new Toolbox()
                        {
                            CompanyID = 1
                        },
                        new Toolbox()
                        {
                            CompanyID = 2
                        },
                        new Toolbox()
                        {
                            CompanyID = 2
                        },
                        new Toolbox()
                        {
                            CompanyID = 2
                        },
                        new Toolbox()
                        {
                            CompanyID = 2
                        },
                        new Toolbox()
                        {
                            CompanyID = 2
                        },
                    };
                if (!context.Toolboxes.Any())
                {
                    context.AddRange(toolboxes);
                    context.SaveChanges();
                }

                //Trucks
                var trucks = new List<Truck>()
                    {
                        new Truck()
                        {
                            Model = "Ford F150",
                            LicensePlate = "ABC123",
                            ToolboxID = 1,
                            CompanyID = 1
                        },
                        new Truck()
                        {
                            Model = "Chevy Silverado",
                            LicensePlate = "DEF456",
                            ToolboxID = 2,
                            CompanyID = 1
                        },
                        new Truck()
                        {
                            Model = "GMC Sierra",
                            LicensePlate = "GHI789",
                            ToolboxID = 3,
                            CompanyID = 1
                        },
                        new Truck()
                        {
                            Model = "Dodge Ram",
                            LicensePlate = "JKL012",
                            ToolboxID = 4,
                            CompanyID = 1
                        },
                        new Truck()
                        {
                            Model = "Toyota Taconma",
                            LicensePlate = "MNO345",
                            ToolboxID = 5,
                            CompanyID = 1
                        },
                        new Truck()
                        {
                            Model = "Ford F150",
                            LicensePlate = "PQR678",
                            ToolboxID = 6,
                            CompanyID = 2
                        },
                        new Truck()
                        {
                            Model = "Chevy Silverado",
                            LicensePlate = "STU901",
                            ToolboxID = 7,
                            CompanyID = 2
                        },
                        new Truck()
                        {
                            Model = "GMC Sierra",
                            LicensePlate = "VWX234",
                            ToolboxID = 8,
                            CompanyID = 2
                        },
                        new Truck()
                        {
                            Model = "Dodge Ram",
                            LicensePlate = "YZA567",
                            ToolboxID = 9,
                            CompanyID = 2
                        },
                        new Truck()
                        {
                            Model = "Toyota Taconma",
                            ToolboxID = 10,
                            LicensePlate = "BCD890",
                            CompanyID = 2
                        }
                    };
                if (!context.Trucks.Any())
                {
                    context.AddRange(trucks);
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
                        CompanyID = 1
                    },
                    new StockItem()
                    {
                        Name = "Light Bulb",
                        Type = StockItemType.Lighting,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 1
                    },
                    new StockItem()
                    {
                        Name = "Switch",
                        Type = StockItemType.Switches,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 1
                    },
                    new StockItem()
                    {
                        Name = "Circuit Breaker",
                        Type = StockItemType.Electrical,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 1
                    },
                    new StockItem()
                    {
                        Name = "Extension Cord",
                        Type = StockItemType.Electrical,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 1
                    },
                    new StockItem()
                    {
                        Name = "Cable",
                        Type = StockItemType.Wiring,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 2
                    },
                    new StockItem()
                    {
                        Name = "Light Bulb",
                        Type = StockItemType.Lighting,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 2
                    },
                    new StockItem()
                    {
                        Name = "Switch",
                        Type = StockItemType.Switches,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 2
                    },
                    new StockItem()
                    {
                        Name = "Circuit Breaker",
                        Type = StockItemType.Electrical,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 2
                    },
                    new StockItem()
                    {
                        Name = "Extension Cord",
                        Type = StockItemType.Electrical,
                        Quantity = 500,
                        QuantityState = QuantityState.High,
                        CompanyID = 2
                    }
                };
                if (!context.StockItems.Any())
                {
                    context.StockItems.AddRange(stockitems);
                    context.SaveChanges();
                }

                //TruckStockItem
                var truckstockitem = new List<TruckStockItem>()
                {
                    new TruckStockItem()
                    {
                        TruckID = 1,
                        StockItemID = 1,
                        QuantityInTruck = 50,
                        CompanyID = 1
                    },
                    new TruckStockItem()
                    {
                        TruckID = 1,
                        StockItemID = 2,
                        QuantityInTruck = 50,
                        CompanyID = 1
                    },
                    new TruckStockItem()
                    {
                        TruckID = 1,
                        StockItemID = 3,
                        QuantityInTruck = 50,
                        CompanyID = 1
                    },
                    new TruckStockItem()
                    {
                        TruckID = 1,
                        StockItemID = 4,
                        QuantityInTruck = 50,
                        CompanyID = 1
                    },
                    new TruckStockItem()
                    {
                        TruckID = 1,
                        StockItemID = 5,
                        QuantityInTruck = 50,
                        CompanyID = 1
                    },
                    new TruckStockItem()
                    {
                        TruckID = 6,
                        StockItemID = 6,
                        QuantityInTruck = 50,
                        CompanyID = 2
                    },
                    new TruckStockItem()
                    {
                        TruckID = 6,
                        StockItemID = 7,
                        QuantityInTruck = 50,
                        CompanyID = 2
                    },
                    new TruckStockItem()
                    {
                        TruckID = 6,
                        StockItemID = 8,
                        QuantityInTruck = 50,
                        CompanyID = 2
                    },
                    new TruckStockItem()
                    {
                        TruckID = 6,
                        StockItemID = 9,
                        QuantityInTruck = 50,
                        CompanyID = 2
                    },
                    new TruckStockItem()
                    {
                        TruckID = 6,
                        StockItemID = 10,
                        QuantityInTruck = 50,
                        CompanyID = 2
                    }
                };
                if (!context.TruckStockItems.Any())
                {
                    context.TruckStockItems.AddRange(truckstockitem);
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
                            QualityState = QualityState.High,
                            CompanyID = 1,
                        },
                        new Equipment()
                        {
                            Name = "Pliers",
                            Quantity = 100,
                            Type = EquipmentType.HandTool,
                            QualityState = QualityState.High,
                            CompanyID = 1,
                        },
                        new Equipment()
                        {
                            Name = "Voltage Tester",
                            Quantity = 100,
                            Type = EquipmentType.ElectricalTool,
                            QualityState = QualityState.High,
                            CompanyID = 1,
                        },
                        new Equipment()
                        {
                            Name = "Drill",
                            Quantity = 100,
                            Type = EquipmentType.PowerTool,
                            QualityState = QualityState.High,
                            CompanyID = 1,
                        },
                        new Equipment()
                        {
                            Name = "Tape Measure",
                            Quantity = 100,
                            Type = EquipmentType.MeasuringTool,
                            QualityState = QualityState.High,
                            CompanyID = 1,
                        },
                        new Equipment()
                        {
                            Name = "Screwdriver",
                            Quantity = 100,
                            Type = EquipmentType.HandTool,
                            QualityState = QualityState.High,
                            CompanyID = 2,
                        },
                        new Equipment()
                        {
                            Name = "Pliers",
                            Quantity = 100,
                            Type = EquipmentType.HandTool,
                            QualityState = QualityState.High,
                            CompanyID = 2,
                        },
                        new Equipment()
                        {
                            Name = "Voltage Tester",
                            Quantity = 100,
                            Type = EquipmentType.ElectricalTool,
                            QualityState = QualityState.High,
                            CompanyID = 2,
                        },
                        new Equipment()
                        {
                            Name = "Drill",
                            Quantity = 100,
                            Type = EquipmentType.PowerTool,
                            QualityState = QualityState.High,
                            CompanyID = 2,
                        },
                        new Equipment()
                        {
                            Name = "Tape Measure",
                            Quantity = 100,
                            Type = EquipmentType.MeasuringTool,
                            QualityState = QualityState.High,
                            CompanyID = 2,
                        },
                    };
                if (!context.Equipment.Any())
                {
                    context.Equipment.AddRange(equipment);
                    context.SaveChanges();
                }

                //ToolboxEquipment
                var toolboxequipment = new List<ToolboxEquipment>()
                {
                    new ToolboxEquipment()
                    {
                        ToolboxID = 1,
                        EquipmentID = 1,
                        QuantityInToolbox = 5,
                        CompanyID = 1
                    },
                    new ToolboxEquipment()
                    {
                        ToolboxID = 1,
                        EquipmentID = 2,
                        QuantityInToolbox = 5,
                        CompanyID = 1
                    },
                    new ToolboxEquipment()
                    {
                        ToolboxID = 1,
                        EquipmentID = 3,
                        QuantityInToolbox = 5,
                        CompanyID = 1
                    },
                    new ToolboxEquipment()
                    {
                        ToolboxID = 1,
                        EquipmentID = 4,
                        QuantityInToolbox = 5,
                        CompanyID = 1
                    },
                    new ToolboxEquipment()
                    {
                        ToolboxID = 1,
                        EquipmentID = 5,
                        QuantityInToolbox = 5,
                        CompanyID = 1
                    },
                    new ToolboxEquipment()
                    {
                        ToolboxID = 6,
                        EquipmentID = 6,
                        QuantityInToolbox = 5,
                        CompanyID = 2
                    },
                    new ToolboxEquipment()
                    {
                        ToolboxID = 6,
                        EquipmentID = 7,
                        QuantityInToolbox = 5,
                        CompanyID = 2
                    },
                    new ToolboxEquipment()
                    {
                        ToolboxID = 6,
                        EquipmentID = 8,
                        QuantityInToolbox = 5,
                        CompanyID = 2
                    },
                    new ToolboxEquipment()
                    {
                        ToolboxID = 6,
                        EquipmentID = 9,
                        QuantityInToolbox = 5,
                        CompanyID = 2
                    },
                    new ToolboxEquipment()
                    {
                        ToolboxID = 6,
                        EquipmentID = 10,
                        QuantityInToolbox = 5,
                        CompanyID = 2
                    }
                };
                if (!context.ToolboxEquipment.Any())
                {
                    context.AddRange(toolboxequipment);
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
                        CompanyID = 1
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
                        CompanyID = 2
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
                        CompanyID = 1
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
                        CompanyID = 2
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
                        PhoneNumber = "0898237581",
                        PhoneNumberConfirmed = true,
                        Email = staffAEmail,
                        EmailConfirmed = true,
                        CompanyID = 1
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
                        PhoneNumber = "0898237581",
                        PhoneNumberConfirmed = true,
                        Email = staffBEmail,
                        EmailConfirmed = true,
                        CompanyID = 1
                    };
                    await userManager.CreateAsync(newAppUser, "staffB@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Staff);
                }
            }
        }
    }
}
