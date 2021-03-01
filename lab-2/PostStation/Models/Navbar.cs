using System.Collections.Generic;

namespace PostStation.Models
{
    public static class Navbar
    {
        public static int Count = 5;
        public static List<string> Items { get; } = new List<string>()
        {
            "Главная",
            "Игры",
            "Разработчики",
            "Платформы",
            "Страны"
        };
        public static List<string> Actions { get; } = new List<string>()
        {
            "Main",
            "Games",
            "Developers",
            "Platforms",
            "Countries"
        };
        public enum Current
        {
            Main,
            Games,
            Developers,
            Platforms,
            Countries
        }
    }
}