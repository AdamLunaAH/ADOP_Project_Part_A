﻿using Assignment_A1_03.Models;
using Assignment_A1_03.Services;

namespace Assignment_A1_03;

class Program
{
    static async Task Main(string[] args)
    {
        OpenWeatherService service = new OpenWeatherService();

        //Register the event
        //Your Code
        
        service.WeatherForecastAvailable += WeatherForecastHandler;


        Task<Forecast>[] tasks = { null, null, null, null };
        Exception exception = null;
        try
        {
            //double latitude = 59.5086798659495;
            double latitude = 60.67452;
            //double longitude = 18.2654625932976;
            double longitude = 17.14174;

            string location = "Borlänge";

            //Create the two tasks and wait for comletion
            tasks[0] = service.GetForecastAsync(latitude, longitude);
            tasks[1] = service.GetForecastAsync(location);

            //Task.WaitAll(tasks[0], tasks[1]);
            //await Task.WhenAll(tasks[0], tasks[1]);
            await Task.WhenAll(tasks[0], tasks[1]);

            tasks[2] = service.GetForecastAsync(latitude, longitude);
            tasks[3] = service.GetForecastAsync(location);

            ////Wait and confirm we get an event showing cahced data avaialable
            await Task.WhenAll(tasks[2], tasks[3]);
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
                    DisplayForecast(task.Result);
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
        }
    }


    //Event handler declaration
    //Your Code

    static void WeatherForecastHandler(object sender, string message)
    {
        Console.WriteLine($"Weather forecast available: {message}");
    }

    static void DisplayForecast(Forecast forecast)
    {
        Console.WriteLine($"Weather forecast for {forecast.City}");
        foreach (var item in forecast.Items.GroupBy(x => x.DateTime.Date))
        {
            Console.WriteLine($"***********************\n" +
                $"Date: {item.Key.ToShortDateString()}");

            foreach (var hour in item)
            {
                Console.WriteLine($"    Time: {hour.DateTime.ToLocalTime().ToShortTimeString()}\n" +
                    $"      Temp: {hour.Temperature}\n" +
                    $"      Wind speed: {hour.WindSpeed}\n" +
                    $"      Condition: {hour.Description}\n" +
                    $"      Icon: {hour.Icon}");
            }
            Console.WriteLine("***********************\n\n");
        }
    }
}

