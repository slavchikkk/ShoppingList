using System.Xml;

namespace ShoppingList;

/// <summary>
/// Страница создания нового списка покупок
/// </summary>
public partial class CreationNewListPage : ContentPage
{
  /// <summary>
  /// Имя списка
  /// </summary>
  private string listName { get; set; }

  /// <summary>
  /// Имя предмета для покупки
  /// </summary>
  private string itemName { get; set; }
  /// <summary>
  /// Единица измерения
  /// </summary>
  private string selectedUnit{ get; set; }
  /// <summary>
  /// Количество предметов для покупки
  /// </summary>
  private int amountItem = 0;
  /// <summary>
  /// Все предметы для покупки
  /// </summary>
  List<ShoppingItem> items = new List<ShoppingItem>();

  /// <summary>
  /// Инициализация страницы создания списка покупок
  /// </summary>
  public CreationNewListPage()
  {
    InitializeComponent();
    UnitPicker.SelectedIndex = 0;
  }

  /// <summary>
  /// Добавление предмета в список 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  void AddItem(object sender, EventArgs e)
  {
    if(string.IsNullOrEmpty(listName) || string.IsNullOrEmpty(itemName) || amountItem == 0)
    {
      DisplayAlert("Ошибка ввода", "Все поля должны быть зелеными", "ОК");
      return;
    }
    selectedUnit = UnitPicker.SelectedItem as string;
    var shoppingItem = new ShoppingItem(itemName, amountItem, selectedUnit);
    items.Add(shoppingItem);
    DisplayAlert("", "Ингредиент добавлен", "OK");

    string newItem = $"{itemName}: {amountItem} {selectedUnit}";
    var itemLabel = new Label
    {
      Text = newItem
    };

    ItemsLayout.Children.Add(itemLabel);
    ItemNameEntry.Text = string.Empty;
    AmountItemEntry.Text = string.Empty;
  }

  /// <summary>
  /// Ввод названия списка завершен
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  void EnterListNameCompleted(object sender, FocusEventArgs e)
  {
    var entry = (Entry)sender;
    listName = entry.Text;

    if (string.IsNullOrEmpty(listName))
    {
      DisplayAlert("Ошибка", "Пустое поле", "OK");
      return;
    }
    entry.BackgroundColor = Colors.LightGreen;
  }

  /// <summary>
  /// Ввод названия предмета для покупки завершен
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  void EnterItemNameCompleted(object sender, FocusEventArgs e)
  {
    var entry = (Entry)sender;
    itemName = entry.Text;

    if (string.IsNullOrEmpty(itemName))
    {
      DisplayAlert("Ошибка", "Пустое поле", "OK");
      return;
    }
    entry.BackgroundColor = Colors.LightGreen;
  }

  /// <summary>
  /// Ввод количества предметов завершен
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  void EnterAmountItemCompleted(object sender, FocusEventArgs e)
  {
    var entry = (Entry)sender;
    var input = entry.Text;

    if(!int.TryParse(input, out amountItem))
    {
      DisplayAlert("Ошибка", "Введите число", "OK");
      return;
    }
    if (amountItem > 0)
    {
      entry.BackgroundColor = Colors.LightGreen;
    }
    else
    {
      DisplayAlert("Ошибка", "Число должно быть больше 0", "OK");
      return;
    }
  }

  /// <summary>
  /// Создать новый список покупок
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  async void CreateNewShoppingList(object sender, EventArgs e)
  {
    if(items.Count == 0)
    {
      DisplayAlert("Ошибка", "В списке покупок должен быть хотя бы один объект", "Ок");
      return;
    }

    var shoppingList = new ShoppingList(listName, items);
    AllShoppingLists.AddNewList(shoppingList);
    DisplayAlert(string.Empty, "Новый список создан. Возврат на главную страницу", "Ок");
    await Navigation.PushAsync(new MainPage());
  }
}