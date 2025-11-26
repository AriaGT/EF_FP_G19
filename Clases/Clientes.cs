using EF_FP_G19.Entidades;

namespace EF_FP_G19.Clases
{
    internal class G19_Clientes
    {
        public static List<G19_Cliente> listaClientes = new List<G19_Cliente>();
        public static void G19_añadirCliente(G19_Cliente cliente)
        {
            listaClientes.Add(cliente);
        }
    }
}
