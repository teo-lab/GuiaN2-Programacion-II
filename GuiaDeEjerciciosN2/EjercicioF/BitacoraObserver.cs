namespace EjercicioF
{
    // Suscriptor: anota todo en la bitácora única del sistema (usa el Singleton).
    public class BitacoraObserver : IPlayaObserver
    {
        public void Actualizar(EventoPlaya evento)
            => Bitacora.Instancia.Registrar(evento.Descripcion());
    }
}
