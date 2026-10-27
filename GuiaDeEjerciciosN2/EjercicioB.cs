// ========================================================
//                     SECCION B 
// ========================================================

// 
// SECCION B1 (Singleton: ¿cuantos contadores hay?)
//
// ¿Que imprime?:
//   Creando contador
//   1
//   2
//   3
//   True
//
// ¿Cuantas veces aparece "Creando contador" y por que?:
//
//   Una sola vez. La primera vez que se pide Contador.Instancia, _instancia es
//   null y se ejecuta el constructor. Las siguientes veces _instancia ya tiene
//   un objeto, asi que el if no entra y se devuelve el mismo.
//
// ¿Por que b.Siguiente() no devuelve 1?:
//
//   Porque a y b no son dos contadores: son dos variables que apuntan al MISMO
//   objeto. a.Siguiente() deja _valor en 1, y b.Siguiente() lo sube a 2.
//   La tercera llamada tambien usa ese mismo objeto: 3. Por eso ReferenceEquals(a, b) da True.


namespace EjercicioB1
{
    public class Contador
    {
        private static Contador _instancia;
        private int _valor = 0;

        private Contador() { Console.WriteLine("Creando contador"); }

        public static Contador Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new Contador();
                return _instancia;
            }
        }

        public int Siguiente()
        {
            _valor++;
            return _valor;
        }
    }

    public static class Demo
    {
        public static void Ejecutar()
        {
            var a = Contador.Instancia;
            var b = Contador.Instancia;
            Console.WriteLine(a.Siguiente());
            Console.WriteLine(b.Siguiente());
            Console.WriteLine(Contador.Instancia.Siguiente());
            Console.WriteLine(ReferenceEquals(a, b));
        }
    }
}


// SECCION B2 (Strategy: la cuenta del restaurante)
//
// ¿Que imprime?:
//   Sin propina: 20000
//   10%: 22000
//   Fija: 21500
//   (el formato {final:0} no pone separador de miles)
//
// ¿Quien es el contexto y quienes son las estrategias?:
//
//   Contexto: Cuenta (guarda un IPropina y le delega el calculo en Mostrar).
//   Interfaz estrategia: IPropina.
//   Estrategias concretas: SinPropina, Propina10 y PropinaFija.
//   El cliente es Program.cs, que elige que estrategia darle.
//
// ¿Que hubo que modificar de Cuenta para usar PropinaFija? ¿Que principio?:
//
//   No se modifico nada. Cuenta solo conoce IPropina; PropinaFija es una clase nueva que
//   cumple ese contrato. Se el cumple principio de Open/Closed: el sistema se extiende
//   agregando una clase, sin abrir una que ya funcionaba.


namespace EjercicioB2
{
    public interface IPropina
    {
        string Nombre { get; }
        decimal Calcular(decimal total);
    }

    public class SinPropina : IPropina
    {
        public string Nombre => "Sin propina";
        public decimal Calcular(decimal total) => 0;
    }

    public class Propina10 : IPropina
    {
        public string Nombre => "10%";
        public decimal Calcular(decimal total) => total * 0.10m;
    }

    public class PropinaFija : IPropina
    {
        private readonly decimal _monto;
        public PropinaFija(decimal monto) => _monto = monto;
        public string Nombre => "Fija";
        public decimal Calcular(decimal total) => _monto;
    }

    public class Cuenta
    {
        private IPropina _propina;
        public Cuenta(IPropina propina) => _propina = propina;
        public void CambiarPropina(IPropina propina) => _propina = propina;

        public void Mostrar(decimal total)
        {
            decimal final = total + _propina.Calcular(total);
            Console.WriteLine($"{_propina.Nombre}: {final:0}");
        }
    }

    public static class Demo
    {
        public static void Ejecutar()
        {
            var cuenta = new Cuenta(new SinPropina());
            cuenta.Mostrar(20000);
            cuenta.CambiarPropina(new Propina10());
            cuenta.Mostrar(20000);
            cuenta.CambiarPropina(new PropinaFija(1500));
            cuenta.Mostrar(20000);
        }
    }
}


// SECCION B3 (Observer: el canal y sus suscriptores)
//
// ¿Que imprime?:
//   Nuevo video: Patrones en C#
//     Lu mira Patrones en C#
//     Tomi mira Patrones en C#
//     Avisos registrados: 1
//   Nuevo video: Observer en WinForms
//     Tomi mira Observer en WinForms
//     Avisos registrados: 2
//
// ¿Por que Lu no aparece en la segunda publicación?:
//
//   Porque canal.Desuscribir(lu) la sacó de la lista y Publicar recorre la
//   lista tal como esta en ese momento: quedan Tomi y Estadisticas.
//  
//
// ¿Canal conoce la clase Fan? ¿Como te das cuenta?:
//
//   No, canal no conoce la clase Fan. En todo el codigo de Canal la palabra "Fan" no aparece: la lista es
//   List<IObservador> y los metodos reciben IObservador. Solo conoce el contrato. El que conoce a Fan es Program.cs, que hace los new.



namespace EjercicioB3
{
    public interface IObservador
    {
        void Actualizar(string video);
    }

    public class Canal
    {
        private readonly List<IObservador> _suscriptores = new List<IObservador>();

        public void Suscribir(IObservador o) => _suscriptores.Add(o);
        public void Desuscribir(IObservador o) => _suscriptores.Remove(o);

        public void Publicar(string video)
        {
            Console.WriteLine($"Nuevo video: {video}");
            foreach (var s in _suscriptores.ToList())
                s.Actualizar(video);
        }
    }

    public class Fan : IObservador
    {
        private readonly string _nombre;
        public Fan(string nombre) => _nombre = nombre;
        public void Actualizar(string video) => Console.WriteLine($"  {_nombre} mira {video}");
    }

    public class Estadisticas : IObservador
    {
        private int _avisos = 0;
        public void Actualizar(string video)
        {
            _avisos++;
            Console.WriteLine($"  Avisos registrados: {_avisos}");
        }
    }

    public static class Demo
    {
        public static void Ejecutar()
        {
            var canal = new Canal();
            var lu = new Fan("Lu");
            canal.Suscribir(lu);
            canal.Suscribir(new Fan("Tomi"));
            canal.Suscribir(new Estadisticas());

            canal.Publicar("Patrones en C#");
            canal.Desuscribir(lu);
            canal.Publicar("Observer en WinForms");
        }
    }
}

