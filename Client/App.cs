using Services;
using Services.Model;
using Services.Validators;

namespace Client;

public class App(ILoggingService logService, ISlotMachineService slotMachineService)
{
    public void Run()
    {
        var userInput = GetUserInput();

        var results = new List<SpinResult>();


        var balance = userInput.StartBalance;

        for (var i = 0; i < userInput.SpinCount; i++)
        {
            var result = slotMachineService.Spin();
            balance = slotMachineService.IsWin(result.Symbols)
                ? balance + userInput.Bet * result.Payout
                : balance - userInput.Bet;
            results.Add(result);
            var logData = new LogData
            {
                Result = result,
                SpinCount = i,
                Balance = balance
            };
            logService.LogSpinResult(logData);

            if (balance < userInput.Bet) break;
        }

        logService.LogSummary(results);


        Console.WriteLine("press any key to exit...");
        Console.ReadKey();
    }

    private static UserInput GetUserInput()
    {
        var userInputValidator = new UserInputValidator();
        var isValid = false;
        var userInput = new UserInput();

        while (!isValid)
        {
            userInput.Bet = GetInput("Bet:", 0);
            userInput.StartBalance = GetInput("Balance:", 0);
            userInput.SpinCount = GetInput("Spin:", 0);

            var validationResult = userInputValidator.Validate(userInput);
            isValid = validationResult.IsValid;

            if (isValid) continue;

            foreach (var message in validationResult.Errors)
            {
                Console.WriteLine(message.ErrorMessage);
            }
        }

        return userInput;
    }

    private static long GetInput(string prompt, int minValue)
    {
        Console.Write(prompt);

        return long.Parse(Console.ReadLine());
    }
}