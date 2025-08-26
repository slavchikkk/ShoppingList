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
      DisplayAlert("������ �����", "��� ���� ������ ���� ��������", "��");
      return;
    }
    selectedUnit = UnitPicker.SelectedItem as string;
    var shoppingItem = new ShoppingItem(itemName, amountItem, selectedUnit);
    items.Add(shoppingItem);
    DisplayAlert("�����", "���������� ��������", "OK");

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
      DisplayAlert("������", "������ ����", "OK");
      return;
    }
    entry.BackgroundColor = Colors.LightGreen;
    DisplayAlert("��������", $"{listName}", "OK");
  }

  void EnterItemNameCompleted(object sender, FocusEventArgs e)
  {
    var entry = (Entry)sender;
    itemName = entry.Text;

    if (string.IsNullOrEmpty(itemName))
    {
      DisplayAlert("������", "������ ����", "OK");
      return;
    }
    entry.BackgroundColor = Colors.LightGreen;
    DisplayAlert("��������", $"{itemName}", "OK");
  }

  void EnterAmountItemCompleted(object sender, FocusEventArgs e)
  {
    var entry = (Entry)sender;
    var input = entry.Text;

    if(!int.TryParse(input, out amountItem))
    {
      DisplayAlert("������", "������� �����", "OK");
      return;
    }
    if (amountItem > 0)
    {
      entry.BackgroundColor = Colors.LightGreen;
      DisplayAlert("��������", $"{amountItem}", "OK");
    }
    else
    {
      DisplayAlert("������", "����� ������ ���� ������ 0", "OK");
      return;
    }
  }

  async void CreateNewShoppingList(object sender, EventArgs e)
  {
    if(items.Count == 0)
    {
      DisplayAlert("������", "� ������ ������� ������ ���� ���� �� ���� ������", "��");
      return;
    }

    var shoppingList = new ShoppingList(listName, items);
    AllShoppingLists.AddNewList(shoppingList);
    DisplayAlert(string.Empty, "����� ������ ������. ������� �� ������� ��������", "��");
    await Navigation.PushAsync(new MainPage());
  }
}