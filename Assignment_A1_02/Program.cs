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

        service.WeatherForecastAvailable += WeatherForecastHandler;

        Task<Forecast>[] tasks = { null, null };
        Exception exception = null;
        try
        {
            //double latitude = 59.5086798659495;
            double latitude = 60.67452;
            //double longitude = 18.2654625932976;
            double longitude = 17.14174;

            //Create the two tasks and wait for comletion
            tasks[0] = service.GetForecastAsync(latitude, longitude);
            tasks[1] = service.GetForecastAsync("Falun");

            Task.WaitAll(tasks[0], tasks[1]);
            //Console.WriteLine("Tasks completed");
        }
        catch (Exception ex)
        {
            exception = ex;
            //How to handle an exception
            //Your Code

            Console.WriteLine($"An error occured: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }

            //if (tasks[0] == null)
            //{
            //    Console.WriteLine($"Coordinates not found: {ex.Message}");
            //}
            //else if (tasks[1] == null)
            //{
            //    Console.WriteLine($"Location not found: {ex.Message}");
            //}
            //else
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}
        }

        foreach (var task in tasks)
        {
            //How to deal with successful and fault tasks
            //Your Code

            if (task == null)
            {
                Console.WriteLine("Task is null");
                continue;
            }

            switch (task.Status)
            {
                // Successful task
                case TaskStatus.RanToCompletion:
                    Console.WriteLine("Task completed successfully");
                    break;

                case TaskStatus.Faulted:
                    Console.WriteLine($"Task faulted with exception:");
                    foreach (var innerEx in task.Exception?.InnerExceptions ?? Enumerable.Empty<Exception>())
                    {
                        Console.WriteLine($" - {innerEx.Message}");
                    }
                    break;

                case TaskStatus.Canceled:
                    Console.WriteLine("Task was canceled.");
                    break;
                default:
                    Console.WriteLine($"Task is in an unexpected state: {task.Status}");
                    break;
            }


            //if (task.Status == TaskStatus.RanToCompletion)
            //{
            //    // The task completed successfully
            //    Forecast forecast = task.Result; // Access the result of the task
            //    Console.WriteLine($"Task successfully completed");
            //    //Console.WriteLine($"Forecast retrieved: {forecast.Summary}");
            //}
            //else if (task.Status == TaskStatus.Faulted)
            //{
            //    // The task encountered an exception
            //    Console.WriteLine($"Task faulted with exception: {task.Exception?.GetBaseException().Message}");
            //}
            //else if (task.Status == TaskStatus.Canceled)
            //{
            //    // The task was canceled
            //    Console.WriteLine("Task was canceled.");
            //}
            //else
            //{
            //    // Message for unexpected task errors
            //    Console.WriteLine($"Task is in an unexpected state: {task.Status}");
            //}








            ////Console.WriteLine($"\nLocation: {task.Result.City}");

            ////foreach (var date in task)
            ////{
            ////    //Console.WriteLine($"Date: {task.Result.Items[0].DateTime.ToShortDateString()}");

            ////    Console.WriteLine($"Date: {date.Key.ToShortDateString()}");

            ////    foreach (var hour in hour)
            ////    {
            ////        Console.WriteLine($"Time: {hour.DateTime.ToLocalTime().ToShortTimeString()}\n" +
            ////        $"  Temp: {hour.Temperature}\n" +
            ////        $"  Wind speed: {hour.WindSpeed}\n" +
            ////        $"  Condition: {hour.Description}\n" +
            ////        $"  Icon: {hour.Icon}");
            ////    }
            ////}
            ////Console.WriteLine();


            //////if (task != null)
            //////{
            //////    Console.WriteLine($"\nTask result: {exception.Message}");
            //////}

            //////Console.WriteLine($"Date: {task.Result.Items[0].DateTime.ToShortDateString()}");
            //////foreach (var hour in task)
            //////{
            //////    Console.WriteLine($"Time: {hour.DateTime.ToLocalTime().ToShortTimeString()}\n" +
            //////        $"  Temp: {hour.Temperature}\n" +
            //////        $"  Wind speed: {hour.WindSpeed}\n" +
            //////        $"  Condition: {hour.Description}\n" +
            //////        $"  Icon: {hour.Icon}");

            //////}
            ////Console.WriteLine();

        }
    }

    //Event handler declaration
    //Your Code 
    static void WeatherForecastHandler(object sender, string message)
    {
        Console.WriteLine($"Weather forecast available: {message}");
    }

}
