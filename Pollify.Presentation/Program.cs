
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pollify.Application;
using Pollify.Application.Contracts;
using Pollify.Domain.DTOs;
using Pollify.Domain.Entities.UserAgg;
using Pollify.Framework;
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

            AnsiConsole.Write(
                new FigletText("Pollify")
                    .Centered()
                    .Color(Color.Aqua)
            );

            AnsiConsole.MarkupLine("[yellow]==============================[/]");
            AnsiConsole.MarkupLine("[bold cyan]Welcome to the Survey System![/]");
            AnsiConsole.MarkupLine("[yellow]==============================[/]\n");

            var selectedItem = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Choose an option:[/]")
                    .AddChoices("🔑 Login", "📝 Register", "🚪 Exit")
                    .HighlightStyle("bold cyan")
            );

            switch (selectedItem)
            {
                case "🔑 Login":
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.MarkupLine("[bold underline cyan]🔐 User Login[/]\n");

                        var username = AnsiConsole.Ask<string>("[green]Enter username:[/]");
                        var password = AnsiConsole.Prompt(
                            new TextPrompt<string>("[green]Enter password:[/]")
                                .PromptStyle("red")
                                .Secret()
                        );

                        currentUser = userService.Login(username, password);

                        AnsiConsole.MarkupLine("[green]✅ Login successful![/]\n");

                        if (currentUser.Role == UserRoleEnum.Admin)
                        {

                            AdminMenu(serviceProvider);

                        }
                        else if (currentUser.Role == UserRoleEnum.Member)
                        {

                            MemberMenu(serviceProvider);

                        }

                        AnsiConsole.MarkupLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    }

                case "📝 Register":
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.MarkupLine("[bold underline green]📝 Register New User[/]\n");

                        var firstName = AnsiConsole.Ask<string>("[cyan]Enter first name:[/]");
                        var lastName = AnsiConsole.Ask<string>("[cyan]Enter last name:[/]");
                        var username = AnsiConsole.Ask<string>("[cyan]Choose a username:[/]");
                        var password = AnsiConsole.Prompt(
                            new TextPrompt<string>("[cyan]Choose a password:[/]")
                                .PromptStyle("red")
                                .Secret()
                        );

                        userService.Register(firstName, lastName, username, password);

                        AnsiConsole.MarkupLine("[green]✅ User registered successfully![/]");
                        AnsiConsole.MarkupLine("\nPress any key to return to menu...");
                        Console.ReadKey();
                        break;
                    }

                case "🚪 Exit":
                    AnsiConsole.MarkupLine("\n[bold red]Goodbye! 👋[/]");
                    Thread.Sleep(600);
                    return;
            }
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"\n[red]❌ {e.Message}[/]");
            AnsiConsole.MarkupLine("[yellow]Press any key to try again...[/]");
            Console.ReadKey();
        }
    }
}

void MemberMenu(IServiceProvider serviceProvider)
{
    var userService = serviceProvider.GetRequiredService<ISurveyService>();
    var surveyService = serviceProvider.GetRequiredService<ISurveyService>();
    var questionService = serviceProvider.GetRequiredService<IQuestionService>();
    var voteService = serviceProvider.GetRequiredService<IVoteService>();

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
                        var surveys = surveyService.GetAll();

                        AnsiConsole.Clear();
                        AnsiConsole.Write(
                            new FigletText("🗳 Survey List 🗳")
                                .Centered()
                                .Color(Color.Cyan1)
                        );

                        if (surveys == null || !surveys.Any())
                        {
                            AnsiConsole.MarkupLine("[red]❌ No surveys found.[/]");
                            AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
                            Console.ReadKey();
                            break;
                        }

                        var table = new Table()
                            .Centered()
                            .Border(TableBorder.Rounded)
                            .Title("[bold yellow]Available Surveys[/]")
                            .AddColumn("[green]ID[/]")
                            .AddColumn("[aqua]Title[/]");

                        foreach (var s in surveys)
                        {
                            table.AddRow($"[white]{s.Id}[/]", $"[silver]{s.Tilte}[/]");
                        }

                        AnsiConsole.Write(table);
                        Console.ReadKey();
                        break;
                    }
                case 2:
                    {
                        var surveys = surveyService.GetAll();
                        Console.WriteLine("📋 Available Surveys:");
                        Console.WriteLine("──────────────────────────────────────────");

                        foreach (var s in surveys)
                        {
                            Console.WriteLine($"ID: {s.Id,-3}  |  Title: {s.Tilte}");
                        }

                        Console.WriteLine("──────────────────────────────────────────");
                        Console.Write("\nEnter the ID of the survey you want to answer: ");
                        int surveyId = int.Parse(Console.ReadLine()!);
                        bool votedBefore = surveyService.IsVotedBefore(surveyId, currentUser.Id);
                        if (votedBefore)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("❌ you voted on this survey before.");
                            Console.ResetColor();
                            Console.ReadKey();
                            break;
                        }

                        var questions = questionService.GetQuestions(surveyId);

                        Console.Clear();
                        Console.WriteLine($"📝 Answering Survey ID: {surveyId}");
                        Console.WriteLine("──────────────────────────────────────────\n");

                        foreach (var question in questions)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"❓ Question: {question.Title}");
                            Console.ResetColor();
                            Console.WriteLine("------------------------------------------");

                            for (int i = 0; i < question.Options.Count; i++)
                            {
                                var option = question.Options[i];
                                Console.WriteLine($"{i + 1}. {option.Text}");
                            }

                            Console.WriteLine("------------------------------------------");

                            int selectedNumber;
                            int selectedOptionId;

                            while (true)
                            {
                                Console.Write("👉 Select an option number: ");
                                if (int.TryParse(Console.ReadLine(), out selectedNumber) &&
                                    selectedNumber >= 1 && selectedNumber <= question.Options.Count)
                                {
                                    selectedOptionId = question.Options[selectedNumber - 1].Id;
                                    break;
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❌ Invalid choice. Please try again.");
                                Console.ResetColor();
                            }

                            voteService.Create(currentUser.Id, selectedOptionId);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("✅ Vote submitted!\n");
                            Console.ResetColor();
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("🎉 Thank you! All your votes have been recorded.");
                        Console.ResetColor();

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
    var surveyService = serviceProvider.GetRequiredService<ISurveyService>();
    var questionService = serviceProvider.GetRequiredService<IQuestionService>();
    var optionService = serviceProvider.GetRequiredService<IOptionService>();
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("1. Create a Survey");
            Console.WriteLine("2. Add Question to a Survey");
            Console.WriteLine("3. Delete a Survey");
            Console.WriteLine("4. Result a Survey");
            Console.WriteLine("5. Logout");

            Console.Write("\nSelect an option:");
            var selectedItem = int.Parse(Console.ReadLine()!);

            switch (selectedItem)
            {
                case 1:
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.Write(
                            new FigletText("📝 Create Survey")
                                .Centered()
                                .Color(Color.Cyan1)
                        );

                        Console.Write("Enter a title for the new survey: ");
                        var surveyTitle = Console.ReadLine()!;
                        var surveyId = surveyService.Create(surveyTitle, currentUser.Id);

                        AnsiConsole.MarkupLine($"[green]✔ Survey '{surveyTitle}' created successfully![/]");
                        Console.WriteLine();

                        Console.Write("How many questions do you want to add? ");
                        if (!int.TryParse(Console.ReadLine(), out int questionCount) || questionCount <= 0)
                        {
                            AnsiConsole.MarkupLine("[red]❌ Invalid number. Returning to main menu...[/]");
                            break;
                        }

                        for (int i = 1; i <= questionCount; i++)
                        {
                            AnsiConsole.Clear();
                            AnsiConsole.MarkupLine($"[yellow]📋 Creating question {i}/{questionCount}[/]");
                            Console.Write("Enter question text: ");
                            var questionText = Console.ReadLine()!;

                            var questionId = questionService.Create(questionText, surveyId);
                            AnsiConsole.MarkupLine($"[green]✔ Question added successfully![/]");
                            Console.WriteLine();

                            for (int j = 1; j <= 4; j++)
                            {
                                Console.Write($"Enter option {j} text: ");
                                var optionText = Console.ReadLine()!;
                                optionService.Create(optionText, questionId);
                            }

                            AnsiConsole.MarkupLine("[green]✔ 4 options added successfully![/]");
                            Console.WriteLine();
                        }

                        AnsiConsole.MarkupLine("[cyan]🎉 Survey created successfully with all questions and options![/]");
                        Console.WriteLine("Press any key to return to the main menu...");
                        Console.ReadKey();
                        break;
                    }

                case 2:
                    {
                        Console.WriteLine("coming soon!");
                        Console.ReadKey();
                        break;
                    }
                case 3:
                    {
                        var surveys = surveyService.GetAll();

                        AnsiConsole.Clear();
                        AnsiConsole.Write(
                            new FigletText("🗳 Survey List 🗳")
                                .Centered()
                                .Color(Color.Cyan1)
                        );

                        if (surveys == null || !surveys.Any())
                        {
                            AnsiConsole.MarkupLine("[red]❌ No surveys found.[/]");
                            AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
                            Console.ReadKey();
                            break;
                        }

                        var table = new Table()
                            .Centered()
                            .Border(TableBorder.Rounded)
                            .Title("[bold yellow]Available Surveys[/]")
                            .AddColumn("[green]ID[/]")
                            .AddColumn("[aqua]Title[/]");

                        foreach (var s in surveys)
                        {
                            table.AddRow($"[white]{s.Id}[/]", $"[silver]{s.Tilte}[/]");
                        }

                        AnsiConsole.Write(table);

                        AnsiConsole.MarkupLine("\n[bold cyan]Enter the ID of the survey you want to delete:[/]");
                        int surveyId = AnsiConsole.Ask<int>("[yellow]>[/] ");

                        var confirm = AnsiConsole.Confirm($"Are you sure you want to delete survey [red]{surveyId}[/]?");
                        if (confirm)
                        {
                            try
                            {
                                surveyService.Delete(surveyId);
                                AnsiConsole.MarkupLine($"[green]✔ Survey with ID {surveyId} deleted successfully![/]");
                            }
                            catch (Exception ex)
                            {
                                AnsiConsole.MarkupLine($"[red]⚠ Error:[/] {ex.Message}");
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[grey]Deletion cancelled.[/]");
                        }

                        AnsiConsole.MarkupLine("\n[italic yellow]Press any key to return...[/]");
                        Console.ReadKey();
                        break;
                    }

                case 4:
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.Write(
                            new FigletText("📊 Survey Results 📊")
                                .Centered()
                                .Color(Color.Cyan1)
                        );

                        var surveys = surveyService.GetAll();

                        if (surveys == null || !surveys.Any())
                        {
                            AnsiConsole.MarkupLine("[red]❌ No surveys found.[/]");
                            AnsiConsole.MarkupLine("[yellow]Press any key to return...[/]");
                            Console.ReadKey();
                            break;
                        }

                        var surveyTable = new Table()
                            .Border(TableBorder.Rounded)
                            .Title("[bold yellow]Available Surveys[/]")
                            .AddColumn("[green]ID[/]")
                            .AddColumn("[aqua]Title[/]");

                        foreach (var s in surveys)
                            surveyTable.AddRow($"[white]{s.Id}[/]", $"[silver]{s.Tilte}[/]");

                        AnsiConsole.Write(surveyTable);

                        int surveyId = AnsiConsole.Ask<int>("\n[bold cyan]Enter the ID of the survey to view its results:[/]");

                        var resultSurvey = surveyService.ResultSurvey(surveyId);
                        if (resultSurvey == null)
                        {
                            AnsiConsole.MarkupLine($"[red]❌ No results found for survey ID {surveyId}.[/]");
                            Console.ReadKey();
                            break;
                        }

                        AnsiConsole.Clear();
                        AnsiConsole.Write(
                            new FigletText("📋 Survey Report 📋")
                                .Centered()
                                .Color(Color.Yellow)
                        );

                        AnsiConsole.MarkupLine($"[bold cyan]Survey ID:[/] [white]{surveyId}[/]");
                        AnsiConsole.MarkupLine($"[bold green]Total Participants:[/] [white]{resultSurvey.TotalParticipantCount}[/]\n");

                        if (resultSurvey.Participants != null && resultSurvey.Participants.Any())
                        {
                            var participantsTable = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold lime]Participants[/]")
                                .AddColumn("[yellow]Name[/]")
                                .AddColumn("[aqua]Username[/]");

                            foreach (var p in resultSurvey.Participants)
                                participantsTable.AddRow($"[white]{p.Name}[/]", $"[silver]{p.UserName}[/]");

                            AnsiConsole.Write(participantsTable);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[grey]No participants available.[/]");
                        }

                        if (resultSurvey.QuestionsResult != null && resultSurvey.QuestionsResult.Any())
                        {
                            foreach (var question in resultSurvey.QuestionsResult)
                            {
                                AnsiConsole.MarkupLine($"\n[bold underline darkorange3]❓ {question.QuestionTitle}[/]");

                                var sortedOptions = question.OptionsResults
                                    .OrderByDescending(o => o.OptionCount)
                                    .ToList();

                                var questionTable = new Table()
                                    .Border(TableBorder.Heavy)
                                    .AddColumn("[green]Option[/]")
                                    .AddColumn("[aqua]Votes[/]")
                                    .AddColumn("[yellow]Percent[/]");

                                foreach (var opt in sortedOptions)
                                {
                                    questionTable.AddRow(
                                        $"[bold white]{opt.OptionText}[/]",
                                        $"[silver]{opt.OptionCount}[/]",
                                        $"[bold yellow]{opt.OptionPercent:0.##}%[/]"
                                    );
                                }

                                AnsiConsole.Write(questionTable);

                                var chart = new BarChart()
                                    .Width(70)
                                    .Label("[bold cyan]Vote Distribution[/]")
                                    .CenterLabel()
                                    .AddItems(sortedOptions.Select(opt =>
                                        new BarChartItem(
                                            opt.OptionText,
                                            (double)opt.OptionPercent,
                                            opt.OptionPercent > 50 ? Color.Orange1 : Color.Yellow1
                                        )));

                                AnsiConsole.Write(chart);
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[grey]No questions or results found for this survey.[/]");
                        }

                        AnsiConsole.MarkupLine("\n[italic yellow]Press any key to return...[/]");
                        Console.ReadKey();
                        break;
                    }



                case 5:
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

