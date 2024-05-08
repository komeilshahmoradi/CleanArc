using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(People people);
        public int? ValidateJwtToken(string? token);
    }
}
