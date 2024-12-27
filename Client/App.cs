using Microsoft.Extensions.Logging.Console;
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
                Balance = balance,
                Bet =  userInput.Bet
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
            userInput.StartBalance = GetInput("Starting balance (in cents):", 1);
            userInput.Bet = GetInput("Bet (in cents):", 1);
            userInput.SpinCount = GetInput("Number of spins to play:", 1);

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
        var parseResult = false;
        long result = 0;

        while (!(parseResult && result >= minValue))
        {
            Console.WriteLine(prompt);
            var promptValue = Console.ReadLine();
            parseResult = long.TryParse(promptValue, out result);
            if (result < minValue)
            {
                Console.WriteLine("Please enter a number bigger or equal than {0}", minValue);
            }
        }
        
        return result;
    }
}