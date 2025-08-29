using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
  public class JsonDataService : IJsonDataService
  {
    private readonly string filePath;

    public JsonDataService()
    {
        filePath = Path.Combine(FileSystem.AppDataDirectory, "shopping_data.json");
        Debug.WriteLine($"Путь к файлу: {filePath}");
    }

    public async Task SaveAllData()
    {
      try
      {
        Debug.WriteLine("Начало сохранения данных...");

        var dataToSave = new
        {
          ShoppingLists = AllShoppingLists.AllLists,
          NextId = ShoppingList.nextId
        };

        var options = new JsonSerializerOptions
        {
          WriteIndented = true,
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string json = JsonSerializer.Serialize(dataToSave, options);

        await File.WriteAllTextAsync(filePath, json);

        Debug.WriteLine("Данные успешно сохранены в JSON файл");
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
        throw;
      }
    }

    public async Task LoadAllData()
    {
      try
      {
        Debug.WriteLine("Попытка загрузки данных...");

        if (!File.Exists(filePath))
        {
          Debug.WriteLine("Файл данных не существует, используем пустые данные");
          AllShoppingLists.AllLists.Clear();
          ShoppingList.nextId = 1;
          return;
        }

        string json = await File.ReadAllTextAsync(filePath);

        var options = new JsonSerializerOptions
        {
          PropertyNameCaseInsensitive = true
        };

        var data = JsonSerializer.Deserialize<JsonData>(json, options);

        AllShoppingLists.AllLists.Clear();
        if (data?.ShoppingLists != null)
        {
          foreach (var list in data.ShoppingLists)
          {
            AllShoppingLists.AllLists.Add(list);
          }
        }

        ShoppingList.nextId = data?.NextId ?? 1;

        Debug.WriteLine($"Загружено {AllShoppingLists.AllLists.Count} списков");
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
        AllShoppingLists.AllLists.Clear();
        ShoppingList.nextId = 1;
      }
    }

    private class JsonData
    {
      public ObservableCollection<ShoppingList> ShoppingLists { get; set; }
      public int NextId { get; set; }
    }
  }
}
