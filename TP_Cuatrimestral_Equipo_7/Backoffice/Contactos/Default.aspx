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
                            <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="block w-full min-w-[8rem] rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder:text-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                                <asp:ListItem Value="">Todos</asp:ListItem>
                                <asp:ListItem Value="Proveedor">Proveedores</asp:ListItem>
                                <asp:ListItem Value="Cliente">Clientes</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                          <a href="/Backoffice/Contactos/Default.aspx" class="flex gap-2 text-gray-500 dark:text-gray-400">
                                <svg class="w-6 h-6 text-gray-800 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17.651 7.65a7.131 7.131 0 0 0-12.68 3.15M18.001 4v4h-4m-7.652 8.35a7.13 7.13 0 0 0 12.68-3.15M6 20v-4h4"/>
                                </svg>
                                Limpiar filtros
                            </a>
                    </div>
                </div>
            </div>
            <div class="mt-6 flow-root sm:mt-8">
                <div class="divide-y divide-gray-200 dark:divide-gray-700">
                    <% if (contactos != null && contactos.Count > 0) foreach (Dominio.Modelos.ContactoModelo contacto in contactos)
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
                    <% } else { %>
                        <div class="flex flex-wrap items-center gap-y-4 py-6 justify-between">
                            <p class="text-base font-semibold text-gray-900 dark:text-white">No se encontraron contactos</p>
                        </div>
                    <% } %>
                </div>
            </div>
            <nav class="mt-6 flex items-center justify-center sm:mt-8" aria-label="Page navigation example">
                <ul class="flex h-8 items-center -space-x-px text-sm">
                    <asp:PlaceHolder runat="server" ID="phPaginado" Visible="False"></asp:PlaceHolder>
                </ul>
            </nav>
        </div>
    </div>
</asp:Content>