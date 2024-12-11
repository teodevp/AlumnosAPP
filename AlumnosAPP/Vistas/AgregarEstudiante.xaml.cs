namespace AlumnosAPP.Vistas
{
    public partial class AgregarEstudiante : ContentPage
    {
        // Definir los controles de la UI
        Entry nombreEntry;
        Entry correoEntry;
        Entry edadEntry;
        Entry cursoEntry;
        Switch activoSwitch;

        public AgregarEstudiante()
        {
            InitializeComponent();

            // Inicializar los controles si no se inicializan en XAML
            nombreEntry = new Entry { Placeholder = "Nombre Completo" };
            correoEntry = new Entry { Placeholder = "Correo Electrónico" };
            edadEntry = new Entry { Placeholder = "Edad", Keyboard = Keyboard.Numeric };
            cursoEntry = new Entry { Placeholder = "Curso" };
            activoSwitch = new Switch();

            // Agregar controles al layout
            Content = new StackLayout
            {
                Children = {
                    nombreEntry,
                    correoEntry,
                    edadEntry,
                    cursoEntry,
                    activoSwitch,
                    new Label { Text = "Activo", VerticalOptions = LayoutOptions.Center },
                    new Button { Text = "Guardar", Command = new Command(OnSaveButtonClicked) }
                }
            };
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private async void OnSaveButtonClicked()
        {
            // Crear un nuevo estudiante con los datos de los controles
            var newStudent = new Estudiante
            {
                Nombre = nombreEntry.Text,
                Correo = correoEntry.Text,
                Edad = int.TryParse(edadEntry.Text, out int edad) ? edad : 0,
                Curso = cursoEntry.Text,
                Activo = activoSwitch.IsToggled
            };

            // Lógica para agregar el estudiante a la lista
            if (Navigation.NavigationStack.Count > 1)
            {
                var listarAlumnosPage = (ListarAlumnos)Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                listarAlumnosPage.AddStudent(newStudent);
            }

            // Regresar a la página anterior
            await Navigation.PopAsync();
        }
    }
}
