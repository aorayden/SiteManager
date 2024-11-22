using ManageSite;
using SiteManager;

AppMenu();

void AppMenu()
{
    Console.CursorVisible = false;

    Console.WriteLine("Welcome to the Site Manager!");
    Console.WriteLine("Please select an option:");

    var menuItems = new Dictionary<int, string>()
    {
        { 1, "Upload the build archive to the server" },
        { 2, "Uploading the finished build to GitHub" },
        { 3, "Launch VSCode for work on build" },
        { 4, "View list of versions" },
        { 5, "Exit" }
    };

    int selectedItemIndex = 0;
    var menuKeys = menuItems.Keys.ToArray();

    while (true)
    {
        Console.Clear();
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
                // Console.WriteLine($"You selected: {menuItems[selectedKey]}");

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
}

//void LaunchVSCode()
//{
//    // Логика для запуска VSCode
//    Console.WriteLine("Launching VSCode...");
//    // Пример: Process.Start("code");
//}

//void ViewVersionList()
//{
//    // Логика для отображения списка версий
//    Console.WriteLine("Viewing list of versions...");
//    // Пример: Console.WriteLine("Version 1.0, Version 1.1, ...");
//}

//class BuildUploader
//{
//    public void UploadMenu()
//    {
//        // Логика для загрузки архива сборки на сервер
//        Console.WriteLine("Uploading the build archive to the server...");
//    }
//}

//class GitUploader
//{
//    public void UploadToGitHub()
//    {
//        // Логика для загрузки сборки на GitHub
//        Console.WriteLine("Uploading the finished build to GitHub...");
//    }
//}
