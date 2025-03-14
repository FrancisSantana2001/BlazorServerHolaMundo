using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Web.Domain.Entities;

[Table("Empleados")]
public class Empleado
{
    [Key]
    public int Id { get; set; }
    public int PersonaId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Sueldo { get; set; }

    [ForeignKey(nameof(PersonaId))]
    public virtual Persona DatosPersonales { get; set; } = null!;
    public static Empleado Create(
        string nombre,
        DateTime? fechaNacimiento,
        decimal sueldo)
    => new()
    {
        Sueldo = sueldo,
        DatosPersonales =
        Persona.Create(nombre, fechaNacimiento)
    };

    public bool Update(
        string nombre,
        DateTime? fechaNacimiento,
        decimal sueldo)
    {
        var change = false;
        if(DatosPersonales.Nombre != nombre)
        {
            DatosPersonales.Nombre = nombre;
            change = true;
        }
        return change;
    }
}
