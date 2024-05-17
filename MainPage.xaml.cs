using AppLibrosMobile.PageControllers;

namespace AppLibrosMobile
{
    public partial class MainPage : ContentPage
    {        

        public MainPage()
        {
            InitializeComponent();
        }

        private void cmdNextPageGenres_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Genres());
        }

        private void cmdNextPageBooks_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Books());
        }
    }

}
