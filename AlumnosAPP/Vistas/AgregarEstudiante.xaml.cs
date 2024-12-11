using System;
using Microsoft.Maui.Controls;
using static AlumnosAPP.RegistroAlumnos.Modelos;

namespace AlumnosAPP.Vistas
{
    public partial class AgregarEstudiante : ContentPage
    {
        public event EventHandler<Estudiante> StudentAdded;
        public event EventHandler<Estudiante> StudentUpdated;

        public AgregarEstudiante()
        {
            InitializeComponent();
        }

        public AgregarEstudiante(Estudiante student)
        {
            InitializeComponent();
            nombreEntry.Text = student.Nombre;
            correoEntry.Text = student.Correo;
            edadEntry.Text = student.Edad.ToString();
            cursoEntry.Text = student.Curso;
            activoSwitch.IsToggled = student.Activo;

        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var estudiante = new Estudiante
            {
                Nombre = nombreEntry.Text,
                Correo = correoEntry.Text,
                Edad = int.Parse(edadEntry.Text),
                Curso = cursoEntry.Text,
                Activo = activoSwitch.IsToggled
            };

            if (StudentUpdated != null)
                StudentUpdated(this, estudiante);

            if (StudentAdded != null)
                StudentAdded(this, estudiante);

            Navigation.PopAsync();  
        }
    }
}
