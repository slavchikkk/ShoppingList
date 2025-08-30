namespace ShoppingList
{
    public partial class MainPage : ContentPage
    {
        
      /// <summary>
      /// Инициализация главной страницы
      /// </summary>
      public MainPage()
      {
        InitializeComponent();
      }

      /// <summary>
      /// Анимация при удержании кнопки
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void OnButtonPressed(object sender, EventArgs e)
      {
        var button = (Button)sender;
        await Task.WhenAll(
            button.ScaleTo(0.9, 100, Easing.SinInOut),
            button.FadeTo(0.8, 100, Easing.SinInOut)
        );
      }
      
      /// <summary>
      /// Анимации при отпускании кнопки
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void OnButtonReleased(object sender, EventArgs e)
      {
        var button = (Button)sender;
        await Task.WhenAll(
            button.ScaleTo(1.0, 100, Easing.SinInOut),
            button.FadeTo(1.0, 100, Easing.SinInOut)
        );
      }
      
      /// <summary>
      /// Переход на страницу создания списков после нажатия на кнопку
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void OnAddButtonClicked(object sender, EventArgs e)
      {
        var button = (Button)sender;

        // Анимация нажатия
        await Task.WhenAll(
            button.ScaleTo(1.1, 50, Easing.SinOut),
            button.FadeTo(0.9, 50, Easing.SinOut)
        );

        // Возврат к исходному состоянию
        await Task.WhenAll(
            button.ScaleTo(1.0, 50, Easing.SinIn),
            button.FadeTo(1.0, 50, Easing.SinIn)
        );
        await Navigation.PushAsync(new CreationNewListPage());
      }
      
      /// <summary>
      /// Переход на страницу со всеми списками после нажатия на кнопку
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void OnShowAllListsButtonClicked(object sender, EventArgs e)
      {
        await Navigation.PushAsync(new AllShoppingListPage());
      }
    }
}
