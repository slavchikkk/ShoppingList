using System.Diagnostics;
using ShoppingList.Services;

namespace ShoppingList
{
  public partial class App : Application
  {
    private IJsonDataService _jsonDataService;

    public App()
    {
      try
      {
        InitializeComponent();

        // Безопасное получение сервиса с проверкой на null
        if (Current?.Handler?.MauiContext != null)
        {
          _jsonDataService = Current.Handler.MauiContext.Services.GetService<IJsonDataService>();
          Debug.WriteLine($"Сервис получен: {_jsonDataService != null}");
        }
        else
        {
          Debug.WriteLine("Handler или MauiContext не доступен");
          // Создаем сервис вручную как fallback
          _jsonDataService = new JsonDataService();
        }

        // Оберните MainPage в NavigationPage
        MainPage = new NavigationPage(new MainPage());
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Ошибка в конструкторе App: {ex}");
        MainPage = CreateErrorPage(ex);
      }
    }

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

    private ContentPage CreateErrorPage(Exception ex)
    {
      return new ContentPage
      {
        Content = new ScrollView
        {
          Content = new StackLayout
          {
            Padding = 30,
            Children =
                        {
                            new Label { Text = "Ошибка инициализации", FontSize = 20, TextColor = Colors.Red },
                            new Label { Text = ex.Message, Margin = new Thickness(0, 10, 0, 0) }
                        }
          }
        }
      };
    }
  }
}