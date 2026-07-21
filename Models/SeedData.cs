using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Utlanssystem.Data;
using System;
using System.Linq;

namespace Utlanssystem.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UtlanssystemContext(
                serviceProvider.GetRequiredService<DbContextOptions<UtlanssystemContext>>()))
            {
                // Seed studenter, kun hvis tabellen er tom
                if (!context.Students.Any())
                {
                    string[] firstNames = new[]
                    {
                        "Ola", "Kari", "Per", "Anne", "Lars", "Ingrid", "Erik", "Maria",
                        "Jonas", "Emma", "Henrik", "Sofie", "Andreas", "Nora", "Mathias",
                        "Ida", "Sander", "Julie", "Kristian", "Thea", "Magnus", "Frida",
                        "Fredrik", "Sara", "Sondre", "Amalie", "Marius", "Vilde", "Tobias", "Live"
                    };

                    string[] lastNames = new[]
                    {
                        "Hansen", "Johansen", "Olsen", "Larsen", "Andersen", "Pedersen",
                        "Nilsen", "Kristiansen", "Jensen", "Karlsen", "Johnsen", "Pettersen",
                        "Eriksen", "Berg", "Haugen", "Hagen", "Johannessen", "Andreassen",
                        "Jacobsen", "Dahl", "Halvorsen", "Henriksen", "Lund", "Sørensen",
                        "Moen", "Iversen", "Solberg", "Bakke", "Strand", "Nygård"
                    };

                    var random = new Random();

                    for (int i = 1; i <= 30; i++)
                    {
                        context.Students.Add(new Student
                        {
                            FirstName = firstNames[random.Next(firstNames.Length)],
                            LastName = lastNames[random.Next(lastNames.Length)],
                            StudentNumber = (1000 + i).ToString(),
                            HasActiveLoan = false
                        });
                    }
                }

                // Seed enheter - bredt utvalg av ting man kan låne
                if (!context.Devices.Any())
                {
                    context.Devices.AddRange(
                        // Elektronikk fra oppgaveteksten
                        new Device { Name = "Raspberry Pi 4", DeviceType = "SBC", ModelName = "RPi 4 Model B", Specifications = "4GB RAM, Quad-core Cortex-A72", IsAvailable = true },
                        new Device { Name = "STM32 Discovery Board", DeviceType = "Microcontroller", ModelName = "STM32F407", Specifications = "ARM Cortex-M4, 168 MHz", IsAvailable = true },
                        new Device { Name = "USB Ethernet Adapter", DeviceType = "Network adapter", ModelName = "AWUS036AXML", Specifications = "USB 3.0 to Gigabit Ethernet", IsAvailable = true },

                        // Bøker
                        new Device { Name = "Clean Code", DeviceType = "Book", ModelName = "Robert C. Martin", Specifications = "464 sider, programvareutvikling", IsAvailable = true },
                        new Device { Name = "Design Patterns", DeviceType = "Book", ModelName = "Gang of Four", Specifications = "395 sider, softwarearkitektur", IsAvailable = true },
                        new Device { Name = "The Pragmatic Programmer", DeviceType = "Book", ModelName = "Hunt & Thomas", Specifications = "352 sider, beste praksis", IsAvailable = true },

                        // Strøm og kabler
                        new Device { Name = "USB-C Lader 65W", DeviceType = "Charger", ModelName = "Anker PowerPort III", Specifications = "65W, GaN-teknologi", IsAvailable = true },
                        new Device { Name = "USB-C Lader 30W", DeviceType = "Charger", ModelName = "Apple 30W", Specifications = "30W, kompakt", IsAvailable = true },
                        new Device { Name = "HDMI-kabel 2m", DeviceType = "Cable", ModelName = "Generic HDMI 2.1", Specifications = "Støtter 4K@120Hz", IsAvailable = true },
                        new Device { Name = "USB-C til USB-A kabel", DeviceType = "Cable", ModelName = "Generic 1m", Specifications = "Data + lading", IsAvailable = true },

                        // Verktøy
                        new Device { Name = "Loddebolt-sett", DeviceType = "Tool", ModelName = "Weller WE1010", Specifications = "70W, digital temperaturkontroll", IsAvailable = true },
                        new Device { Name = "Multimeter", DeviceType = "Tool", ModelName = "Fluke 117", Specifications = "True-RMS, auto-range", IsAvailable = true },
                        new Device { Name = "Skrutrekkersett", DeviceType = "Tool", ModelName = "iFixit Pro Tech Toolkit", Specifications = "64-delers presisjonssett", IsAvailable = true },

                        // Skjermer og periferi
                        new Device { Name = "Bærbar skjerm 15\"", DeviceType = "Monitor", ModelName = "ASUS ZenScreen", Specifications = "Full HD, USB-C tilkobling", IsAvailable = true },
                        new Device { Name = "Trådløs mus", DeviceType = "Peripheral", ModelName = "Logitech MX Master 3", Specifications = "Bluetooth, oppladbar", IsAvailable = true },
                        new Device { Name = "Mekanisk tastatur", DeviceType = "Peripheral", ModelName = "Keychron K2", Specifications = "Bluetooth/USB-C, brune brytere", IsAvailable = true },

                        // Nettverksutstyr
                        new Device { Name = "USB WiFi-adapter", DeviceType = "Network adapter", ModelName = "TP-Link AC600", Specifications = "Dual-band, USB 2.0", IsAvailable = true },
                        new Device { Name = "Bluetooth-adapter", DeviceType = "Network adapter", ModelName = "ASUS USB-BT500", Specifications = "Bluetooth 5.0", IsAvailable = true }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}