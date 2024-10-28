
using FluentValidation;
using PropiedadesMinimalApi.Modelos.DTOS;

namespace PropiedadesMinimalApi.Validaciones
{
    public class ValidacionActualizarPropiedad : AbstractValidator<ActualizarPropiedadDTO>
    {
        public ValidacionActualizarPropiedad()
        {
            RuleFor(modelo => modelo.IdPropiedad).NotEmpty().GreaterThan(0);

            RuleFor(modelo => modelo.Nombre).NotEmpty().WithMessage("El nombre no puede estar vacio");
            RuleFor(modelo => modelo.Nombre).MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres");
            RuleFor(modelo => modelo.Descripcion).NotEmpty().WithMessage("El nombre no puede estar vacio");
            RuleFor(modelo => modelo.Ubicacion).NotEmpty().WithMessage("El nombre no puede estar vacio");
        }
    }
}
