using EF_FP_G19.Clases;
using EF_FP_G19.Entidades;

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
            return numeros < 10 ? 1 : 1 + G19_contarDigitos(numeros / 10);
        }
        private void G19_refrescarListaClientes()
        {
            G19_lstClientes.Items.Clear();
            foreach (var cliente in G19_Clientes.listaClientes)
            {
                var item = new ListViewItem(cliente.G19_NombreCliente);
                item.SubItems.Add(cliente.G19_ApellidosCliente);
                item.SubItems.Add(cliente.G19_DniCliente.ToString());
                item.SubItems.Add(cliente.G19_CelularCliente.ToString());
                item.SubItems.Add(cliente.G19_CalcularTotalGastado().ToString());
                item.Tag = cliente;
                G19_lstClientes.Items.Add(item);
            }
        }
        private void G19_refrescarListaAsignarCliente()
        {
            G19_cmbClienteAsignar.Items.Clear();
            foreach (var cliente in G19_Clientes.listaClientes)
            {
                G19_cmbClienteAsignar.Items.Add($"{cliente.G19_NombreCliente} {cliente.G19_ApellidosCliente} - DNI: {cliente.G19_DniCliente}");
            }
        }
        private void G19_refrescarListaProductos()
        {
            G19_lstProductos.Items.Clear();
            foreach (var producto in G19_Productos.listaProductos)
            {
                var item = new ListViewItem(producto.G19_CodigoProducto.ToString());
                item.SubItems.Add(producto.G19_NombreProducto);
                item.SubItems.Add(producto.G19_CategoriaProducto);
                item.SubItems.Add(producto.G19_PrecioProducto.ToString());
                item.SubItems.Add(producto.G19_StockProducto.ToString());
                item.Tag = producto;
                G19_lstProductos.Items.Add(item);
            }
        }
        private void G19_refrescarListaAsignarProducto()
        {
            G19_cmbProductoAsignar.Items.Clear();
            foreach (var producto in G19_Productos.listaProductos)
            {
                G19_cmbProductoAsignar.Items.Add($"{producto.G19_NombreProducto} - Código: {producto.G19_CodigoProducto}");
            }
        }

        private void SistemaSupermercado_Load(object sender, EventArgs e)
        {
            G19_refrescarListaAsignarCliente();
            G19_refrescarListaAsignarProducto();
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
            G19_Cliente nuevoCliente = new G19_Cliente
            (
                G19_nombreCliente,
                G19_apellidosCliente,
                int.Parse(G19_dniCliente),
                int.Parse(G19_celularCliente)
            );
            G19_Clientes.G19_añadirCliente(nuevoCliente);
            G19_refrescarListaClientes();
            G19_refrescarListaAsignarCliente();
            MessageBox.Show("Cliente registrado correctamente.");
        }

        private void G19_btnRegistrarProducto_Click(object sender, EventArgs e)
        {
            string G19_codigoProducto = G19_txtCodigoProducto.Text;
            string G19_nombreProducto = G19_txtNombreProducto.Text;
            string G19_categoriaProducto = G19_cmbCategoríaProducto.Text;
            string G19_precioProducto = G19_txtPrecioProducto.Text;
            string G19_stockProducto = G19_txtStockProducto.Text;
            if (string.IsNullOrWhiteSpace(G19_codigoProducto) ||
                string.IsNullOrWhiteSpace(G19_nombreProducto) ||
                string.IsNullOrWhiteSpace(G19_categoriaProducto) ||
                string.IsNullOrWhiteSpace(G19_precioProducto) ||
                string.IsNullOrWhiteSpace(G19_stockProducto))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }
            try
            {
                int codigo = int.Parse(G19_codigoProducto);
                double precio = double.Parse(G19_precioProducto);
                int stock = int.Parse(G19_stockProducto);
            }
            catch (FormatException)
            {
                MessageBox.Show("El código, el precio y/o el stock deben contener solo números.");
                return;
            }
            G19_Producto G19_nuevoProducto = new G19_Producto
            (
                int.Parse(G19_codigoProducto),
                G19_nombreProducto,
                G19_categoriaProducto,
                double.Parse(G19_precioProducto),
                int.Parse(G19_stockProducto)
            );
            G19_Productos.G19_añadirProducto(G19_nuevoProducto);
            G19_refrescarListaProductos();
            G19_refrescarListaAsignarProducto();
            MessageBox.Show("Producto registrado correctamente.");
        }

        private void G19_btnAsignarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                G19_Cliente G19_clienteSeleccionado;
                G19_Producto G19_productoSeleccionado;
                int G19_cantidadProducto = int.Parse(G19_numCantidadAsignar.Value.ToString());
                if (G19_cmbClienteAsignar.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un cliente.");
                    return;
                }
                else
                {
                    G19_clienteSeleccionado = G19_Clientes.listaClientes[G19_cmbClienteAsignar.SelectedIndex];
                }
                if (G19_cmbProductoAsignar.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un producto.");
                    return;
                }
                else
                {
                    G19_productoSeleccionado = G19_Productos.listaProductos[G19_cmbProductoAsignar.SelectedIndex];
                }
                if (G19_cantidadProducto <= 0)
                {
                    MessageBox.Show("La cantidad debe ser mayor a cero.");
                    return;
                }
                if (G19_productoSeleccionado.G19_StockProducto < G19_cantidadProducto)
                {
                    MessageBox.Show("No hay suficiente stock del producto seleccionado.");
                    return;
                }
                G19_Asignacion nuevaAsignacion = new G19_Asignacion(
                    G19_productoSeleccionado.G19_CodigoProducto,
                    G19_cantidadProducto,
                    G19_productoSeleccionado.G19_StockProducto);
                G19_clienteSeleccionado.G19_AñadirAsignacion(nuevaAsignacion);
                G19_productoSeleccionado.G19_StockProducto -= G19_cantidadProducto;
                G19_refrescarListaClientes();
                G19_refrescarListaProductos();
                MessageBox.Show("Producto asignado al cliente correctamente.");
            }
            catch (FormatException)
            {
                MessageBox.Show("La cantidad debe ser un número válido.");
                return;
            }
        }
    }
}
