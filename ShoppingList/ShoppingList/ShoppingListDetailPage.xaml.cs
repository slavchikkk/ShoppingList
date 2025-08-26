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
      Text = "Назад",
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

    // Элементы списка
    if (_shoppingList.Items.Count == 0)
    {
      var emptyLabel = new Label
      {
        Text = "Список пуст",
        FontSize = 16,
        TextColor = Colors.Gray,
        HorizontalOptions = LayoutOptions.Center
      };
      mainLayout.Children.Add(emptyLabel);
    }
    else
    {
      // Заголовок раздела
      var itemsTitle = new Label
      {
        Text = "Элементы списка:",
        FontSize = 18,
        FontAttributes = FontAttributes.Bold,
        Margin = new Thickness(0, 20, 0, 10)
      };
      mainLayout.Children.Add(itemsTitle);

      // Добавляем каждый элемент списка
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
    // Создаем карточку для элемента списка
    var frame = new Frame
    {
      CornerRadius = 8,
      BackgroundColor = Color.FromArgb("#FFFFFF"),
      BorderColor = Color.FromArgb("#E9ECEF"),
      Padding = new Thickness(12)
    };

    var contentLayout = new HorizontalStackLayout
    {
      Spacing = 10,
      VerticalOptions = LayoutOptions.Center
    };

    // Название элемента
    var nameLabel = new Label
    {
      Text = item.Name,
      FontSize = 16,
      TextColor = Color.FromArgb("#212529"),
      VerticalOptions = LayoutOptions.Center
    };

    // Количество и единица измерения
    var quantityLabel = new Label
    {
      Text = $"{item.Amount} {item.Unit}",
      FontSize = 14,
      TextColor = Color.FromArgb("#6C757D"),
      VerticalOptions = LayoutOptions.Center,
      FontAttributes = FontAttributes.Bold
    };

    contentLayout.Children.Add(nameLabel);
    contentLayout.Children.Add(quantityLabel);
    frame.Content = contentLayout;

    return frame;
  }
}