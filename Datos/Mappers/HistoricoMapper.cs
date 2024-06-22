using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Modelos;

namespace Datos.Mappers
{
    internal class HistoricoMapper
    {
        public static HistoricoModelo EntidadAModelo(HISTORICOENTIDAD entidad)
        {
            HistoricoModelo modelo = new HistoricoModelo
            {
                IdHistorico = entidad.id_historico,
                IdEntidad = entidad.id_entidad,
                Fecha = entidad.fecha,
                Justificacion = entidad.justificacion
            };

            return modelo;
        }

        public static HISTORICOENTIDAD ModeloAEntidad(HistoricoModelo modelo)
        {
            HISTORICOENTIDAD entidad = new HISTORICOENTIDAD
            {
                id_historico = modelo.IdHistorico,
                fecha = modelo.Fecha,
                id_entidad = modelo.IdEntidad,
                justificacion = modelo.Justificacion,
            };

            return entidad;

        }
    }
}