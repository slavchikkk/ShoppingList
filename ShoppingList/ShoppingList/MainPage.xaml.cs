namespace ShoppingList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

    private async void OnButtonPressed(object sender, EventArgs e)
    {
      var button = (Button)sender;

      // Уменьшаем размер и затемняем
      await Task.WhenAll(
          button.ScaleTo(0.9, 100, Easing.SinInOut),
          button.FadeTo(0.8, 100, Easing.SinInOut)
      );
    }

    // Анимация при отпускании
    private async void OnButtonReleased(object sender, EventArgs e)
    {
      var button = (Button)sender;

      // Возвращаем исходный размер и прозрачность
      await Task.WhenAll(
          button.ScaleTo(1.0, 100, Easing.SinInOut),
          button.FadeTo(1.0, 100, Easing.SinInOut)
      );
    }

    // Обработчик клика с дополнительной анимацией
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

      // Переход на вторую страницу
      await Navigation.PushAsync(new CreationNewListPage());
    }

    // Дополнительная анимация "успеха"
    private async Task AnimateButtonSuccess(Button button)
    {
      // Сохраняем оригинальный цвет
      var originalColor = button.BackgroundColor;

      // Меняем цвет на зелёный
      button.BackgroundColor = Colors.Green;

      // Анимация "прыжка"
      await button.ScaleTo(1.2, 100, Easing.SinOut);
      await button.ScaleTo(1.0, 100, Easing.SinIn);

      // Возвращаем оригинальный цвет
      button.BackgroundColor = originalColor;
    }

  }
}
