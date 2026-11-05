namespace EjercicioF
{
    public class Playa
    {
        private readonly int _capacidad;

        private readonly Dictionary<string, ITarifa> _vehiculos = new Dictionary<string, ITarifa>();

        private readonly List<IPlayaObserver> _observadores = new List<IPlayaObserver>();

        public Playa(int capacidad)
        {
            if (capacidad <= 0)
                throw new ArgumentException("La capacidad tiene que ser mayor a cero.");
            _capacidad = capacidad;
        }

        public int LugaresLibres => _capacidad - _vehiculos.Count;

        // ---------- Observer: alta, baja y aviso ----------

        public void Suscribir(IPlayaObserver observador)
        {
            if (!_observadores.Contains(observador))
                _observadores.Add(observador);
        }

        public void Desuscribir(IPlayaObserver observador) => _observadores.Remove(observador);

        private void Notificar(EventoPlaya evento)
        {
            // Se recorre una COPIA: si un suscriptor se da de baja durante el aviso,
            // la lista original cambia pero el recorrido no se rompe.
            foreach (var observador in _observadores.ToList())
                observador.Actualizar(evento);
        }

        // ---------- Lógica de negocio ----------

        public void Ingresar(string patente, string codigoTarifa)
        {
            if (string.IsNullOrWhiteSpace(patente))
                throw new InvalidOperationException("La patente no puede estar vacía.");

            patente = patente.Trim().ToUpper();

            if (_vehiculos.ContainsKey(patente))
                throw new InvalidOperationException($"El vehículo {patente} ya está en la playa.");

            if (LugaresLibres == 0)
                throw new InvalidOperationException("La playa está llena.");

            ITarifa tarifa = TarifaFactory.Crear(codigoTarifa);
            _vehiculos.Add(patente, tarifa);

            Notificar(new EventoPlaya(TipoEventoPlaya.Ingreso, patente, 0, LugaresLibres));
        }

        public decimal Egresar(string patente, int minutos)
        {
            if (string.IsNullOrWhiteSpace(patente))
                throw new InvalidOperationException("La patente no puede estar vacía.");
            if (minutos < 0)
                throw new ArgumentException("Los minutos no pueden ser negativos.");

            patente = patente.Trim().ToUpper();

            if (!_vehiculos.TryGetValue(patente, out ITarifa tarifa))
                throw new InvalidOperationException($"El vehículo {patente} no está en la playa.");

            decimal importe = tarifa.Calcular(minutos);
            _vehiculos.Remove(patente);

            Notificar(new EventoPlaya(TipoEventoPlaya.Egreso, patente, importe, LugaresLibres));
            return importe;
        }
    }
}
