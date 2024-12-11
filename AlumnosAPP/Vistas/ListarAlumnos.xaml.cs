using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using static AlumnosAPP.RegistroAlumnos.Modelos;

namespace AlumnosAPP.Vistas
{
    public partial class ListarAlumnos : ContentPage
    {
        // ObservableCollection para la propiedad que se enlaza a la CollectionView
        public ObservableCollection<Estudiante> FilteredStudents { get; set; }

        // Lista con todos los estudiantes (sin filtrar)
        private ObservableCollection<Estudiante> AllStudents { get; set; }

        public ListarAlumnos()
        {
            InitializeComponent();  // Este método es generado automáticamente por MAUI
            AllStudents = new ObservableCollection<Estudiante>();  // Inicializamos la lista vacía de estudiantes
            FilteredStudents = new ObservableCollection<Estudiante>();  // Inicializamos FilteredStudents vacía
            BindingContext = this;  // Establecer el BindingContext para enlazar la propiedad con la UI
        }

        // Método para agregar un estudiante
        public void AddStudent(Estudiante newStudent)
        {
            // Agregar el nuevo estudiante a la lista original (AllStudents)
            AllStudents.Add(newStudent);

            // Actualizar la lista de estudiantes filtrados (FilteredStudents)
            UpdateFilteredStudents();
        }

        // Método para actualizar FilteredStudents basándose en AllStudents
        private void UpdateFilteredStudents()
        {
            FilteredStudents.Clear();
            foreach (var student in AllStudents)
            {
                FilteredStudents.Add(student);
            }
        }

        // Este método es el que se llama cuando cambia el texto de búsqueda
        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue.ToLower();
            var filteredList = new ObservableCollection<Estudiante>();

            // Filtrar los estudiantes por nombre o correo
            foreach (var estudiante in AllStudents)
            {
                if (estudiante.Nombre.ToLower().Contains(searchText) || estudiante.Correo.ToLower().Contains(searchText))
                {
                    filteredList.Add(estudiante);
                }
            }

            // Actualizar la colección de estudiantes filtrados
            FilteredStudents.Clear();
            foreach (var student in filteredList)
            {
                FilteredStudents.Add(student);
            }
        }

        // Este método se llama cuando el botón de agregar estudiante es presionado
        private async void OnAddStudentClicked(object sender, EventArgs e)
        {
            // Navegar a la página para agregar un estudiante
            var agregarEstudiantePage = new AgregarEstudiante();
            agregarEstudiantePage.StudentAdded += (s, newStudent) =>
            {
                // Cuando el estudiante es agregado, actualizamos la lista
                AddStudent(newStudent);
            };
            await Navigation.PushAsync(agregarEstudiantePage);
        }
    }
}
