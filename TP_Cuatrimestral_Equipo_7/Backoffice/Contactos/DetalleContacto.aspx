<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTailwind.Master" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeBehind="DetalleContacto.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Contactos.DetalleContacto" %>
<%@ Import Namespace="Dominio.Modelos" %>
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
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
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
                    <dd class="mt-2 dark:bg-gray-800 dark:text-white">
                        <asp:Literal runat="server" ID="litInformacionPersonal" />
                    </dd>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="space-y-4 border-b py-8 dark:border-gray-700">
                            <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Ordenes del Contacto</h4>
                            <div class="mt-2 dark:text-white">
                                <asp:PlaceHolder runat="server" ID="phListaDeOrdenes"></asp:PlaceHolder>
                            </div>
                        </div>               
                        <div class="space-y-4 border-b py-8 dark:border-gray-700">
                            <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Eventos del Contacto</h4>
                            <div class="mt-2 dark:text-white">
                                <asp:PlaceHolder runat="server" ID="phListaDeEventos"></asp:PlaceHolder>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
                    <!-- Main modal -->
                    <div id="timeline-modal" tabindex="-1" aria-hidden="true" class="hidden overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full">
                        <div class="relative p-4 w-full max-w-md max-h-full">
                            <!-- Modal content -->
                            <div class="relative bg-white rounded-lg shadow dark:bg-gray-700">
                                    <!-- Modal header -->
                                    <div class="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
                                        <h3 class="text-lg font-semibold text-gray-900 dark:text-white">
                                            Historial de la orden
                                        </h3>
                                        <button type="button" class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm h-8 w-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-toggle="timeline-modal">
                                            <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                                            </svg>
                                            <span class="sr-only">Close modal</span>
                                        </button>
                                    </div>
                                    <!-- Modal body -->
                                    <div class="p-4 md:p-5">
                                        <ol class="relative border-s border-gray-200 dark:border-gray-600 ms-3.5 mb-4 md:mb-5">          
                                            <% if (historicos != null) foreach (HistoricoModelo historico in historicos)
                                               { %>
                                                    <li class="mb-10 ms-8">            
                                                       <span class="absolute flex items-center justify-center w-6 h-6 bg-gray-100 rounded-full -start-3.5 ring-8 ring-white dark:ring-gray-700 dark:bg-gray-600">
                                                           <svg class="w-2.5 h-2.5 text-gray-500 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 20 20"><path fill="currentColor" d="M6 1a1 1 0 0 0-2 0h2ZM4 4a1 1 0 0 0 2 0H4Zm7-3a1 1 0 1 0-2 0h2ZM9 4a1 1 0 1 0 2 0H9Zm7-3a1 1 0 1 0-2 0h2Zm-2 3a1 1 0 1 0 2 0h-2ZM1 6a1 1 0 0 0 0 2V6Zm18 2a1 1 0 1 0 0-2v2ZM5 11v-1H4v1h1Zm0 .01H4v1h1v-1Zm.01 0v1h1v-1h-1Zm0-.01h1v-1h-1v1ZM10 11v-1H9v1h1Zm0 .01H9v1h1v-1Zm.01 0v1h1v-1h-1Zm0-.01h1v-1h-1v1ZM10 15v-1H9v1h1Zm0 .01H9v1h1v-1Zm.01 0v1h1v-1h-1Zm0-.01h1v-1h-1v1ZM15 15v-1h-1v1h1Zm0 .01h-1v1h1v-1Zm.01 0v1h1v-1h-1Zm0-.01h1v-1h-1v1ZM15 11v-1h-1v1h1Zm0 .01h-1v1h1v-1Zm.01 0v1h1v-1h-1Zm0-.01h1v-1h-1v1ZM5 15v-1H4v1h1Zm0 .01H4v1h1v-1Zm.01 0v1h1v-1h-1Zm0-.01h1v-1h-1v1ZM2 4h16V2H2v2Zm16 0h2a2 2 0 0 0-2-2v2Zm0 0v14h2V4h-2Zm0 14v2a2 2 0 0 0 2-2h-2Zm0 0H2v2h16v-2ZM2 18H0a2 2 0 0 0 2 2v-2Zm0 0V4H0v14h2ZM2 4V2a2 2 0 0 0-2 2h2Zm2-3v3h2V1H4Zm5 0v3h2V1H9Zm5 0v3h2V1h-2ZM1 8h18V6H1v2Zm3 3v.01h2V11H4Zm1 1.01h.01v-2H5v2Zm1.01-1V11h-2v.01h2Zm-1-1.01H5v2h.01v-2ZM9 11v.01h2V11H9Zm1 1.01h.01v-2H10v2Zm1.01-1V11h-2v.01h2Zm-1-1.01H10v2h.01v-2ZM9 15v.01h2V15H9Zm1 1.01h.01v-2H10v2Zm1.01-1V15h-2v.01h2Zm-1-1.01H10v2h.01v-2ZM14 15v.01h2V15h-2Zm1 1.01h.01v-2H15v2Zm1.01-1V15h-2v.01h2Zm-1-1.01H15v2h.01v-2ZM14 11v.01h2V11h-2Zm1 1.01h.01v-2H15v2Zm1.01-1V11h-2v.01h2Zm-1-1.01H15v2h.01v-2ZM4 15v.01h2V15H4Zm1 1.01h.01v-2H5v2Zm1.01-1V15h-2v.01h2Zm-1-1.01H5v2h.01v-2Z"/></svg>
                                                       </span>
                                                       <%-- <h3 class="flex items-start mb-1 text-lg font-semibold text-gray-900 dark:text-white">Flowbite Application UI v2.0.0</h3> --%>
                                                       <p class="flex items-start mb-1 text-sm font-semibold text-gray-200"><%: historico.Justificacion %></p>
                                                       <time class="block mb-3 text-sm font-normal leading-none text-gray-500 dark:text-gray-400"><%: historico.Fecha.ToLocalTime() %></time>
                                                   </li>
                                               <%}%>
                                           
                                        </ol>
                                    </div>
                                </div>
                        </div>
                    </div> 
        </form> 
    <% } %>
</asp:Content>
