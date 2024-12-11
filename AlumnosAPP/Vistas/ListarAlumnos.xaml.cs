using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using static AlumnosAPP.RegistroAlumnos.Modelos;

namespace AlumnosAPP.Vistas
{
    public partial class ListarAlumnos : ContentPage
    {
        public ObservableCollection<Estudiante> FilteredStudents { get; set; }
        private ObservableCollection<Estudiante> AllStudents { get; set; }

        public ListarAlumnos()
        {
            InitializeComponent();
            AllStudents = new ObservableCollection<Estudiante>();
            FilteredStudents = new ObservableCollection<Estudiante>();
            BindingContext = this;
        }

        public void AddStudent(Estudiante newStudent)
        {
            AllStudents.Add(newStudent);
            UpdateFilteredStudents();
        }

        private void UpdateFilteredStudents()
        {
            FilteredStudents.Clear();
            foreach (var student in AllStudents)
            {
                FilteredStudents.Add(student);
            }
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue.ToLower();
            var filteredList = new ObservableCollection<Estudiante>();

            foreach (var estudiante in AllStudents)
            {
                if (estudiante.Nombre.ToLower().Contains(searchText) || estudiante.Correo.ToLower().Contains(searchText))
                {
                    filteredList.Add(estudiante);
                }
            }

            FilteredStudents.Clear();
            foreach (var student in filteredList)
            {
                FilteredStudents.Add(student);
            }
        }

        private async void OnStudentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedStudent = e.CurrentSelection.FirstOrDefault() as Estudiante;

            if (selectedStudent != null)
            {
                var agregarEstudiantePage = new AgregarEstudiante(selectedStudent);
                agregarEstudiantePage.StudentUpdated += (s, updatedStudent) =>
                {
                    var index = AllStudents.IndexOf(selectedStudent);
                    if (index >= 0)
                    {
                        AllStudents[index] = updatedStudent;
                        UpdateFilteredStudents();
                    }
                };
                await Navigation.PushAsync(agregarEstudiantePage);
            }
        }

        private async void OnAddStudentClicked(object sender, EventArgs e)
        {
            var agregarEstudiantePage = new AgregarEstudiante();
            agregarEstudiantePage.StudentAdded += (s, newStudent) =>
            {
                AddStudent(newStudent);
            };
            await Navigation.PushAsync(agregarEstudiantePage);
        }

        private async void OnEditStudentClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var student = button?.BindingContext as Estudiante;

            if (student != null)
            {
                var agregarEstudiantePage = new AgregarEstudiante(student);
                agregarEstudiantePage.StudentUpdated += (s, updatedStudent) =>
                {
                    var index = AllStudents.IndexOf(student);
                    if (index >= 0)
                    {
                        AllStudents[index] = updatedStudent;
                        UpdateFilteredStudents();
                    }
                };
                await Navigation.PushAsync(agregarEstudiantePage);
            }
        }
    }
}
