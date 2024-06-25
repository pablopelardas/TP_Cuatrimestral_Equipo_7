<%@ Control Language="C#" CodeBehind="ListaDeOrdenes.ascx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Components.ListaDeOrdenes" %>

<div class="flow-root">
    <div class="gap-4 space-y-4 sm:flex sm:items-center sm:space-y-0">
        <div>
            <label for="order-type" class="sr-only mb-2 block text-sm font-medium text-gray-900 dark:text-white">Seleccione tipo de orden</label>
            <asp:DropDownList CssClass="block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder:text-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500" runat="server" ID="ddEstado" OnSelectedIndexChanged="ddEstadoChanged" AutoPostBack="True">
                <asp:ListItem Value="0">Todas</asp:ListItem>
                <asp:ListItem Value="1">Pendientes</asp:ListItem>
                <asp:ListItem Value="2">En proceso</asp:ListItem>
                <asp:ListItem Value="3">Finalizadas</asp:ListItem>
                <asp:ListItem Value="4">Entregadas</asp:ListItem>
                <asp:ListItem Value="5">Canceladas</asp:ListItem>
            </asp:DropDownList>
        </div>

        <span class="inline-block text-gray-500 dark:text-gray-400">desde/hasta </span>

        <div>
            <label for="duration" class="sr-only mb-2 block text-sm font-medium text-gray-900 dark:text-white">Seleccione intervalo de tiempo</label>
            <asp:DropDownList CssClass="block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder:text-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500" runat="server" ID="ddIntervalo" OnSelectedIndexChanged="ddIntervaloChanged" AutoPostBack="True">
                <asp:ListItem Value="1">1 semana</asp:ListItem>
                <asp:ListItem Value="4">1 mes</asp:ListItem>
                <asp:ListItem Value="12">3 meses</asp:ListItem>
                <asp:ListItem Value="24">6 meses</asp:ListItem>
                <asp:ListItem Value="48">1 año</asp:ListItem>
                <asp:ListItem Value="0">Todas</asp:ListItem>
            </asp:DropDownList>

        </div>

        <a href="javascript:__doPostBack('clearFilters', 'async')" class="flex gap-2 text-gray-500 dark:text-gray-400">
            <svg class="w-6 h-6 text-gray-800 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17.651 7.65a7.131 7.131 0 0 0-12.68 3.15M18.001 4v4h-4m-7.652 8.35a7.13 7.13 0 0 0 12.68-3.15M6 20v-4h4"/>
            </svg>
            Limpiar filtros
        </a>
    </div>
    <div class="divide-y divide-gray-200 dark:divide-gray-700">

        <% if (ordenes != null && ordenes.Count > 0)
               foreach (Dominio.Modelos.OrdenModelo orden in ordenes)
               { %>
            <div class="flex flex-wrap items-center gap-y-4 py-6 justify-between">
                <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                    <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Orden</dt>
                    <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white">
                        <%-- show last part of id concat with #...id --%>
                        <a href="/Backoffice/Ordenes/DetalleOrden.aspx?id=<%: orden.IdOrden %>&redirect_to=<%: Request.Url.AbsolutePath %>?<%: Request.QueryString %>" title="<%: orden.IdOrden %>" class="hover:underline"><%: $"{orden.ShortId}..." %></a>
                    </dd>
                </dl>
                <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                    <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Fecha</dt>
                    <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white">
                        <%-- show last part of id concat with #...id --%>
                        <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%: $"{orden.Evento.Fecha.ToShortDateString()}" %></dd>
                    </dd>
                </dl>

                <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                    <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Cliente</dt>
                    <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%: $"{orden.Cliente.NombreApellido}" %></dd>
                </dl>

                <div class="flex mr-4">
                    <dl class="w-auto lg:flex-1">
                        <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Estado</dt>
                        <dd class="<%: orden.Estado.PillClass %> whitespace-nowrap">
                            <%: orden.Estado.Nombre %>
                        </dd>
                    </dl>
                    <dl class="w-auto lg:flex-1">
                        <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Estado Pago</dt>
                        <dd class="<%: orden.EstadoPago.PillClass %> whitespace-nowrap">
                            <%: orden.EstadoPago.Nombre %>
                        </dd>
                    </dl>
                </div>
                <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                <%-- redirect_to current url with query string id=currentID--%>
                    <a href="/Backoffice/Ordenes/DetalleOrden.aspx?id=<%: orden.IdOrden %>&redirect_to=<%: Request.Url.AbsolutePath %>?<%: Request.QueryString %>" class="w-full inline-flex justify-center rounded-lg  border border-gray-200 bg-white px-3 py-2 lg:py-4 text-sm font-medium text-gray-900 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:outline-none focus:ring-4 focus:ring-gray-100 dark:border-gray-600 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white dark:focus:ring-gray-700">Ver Detalle</a>
                </dl>
            </div>
        <% }
           else
           { %>
            <div class="flex flex-wrap items-center gap-y-4 py-6 justify-between">
                <p class="text-base font-medium text-gray-900 dark:text-white">No se encontraron ordenes</p>
            </div>
        <% } %>
    </div>
    <nav class="mt-6 flex items-center justify-center sm:mt-8" aria-label="Page navigation example">
        <ul class="flex h-8 items-center -space-x-px text-sm">
            <asp:PlaceHolder runat="server" ID="phPaginado" Visible="False"></asp:PlaceHolder>
        </ul>
    </nav>
</div>