using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Web.Domain.Dto
{
    public class PersonaDto
    {
        public int Id { get; set; }//No requerir (Generado por la DB)
        public string Nombre { get; set; } = null!; //Si
        public DateTime? FechaDeNacimiento { get; set; }//Si
    }
    public class ClienteDto
    {
        public int Id { get; set; }//No requerir (Generado por la DB)
        public int PersonaId { get; set; }//No requerir (Generado por la DB)
        public decimal LimiteDeCredito { get; set; }//Si
        public PersonaDto DatosPersonales { get; set; } = new PersonaDto();
    }

    public class EmpleadoDto
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public decimal sueldo { get; set; }
        public PersonaDto DatosPersonales { get; set; } = new PersonaDto();
    }
}
