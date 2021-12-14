using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoramentoHoras.Dtos
{
    public class ValidarCredenciasDto
    {
        public ValidarCredenciasDto(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
