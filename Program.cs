using System;
using System.Collections.Generic;

namespace AgendaPro
{
    class Persona //1Er comentario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        public Persona(int id, string nombre, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
        }
    }

    class Cita //segundo comentario
    {
        public int PersonaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }

        public Cita(int personaId, DateTime fecha, string descripcion)
        {
            PersonaId = personaId;
            Fecha = fecha;
            Descripcion = descripcion;
        }
    }

    class Program
    {
        static List<Persona> personas = new List<Persona>();
        static List<Cita> citas = new List<Cita>();

        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n=== AgendaPro - Menú ===");
                Console.WriteLine("a. Registrar persona");
                Console.WriteLine("b. Listar personas");
                Console.WriteLine("c. Crear cita para una persona");
                Console.WriteLine("d. Listar citas por PersonaId");
                Console.WriteLine("e. Mostrar todas las citas");
                Console.WriteLine("f. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion?.ToLower())
                {
                    case "a":
                        RegistrarPersona();
                        break;
                    case "b":
                        ListarPersonas();
                        break;
                    case "c":
                        CrearCita();
                        break;
                    case "d":
                        ListarCitasPorPersona();
                        break;
                    case "e":
                        MostrarTodasCitas();
                        break;
                    case "f":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intente de nuevo.");
                        break;
                }
            }

            Console.WriteLine("Saliendo... ¡Hasta luego!");
        }

        static void RegistrarPersona()
        {
            try
            {
                Console.Write("Ingrese Id (número): ");
                string idStr = Console.ReadLine();
                int id = int.Parse(idStr); 

                bool existe = personas.Exists(p => p.Id == id);
                if (existe)
                {
                    Console.WriteLine("Error: Ya existe una persona con ese Id.");
                    return;
                }

                Console.Write("Ingrese Nombre: ");
                string nombre = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(nombre))
                {
                    Console.WriteLine("Error: El nombre no puede quedar vacío.");
                    return;
                }

                Console.Write("Ingrese Teléfono: ");
                string tel = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(tel))
                {
                    Console.WriteLine("Error: El número de teléfono no puede quedar vacío.");
                    return;
                }

                Persona nueva = new Persona(id, nombre, tel);
                personas.Add(nueva);
                Console.WriteLine("Persona registrada correctamente.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Id debe ser un número entero.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al registrar la persona: " + ex.Message);
            }
        }

        static void ListarPersonas()
        {
            if (personas.Count == 0)
            {
                Console.WriteLine("No hay personas registradas.");
                return;
            }

            Console.WriteLine("\nLista de personas:");
            foreach (var p in personas)
            {
                Console.WriteLine($"Id: {p.Id} | Nombre: {p.Nombre} | Teléfono: {p.Telefono}");
            }
        }

        static void CrearCita()
        {
            try
            {
                Console.Write("Ingrese PersonaId (número): ");
                string idStr = Console.ReadLine();
                int pid = int.Parse(idStr);
                Persona persona = personas.Find(x => x.Id == pid);
                if (persona == null)
                {
                    Console.WriteLine("Error: No existe persona con ese Id.");
                    return;
                }

                Console.Write("Ingrese fecha y hora de la cita (ej: 2025-10-25 14:30): ");
                string fechaStr = Console.ReadLine();
                DateTime fecha = DateTime.Parse(fechaStr); 

                Console.Write("Ingrese descripción de la cita: ");
                string desc = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(desc))
                {
                    Console.WriteLine("Error: La descripción no puede estar vacía.");
                    return;
                }

                Cita nueva = new Cita(pid, fecha, desc);
                citas.Add(nueva);
                Console.WriteLine("Cita creada correctamente para " + persona.Nombre + ".");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Formato de número o fecha inválido. Revise e intente de nuevo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al crear la cita: " + ex.Message);
            }
        }

        static void ListarCitasPorPersona()
        {
            try
            {
                Console.Write("Ingrese PersonaId para listar sus citas: ");
                string idStr = Console.ReadLine();
                int pid = int.Parse(idStr);

                Persona persona = personas.Find(x => x.Id == pid);
                if (persona == null)
                {
                    Console.WriteLine("Error: No existe persona con ese Id.");
                    return;
                }

                var citasPersona = citas.FindAll(c => c.PersonaId == pid);
                if (citasPersona.Count == 0)
                {
                    Console.WriteLine($"No hay citas para {persona.Nombre} (Id {pid}).");
                    return;
                }

                Console.WriteLine($"\nCitas de {persona.Nombre}:");
                foreach (var c in citasPersona)
                {
                    Console.WriteLine($"Fecha: {c.Fecha} | Descripción: {c.Descripcion}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Id debe ser un número entero.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al listar las citas: " + ex.Message);
            }
        }

        static void MostrarTodasCitas()
        {
            if (citas.Count == 0)
            {
                Console.WriteLine("No hay citas registradas.");
                return;
            }

            Console.WriteLine("\nTodas las citas (PersonaId|Fecha|Descripcion):");
            foreach (var c in citas)
            {
                Console.WriteLine($"{c.PersonaId}|{c.Fecha}|{c.Descripcion}");
            }
        }
    }
}
