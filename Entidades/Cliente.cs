namespace EF_FP_G19.Entidades
{
    public class G19_Cliente
    {
        public string G19_NombreCliente { get; set; }
        public string G19_ApellidosCliente { get; set; }
        public int G19_DniCliente { get; set; }
        public int G19_CelularCliente { get; set; }
        public double G19_TotalGastadoDineroCliente { get; set; }
        public List<G19_Asignacion> G19_ProductosAsignados { get; set; }

        public G19_Cliente(string nombre, string apellidos, int dni, int celular)
        {
            this.G19_NombreCliente = nombre;
            this.G19_ApellidosCliente = apellidos;
            this.G19_DniCliente = dni;
            this.G19_CelularCliente = celular;
            this.G19_TotalGastadoDineroCliente = 0;
            this.G19_ProductosAsignados = new List<G19_Asignacion>();
        }
        public void G19_AñadirAsignacion(G19_Asignacion asignacion)
        {
            G19_ProductosAsignados.Add(asignacion);
            G19_RecalcularTotalGastado();
        }
        public void G19_RecalcularTotalGastado()
        {
            G19_TotalGastadoDineroCliente = G19_ProductosAsignados.Sum(a => a.G19_Importe());
        }
        public double G19_CalcularTotalGastado()
        {
            return G19_ProductosAsignados.Sum(a => a.G19_Importe());
        }

        public override string ToString()
        {
            return $"{G19_NombreCliente}|{G19_ApellidosCliente}|{G19_DniCliente}|{G19_CelularCliente}|{G19_TotalGastadoDineroCliente}";
        }
    }

}
