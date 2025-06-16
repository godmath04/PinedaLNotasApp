namespace PinedaLNotasApp.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
    private string _fileName;

    public NotePage()
    {
        InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        _fileName = Path.Combine(appDataPath, $"{DateTime.Now:yyyyMMdd_HHmmss}.notas.txt");


        LoadNote(_fileName);
    }

    private void LoadNote(string fileName)
    {
        var noteModel = new Models.Note
        {
            FileName = fileName
        };

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }

    public string ItemId
    {
        set { LoadNote(value); }
    }

    private async void btnGuardar(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
            File.WriteAllText(note.FileName, txtEditor.Text);

        await Shell.Current.GoToAsync("..");
    }

    private async void btnEliminar(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            // Delete the file.
            if (File.Exists(note.FileName))
                File.Delete(note.FileName);
        }

        await Shell.Current.GoToAsync("..");
    }
}
