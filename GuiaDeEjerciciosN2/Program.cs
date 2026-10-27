using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-AR");
CultureInfo.CurrentCulture = new CultureInfo("es-AR");

Titulo("B1 — Singleton: ¿cuántos contadores hay?");
EjercicioB1.Demo.Ejecutar();

Titulo("B2 — Strategy: la cuenta del restaurante");
EjercicioB2.Demo.Ejecutar();

Titulo("B3 — Observer: el canal y sus suscriptores");
EjercicioB3.Demo.Ejecutar();

Titulo("C1 — La alarma (corregida)");
EjercicioC1.Demo.Ejecutar();

Titulo("C2 — Strategy bien aplicada");
EjercicioC2.Demo.Ejecutar();

Titulo("C3 — La factory (corregida)");
EjercicioC3.Demo.Ejecutar();

Titulo("D1 — Observer: el termómetro de la cocina");
EjercicioD1.Demo.Ejecutar();

Titulo("D2 — Factory + Strategy: descuentos");
EjercicioD2.Demo.Ejecutar();

Titulo("F — Playa de estacionamiento Plaza Colón");
EjercicioF.Demo.Ejecutar();

static void Titulo(string texto)
{
    Console.WriteLine();
    Console.WriteLine("==================================================");
    Console.WriteLine(texto);
    Console.WriteLine("==================================================");
}
