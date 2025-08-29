namespace ShoppingList;

public partial class ShoppingListDetailPage : ContentPage
{
  private ShoppingList _shoppingList;

  public ShoppingListDetailPage(ShoppingList shoppingList)
  {
    InitializeComponent();
    _shoppingList = shoppingList;
    BuildUI();
  }

  private void BuildUI()
  {
    var scrollView = new ScrollView();
    var mainLayout = new VerticalStackLayout
    {
      Spacing = 15,
      Padding = new Thickness(20, 10, 20, 20)
    };

    var backButton = new Button
    {
      Text = "�����",
      HorizontalOptions = LayoutOptions.Start,
      Margin = new Thickness(0, 0, 0, 20)
    };
    backButton.Clicked += async (s, e) => await Navigation.PopAsync();
    mainLayout.Children.Add(backButton);

    var titleLabel = new Label
    {
      Text = _shoppingList.ListName,
      FontSize = 24,
      FontAttributes = FontAttributes.Bold,
      HorizontalOptions = LayoutOptions.Center
    };
    mainLayout.Children.Add(titleLabel);

    // ������ ���������� ������ ��������
    var addButton = new Button
    {
      Text = "�������� �����",
      BackgroundColor = Color.FromArgb("#007bff"),
      TextColor = Colors.White,
      Margin = new Thickness(0, 10, 0, 20)
    };
    addButton.Clicked += OnAddItemClicked;
    mainLayout.Children.Add(addButton);

    // �������� ������
    if (_shoppingList.Items.Count == 0)
    {
      var emptyLabel = new Label
      {
        Text = "������ ����",
        FontSize = 16,
        TextColor = Colors.Gray,
        HorizontalOptions = LayoutOptions.Center
      };
      mainLayout.Children.Add(emptyLabel);
    }
    else
    {
      // ��������� �������
      var itemsTitle = new Label
      {
        Text = "�������� ������:",
        FontSize = 18,
        FontAttributes = FontAttributes.Bold,
        Margin = new Thickness(0, 20, 0, 10)
      };
      mainLayout.Children.Add(itemsTitle);

      // ��������� ������ ������� ������
      foreach (var item in _shoppingList.Items)
      {
        var itemCard = CreateItemCard(item);
        mainLayout.Children.Add(itemCard);
      }
    }

    scrollView.Content = mainLayout;
    Content = scrollView;
  }

  private Frame CreateItemCard(ShoppingItem item)
  {
    // ������� �������� ��� �������� ������
    var frame = new Frame
    {
      CornerRadius = 8,
      BackgroundColor = item.IsBought ? Color.FromArgb("#e8f5e8") : Color.FromArgb("#FFFFFF"),
      BorderColor = Color.FromArgb("#E9ECEF"),
      Padding = new Thickness(12)
    };

    var contentLayout = new Grid
    {
      ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
            },
      RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
            }
    };

    // ������� ��� ������� �������
    var checkBox = new CheckBox
    {
      IsChecked = item.IsBought,
      Color = Color.FromArgb("#28a745"),
      VerticalOptions = LayoutOptions.Center
    };
    checkBox.CheckedChanged += (s, e) => OnItemCheckedChanged(item, e.Value);
    Grid.SetColumn(checkBox, 0);
    Grid.SetRow(checkBox, 0);
    contentLayout.Children.Add(checkBox);

    // �������� ��������
    var nameLabel = new Label
    {
      Text = item.Name,
      FontSize = 16,
      TextColor = item.IsBought ? Color.FromArgb("#6c757d") : Color.FromArgb("#212529"),
      VerticalOptions = LayoutOptions.Center,
      TextDecorations = item.IsBought ? TextDecorations.Strikethrough : TextDecorations.None
    };
    Grid.SetColumn(nameLabel, 1);
    Grid.SetRow(nameLabel, 0);
    contentLayout.Children.Add(nameLabel);

    // ���������� � ������� ���������
    var quantityLabel = new Label
    {
      Text = $"{item.Amount} {item.Unit}",
      FontSize = 14,
      TextColor = item.IsBought ? Color.FromArgb("#6c757d") : Color.FromArgb("#6C757D"),
      VerticalOptions = LayoutOptions.Center,
      FontAttributes = FontAttributes.Bold,
      TextDecorations = item.IsBought ? TextDecorations.Strikethrough : TextDecorations.None
    };
    Grid.SetColumn(quantityLabel, 2);
    Grid.SetRow(quantityLabel, 0);
    contentLayout.Children.Add(quantityLabel);

    // ������ �������� ��������
    var deleteButton = new Button
    {
      Text = "�������",
      FontSize = 14,
      BackgroundColor = Colors.Transparent,
      TextColor = Color.FromArgb("#DC3545"),
      WidthRequest = 90,
      HeightRequest = 40,
      CornerRadius = 15,
      Margin = new Thickness(15, 0, 0, 0)
    };
    deleteButton.Clicked += (s, e) => OnDeleteItemClicked(item);

    // ��������� ������ �������� �� ������ ������
    Grid.SetColumn(deleteButton, 2);
    Grid.SetRow(deleteButton, 0);
    contentLayout.Children.Add(deleteButton);

    frame.Content = contentLayout;
    return frame;
  }

  private void OnItemCheckedChanged(ShoppingItem item, bool isChecked)
  {
    item.IsBought = isChecked;
    // ������������� UI ��� ��������� ���������
    BuildUI();
  }

  private async void OnDeleteItemClicked(ShoppingItem item)
  {
    bool confirm = await DisplayAlert(
        "������������� ��������",
        $"�� �������, ��� ������ ������� \"{item.Name}\" �� ������?",
        "��", "���");

    if (confirm)
    {
      _shoppingList.Items.Remove(item);
      BuildUI();
    }
  }

  private async void OnAddItemClicked(object sender, EventArgs e)
  {
    // ������ �������� ������
    string name = await DisplayPromptAsync(
        "�������� �����",
        "������� �������� ������:",
        "��������",
        "������",
        placeholder: "�������� ������");

    if (string.IsNullOrWhiteSpace(name))
      return;

    // ������ ���������� - ������������ �����
    string amountStr = await DisplayPromptAsync(
        "����������",
        "������� ����������:",
        "OK",
        "������",
        initialValue: "1",
        keyboard: Keyboard.Numeric);

    if (string.IsNullOrWhiteSpace(amountStr) || !int.TryParse(amountStr, out int amount))
      amount = 1;

    // ������ ������� ���������
    string unit = await DisplayPromptAsync(
        "������� ���������",
        "������� ������� ��������� (��, ��, �, � � �.�.):",
        "������",
        "������",
        placeholder: "��");

    if (string.IsNullOrWhiteSpace(unit))
      unit = "��";

    // ��������� ����� �����
    var newItem = new ShoppingItem(name, amount, unit);
    _shoppingList.Items.Add(newItem);

    BuildUI();
  }
}