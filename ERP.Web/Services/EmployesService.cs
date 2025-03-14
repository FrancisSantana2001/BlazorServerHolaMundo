namespace ERP.Web.Services;

using ERP.Web.Components.Pages;
using ERP.Web.Data;
using ERP.Web.Domain.Dto;
using ERP.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;

   public interface IEmployesService
   {
        Task<List<EmpleadoDto>> Consultar(string Filtro);
        Task<bool> Crear(EmpleadoDto request);
        Task<bool> Eliminar(int Id);
        Task<(bool save, string message)> Actualizar(EmpleadoDto request);
   }

    public class EmployeService : IEmployesService
    {
        private readonly AppDbContext _Context;
        
        public EmployeService(AppDbContext Context)
        {
            _Context = Context;
        }
        
        public async Task<List<EmpleadoDto>> Consultar(string filtro)
        {
            var Empleados = await _Context.Empleados.Include(e => e.DatosPersonales)
            .Where(e => e.DatosPersonales.Nombre.Contains(filtro))
            .Select(
            e => new EmpleadoDto()
            {
                Id = e.Id,
                PersonaId = e.PersonaId,
                sueldo = e.Sueldo,
                DatosPersonales = new PersonaDto()
                {
                    Id = e.DatosPersonales.Id,
                    Nombre = e.DatosPersonales.Nombre,
                    FechaDeNacimiento = e.DatosPersonales.FechaDeNacimiento
                }

            }).ToListAsync();
            return Empleados;
        }

        public async Task<bool> Crear(EmpleadoDto request)
        {
            var empleado = Empleado.Create(
            request.DatosPersonales.Nombre,
            request.DatosPersonales.FechaDeNacimiento,
            request.sueldo
            );

             _Context.Empleados.Add(empleado);
            return (await _Context.SaveChangesAsync()) > 0;
        }
        

        public async Task<(bool save,string message)> Actualizar(EmpleadoDto request)
        {
            var Empleado = await _Context.Empleados.Include(e => e.DatosPersonales)
            .FirstOrDefaultAsync(e => e.Id == request.Id);
            if (Empleado == null) 
            return (false, "El empleado no existe");

            var change = Empleado.Update(request.DatosPersonales.Nombre, request.DatosPersonales.FechaDeNacimiento, request.sueldo);
            if (!change) return (false, "No se realizo ningun cambio");

            return ((await _Context.SaveChangesAsync()) > 0, "Registro exitosa");
        }

        public async Task<bool> Eliminar(int Id)
        {
            var Empleado = await _Context.Empleados.FirstOrDefaultAsync(e => e.Id == Id);
            _Context.Empleados.Remove(Empleado!);
            return (await _Context.SaveChangesAsync()) > 0;
        }

    }
   

    

