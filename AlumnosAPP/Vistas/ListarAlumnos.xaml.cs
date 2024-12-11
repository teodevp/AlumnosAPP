using System.Collections.ObjectModel;

namespace AlumnosAPP
{
    public partial class ListarAlumnos : ContentPage
    {
        private List<Estudiante> estudiantes; // Usar el tipo correcto Estudiante

        public ListarAlumnos()
        {
            InitializeComponent();
            estudiantes = new List<Estudiante>();  // Lista vac�a de estudiantes
            BindingContext = this;
            FilteredStudents = new ObservableCollection<Estudiante>(estudiantes);
        }

        // Lista de estudiantes filtrados
        public ObservableCollection<Estudiante> FilteredStudents { get; set; }

        // Evento para el cambio de texto en la barra de b�squeda
        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue.ToLower();
            FilteredStudents.Clear();

            // Filtra los estudiantes seg�n el nombre o correo
            foreach (var estudiante in estudiantes.Where(s => s.Nombre.ToString().ToLower().Contains(searchText) || s.Correo.ToLower().Contains(searchText)))
            {
                FilteredStudents.Add(estudiante);
            }
        }

        // Evento para el bot�n de agregar estudiante
        private async void OnAddStudentClicked(object sender, EventArgs e)
        {
            // Navegar a la p�gina de agregar estudiante
            await Navigation.PushAsync(new AgregarEstudiante());
        }

        // M�todo para agregar estudiante
        public void AddStudent(Estudiante newStudent)
        {
            estudiantes.Add(newStudent);
            FilteredStudents.Add(newStudent);
        }
    }

    public class Estudiante
    {
        internal int Edad;
        internal string Curso;
        internal bool Activo;

        public string Nombre { get; set; } // Cambiar el tipo a string
        public string Correo { get; set; } // Agregar la propiedad Correo
    }
}
