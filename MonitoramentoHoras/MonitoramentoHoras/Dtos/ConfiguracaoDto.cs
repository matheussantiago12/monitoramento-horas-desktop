using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoramentoHoras.Dtos
{
    public class ConfiguracaoDto
    {
        public ConfiguracaoDto(long pausaPeriodo, long tempoLimiteOciosidade, long id)
        {
            PausaPeriodo = pausaPeriodo;
            TempoLimiteOciosidade = tempoLimiteOciosidade;
            Id = id;
        }

        public long PausaPeriodo { get; set; }
        public long TempoLimiteOciosidade { get; set; }
        public long Id { get; set; }
    }
}
