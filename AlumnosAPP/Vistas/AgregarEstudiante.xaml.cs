using static AlumnosAPP.RegistroAlumnos.Modelos;

namespace AlumnosAPP.Vistas
{
    public partial class AgregarEstudiante : ContentPage
    {
        // Evento para notificar cuando se agrega un estudiante
        public event EventHandler<Estudiante> StudentAdded;

        public AgregarEstudiante()
        {
            InitializeComponent();
        }

        // Método que maneja el evento de guardar un estudiante
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Obtener los valores de los controles de entrada
            string nombre = nombreEntry.Text;
            string correo = correoEntry.Text;
            int edad = int.TryParse(edadEntry.Text, out int edadResultado) ? edadResultado : 0;
            string curso = cursoEntry.Text;
            bool activo = activoSwitch.IsToggled;

            // Verificar si los valores son válidos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || edad <= 0 || string.IsNullOrEmpty(curso))
            {
                DisplayAlert("Error", "Por favor, completa todos los campos correctamente.", "OK");
                return;
            }

            // Crear un nuevo estudiante con los valores capturados
            var newStudent = new Estudiante
            {
                Nombre = nombre,
                Correo = correo,
                Edad = edad,
                Curso = curso,
                Activo = activo
            };

            // Disparar el evento para pasar el nuevo estudiante a la página de listado
            StudentAdded?.Invoke(this, newStudent);

            // Volver a la página anterior
            Navigation.PopAsync();
        }
    }
}
