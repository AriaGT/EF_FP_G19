using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_FP_G19.Entidades
{
    public class G19_Cliente
    {
        public string G19_NombreCliente { get; set; }
        public string G19_ApellidosCliente { get; set; }
        public int G19_DniCliente { get; set; }
        public int G19_CelularCliente { get; set; }
        public double G19_TotalGastadoDineroCliente { get; set; }

        public G19_Cliente(string nombre, string apellidos, int dni, int celular)
        {
            this.G19_NombreCliente = nombre;
            this.G19_ApellidosCliente = apellidos;
            this.G19_DniCliente = dni;
            this.G19_CelularCliente = celular;
            this.G19_TotalGastadoDineroCliente = 0;
        }

        public override string ToString()
        {
            return $"{G19_NombreCliente}|{G19_ApellidosCliente}|{G19_DniCliente}|{G19_CelularCliente}|{G19_TotalGastadoDineroCliente}";
        }
    }

}
