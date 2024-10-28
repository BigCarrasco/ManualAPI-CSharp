
using FluentValidation;
using PropiedadesMinimalApi.Modelos.DTOS;

namespace PropiedadesMinimalApi.Validaciones
{
    public class ValidacionCrearPropiedad : AbstractValidator<CrearPropiedadDTO>
    {
        public ValidacionCrearPropiedad()
        {
            RuleFor(modelo => modelo.Nombre).NotEmpty().WithMessage("El nombre no puede estar vacio");
            RuleFor(modelo => modelo.Nombre).MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres");
            RuleFor(modelo => modelo.Descripcion).NotEmpty().WithMessage("El nombre no puede estar vacio");
            RuleFor(modelo => modelo.Ubicacion).NotEmpty().WithMessage("El nombre no puede estar vacio");
        }
    }
}
