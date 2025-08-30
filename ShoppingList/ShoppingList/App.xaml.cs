using System.Diagnostics;
using ShoppingList.Services;

namespace ShoppingList
{
  /// <summary>
  /// Логика программы при запуске и закрытии
  /// </summary>
  public partial class App : Application
  {
    private IJsonDataService _jsonDataService;
    /// <summary>
    /// Инициализация главной страницы
    /// </summary>
    public App()
    {
      try
      {
        InitializeComponent();

        if (Current?.Handler?.MauiContext != null)
        {
          _jsonDataService = Current.Handler.MauiContext.Services.GetService<IJsonDataService>();
          Debug.WriteLine($"Сервис получен: {_jsonDataService != null}");
        }
        else
        {
          Debug.WriteLine("Handler или MauiContext не доступен");
          _jsonDataService = new JsonDataService();
        }

        MainPage = new NavigationPage(new MainPage());
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Ошибка в конструкторе App: {ex}");
      }
    }

    /// <summary>
    /// Действия после запуска программы
    /// </summary>
    protected override async void OnStart()
    {
      base.OnStart();

      try
      {
        if (_jsonDataService != null)
        {
          await _jsonDataService.LoadAllData();
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Ошибка загрузки: {ex}");
      }
    }

    /// <summary>
    /// Действия после закрытия программы
    /// </summary>
    protected override async void OnSleep()
    {
      base.OnSleep();

      try
      {
        if (_jsonDataService != null)
        {
          await _jsonDataService.SaveAllData();
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Ошибка сохранения: {ex}");
      }
    }
  }
}