using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinedaLNotasApp.Models
{
    internal class Note
    {
        public string FileName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        //Constructor
        public Note()
        {
            FileName = $"{Path.GetRandomFileName()}.notas.txt";
            Date = DateTime.Now;
            Text = "";
        }

        //Metodos para el control, guardado o eliminacion
        public void Save() =>
            File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, FileName), Text);

        public void Delete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, FileName));

        //Cargar una nota por nombre de archivo
        public static Note Load(string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return
                new()
                {
                    FileName = Path.GetFileName(filename),
                    Text = File.ReadAllText(filename),
                    Date = File.GetLastWriteTime(filename)
                };
        }

        //Enumerar notas del dispositivos y cargarlas a la clase 
        public static IEnumerable<Note> LoadAll()
        {
            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            return Directory

                    // Select the file names from the directory
                    .EnumerateFiles(appDataPath, "*.notas.txt")

                    // Each file name is used to load a note
                    .Select(filename => Note.Load(Path.GetFileName(filename)))

                    // With the final collection of notes, order them by date
                    .OrderByDescending(note => note.Date);
        }

    }
}
