namespace ShoppingList
{
    public partial class App : Application
    {
        public App()
        {
          MainPage = new NavigationPage(new MainPage());
        }
    }
}