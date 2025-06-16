using System.Collections.ObjectModel;

namespace PinedaLNotasApp.Models;

internal class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    public AllNotes() =>
        LoadNotes();

    public void LoadNotes()
    {
        Notes.Clear();

        // Obtener la carpeta de donde se guardan los datos de la aplicación
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions para carga todos los archivos
        IEnumerable<Note> notes = Directory

            // Seleccionar los archivos que terminan con .notas.txt
            .EnumerateFiles(appDataPath, "*.notas.txt")

            // De cada archivo, crear un objeto Note
            .Select(filename => new Note()
            {
                FileName = filename,
                Text = File.ReadAllText(filename),
                Date = File.GetLastWriteTime(filename)
            })

            // Ordenar los objetos Note por fecha
            .OrderBy(note => note.Date);

        // Anadir cada objeto Note a la colección ObservableCollection
        foreach (Note note in notes)
            Notes.Add(note);
    }
}