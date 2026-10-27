namespace EjercicioF
{
    // SINGLETON: una única bitácora para todo el sistema.
    public class Bitacora
    {
        // Regla 2: campo estático privado que guarda LA instancia.
        private static Bitacora _instancia;

        private readonly List<string> _registros = new List<string>();

        // Regla 1: constructor privado -> nadie de afuera puede hacer new Bitacora().
        private Bitacora() { }

        // Regla 3: único punto de acceso. Lazy: se crea la primera vez que se pide.
        public static Bitacora Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new Bitacora();
                return _instancia;
            }
        }

        public void Registrar(string texto)
        {
            int numero = _registros.Count + 1;
            _registros.Add($"{numero:00}. {texto}");
        }

        public void MostrarTodo()
        {
            Console.WriteLine("--- Bitácora ---");
            foreach (var registro in _registros)
                Console.WriteLine(registro);
        }
    }
}
