using System.Xml;

namespace ShoppingList;

/// <summary>
/// �������� �������� ������ ������ �������
/// </summary>
public partial class CreationNewListPage : ContentPage
{
  /// <summary>
  /// ��� ������
  /// </summary>
  private string listName { get; set; }

  /// <summary>
  /// ��� �������� ��� �������
  /// </summary>
  private string itemName { get; set; }
  /// <summary>
  /// ������� ���������
  /// </summary>
  private string selectedUnit{ get; set; }
  /// <summary>
  /// ���������� ��������� ��� �������
  /// </summary>
  private int amountItem = 0;
  /// <summary>
  /// ��� �������� ��� �������
  /// </summary>
  List<ShoppingItem> items = new List<ShoppingItem>();

  /// <summary>
  /// ������������� �������� �������� ������ �������
  /// </summary>
  public CreationNewListPage()
  {
    InitializeComponent();
    UnitPicker.SelectedIndex = 0;
  }

  /// <summary>
  /// ���������� �������� � ������ 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  void AddItem(object sender, EventArgs e)
  {
    if(string.IsNullOrEmpty(listName) || string.IsNullOrEmpty(itemName) || amountItem == 0)
    {
      DisplayAlert("������ �����", "��� ���� ������ ���� ��������", "��");
      return;
    }
    selectedUnit = UnitPicker.SelectedItem as string;
    var shoppingItem = new ShoppingItem(itemName, amountItem, selectedUnit);
    items.Add(shoppingItem);
    DisplayAlert("", "���������� ��������", "OK");

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
  /// ���� �������� ������ ��������
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
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
  }

  /// <summary>
  /// ���� �������� �������� ��� ������� ��������
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
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
  }

  /// <summary>
  /// ���� ���������� ��������� ��������
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
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
    }
    else
    {
      DisplayAlert("������", "����� ������ ���� ������ 0", "OK");
      return;
    }
  }

  /// <summary>
  /// ������� ����� ������ �������
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
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