namespace SiteManager
{
    internal class AppMenu
    {
        public void Welcome()
        {
            Console.CursorVisible = false;

            Console.WriteLine("Welcome to the Site Manager!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Upload the build archive to the server");
            Console.WriteLine("2. Uploading the finished build to GitHub");
            Console.WriteLine("3. Launch VScode for work on build");
            Console.WriteLine("4. View list of versions");
            Console.WriteLine("5. Exit");

            string[] menuItems = { "Upload the build archive to the server", "Uploading the finished build to GitHub", "Launch VScode for work on build", "View list of versions", "Exit" };

            int selectedItem = 0;

            while (true)
            {
                Console.Clear();
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedItem)
                    {
                        // Подсветка текущего выбранного пункта.
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ResetColor();
                    }

                    Console.WriteLine(menuItems[i]);
                }
                Console.ResetColor();

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.NumPad8:
                        selectedItem = (selectedItem == 0) ? menuItems.Length - 1 : selectedItem - 1;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.NumPad2:
                        selectedItem = (selectedItem == menuItems.Length - 1) ? 0 : selectedItem + 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.WriteLine($"You selected: {menuItems[selectedItem]}");
                        // Здесь можно добавить логику для выполнения выбранного действия.
                        return;
                }
            }
        }
    }
}
