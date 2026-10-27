namespace EjercicioF
{
    // SIMPLE FACTORY: el ÚNICO lugar del sistema que hace new de tarifas
    // y traduce el código de texto (HORA, ESTADIA, NOCHE) al objeto correcto.
    public static class TarifaFactory
    {
        public static ITarifa Crear(string codigo)
        {
            switch (codigo?.Trim().ToUpper())
            {
                case "HORA": return new TarifaHora();
                case "ESTADIA": return new TarifaEstadia();
                case "NOCHE": return new TarifaNocturna();
                default:
                    throw new ArgumentException($"Código de tarifa desconocido: {codigo}");
            }
        }

        public static List<string> CodigosDisponibles()
            => new List<string> { "HORA", "ESTADIA", "NOCHE" };
    }
}
