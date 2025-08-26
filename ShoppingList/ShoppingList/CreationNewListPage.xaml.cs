using System.Xml;

namespace ShoppingList;

public partial class CreationNewListPage : ContentPage
{
  private string listName, itemName, selectedUnit;
  private int amountItem = 0;
  List<ShoppingItem> items = new List<ShoppingItem>();

  public CreationNewListPage()
  {
    InitializeComponent();
    UnitPicker.SelectedIndex = 0;
  }

  void AddIgridient(object sender, EventArgs e)
  {
    if(string.IsNullOrEmpty(listName) || string.IsNullOrEmpty(itemName) || amountItem == 0)
    {
      DisplayAlert("Ошибка ввода", "Все поля должны быть зелеными", "ОК");
      return;
    }
    selectedUnit = UnitPicker.SelectedItem as string;
    var shoppingItem = new ShoppingItem(itemName, amountItem, selectedUnit);
    items.Add(shoppingItem);
    DisplayAlert("Успех", "Ингредиент добавлен", "OK");

    string newItem = $"{itemName}: {amountItem} {selectedUnit}";
    var itemLabel = new Label
    {
      Text = newItem
    };

    ItemsLayout.Children.Add(itemLabel);
    ItemNameEntry.Text = string.Empty;
    AmountItemEntry.Text = string.Empty;
  }

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
    DisplayAlert("Название", $"{listName}", "OK");
  }

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
    DisplayAlert("Название", $"{itemName}", "OK");
  }

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
      DisplayAlert("Название", $"{amountItem}", "OK");
    }
    else
    {
      DisplayAlert("Ошибка", "Число должно быть больше 0", "OK");
      return;
    }
  }

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