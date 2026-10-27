// ============================================
//                SECCION A 
// ============================================



// Pregunta A1
// Respuesta: Opcion C
//
// ¿Por que?: La regla que impide que otra clase escriba new AppLogger() es el constructor "private AppLogger()", ya que al ser privado no se pude acceder desde otra clase.
// Opcion descartada: Opcion D, ya que una clase estatica nunca se puede instanciar.


// Pregunta A2
// Respuesta: Opcion A
//
// ¿Por que?: La propiedad instancia tiene un if que verfica si _instancia es null, si es null crea la instancia, sino devuelve la misma instancia. 
// Opcion descartada: Opcion C, ya que si se creara una instancia cada vez que se accede a la propiedad, dejararia de ser Singleton.


// Pregunta A3
// Respuesta: Opcion D
//
// ¿Por que?: Strategy reemplaza un condicional que elije entre variantes de un algoritmo por una clase por cada variante, todas detras de la misma interfaz.
// Opcion descartada: Opcion B, ya que avisarle a varios objetos que algo cambio es el patron Observer.


// Pregunta A4
// Respuesta: Opcion B
//
// ¿Por que?: El contexto (Cuenta, CalculadoraPrecio) tiene una referencia a la interfaz y le delega el calculo. No sabe que clase concreta hay detras.
// Opcion descartada: Opcion A, ya que tener todas las formulas en un mismo metodo es lo que se quiere evitar con el patron Strategy. 

// Pregunta A5
// Respuesta: Opcion C
//
// ¿Por que?: El medio de pago llega como texto (“QR”, “EFECTIVO”) desde un ComboBox y desde un JSON, y hay que crear el objeto correcto en varios lugares.


// Pregunta A6
// Respuesta: Opcion A
//
// ¿Por que?: En Factory Method, una superclase con logica de negocio declara el metodo de creacion y la subclase decide que producto concreto crear.
// Opcion descartada: Opcion C, ya que la clase static con switch describe a la Simple Factory, no al Factory Method.


// Pregunta A7
// Respuesta: Opcion B
//
// ¿Por que?: El notificador guarda referencias a objetos a través de la interfaz suscriptora, así no depende de clases concretas.
// Opcion descartada: Opcion A, ya que guardar listas de clases concretas lo acopla a cada suscriptor y obliga a modificarlo para agregar uno nuevo.


// Pregunta A8
// Respuesta: Opcion D
//
// ¿Por que?: El controller le sigue avisando a un formulario destruido: puede saltar una excepción y el objeto nunca se libera de memoria.
// Opcion descartada: Opcion C, ya que cerrar el formulario no lo saca de la lista del controller, la referencia sigue estando ahi.


// Pregunta A9
// Respuesta: Opcion B
//
// ¿Por que?: btnGuardar es el notificador, Click es el evento y btnGuardar_Click es el suscriptor.
// Opcion descartada: Opcion A, ya que el notificador es btnGuardar, no el método que maneja el evento.


// Pregunta A10
// Respuesta: Opcion C
//
// ¿Por que?: Se agrega una clase nueva que implementa la interfaz suscriptora y se suscribe en Program.cs, sin modificar el codigo que ya funcionaba (Open/Closed).
// Opcion descartada: Opcion B, ya que agregar un if en cada suscriptor obliga a modificar codigo que ya funciona y mezcla responsabilidades que no les corresponden.

