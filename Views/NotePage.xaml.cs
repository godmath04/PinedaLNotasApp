namespace PinedaLNotasApp.Views;

public partial class NotePage : ContentPage
{
	string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notas.txt");
	public NotePage()
	{
		InitializeComponent();

		if(File.Exists(_fileName))
			txtEditor.Text = File.ReadAllText(_fileName);
	}

	private void btnGuardar(Object sender, EventArgs e) { 
		File.WriteAllText(_fileName, txtEditor.Text);
	}

	private void btnEliminar(Object sender, EventArgs e) {
		if(File.Exists(_fileName))
			File.Delete(_fileName);

		txtEditor.Text = string.Empty;

	}
}