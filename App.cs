using SiteManager;

AppMenu();
return;

void AppMenu()
{
    while (true)
    {
        var appConfig = new AppConfig();

        InitApp();

        Console.CursorVisible = false;

        var menuItems = new Dictionary<int, string>()
        {
            { 1, "Upload the build archive to the server" },
            { 2, "Uploading the finished build to GitHub" },
            { 3, "Launch VSCode for work on build" },
            { 4, "View list of versions" },
            { 5, "Exit" }
        };
        int selectedItemIndex = 0;
        int[] menuKeys = menuItems.Keys.ToArray();
        while (true)
        {
            Console.Clear();
            PrintHelloAndOptions();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == selectedItemIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ResetColor();
                }

                Console.WriteLine(menuItems[menuKeys[i]]);
            }

            Console.ResetColor();
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.NumPad8:
                    selectedItemIndex = (selectedItemIndex == 0) ? menuItems.Count - 1 : selectedItemIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.NumPad2:
                    selectedItemIndex = (selectedItemIndex == menuItems.Count - 1) ? 0 : selectedItemIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    int selectedKey = menuKeys[selectedItemIndex];
                    Console.WriteLine($"You selected: {menuItems[selectedKey]}");
                    switch (selectedKey)
                    {
                        case 1:
                            var buildUploader = new BuildUploader();
                            buildUploader.UploadMenu();
                            return;
                        case 2:
                            var gitUploader = new GitHubUploader();
                            gitUploader.UploaderMenu();
                            return;
                        case 3:
                            var vscLauncher = new LaunchVSCode();
                            vscLauncher.LauncherMenu();
                            return;
                        case 4:
                            var viewerVersions = new ViewVersionList();
                            viewerVersions.ViewVersionListMenu();
                            return;
                        case 5:
                            Console.WriteLine("Leaving the application.");
                            Thread.Sleep(500);
                            Environment.Exit(0);
                            return;
                    }

                    return;
            }
        }

        void PrintHelloAndOptions() => Console.WriteLine("Welcome to the Site Manager!\nPlease select an option:\n");

        void InitApp()
        {
            Console.WriteLine("Application initialization..");
            Thread.Sleep(1000);

            appConfig.ReadAndCheckConfig(appConfig.СonfigPath);
            Thread.Sleep(1500);

            appConfig.CheckAndCreateVersionsDirectory(appConfig.VersionsDirectory, appConfig.VersionConfigPath);
            Thread.Sleep(1500);
        }
    }
}