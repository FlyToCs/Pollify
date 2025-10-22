
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pollify.Application;
using Pollify.Application.Contracts;
using Pollify.Domain.DTOs;
using Pollify.Domain.Entities.UserAgg;
using ServiceRegistry;
using Spectre.Console;
Console.OutputEncoding = System.Text.Encoding.UTF8;



// AnsiConsole.Clear();
// AnsiConsole.Write(
//     new FigletText("🚀 MyApp 🚀")
//         .Centered()
//         .Color(Color.Aqua)
// );
//
// Console.WriteLine();
// AnsiConsole.Write(
//     new Rule("[yellow]Starting Application[/]")
//         .Centered()
//         .RuleStyle("grey")
// );
//
//
// await AnsiConsole.Progress()
//     .AutoClear(false)
//     .HideCompleted(false)
//     .Columns(new ProgressColumn[]
//     {
//         new TaskDescriptionColumn(), 
//         new ProgressBarColumn(),     
//         new PercentageColumn(),    
//         new SpinnerColumn(Spinner.Known.Dots12) 
//     })
//     .StartAsync(async ctx =>
//     {
//         var dbTask = ctx.AddTask("[cyan]Connecting to database[/]");
//         var appTask = ctx.AddTask("[yellow]Loading modules[/]");
//         var uiTask = ctx.AddTask("[green]Building UI[/]");
//
//         while (!ctx.IsFinished)
//         {
//             await Task.Delay(150);
//
//             dbTask.Increment(4.5);
//             appTask.Increment(2.5);
//             uiTask.Increment(3.5);
//         }
//     });
//
//
// AnsiConsole.Write(
//     new FigletText("✔ Application Started!")
//         .Centered()
//         .Color(Color.Gold1)
// );
//
// AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
// Console.ReadKey(true);

var host = CreateHostBuilder().Build();
var serviceProvider = host.Services;

UserDto currentUser = null!;

AuthenticationMenu(serviceProvider);

void AuthenticationMenu(IServiceProvider serviceProvider)
{
    var userService = serviceProvider.GetRequiredService<IUserService>();

    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");

            Console.Write("\nSelect an option:");
            var selectedItem = int.Parse(Console.ReadLine()!);

            switch (selectedItem)
            {
                case 1:
                    {
                        Console.Write("Enter username: ");
                        var username = Console.ReadLine()!;
                        Console.Write("Enter password: ");
                        var password = Console.ReadLine()!;

                        currentUser = userService.Login(username, password);

                        if (currentUser.Role == UserRoleEnum.Admin)
                        {
                            AdminMenu(serviceProvider);
                        }
                        else if (currentUser.Role == UserRoleEnum.Member)
                        {
                            MemberMenu(serviceProvider);
                        }

                        Console.ReadKey();
                        break;
                    }
                case 2:
                    {
                        Console.Write("Enter first name: ");
                        var firstName = Console.ReadLine()!;
                        Console.Write("Enter last name: ");
                        var lastName = Console.ReadLine()!;
                        Console.Write("Enter username: ");
                        var username = Console.ReadLine()!;
                        Console.Write("Enter password: ");
                        var password = Console.ReadLine()!;

                        userService.Register(firstName, lastName, username, password);
                        Console.WriteLine("✅ User registered successfully!");
                        Console.ReadKey();
                        break;
                    }
                case 3:
                    return;
                default:
                    Console.WriteLine("❌ Invalid input.");
                    Console.ReadKey();
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }


    }
}

void MemberMenu(IServiceProvider serviceProvider)
{
    var userService = serviceProvider.GetRequiredService<ISurveyService>();

    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("1. Survey List");
            Console.WriteLine("2. Vote");
            Console.WriteLine("3. Logout");

            Console.Write("\nSelect an option:");
            var selectedItem = int.Parse(Console.ReadLine()!);

            switch (selectedItem)
            {
                case 1:
                    {
                        break;
                    }
                case 2:
                    {

                        break;
                    }
                case 3:
                    {
                        AuthenticationMenu(serviceProvider);
                        break;
                    }
                default:
                    Console.WriteLine("❌ Invalid input.");
                    Console.ReadKey();
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }


    }
}

void AdminMenu(IServiceProvider serviceProvider)
{
    var userService = serviceProvider.GetRequiredService<ISurveyService>();
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("1. Create a Survey");
            Console.WriteLine("2. Delete a Survey");
            Console.WriteLine("3. Result a Survey");
            Console.WriteLine("4. Logout");

            Console.Write("\nSelect an option:");
            var selectedItem = int.Parse(Console.ReadLine()!);

            switch (selectedItem)
            {
                case 1:
                {
                    break;
                }
                case 2:
                {

                    break;
                }
                case 3:
                {

                    break;
                }
                case 4:
                {
                    AuthenticationMenu(serviceProvider);
                    break;
                }
                default:
                    Console.WriteLine("❌ Invalid input.");
                    Console.ReadKey();
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }
    }
}














// static IHostBuilder CreateHostBuilder()
// {
//     return Host.CreateDefaultBuilder().ConfigureServices(services =>
//         services.Initialize());
// }

static IHostBuilder CreateHostBuilder()
{
    return Host.CreateDefaultBuilder()
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        })
        .ConfigureServices(services =>
        {
            services.Initialize();
        });
}

