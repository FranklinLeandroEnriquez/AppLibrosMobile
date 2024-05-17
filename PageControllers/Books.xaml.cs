using AppLibrosMobile.ApiConsumer;
using AppLibrosMobile.Model;

namespace AppLibrosMobile.PageControllers;
using AppLibrosMobile.ApiConsumer;
using AppLibrosMobile.Model;
using CommunityToolkit.Maui.Alerts;

public partial class Books : ContentPage
{
	public Books()
	{
		InitializeComponent();
        loadBooks();
    }

    private String url = "https://apiconsumermmovil.azurewebsites.net";
    private CancellationTokenSource cancellation = new CancellationTokenSource();

    public void loadBooks()
	{
		var books = ApiConsumer<Book>.Read(url + "/api/Libros");
        lstItems.ItemsSource = books;
        lblListado.Text = "Listado de Libros";
    }

    private async void cmdGetbyIdBook_Clicked(object sender, EventArgs e)
    {
        int id = txtId != null ? Convert.ToInt32(txtId.Text) : 0;
        var result = ApiConsumer<Book>.Read(url + "/api/Libros", id);

        if (result.Titulo == null)
        {
            txtTitle.Text = "";
            txtAutor.Text = "";
            txtEditorial.Text = "";
            txtAnio.Text = "";
            txtPaginas.Text = "";
            txtGenero.Text = "";
            var toast = Toast.Make("Book not found");
            await toast.Show(cancellation.Token);
            return;
        }

        txtTitle.Text = result.Titulo;
        txtAutor.Text = result.Autor;
        txtEditorial.Text = result.Editorial;
        txtAnio.Text = result.Anio.ToString();
        txtPaginas.Text = result.Paginas.ToString();
        txtGenero.Text = result.GeneroId.ToString();

        var toast2 = Toast.Make("Book found");
        await toast2.Show(cancellation.Token);
    }

    private async void cmdPostBook_Clicked(object sender, EventArgs e)
    {
        Book book = new Book
        {
            Titulo = txtTitle.Text,
            Autor = txtAutor.Text,
            Editorial = txtEditorial.Text,
            Anio = Convert.ToInt32(txtAnio.Text),
            Paginas = Convert.ToInt32(txtPaginas.Text),
            GeneroId = Convert.ToInt32(txtGenero.Text)
        };

        var result = ApiConsumer<Book>.Create(url + "/api/Libros", book);
        if (result.Titulo == null)
        {
            var toast = Toast.Make("Book not created");
            await toast.Show(cancellation.Token);
            return;
        }

        loadBooks();

        var txtId = result.Id.ToString();
        var toast2 = Toast.Make("Book created with id: " + txtId);
        await toast2.Show(cancellation.Token);

    }

    private async void cmdPutBook_Clicked(object sender, EventArgs e)
    {
        Book book = new Book
        {
            Id = int.Parse(txtId.Text),
            Titulo = txtTitle.Text,
            Autor = txtAutor.Text,
            Editorial = txtEditorial.Text,
            Anio = Convert.ToInt32(txtAnio.Text),
            Paginas = Convert.ToInt32(txtPaginas.Text),
            GeneroId = Convert.ToInt32(txtGenero.Text)
        };
        var result = ApiConsumer<Book>.Update(url + "/api/Libros", book.Id, book);
        loadBooks();

        var toast2 = Toast.Make("Book updated");
        await toast2.Show(cancellation.Token);

    }

    private async void cmdDeleteBook_Clicked(object sender, EventArgs e)
    {

        if (sender is Button button && button.CommandParameter is int bookid)
        {
            ApiConsumer<Book>.Delete(url + "/api/Libros", bookid);
            loadBooks();
            var toast2 = Toast.Make("Book deleted");
            await toast2.Show(cancellation.Token);
           
        }

    }
}