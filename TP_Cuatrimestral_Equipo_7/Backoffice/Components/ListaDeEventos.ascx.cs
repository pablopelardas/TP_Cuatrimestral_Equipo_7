using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Dominio.Modelos;

namespace TP_Cuatrimestral_Equipo_7.Backoffice.Components
{
    public partial class ListaDeEventos : UserControl
    {
        public List<EventoModelo> eventos;
        private ContactoModelo contacto;
        
        public void InicializarGrilla(ContactoModelo _contacto)
        {
            contacto = _contacto;
            
            if (ViewState["eventos"] != null)
            {
                eventos = (List<EventoModelo>)ViewState["eventos"];
            }
            else
            {
                ListarEventos(contacto);
            }
            
                
        }
        
        private void ListarEventos(ContactoModelo contacto)
        {
            Negocio.Servicios.EventoServicio eventoServicio = new Negocio.Servicios.EventoServicio();
            eventos = eventoServicio.ListarEventosPorCliente(contacto.Id, true);
            ViewState["eventos"] = eventos;
        }
    }
}