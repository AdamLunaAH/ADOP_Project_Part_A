using Assignment_A1_02.Models;
using Assignment_A1_02.Services;

namespace Assignment_A1_02;

class Program
{
    static async Task Main(string[] args)
    {
        OpenWeatherService service = new OpenWeatherService();

        //Register the event
        //Your Code

        Task<Forecast>[] tasks = { null, null };
        Exception exception = null;
        try
        {
            //double latitude = 59.5086798659495;
            double latitude = 60.674722;
            //double longitude = 18.2654625932976;
            double longitude = 17.144444;

            //Create the two tasks and wait for comletion
            tasks[0] = service.GetForecastAsync(latitude, longitude);
            tasks[1] = service.GetForecastAsync("Falun");

            Task.WaitAll(tasks[0], tasks[1]);
            Console.WriteLine("Tasks completed");
        }
        catch (Exception ex)
        {
            exception = ex;
            //How to handle an exception
            //Your Code
            if (tasks[0] == null)
            {
                Console.WriteLine($"Coordinates not found: {ex.Message}");
            }
            else if (tasks[1] == null)
            {
                Console.WriteLine($"Location not found: {ex.Message}");
            }
            else
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        foreach (var task in tasks)
        {
            //How to deal with successful and fault tasks
            //Your Code
            //Console.WriteLine($"\nTask result: {task.Result.City}");
            //if (task != null)
            //{
            //    Console.WriteLine($"\nTask result: {exception.Message}");
            //}
        }
    }

    //Event handler declaration
    //Your Code 
}
