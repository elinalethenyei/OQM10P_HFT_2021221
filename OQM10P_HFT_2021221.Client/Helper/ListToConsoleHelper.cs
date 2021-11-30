using OQM10P_HFT_2021221.Models.ResponseObjects;
using System;
using System.Collections.Generic;

namespace OQM10P_HFT_2021221.Client.Helper
{
    public static class ListToConsoleHelper
    {
        public static void ToConsole<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }

        public static void ToConsole<TKey, TValue>(this IDictionary<TKey, TValue> collection, string keyName, string valueName)
        {
            foreach (var item in collection)
            {
                Console.WriteLine($"{keyName}: {item.Key}, {valueName}: {item.Value}");
            }
        }

        public static void ToConsole(this ApiResult apiResult, string action)
        {
            if (apiResult.isSuccess)
            {
                Console.WriteLine($"{action} was successful");
            }
            else
            {
                Console.WriteLine($"{action} failed. Errors:");
                apiResult.errorMessages.ToConsole();
            }
        }
    }
}
