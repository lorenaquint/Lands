



namespace Lands.Views
{
    using Xamarin.Forms;
    public class LandsPage : ContentPage
    {
        public LandsPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

