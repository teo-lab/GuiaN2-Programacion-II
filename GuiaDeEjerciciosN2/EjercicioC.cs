// ==============================================
//                  SECCIÓN C 
// ==============================================

// SECCION C1 (La alarma que se rompe sola)
//
// ¿Cual es el error y de que tipo es?:
//
//   Es un error de ejecucion. Compila bien, pero al llamar alarma.Sonar() se rompe con
//   InvalidOperationException.
//
// ¿Por qué ocurre?:
//
//   Porque el foreach esta recorriendo directamente _observadores. Si el primer suscriptor
//   se da de baja dentro de su Actualizar se esta modificando la lista mientras el foreach la está recorriendo,
//   entonces cuando el foreach quiere pasar al siguiente elemento, detecta que la lista cambió y lanza la
//   excepción.
//
// ¿Como se corrige?:
//
//   Se puede corregir recorriendo una copia de la lista, asi AvisoUnico puede sacarse de la lista original sin romper el recorrido.
//
// Salida corregida:
//   ¡Riiiing!
//   Me avisaron una vez y me doy de baja
//   Luz encendida


namespace EjercicioC1
{
    public interface IAlarmaObserver
    {
        void Actualizar(Alarma alarma);
    }

    public class Alarma
    {
        private readonly List<IAlarmaObserver> _observadores = new List<IAlarmaObserver>();

        public void Suscribir(IAlarmaObserver o) => _observadores.Add(o);
        public void Desuscribir(IAlarmaObserver o) => _observadores.Remove(o);

        public void Sonar()
        {
            Console.WriteLine("¡Riiiing!");
            foreach (var o in _observadores.ToList())   // CORREGIDO: se recorre una copia
                o.Actualizar(this);
        }
    }

    public class AvisoUnico : IAlarmaObserver
    {
        public void Actualizar(Alarma alarma)
        {
            Console.WriteLine("Me avisaron una vez y me doy de baja");
            alarma.Desuscribir(this);
        }
    }

    public class Luz : IAlarmaObserver
    {
        public void Actualizar(Alarma alarma) => Console.WriteLine("Luz encendida");
    }

    public static class Demo
    {
        public static void Ejecutar()
        {
            var alarma = new Alarma();
            alarma.Suscribir(new AvisoUnico());
            alarma.Suscribir(new Luz());
            alarma.Sonar();
            Console.WriteLine("-- suena otra vez (AvisoUnico ya no está) --");
            alarma.Sonar();
        }
    }
}


// SECCION C2 (Una "Strategy" sospechosa)
//
// ¿Cual es el error y de que tipo es?:
//
//   Es un error de diseño. El código anda bien, pero en realidad no es un
//   Strategy porque hay una sola clase que decide con un if qué fórmula usar según
//   un texto. Es básicamente un switch metido en otra clase.
//
// ¿Que pasa cuando llega un tipo nuevo, por ejemplo "Bicicleta"?:
//
//   Hay que modificar EnvioStrategy y agregar otro if. Eso romperia el
//   principio de Open/Closed.
//
// ¿Como se corrige?:
//
//   Crear una clase por cada forma de calcular, todas implementando
//   IEnvioStrategy y sin un if. Si el tipo llega como texto, una factory se
//   encarga de elegir la clase.


namespace EjercicioC2
{
    public interface IEnvioStrategy
    {
        decimal Calcular(decimal pesoKg);
    }

    public class TarifaMoto : IEnvioStrategy
    {
        public decimal Calcular(decimal pesoKg) => 1500 + pesoKg * 100;
    }

    public class TarifaCamion : IEnvioStrategy
    {
        public decimal Calcular(decimal pesoKg) => 8000 + pesoKg * 40;
    }

    // El "return" final del código original: lo que se cobraba para cualquier otro tipo.
    public class TarifaComun : IEnvioStrategy
    {
        public decimal Calcular(decimal pesoKg) => 3000 + pesoKg * 70;
    }

    public static class EnvioStrategyFactory
    {
        public static IEnvioStrategy Crear(string tipo)
        {
            switch (tipo)
            {
                case "Moto": return new TarifaMoto();
                case "Camion": return new TarifaCamion();
                case "Comun": return new TarifaComun();
                default: throw new ArgumentException($"Tipo de envío desconocido: '{tipo}'.");
            }
        }
    }

    public static class Demo
    {
        public static void Ejecutar()
        {
            foreach (var tipo in new[] { "Moto", "Camion", "Comun" })
            {
                IEnvioStrategy estrategia = EnvioStrategyFactory.Crear(tipo);
                Console.WriteLine($"{tipo}, 10 kg: {estrategia.Calcular(10)}");
            }
        }
    }
}

// SECCION C3 (La factory que no compila)
//
// ¿Cuál es el error y de qué tipo es?:
//
//   Es un error de compilación. El metodo tiene que devolver un INotificador, pero si el canal no es
//   "MAIL" ni "SMS" termina sin devolver nada.
//
// ¿Cómo se corrige?:
//
//   Agregando al final un throw new ArgumentException con un mensaje que
//   diga que valor llego.
//
// ¿Por qué conviene lanzar una excepción con un mensaje claro y no devolver null?:
//
//   Si devolvés null, el error salta despues y en otro lado, cuando alguien
//   hace notificador.Enviar() aparece un NullReferenceException que no dice
//   que paso. Con una excepcion, falla justo donde está el problema y el
//   mensaje (por ejemplo "Canal desconocido: 'WHATSAPP'") ya te dice que
//   arreglar.

namespace EjercicioC3
{
    public interface INotificador
    {
        void Enviar(string destino, string mensaje);
    }

    public class NotificadorMail : INotificador
    {
        public void Enviar(string destino, string mensaje)
            => Console.WriteLine($"[Mail a {destino}] {mensaje}");
    }

    public class NotificadorSms : INotificador
    {
        public void Enviar(string destino, string mensaje)
            => Console.WriteLine($"[SMS a {destino}] {mensaje}");
    }

    public static class NotificadorFactory
    {
        public static INotificador Crear(string canal)
        {
            if (canal == "MAIL") return new NotificadorMail();
            if (canal == "SMS") return new NotificadorSms();
            throw new ArgumentException($"Canal desconocido: '{canal}'.");   // CORREGIDO
        }
    }

    public static class Demo
    {
        public static void Ejecutar()
        {
            NotificadorFactory.Crear("MAIL").Enviar("ana@mail.com", "Tu pedido salió");
            NotificadorFactory.Crear("SMS").Enviar("223-555-1234", "Tu pedido salió");
            try
            {
                NotificadorFactory.Crear("WHATSAPP");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
