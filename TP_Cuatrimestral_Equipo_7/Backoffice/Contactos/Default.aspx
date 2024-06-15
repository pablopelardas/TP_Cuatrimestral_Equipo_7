<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTailwind.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Contactos.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mx-auto max-w-screen-xl px-4 2xl:px-0">
        <div class="mx-auto max-w-5xl">
            <div>
                <div class="gap-4 sm:flex sm:items-center sm:justify-between">
                    <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl">Contactos</h2>
                    <a href="EditarContacto.aspx?redirect_to=Default.aspx" class="mt-4 flex items-center gap-2 justify-center rounded-lg bg-primary-700  px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300  dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 sm:mt-0">
                        <svg class="w-6 h-6 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" viewBox="0 0 24 24">
                            <path fill-rule="evenodd" d="M9 4a4 4 0 1 0 0 8 4 4 0 0 0 0-8Zm-2 9a4 4 0 0 0-4 4v1a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2v-1a4 4 0 0 0-4-4H7Zm8-1a1 1 0 0 1 1-1h1v-1a1 1 0 1 1 2 0v1h1a1 1 0 1 1 0 2h-1v1a1 1 0 1 1-2 0v-1h-1a1 1 0 0 1-1-1Z" clip-rule="evenodd"/>
                        </svg>
                        Agregar Contacto
                    </a>

                </div>
                <div class="mt-6 flex justify-between items-center flex-wrap">
                    <%-- Searchbox --%>
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full max-w-[300px] p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Buscar..." AutoPostBack="True" OnTextChanged="txtBuscar_TextChanged"></asp:TextBox>
                    <%-- Dropdown --%>
                    <div class="gap-4 sm:flex sm:items-center">
                        <div class="flex gap-2 items-center">
                            <label for="order-type" class="sr-only mb-2 block text-sm font-medium text-gray-900 dark:text-white">Seleccione tipo de contacto</label>
                            <% if (ddlFiltro.SelectedValue == "0") { %>
                            <svg class="w-8 h-8 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                              <path stroke="currentColor" stroke-linecap="round" stroke-width="2" d="M18.796 4H5.204a1 1 0 0 0-.753 1.659l5.302 6.058a1 1 0 0 1 .247.659v4.874a.5.5 0 0 0 .2.4l3 2.25a.5.5 0 0 0 .8-.4v-7.124a1 1 0 0 1 .247-.659l5.302-6.059c.566-.646.106-1.658-.753-1.658Z"/>
                            </svg>
                            <% } else  { %>
                                <svg class="w-6 h-6 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" viewBox="0 0 24 24">
                                  <path d="M5.05 3C3.291 3 2.352 5.024 3.51 6.317l5.422 6.059v4.874c0 .472.227.917.613 1.2l3.069 2.25c1.01.742 2.454.036 2.454-1.2v-7.124l5.422-6.059C21.647 5.024 20.708 3 18.95 3H5.05Z"/>
                                </svg>
                            <% } %>

                            <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="block w-full min-w-[8rem] rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder:text-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">Proveedores</asp:ListItem>
                                <asp:ListItem Value="2">Clientes</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <span class="inline-block text-gray-500 dark:text-gray-400">de </span>

                        <div>
                            <label for="duration" class="sr-only mb-2 block text-sm font-medium text-gray-900 dark:text-white">Select duration</label>
                            <select id="duration" class="block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder:text-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500">
                                <option selected>this week</option>
                                <option value="this month">this month</option>
                                <option value="last 3 months">the last 3 months</option>
                                <option value="lats 6 months">the last 6 months</option>
                                <option value="this year">this year</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="mt-6 flow-root sm:mt-8">
                <div class="divide-y divide-gray-200 dark:divide-gray-700">
                    <% foreach (Dominio.Modelos.ContactoModelo contacto in contactos)
                       { %>
                        <div class="flex flex-wrap items-center gap-x-4 gap-y-4 py-6 justify-between">
                            <dl class="w-auto">
                                <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Contacto</dt>
                                <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white">
                                    <%-- show last part of id concat with #...id --%>
                                    <a href="DetalleContacto.aspx?id=<%: contacto.Id %>" title="<%: contacto.Id %>" class="hover:underline"><%: $"{contacto.ShortId}..." %></a>
                                </dd>
                            </dl>
                            <dl class="w-auto min-w-[16%] text-center">
                                <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Tipo</dt>
                                <dd class="mt-1.5 text-center font-semibold text-gray-900 dark:text-white">
                                        <span class="flex justify-center">
                                            <% if (contacto.Rol == "Proveedor")
                                               { %>
                                                <svg class="w-6 h-6 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" viewBox="0 0 24 24">
                                                  <path fill-rule="evenodd" d="M4 4a2 2 0 0 0-2 2v9a1 1 0 0 0 1 1h.535a3.5 3.5 0 1 0 6.93 0h3.07a3.5 3.5 0 1 0 6.93 0H21a1 1 0 0 0 1-1v-4a.999.999 0 0 0-.106-.447l-2-4A1 1 0 0 0 19 6h-5a2 2 0 0 0-2-2H4Zm14.192 11.59.016.02a1.5 1.5 0 1 1-.016-.021Zm-10 0 .016.02a1.5 1.5 0 1 1-.016-.021Zm5.806-5.572v-2.02h4.396l1 2.02h-5.396Z" clip-rule="evenodd"/>
                                                </svg>

                                            <% }
                                               else
                                               { %>
                                                <svg class="w-6 h-6 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" viewBox="0 0 24 24">
                                                  <path fill-rule="evenodd" d="M12 4a4 4 0 1 0 0 8 4 4 0 0 0 0-8Zm-2 9a4 4 0 0 0-4 4v1a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2v-1a4 4 0 0 0-4-4h-4Z" clip-rule="evenodd"/>
                                                </svg>

                                            <% } %>
                                        </span>
                                </dd>
                            </dl>
                            <dl class="w-auto flex-1">
                                <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Nombre y Apellido</dt>
                                <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white">
                                    <%-- show last part of id concat with #...id --%>
                                    <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%: $"{contacto.NombreApellido}" %></dd>
                                </dd>
                            </dl>

                            <dl class="w-auto flex-1">
                                <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Correo</dt>
                                <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%: $"{contacto.Email}" %></dd>
                            </dl>
                            
                            <dl class="w-auto min-w-[16%]">
                                <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Teléfono</dt>
                                <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%: $"{contacto.Telefono}" %></dd>
                            </dl>

                            <dl class="w-auto">
                                <a href="DetalleContacto.aspx?id=<%: contacto.Id %>" class="w-full inline-flex justify-center rounded-lg  border border-gray-200 bg-white px-3 py-2 lg:py-4 text-sm font-medium text-gray-900 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:outline-none focus:ring-4 focus:ring-gray-100 dark:border-gray-600 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white dark:focus:ring-gray-700">Ver Detalle</a>
                            </dl>
                        </div>
                    <% } %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>