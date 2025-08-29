namespace ShoppingList;

public partial class AllShoppingListPage : ContentPage
{
  public AllShoppingListPage()
  {
    InitializeComponent();
    BuildUI();
  }

  private Frame CreateListCard(ShoppingList shoppingList)
  {
    var frame = new Frame
    {
      CornerRadius = 12,
      BackgroundColor = Color.FromArgb("#F8F9FA"),
      BorderColor = Color.FromArgb("#E9ECEF"),
      Padding = new Thickness(16),
      HasShadow = true
    };

    var mainLayout = new Grid
    {
      ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
            },
      RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
            }
    };

    var nameLabel = new Label
    {
      Text = shoppingList.ListName,
      FontSize = 18,
      FontAttributes = FontAttributes.Bold,
      TextColor = Color.FromArgb("#212529")
    };
    Grid.SetRow(nameLabel, 0);
    Grid.SetColumn(nameLabel, 0);

    var countLabel = new Label
    {
      Text = $"Элементов: {shoppingList.Items.Count}",
      FontSize = 14,
      TextColor = Color.FromArgb("#6C757D")
    };
    Grid.SetRow(countLabel, 1);
    Grid.SetColumn(countLabel, 0);

    // Кнопка удаления
    var deleteButton = new Button
    {
      Text = "Удалить", 
      FontSize = 14, 
      BackgroundColor = Colors.Transparent,
      TextColor = Color.FromArgb("#DC3545"),
      WidthRequest = 100, 
      HeightRequest = 40,
      CornerRadius = 8, 
      Margin = new Thickness(0, 0, 0, 0)
    };
    deleteButton.Clicked += (s, e) => OnDeleteListClicked(shoppingList);
    Grid.SetRow(deleteButton, 0);
    Grid.SetColumn(deleteButton, 1);
    Grid.SetRowSpan(deleteButton, 2);

    mainLayout.Children.Add(nameLabel);
    mainLayout.Children.Add(countLabel);
    mainLayout.Children.Add(deleteButton);

    var tapGesture = new TapGestureRecognizer();
    tapGesture.Tapped += (s, e) => OnListTapped(shoppingList);
    frame.GestureRecognizers.Add(tapGesture);

    frame.Content = mainLayout;
    return frame;
  }

  private async void OnDeleteListClicked(ShoppingList shoppingList)
  {
    bool confirm = await DisplayAlert(
        "Подтверждение удаления",
        $"Вы уверены, что хотите удалить список \"{shoppingList.ListName}\"?",
        "Да", "Нет");

    if (confirm)
    {
      AllShoppingLists.RemoveList(shoppingList);
      await DisplayAlert("Успех", "Список удален", "OK");
      // Перестраиваем интерфейс
      BuildUI();
    }
  }

  private async void OnListTapped(ShoppingList shoppingList)
  {
    await Navigation.PushAsync(new ShoppingListDetailPage(shoppingList));
  }

  private void BuildUI()
  {
    var scrollView = new ScrollView();
    var mainLayout = new VerticalStackLayout
    {
      Spacing = 15,
      Padding = new Thickness(20, 10, 20, 20)
    };

    if (AllShoppingLists.AllLists.Count == 0)
    {
      var emptyLabel = new Label
      {
        Text = "Списки покупок отсутствуют",
        FontSize = 16,
        TextColor = Colors.Gray,
        HorizontalOptions = LayoutOptions.Center,
        VerticalOptions = LayoutOptions.CenterAndExpand
      };
      mainLayout.Children.Add(emptyLabel);
    }
    else
    {
      // Добавляем каждый список в интерфейс
      foreach (var shoppingList in AllShoppingLists.AllLists)
      {
        var listCard = CreateListCard(shoppingList);
        mainLayout.Children.Add(listCard);
      }
    }

    scrollView.Content = mainLayout;
    Content = scrollView;
  }
}