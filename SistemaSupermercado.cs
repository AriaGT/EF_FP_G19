namespace EF_FP_G19
{
    public partial class SistemaSupermercado : Form
    {
        public SistemaSupermercado()
        {
            InitializeComponent();
        }

        private int G19_contarDigitos(int numeros)
        {
            if (numeros < 10)
            {
                return 1;
            } else
            {
                return 1 + G19_contarDigitos(numeros / 10);
            }
        }

        private void G19_btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            string G19_nombreCliente = G19_txtNombreCliente.Text;
            string G19_apellidosCliente = G19_txtApellidosCliente.Text;
            string G19_dniCliente = G19_txtDniCliente.Text;
            string G19_celularCliente = G19_txtCelularCliente.Text;

            if (string.IsNullOrWhiteSpace(G19_nombreCliente) ||
                string.IsNullOrWhiteSpace(G19_apellidosCliente) ||
                string.IsNullOrWhiteSpace(G19_dniCliente) ||
                string.IsNullOrWhiteSpace(G19_celularCliente))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            try
            {
                int dni = int.Parse(G19_dniCliente);

                if (G19_contarDigitos(dni) != 8)
                {
                    MessageBox.Show("El DNI debe tener 8 dígitos.");
                    return;
                }

                int celular = int.Parse(G19_celularCliente);

                if (G19_contarDigitos(celular) != 9)
                {
                    MessageBox.Show("El celular debe tener 9 dígitos.");
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("El DNI y/o el celular deben contener solo números.");
                return;
            }

            MessageBox.Show("Cliente registrado correctamente.");
        }
    }
}
