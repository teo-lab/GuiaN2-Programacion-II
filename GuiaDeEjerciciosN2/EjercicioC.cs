// ==============================================
//                  SECCIÓN C 
// ==============================================

// SECCION C1 (La alarma que se rompe sola)
//
// ¿Cual es el error y de que tipo es?:
//
//   Error de EJECUCIÓN. Compila bien, pero al llamar alarma.Sonar() explota con
//   InvalidOperationException ("Collection was modified; enumeration operation
//   may not execute"). "Luz encendida" nunca se imprime.
//
// ¿Por qué ocurre?:
//
//   El foreach recorre directamente _observadores. El primer suscriptor
//   (AvisoUnico) se da de baja DENTRO de su Actualizar, o sea que modifica la
//   lista mientras el foreach la está recorriendo. Cuando el foreach quiere
//   pasar al siguiente elemento, detecta que la lista cambió y lanza la
//   excepción. Un suscriptor que se desuscribe a sí mismo es algo normal en
//   Observer, así que el notificador tiene que estar preparado.
//
// ¿Como se corrige?:
//
//   Recorriendo una COPIA de la lista: foreach (var o in _observadores.ToList()).
//   Así AvisoUnico puede sacarse de la lista original sin romper el recorrido.
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
//   Error de DISEÑO. Compila y calcula bien, pero NO es Strategy: hay una sola
//   clase "estrategia" que adentro tiene los if que eligen la fórmula según un
//   texto. Es el mismo switch de siempre, escondido en otra clase. La interfaz
//   está, pero no sirve de nada porque hay una sola implementación.
//
// ¿Qué pasa cuando llega un tipo nuevo, por ejemplo "Bicicleta"?:
//
//   Hay que ABRIR EnvioStrategy.Calcular y agregar otro if: se rompe
//   Open/Closed. Y peor: si alguien escribe "Bicicleta" hoy, no da error,
//   cae en el último return y se cobra la tarifa por defecto sin que nadie se
//   entere (lo mismo pasa con un error de tipeo como "moto" en minúscula).
//
// ¿Cómo se corrige?:
//
//   Una clase por forma de calcular, todas implementando IEnvioStrategy y sin
//   ningún if de tipos adentro. Si el tipo llega como texto, la traducción
//   texto -> objeto va en UNA factory (que además falla con un mensaje claro
//   si el tipo no existe). Sumar "Bicicleta" = una clase nueva + una línea en
//   la factory.


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
// ¿Cual es el error y de que tipo es?:
//
//   Error de COMPILACIÓN: CS0161 "not all code paths return a value". El
//   método promete devolver un INotificador, pero si canal no es "MAIL" ni
//   "SMS" llega a la llave de cierre sin devolver nada.
//
// ¿Cómo se corrige?:
//
//   Cubriendo el camino que falta: al final se lanza una ArgumentException
//   con un mensaje que diga qué valor llegó.
//
// ¿Por qué conviene lanzar una excepción con un mensaje claro y no devolver null?:
//
//   Con null el error aparece más tarde y en otro lugar: alguien hace
//   notificador.Enviar(...) y salta un NullReferenceException que no dice
//   qué pasó ni de dónde vino. La excepción falla en el momento y en el lugar
//   exacto del problema, y el mensaje ("Canal desconocido: 'WHATSAPP'") dice
//   qué arreglar. Además obliga a quien llama a manejarlo (try/catch) en vez
//   de acordarse de chequear null en todos lados.


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
