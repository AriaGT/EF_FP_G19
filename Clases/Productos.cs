using EF_FP_G19.Entidades;

namespace EF_FP_G19.Clases
{
    internal class G19_Productos
    {
        public static List<G19_Producto> listaProductos = new List<G19_Producto>();
        public static void G19_añadirProducto(G19_Producto producto)
        {
            listaProductos.Add(producto);
        }
    }
}
