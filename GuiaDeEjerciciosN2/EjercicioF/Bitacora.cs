namespace EjercicioF
{
    // SINGLETON: una unica bitacora para todo el sistema
    public class Bitacora
    {
        // Regla 2: campo estatico privado que guarda la instancia
        private static Bitacora _instancia;

        private readonly List<string> _registros = new List<string>();

        // Regla 1: constructor privado
        private Bitacora() { }

        // Regla 3: unico punto de acceso, se crea la primera vez que se pide
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
