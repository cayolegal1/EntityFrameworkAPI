using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace APITransferencias.Models
{
    public class UpdateValidator: AbstractValidator<Transfer>
    {
        public UpdateValidator()
        {
            string[] conditions = new string[]
          {
                "ok", "pendiente", "rechazado"
          };
            RuleFor(model => model.estado).NotNull().NotEmpty()
            .Must(x => conditions.Contains(x))
            .WithMessage("Las transferencias solo pueden tener estos estados: ok, pendiente, rechazado");
        }
    }
}
