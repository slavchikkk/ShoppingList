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

      var contentLayout = new VerticalStackLayout{ Spacing = 8 };

      var nameLabel = new Label
      {
        Text = shoppingList.ListName,
        FontSize = 18,
        FontAttributes = FontAttributes.Bold,
        TextColor = Color.FromArgb("#212529")
      };

      var countLabel = new Label
      {
        Text = $"Элементов: {shoppingList.Items.Count}",
        FontSize = 14,
        TextColor = Color.FromArgb("#6C757D")
      };

      contentLayout.Children.Add(nameLabel);
      contentLayout.Children.Add(countLabel);

      var tapGesture = new TapGestureRecognizer();
      tapGesture.Tapped += (s, e) => OnListTapped(shoppingList);
      frame.GestureRecognizers.Add(tapGesture);

      frame.Content = contentLayout;
      return frame;
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