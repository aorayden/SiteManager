using System.Text.Json;
using ManageSite;
using SiteManager;

const string configPath = @"config.json";
AppMenu();

void ReadAndCheckConfig(string configPath)
{
    if (File.Exists(configPath))
    {
        try
        {
            // Читаем содержимое файла конфигурации.
            string reading = File.ReadAllText(configPath);

            // Парсим содержимое файла конфигурации.
            using JsonDocument config = JsonDocument.Parse(reading);
            JsonElement root = config.RootElement;

            // Проверяем наличие необходимых настроек.
            if (root.TryGetProperty("yandexWebdavToken", out JsonElement yandexWebdavToken) &&
                root.TryGetProperty("yandexWebdavUrl", out JsonElement yandexWebdavUrl))
            {
                //Console.WriteLine("Все настройки приложения найдены.");
                //Console.WriteLine($"YandexWebdavUrl: {yandexWebdavUrl}");
                //Console.WriteLine($"YandexWebdavToken: {yandexWebdavToken}");
                Thread.Sleep(1000);
                return;
            }
            else
            {
                Console.WriteLine("В файле конфигурации отсутствуют необходимые настройки.");
                Thread.Sleep(1000);
                throw new Exception();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Файл конфигурации повреждён или изменён.");
            CreateConfig(configPath);
        }
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Файл конфигурации не найден.");
        CreateConfig(configPath);
    }
}

void CreateConfig(string configPath)
{
    if (File.Exists(configPath))
    {
        Console.WriteLine("Файл конфигурации уже существует. Удаляем и создаём новый.");
        File.Delete(configPath);
        try
        {
            Console.Clear();
            Console.WriteLine("Создаём новый файл конфигурации.");

            // Запрос токена для Yandex Webdav.
            Console.Write("Введите токен для Yandex Webdav: ");
            string yandexWebdavToken = Console.ReadLine();

            // Создание структуры файла конфигурации.
            var config = new { yandexWebdavUrl = "https://webdav.yandex.ru", yandexWebdavToken = yandexWebdavToken };

            // Сериализация объекта конфигурации.
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

            // Запись нового файла конфигурации.
            File.WriteAllText(configPath, json);
            Console.WriteLine("\nФайл конфигурации успешно создан.");
            AppMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла конфигурации: {ex.Message}");
        }
    }
    else
    {
        try
        {
            Console.WriteLine("\nСоздаём новый файл конфигурации.");

            // Запрос токена для Yandex Webdav.
            Console.Write("Введите токен Yandex Webdav API: ");
            string yandexWebdavToken = Console.ReadLine();

            // Создание структуры файла конфигурации.
            var config = new { yandexWebdavUrl = "https://webdav.yandex.ru", yandexWebdavToken = yandexWebdavToken };

            // Сериализация объекта конфигурации.
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

            // Запись нового файла конфигурации.
            File.WriteAllText(configPath, json);
            Console.WriteLine("Файл конфигурации успешно создан.");
            AppMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла конфигурации: {ex.Message}");
        }
    }
}

void AppMenu()
{
    while (true)
    {
        ReadAndCheckConfig(configPath);
        Console.CursorVisible = false;
        void PrintHelloAndOptions() => Console.WriteLine("Welcome to the Site Manager!\nPlease select an option:\n");
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
    }
}