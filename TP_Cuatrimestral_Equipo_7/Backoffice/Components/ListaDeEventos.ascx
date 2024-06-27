<%@ Control Language="C#" CodeBehind="ListaDeEventos.ascx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Components.ListaDeEventos" %>
<%@ Import Namespace="Dominio.Modelos" %>

<div class="flow-root">
    <div class="divide-y divide-gray-200 dark:divide-gray-700">
        <% if (eventos != null && eventos.Count > 0)
               foreach (EventoModelo evento in eventos)
               { %>
            <div class="flex flex-wrap items-center gap-y-4 py-6 justify-between">
                <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                    <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Tipo</dt>
                    <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white">
                        <%-- show last part of id concat with #...id --%>
                        <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%: $"{evento.TipoEvento.Nombre}" %></dd>
                    </dd>
                </dl>
                <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                    <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Fecha</dt>
                    <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white">
                        <%-- show last part of id concat with #...id --%>
                        <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%: $"{evento.Fecha.ToShortDateString()}" %></dd>
                    </dd>
                </dl>
                <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                    <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Descripción</dt>
                    <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%: $"{evento.Descripcion ?? "-"}" %></dd>
                </dl>
            </div>
        <% }
           else
           { %>
            <div class="flex flex-wrap items-center gap-y-4 py-6 justify-between">
                <p class="text-base font-medium text-gray-900 dark:text-white">No se encontraron eventos</p>
            </div>
        <% } %>
    </div>
</div>