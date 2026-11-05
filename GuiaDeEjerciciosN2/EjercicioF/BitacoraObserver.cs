namespace EjercicioF
{
    // Suscriptor: anota todo en la unica bitacora del sistema.
    public class BitacoraObserver : IPlayaObserver
    {
        public void Actualizar(EventoPlaya evento)
            => Bitacora.Instancia.Registrar(evento.Descripcion());
    }
}
