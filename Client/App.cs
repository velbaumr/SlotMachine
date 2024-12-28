using Services;
using Services.Model;
using Services.Validators;

namespace Client;

public class App(ILoggingService logService, ISlotMachineService slotMachineService)
{
    public void Run()
    {
        var userInput = GetUserInput();
        int spinCounter;
        var results = new List<SpinResult>();
        var balance = userInput.StartBalance;

        for (spinCounter = 0; spinCounter < userInput.SpinCount; spinCounter++)
        {
            var result = slotMachineService.Spin();
            var isWin = slotMachineService.IsWin(result.Symbols);
            
            balance = isWin
                ? balance + userInput.Bet * result.Multiplier
                : balance - userInput.Bet;

            results.Add(result);
            
            var logData = new LogData
            {
                Result = result,
                SpinCount = spinCounter,
                Balance = balance,
                Bet =  userInput.Bet
            };
            
            logService.LogSpinResult(logData);

            if (balance < userInput.Bet) break;
        }
        
        var totalWin = balance - userInput.StartBalance;
        
        logService.LogSummary(results, slotMachineService.Configuration, totalWin, spinCounter * userInput.Bet, spinCounter, userInput.Bet);
        
        Console.WriteLine();
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
        Console.WriteLine();
        
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
                Console.WriteLine("Please enter a number bigger or equal to {0}", minValue);
            }
        }
        
        return result;
    }
}