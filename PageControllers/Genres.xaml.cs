namespace AppLibrosMobile.PageControllers;

using AppLibrosMobile.ApiConsumer;
using AppLibrosMobile.Model;
using CommunityToolkit.Maui.Alerts;

public partial class Genres : ContentPage
{
	public Genres()
	{
		InitializeComponent();
        loadGenres();
    }

    private String url = "https://apiconsumermmovil.azurewebsites.net";
    private CancellationTokenSource cancellation = new CancellationTokenSource();

    private async void cmdGetByIdGenres_Clicked(object sender, EventArgs e)
    {
        int id = txtId != null ? Convert.ToInt32(txtId.Text) : 0;
        var result = ApiConsumer<Genre>.Read(url + "/api/Generos", id);

        if (result.Nombre == null)
        {
            var toast = Toast.Make("Genre not found");
            await toast.Show(cancellation.Token);
            return;
        }

        txtName.Text = result.Nombre;
        txtDescription.Text = result.Descripcion;
        var toast2 = Toast.Make("Genre found");
        await toast2.Show(cancellation.Token);
    }

    private async void cmdPostGenres_Clicked(object sender, EventArgs e)
    {
        Genre genre = new Genre
        {
            Nombre = txtName.Text,
            Descripcion = txtDescription.Text
        };

        var result = ApiConsumer<Genre>.Create(url + "/api/Generos", genre);
        if (result.Nombre == null)
        {
               var toast = Toast.Make("Genre not created");
            await toast.Show(cancellation.Token);
            return;        
        }

        loadGenres();

        var txtId = result.Id.ToString();
        var toast2 = Toast.Make("Genre created with id: " + txtId);
        await toast2.Show(cancellation.Token);

    }

    private async void cmdPutGenres_Clicked(object sender, EventArgs e)
    {

        Genre genre = new Genre
        {
            Id = int.Parse(txtId.Text),
            Nombre = txtName.Text,
            Descripcion = txtDescription.Text
        };

        int id = txtId != null ? Convert.ToInt32(txtId.Text) : 0;

        if(id == 0)
        {
            var toast = Toast.Make("Genre not found");
            await toast.Show(cancellation.Token);
            return;
        }
        

        var result = ApiConsumer<Genre>.Update(url + "/api/Generos", genre.Id, genre);
        loadGenres();
        var toast2 = Toast.Make("Genre updated");
        await toast2.Show(cancellation.Token);

    }

    private async void cmdDelete_Clicked(object sender, EventArgs e)
    {

        if (sender is Button button && button.CommandParameter is int GeneroId)
        {
            ApiConsumer<Genre>.Delete(url + "/api/Generos", GeneroId);
            loadGenres();
            var toast = Toast.Make("Genre deleted");
            await toast.Show(cancellation.Token);
        }

    }

    private void cmdShowGenres_Clicked(object sender, EventArgs e)
    {
        loadGenres();
        lblListado.Text = "Listado de géneros";

    }

    public void loadGenres()
    {
        var result = ApiConsumer<Genre>.Read(url + "/api/Generos");
        lstItems.ItemsSource = result;
        lblListado.Text = "Listado de géneros";
    }

  
}