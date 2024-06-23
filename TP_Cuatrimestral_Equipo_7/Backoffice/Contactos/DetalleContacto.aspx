<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTailwind.Master" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeBehind="DetalleContacto.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Contactos.DetalleContacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
        function confirmarEliminarContacto() {
            // show alert confirm
            Swal.fire({
                title: '¿Estás seguro?',
                text: "Se eliminará el contacto y no se podrá recuperar",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#274bdb',
                cancelButtonColor: '#d33',
                confirmButtonText: '¡Sí, eliminar!',
                cancelButtonText: 'Cancelar',
            }).then((result) => {
                if (result.isConfirmed) {
                    window.__doPostBack('btnEliminarContacto', 'async');
                }
            })
        }
    
        </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (contacto != null)
       { %>
        <form action="#" class="mx-auto max-w-screen-xl px-4">
            <div class="mx-auto max-w-3xl px-4">
                <div class="flex justify-between items-start">
                    <div class="flex-col">
                        <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl sm:mb-4">Contacto #<%: contacto.Id %></h2>
                        <a href="Default.aspx" class="flex align-items-center gap-2 text-gray-800 dark:text-gray-400 hover:text-gray-900 dark:hover:text-gray-200">
                            <svg class="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h14M5 12l4-4m-4 4 4 4" />
                            </svg>
                            <span>VOLVER
                            </span>
                        </a>
                    </div>
                    <button id="dropdownActionsBtn" data-dropdown-toggle="dropdownActions" class="mt-1 text-gray-100 font-medium text-sm" type="button">
                        <svg class="w-6 h-6 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                            <path stroke="currentColor" stroke-linecap="round" stroke-width="2" d="M12 6h.01M12 12h.01M12 18h.01"/>
                        </svg>
    
                    </button>
                    <!-- Dropdown menu -->
    
                    <div id="dropdownActions" class="z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow w-auto dark:bg-gray-700">
                        <ul class="py-4 px-2 flex flex-col gap-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownActionsBtn">
                            <li class="px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-800">
                                <a href="EditarContacto.aspx?id=<%: contacto.Id %>&redirect_to=DetalleContacto.aspx?id=<%: contacto.Id %>" class="flex items center gap-2">Editar contacto</a>
                            </li>
                            <li class="hover:bg-gray-100 dark:hover:bg-gray-800">
                                <a href="javascript:confirmarEliminarContacto()" class="px-4 py-2 flex items center gap-2">Eliminar contacto</a>
                            </li>
                            <li class="hover:bg-gray-100 dark:hover:bg-gray-800">
                                <button type="button" data-modal-target="timeline-modal" data-modal-toggle="timeline-modal" class="px-4 py-2 flex items center gap-2">Historial del contacto</button>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="mt-6 space-y-4 border-b border-t border-gray-200 py-8 dark:border-gray-700 sm:mt-8">
                    <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Información General</h4>
                    <dl class="w-full flex lg:gap-8 justify-between text-start flex-wrap flex-col lg:flex-row">
                        <div class="mt-5 flex flex-col flex-wrap flex-lg-grow-1">
                            <dt class=" text-base font-medium text-gray-900 dark:text-white">Nombre Completo</dt>
                            <dd class=" mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: contacto.NombreApellido %></dd>
                        </div>     
                        <div class="mt-5 flex flex-col flex-wrap">
                            <dt class="text-base font-medium text-gray-900 dark:text-white">Tipo</dt>
                            <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: contacto.Rol %></dd>
                        </div>
                        <div class="mt-5 flex flex-col flex-wrap">
                            <dt class=" text-base font-medium text-gray-900 dark:text-white">Fuente</dt>
                            <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: contacto.Fuente %></dd>
                        </div>    
                        <div class="mt-5 flex flex-col flex-wrap flex-lg-grow-1">
                            <dt class=" text-base font-medium text-gray-900 dark:text-white">Correo</dt>
                            <dd class=" mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: contacto.Email %></dd>
                        </div>

                        <div class="mt-5 flex flex-col flex-wrap flex-lg-grow-1">
                            <dt class=" text-base font-medium text-gray-900 dark:text-white">Teléfono</dt>
                            <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: contacto.Telefono %></dd>
                        </div>     
                        <div class="mt-5 flex flex-col flex-wrap flex-lg-grow-1">
                            <dt class=" text-base font-medium text-gray-900 dark:text-white">Desea recibir correos</dt>
                            <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%:contacto.DeseaRecibirCorreos ? "Si" : "No"%></dd>
                        </div>
                        <div class=" mt-5 flex flex-col flex-wrap flex-lg-grow-1">
                            <dt class=" text-base font-medium text-gray-900 dark:text-white">Desea recibir whatsapps</dt>
                            <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%:contacto.DeseaRecibirWhatsapp ? "Si" : "No"%></dd>
                        </div>
                    </dl>
                </div>
                <div class="space-y-4 border-b py-8 dark:border-gray-700">
                    <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Direcciones</h4>
                    <dl class="w-full flex justify-between text-start flex-wrap flex-col lg:flex-row">
                        <% foreach (var direccion in contacto.Direcciones)
                           { %>
                            <div class="w-full flex justify-between text-start flex-wrap flex-col sm:flex-row sm:gap-6 border border-gray-700 p-4 mt-4">
                                <div class="mt-5 flex flex-col flex-wrap flex-lg-grow-1">
                                    <dt class=" text-base font-medium text-gray-900 dark:text-white">Calle y Número</dt>
                                    <dd class=" mt-1 text  base font-normal text-gray-500 dark:text-gray-400"><%: direccion.CalleNumero %></dd>
                                </div>
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Piso y Departamento</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: $"{direccion.Piso} {direccion.Departamento}" %></dd>
                                </div>
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class=" text-base font-medium text-gray-900 dark:text-white">Localidad</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: $"{direccion.Localidad} {direccion.CodigoPostal}" %></dd>
                                </div>
                                <div class="mt-5 flex  flex-col flex-wrap">
                                    <dt class=" text-base font-medium text-gray-900 dark:text-white">Provincia</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: direccion.Provincia %></dd>
                                </div>
                            </div>
                        <% } %>
                    </dl>
                </div>
                <div class="space-y-4 border-b py-8 dark:border-gray-700">
                    <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Información Personal</h4>
                    <dd class="mt-2 p-4 dark:bg-gray-800 dark:text-white">
                        <asp:Literal runat="server" ID="litInformacionPersonal" />
                    </dd>
                </div>
                <div class="space-y-4 border-b py-8 dark:border-gray-700">
                    <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Eventos del Contacto</h4>
                    <dd class="mt-2 p-4 dark:bg-gray-800 dark:text-white">
                        <asp:PlaceHolder runat="server" ID="phCalendario"></asp:PlaceHolder>
                    </dd>
                </div>
            </div>
        </form> 
    <% } %>
    
    <%-- $1$ DIV Informacion personal  #1# --%>
    <%-- <div> --%>
    <%--     <h3>Información personal</h3> --%>
    <%--     <asp:Literal ID="litInformacionPersonal" runat="server" /> --%>
    <%-- </div> --%>
    <%-- $1$ DIV Fechas importantes  #1# --%>
    <%-- <div> --%>
    <%--     <div> --%>
    <%--         <h3>Fechas importantes</h3> --%>
    <%--         <button>Nueva fecha</button> --%>
    <%--     </div> --%>
    <%--     <div> --%>
    <%--         No hay fechas importantes agregadas --%>
    <%--     </div> --%>
    <%-- </div> --%>
    <%-- $1$ DIV Ordenes  #1# --%>
    <%-- <div> --%>
    <%--     <div> --%>
    <%--         <h3>Historial de Ordenes</h3> --%>
    <%--         <button>Nueva orden</button> --%>
    <%--     </div> --%>
    <%--     <div> --%>
    <%--         No hay ordenes agregadas --%>
    <%--     </div> --%>
    <%--     </div> --%>
</asp:Content>
