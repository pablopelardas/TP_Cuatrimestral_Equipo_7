<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTailwind.Master" SmartNavigation="true" AutoEventWireup="true" CodeBehind="DetalleOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.DetalleOrden" %>
<%@ Import Namespace="Dominio.Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    function confirmarCancelarOrden() {
        console.log("Cancelando orden")
        // show alert confirm
        Swal.fire({
            title: '¿Estás seguro?',
            text: "¡No podrás revertir esto!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#274bdb',
            cancelButtonColor: '#d33',
            confirmButtonText: '¡Sí, cancelar orden!',
            cancelButtonText: 'Cancelar',
        }).then((result) => {
            if (result.isConfirmed) {
                window.__doPostBack('btnCancelarOrden', 'async');
            }
        })
    }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%-- DIV Detalles de evento  --%>
<asp:ScriptManager ID="sm" runat="server">
</asp:ScriptManager>
<% if (orden != null)
   { %>

    <form action="#" class="mx-auto max-w-screen-xl px-4 ">
    <div class="mx-auto max-w-3xl px-4">
    <asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="flex justify-between items-start">
            <div class="flex-col ">
                <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl sm:mb-4">Orden #<%: orden.IdOrden %></h2>
                <a href="<%: redirect_to%>" class="flex align-items-center gap-2 text-gray-800 dark:text-gray-400 hover:text-gray-900 dark:hover:text-gray-200">
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
                    <% if (orden.Estado.IdOrdenEstado < 4)
                       { %>
                        <li class=" hover:bg-gray-100 dark:hover:bg-gray-800">
                            <a href="EditarOrden.aspx?id=<%: orden.IdOrden %>&redirect_to=DetalleOrden.aspx?id=<%: orden.IdOrden %>" class=" px-4 py-2 flex items center gap-2">Editar orden</a>
                        </li>
                        <li class="hover:bg-gray-100 dark:hover:bg-gray-800">
                            <a href="javascript:confirmarCancelarOrden()" class="px-4 py-2 flex items center gap-2">Cancelar orden</a>
                        </li>
                    <% } %>
                        <li class="hover:bg-gray-100 dark:hover:bg-gray-800">
                            <%-- <a href="javascript:__doPostBack('abrirHistorial', 'async')" class="flex items center gap-2">Historial de la orden</a> --%>
                            <button type="button" data-modal-target="timeline-modal" data-modal-toggle="timeline-modal" class="px-4 py-2 flex items center gap-2">Historial de la orden</button>
                        </li>
                    </ul>
                </div>
        </div>
        <div class="mt-6 space-y-4 border-b border-t border-gray-200 py-8 dark:border-gray-700 sm:mt-8">
            <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Información General</h4>
            <dl class="w-full flex justify-between text-start flex-wrap flex-col lg:flex-row">
                <div class="w-full mt-5 flex flex-col flex-wra">
                    <dt class=" text-base font-medium text-gray-900 dark:text-white">Cliente</dt>
                    <dd class=" mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: orden.Cliente.DatosDeContacto %></dd>
                </div>
                <div class="mt-5 flex flex-col flex-wrap">
                    <dt class="text-base font-medium text-gray-900 dark:text-white">Evento</dt>
                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: orden.Evento.DescripcionEventoOrden %></dd>
                </div>
                <div class="mt-5 flex flex-col flex-wrap">
                    <dt class=" text-base font-medium text-gray-900 dark:text-white">Entrega</dt>
                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: orden.DetalleEntrega %></dd>
                </div>
            </dl>
        </div>
        <% if (orden.TipoEntrega == "Delivery")
           { %>
            <div class="space-y-4 border-b py-8 dark:border-gray-700">
                <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Dirección</h4>
                <dl class="w-full flex justify-between text-start flex-wrap flex-col lg:flex-row">
                    <div class="mt-5 flex flex-col flex-wrap">
                        <dt class=" text-base font-medium text-gray-900 dark:text-white">Calle y Número</dt>
                        <dd class=" mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: orden.DireccionEntrega.CalleNumero %></dd>
                    </div>
                    <div class="mt-5 flex flex-col flex-wrap">
                        <dt class="text-base font-medium text-gray-900 dark:text-white">Piso y Departamento</dt>
                        <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: $"{orden.DireccionEntrega.Piso} {orden.DireccionEntrega.Departamento}" %></dd>
                    </div>
                    <div class="mt-5 flex flex-col flex-wrap">
                        <dt class=" text-base font-medium text-gray-900 dark:text-white">Localidad</dt>
                        <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: $"{orden.DireccionEntrega.Localidad} {orden.DireccionEntrega.CodigoPostal}" %></dd>
                    </div>
                    <div class="mt-5 flex  flex-col flex-wrap">
                        <dt class=" text-base font-medium text-gray-900 dark:text-white">Provincia</dt>
                        <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: orden.DireccionEntrega.Provincia %></dd>
                    </div>
                </dl>
            </div>
        <% } %>
        <div class="space-y-4 border-b py-8 dark:border-gray-700">
            <h4 class="text-lg font-semibold flex flex-wrap text-gray-900 dark:text-white">
                <span class="mr-4">Información de la orden</span>
                <div class="relative <%: orden.Estado.PillClass %>">
                    <%: orden.Estado.Nombre %>
                </div>
                <% if (orden.Estado.IdOrdenEstado < 4)
                   { %>
                    <asp:LinkButton runat="server" ID="btnAvanzarEstado" OnClick="AvanzarEstado" CssClass="flex items-center gap-2 hover:underline">
                        <svg class="w-6 h-6 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m7 16 4-4-4-4m6 8 4-4-4-4"/>
                        </svg>
                        <div class="relative <%: orden.Estado.SiguienteEstado.PillClass %>">
                            <%: orden.Estado.SiguienteEstado.Nombre %>
                        </div>
                    </asp:LinkButton>
                <% } %>

            </h4>
            <div class="w-full mt-5 flex flex-col flex-wrap">
                <dt class=" text-base font-medium text-gray-900 dark:text-white">Observaciones</dt>
                <dd class="mt-2 p-4 dark:bg-gray-800 dark:text-white">
                    <asp:Literal runat="server" ID="litOrdenExtra"></asp:Literal>
                </dd>
            </div>
        </div>
        
        <div class="space-y-4 border-b py-8 dark:border-gray-700 curs">
            <h4 class="text-lg font-semibold text-gray-900 dark:text-white flex flex-wrap justify-between gap-4">
                <div>
                    <span class="mr-4">
                        Pagos
                    </span>
                    <span class="<%: orden.EstadoPago.PillClass %>">
                        <%: orden.EstadoPago.Nombre %>
                    </span>
                </div>
                <% if (orden.EstadoPago.IdOrdenPagoEstado < 3)
                   { %>
                <button type="button" data-modal-target="pago-modal" data-modal-toggle="pago-modal" class="inline-flex items-center justify-center rounded-lg bg-primary-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 cursor-pointer">Agregar Pago</button>
                <% } %>
            </h4>

            <div class="flex justify-between">

                <dl>
                    <dt class="text-base mb-3 font-medium text-gray-900 dark:text-white">Total</dt>
                    <dd class="text-base font-normal text-gray-900 dark:text-white">$<%: orden.Total %></dd>
                </dl>
                <dl>
                    <dt class="text-base mb-3 font-medium text-gray-900 dark:text-white">Pagado</dt>
                    <dd class="text-base font-normal text-gray-900 dark:text-white">$<%: orden.TotalPagado %></dd>
                    <%-- <dd class="text-base font-normal text-gray-900 dark:text-white">$<%: orden.Pagado %></dd> --%>
                </dl>
                <dl>
                    <dt class="text-base mb-3 font-medium text-gray-900 dark:text-white">Restante</dt>
                    <dd id="montoRestante" class="text-base font-normal text-gray-900 dark:text-white">$<%: orden.Total - orden.TotalPagado %></dd>
                    <%-- <dd class="text-base font-normal text-gray-900 dark:text-white">$<%: orden.Restante %></dd> --%>
                </dl>
            </div>
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
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAvanzarEstado" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="mt-6 sm:mt-8">
        <div class="relative overflow-x-auto border-b border-gray-200 dark:border-gray-800">
            <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Productos en la orden</h4>
            <table class="w-full mt-5 text-left font-medium text-gray-900 dark:text-white md:table-fixed">
                <thead class="">
                <tr>
                    <th class="py-4">Product</th>
                    <th class="p-4 text-center hidden md:table-cell">Price</th>
                    <th class="p-4 text-right">Subtotal</th>
                </tr>
                </thead>

                <tbody class="divide-y divide-gray-200 dark:divide-gray-800">

                <% foreach (var detalle in orden.DetalleProductos)
                   { %>
                    <tr>
                        <td class="whitespace nowrap py-4">
                            <div class="flex items center gap-4">
                                <a href="#" class="flex gap-4 align-items-center">
                                    <div class="flex items-center aspect-square w-20 shrink-0 relative">
                                        <div class="w-36 fill-primary-600 category-svg">
                                            <object type="image/svg+xml" data="<%: detalle.Producto.Categoria.ImagenPath %>"></object>
                                        </div>
                                        <div class="absolute bg-primary-300  w-6 h-6 rounded-full text-primary-900 text-center place-content-center text-sm top-1/2 right-4"><%: detalle.Cantidad %></div>
                                    </div>
                                    <a href="#" class="content-center hover:underline"><%: detalle.Producto.Nombre %></a>
                                </a>
                            </div>
                        </td>

                        <td class="p-4 text-center text-base font-bold text-gray-900 dark:text-white hidden md:table-cell">$<%: detalle.PrecioUnitarioActual %></td>

                        <td class="p-4 text-right text-base font-bold text-gray-900 dark:text-white">$<%: detalle.Subtotal %></td>
                    </tr>
                <% } %>
                </tbody>
            </table>
        </div>

        <div class="mt-4 space-y-6">
            <h4 class="text-xl font-semibold text-gray-900 dark:text-white">Resumen</h4>

            <div class="space-y-4">
                <div class="space-y-2">
                    <dl class="flex items-center justify-between gap-4">
                        <dt class="text-gray-500 dark:text-gray-400">Subtotal</dt>
                        <dd class="text-base font-medium text-gray-900 dark:text-white">$<%: orden.Subtotal %></dd>
                    </dl>

                    <dl class="flex items-center justify-between gap-4">
                        <dt class="text-gray-500 dark:text-gray-400">Descuento</dt>
                        <dd class="text-base font-medium text-gray-900 dark:text-white">%<%: orden.DescuentoPorcentaje %></dd>
                    </dl>

                    <dl class="flex items-center justify-between gap-4">
                        <dt class="text-gray-500 dark:text-gray-400">Costo Envío / Extras</dt>
                        <dd class="text-base font-medium text-gray-900 dark:text-white">$<%: orden.CostoEnvio %></dd>
                    </dl>
                </div>

                <dl class="flex items-center justify-between gap-4 border-t border-gray-200 pt-2 dark:border-gray-700">
                    <dt class="text-lg font-bold text-gray-900 dark:text-white">Total</dt>
                    <dd class="text-lg font-bold text-gray-900 dark:text-white">$<%: orden.Total %></dd>
                </dl>
            </div>

            <div class="gap-4 sm:flex sm:items-center">
                <a href="./Default.aspx" type="button" class="w-full text-center rounded-lg  border border-gray-200 bg-white px-5  py-2.5 text-sm font-medium text-gray-900 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:outline-none focus:ring-4 focus:ring-gray-100 dark:border-gray-600 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white dark:focus:ring-gray-700">Volver al dashboard</a>
                <asp:LinkButton ID="btnGenerateShoppingList" runat="server" OnClick="GenerateShoppingList" CssClass="flex w-full items-center justify-center rounded-lg bg-primary-700  px-5 py-2 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300  dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800">
                    <svg class="w-6 h-6 text-white " aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 4h1.5L8 16m0 0h8m-8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm.75-3H7.5M11 7H6.312M17 4v6m-3-3h6"/>
                    </svg>
                    Generar lista de compras
                </asp:LinkButton>
            </div>
        </div>
    </div>
    </div>

    
    </form>
    
        
            <div id="pago-modal" data-modal-backdrop="static" tabindex="-1" aria-hidden="true" class="hidden overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full">
                <div class="relative p-4 w-full max-w-md max-h-full">
                    <!-- Modal content -->
                    <div class="relative bg-white rounded-lg shadow dark:bg-gray-700">
                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                        <!-- Modal header -->
                        <div class="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
                            <h3 class="text-lg font-semibold text-gray-900 dark:text-white">
                                Agregar pago
                            </h3>
                            <button type="button" class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-toggle="pago-modal">
                                <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                                </svg>
                                <span class="sr-only">Cerrar modal</span>
                            </button>
                        </div>
                        <!-- Modal body -->
                        <div class="p-4 md:p-5">
                            <div class="grid gap-4 mb-4 grid-cols-2">
                                <div class="col-span-2 flex gap-4 align-items-center">
                                    <div class="w-full flex flex-col">
                                        <label for="name" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Monto ($)</label>
                                        <asp:TextBox runat="server" step=".01" TextMode="Number" ID="txtMontoPago" ClientIDMode="Static" CssClass="w-full bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="2999"></asp:TextBox> 
                                    </div>
                                    <div class="flex w-full justify-center self-end">
                                        <button  id="setMaxDisponible" type="button" class=" text-center rounded-lg bg-primary-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800">MAX</button> 
                                    </div>
                                </div>
                                <div class="col-span-2">
                                    <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Metodo de pago</label>
                                    <asp:DropDownList runat="server" ID="ddTipoPago" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500">
                                        <asp:ListItem Text="Efectivo" Value="Efectivo"></asp:ListItem>
                                        <asp:ListItem Text="Transferencia" Value="Transferencia"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:Button runat="server" ID="btnAgregarPago" CssClass="p-4 text-white inline-flex items-center bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" OnClick="btnAgregarPago_OnClick" Text="Agregar Pago"/>
                        </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAgregarPago"></asp:PostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                </div>
            </div> 

    <script type="text/javascript">
    
    setMaxDisponible.addEventListener('click', () => {
         console.log("Setting max")
        var montoRestante = document.getElementById('montoRestante').innerText;
         montoRestante = montoRestante.replace('$', '').replace(',', '.');
         montoRestante = parseFloat(montoRestante )
        document.getElementById('txtMontoPago').value = montoRestante;
    });
    </script>
    
<% } %>
</asp:Content>