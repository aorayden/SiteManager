using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SiteManager
{
    internal class AppConfig
    {
        internal string СonfigPath { get; set; } = @"config.json";
        internal string VersionsDirectory { get; set; } = @"Versions";
        internal string VersionConfigPath { get; set; } = @"Versions/versions.json";

        internal void ReadAndCheckConfig(string configPath)
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
                    if (root.TryGetProperty("yandexWebdavToken", out var yandexWebdavToken) &&
                        root.TryGetProperty("yandexWebdavUrl", out var yandexWebdavUrl))
                    {
                        Console.WriteLine("\nФайл конфигурации успешно загружен.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\nВ файле конфигурации отсутствуют необходимые настройки.");
                        Thread.Sleep(1000);
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nФайл конфигурации повреждён или изменён.");
                    CreateConfig(configPath);
                }
            }
            else
            {
                Console.WriteLine("\nФайл конфигурации не найден.");
                CreateConfig(configPath);
            }
        }

        internal void CreateConfig(string configPath)
        {
            if (File.Exists(configPath))
            {
                Console.WriteLine("Файл конфигурации уже существует. Удаляем и создаём заново.");
                File.Delete(configPath);
                try
                {
                    Console.WriteLine("\nСоздаём новый файл конфигурации.");

                    // Запрос токена для Yandex Webdav.
                    Console.Write("Введите токен для Yandex Webdav: ");
                    string yandexWebdavToken = Console.ReadLine();

                    // Создание структуры файла конфигурации.
                    var config = new
                    {
                        yandexWebdavUrl = "https://webdav.yandex.ru",
                        yandexWebdavToken = yandexWebdavToken
                    };

                    // Сериализация объекта конфигурации.
                    string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

                    // Запись нового файла конфигурации.
                    File.WriteAllText(configPath, json);
                    Console.WriteLine("\nФайл конфигурации успешно создан.");
                    return;
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
                    Console.Write("Введите токен для Yandex Webdav: ");
                    string yandexWebdavToken = Console.ReadLine();

                    // Создание структуры файла конфигурации.
                    var config = new
                    {
                        yandexWebdavUrl = "https://webdav.yandex.ru",
                        yandexWebdavToken = yandexWebdavToken
                    };

                    // Сериализация объекта конфигурации.
                    string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

                    // Запись нового файла конфигурации.
                    File.WriteAllText(configPath, json);
                    Console.WriteLine("\nФайл конфигурации успешно создан.");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nОшибка при создании файла конфигурации: {ex.Message}");
                }
            }
        }

        internal void CheckAndCreateVersionsDirectory(string path, string configPath)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Console.WriteLine("\nДиректория версий не найдена.");
                    Console.WriteLine("Создаём директорию версий..");
                    Directory.CreateDirectory(path);

                    File.Create($@"{path}/versions.json");

                    Console.WriteLine("\nДиректория успешно создана.");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nОшибка при создании директории версий: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\nДиректория версий успешно загружена.");

                //if (File.Exists(configPath))
                //{
                //    try
                //    {
                //        // Читаем содержимое файла конфигурации.
                //        string reading = File.ReadAllText(configPath);

                //        // Парсим содержимое файла конфигурации.
                //        using JsonDocument config = JsonDocument.Parse(reading);
                //        JsonElement root = config.RootElement;

                //        // Проверяем наличие необходимых настроек.
                //        if (root.TryGetProperty("yandexWebdavToken", out var yandexWebdavToken) &&
                //            root.TryGetProperty("yandexWebdavUrl", out var yandexWebdavUrl))
                //        {
                //            Console.WriteLine("\nФайл конфигурации успешно загружен.");
                //            return;
                //        }
                //        else
                //        {
                //            Console.WriteLine("\nВ файле конфигурации отсутствуют необходимые настройки.");
                //            Thread.Sleep(1000);
                //            throw new Exception();
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine($"\nФайл конфигурации повреждён или изменён.");
                //        CreateConfig(configPath);
                //    }
                //}
                return;
            }
        }
    }
}