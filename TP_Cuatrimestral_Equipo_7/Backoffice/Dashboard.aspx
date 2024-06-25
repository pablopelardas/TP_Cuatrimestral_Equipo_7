<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTailwind.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Dashboard" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function ShowPopup(title, body) {
            setTimeout(() => {
                const button = document.querySelector('[data-modal-toggle="dayOrders"]');
                button.click();
            }, 1);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" ID="sp"></asp:ScriptManager>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="mx-auto max-w-screen-xl px-4 2xl:px-0">
                 <div class="mx-auto max-w-5xl">
                    <h4 class="text-2xl font-bold dark:text-white my-10">Próximas Ordenes</h4>

                    <asp:PlaceHolder ID="phCalendario" runat="server"></asp:PlaceHolder>
                    
                    <!-- Modal toggle -->
                    <button data-modal-target="dayOrders" data-modal-toggle="dayOrders" class="hidden block text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" type="button">
                        Toggle modal
                    </button>
                    
                    <div id="dayOrders" tabindex="-1" aria-hidden="true" class="hidden overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full">
                        <div class="relative p-4 w-full max-w-2xl max-h-full">
                            <!-- Modal content -->
                            <div class="relative bg-white rounded-lg shadow dark:bg-gray-700">
                                <!-- Modal header -->
                                <div class="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
                                    <h3 class="text-xl font-semibold text-gray-900 dark:text-white"> Eventos del <%: FechaSeleccionada.ToShortDateString() %>
                                    </h3>
                                    <button type="button" class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="dayOrders">
                                        <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                                            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6" />
                                        </svg>
                                        <span class="sr-only">Close modal</span>
                                    </button>
                                </div>
                                <!-- Modal body -->
                                <div class="">
                                    <div class="flex flex-col py-2">
                                        <asp:Label runat="server" ID="lblOrdenes" Text="Ordenes" Visible="False" CssClass="px-4 text-lg font-bold dark:text-white"></asp:Label>
                                        <asp:Repeater ID="rptOrdenes" runat="server">
                                            <ItemTemplate>
                                                <div class="flex flex-wrap items-center justify-between text-center sm:text-start lg:gap-x-6 gap-y-4 py-4 px-4 border-b border-gray-200 dark:border-gray-600">
                                                    <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                                                        <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Orden</dt>
                                                        <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white">
                                                            <a href="/Backoffice/Ordenes/DetalleOrden.aspx?id=<%# Eval("Orden.IdOrden")%>&redirect_to=/Backoffice/Dashboard.aspx" class="hover:underline">#...<%# Eval("Orden.ShortId") %></a>
                                                        </dd>
                                                    </dl>
                                                    <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                                                        <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Cliente</dt>
                                                        <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%# Eval("Cliente.NombreApellido") %></dd>
                                                    </dl>
                    
                                                    <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                                                        <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Hora</dt>
                                                        <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%# Eval("Orden.HoraEntrega") %></dd>
                                                    </dl>
                    
                                                    <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                                                        <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Estado</dt>
                                                        <dd class="me-2 mt-1.5 inline-flex items-center rounded bg-red-100 px-2.5 py-0.5 text-xs font-medium text-red-800 dark:bg-red-900 dark:text-red-300">
                                                            <svg class="me-1 h-3 w-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18 17.94 6M18 18 6.06 6" />
                                                            </svg>
                                                            Cancelled
                                                        </dd>
                                                    </dl>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="flex flex-col py-2">
                                        <asp:Label runat="server" ID="lblEventos" Text="Fechas destacadas" Visible="False" CssClass="px-4 text-lg font-bold dark:text-white"></asp:Label>
                                        <asp:Repeater ID="rptEventos" runat="server">
                                            <ItemTemplate>
                                                <div class="flex flex-wrap items-center justify-between text-center sm:text-start lg:gap-x-6 gap-y-4 py-4 px-4 border-b border-gray-200 dark:border-gray-600">
                                                    <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                                                        <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Orden</dt>
                                                        <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white">
                                                            <a href="/Backoffice/Ordenes/DetalleOrden.aspx?id=<%# Eval("Orden.IdOrden")%>&redirect_to=/Backoffice/Dashboard.aspx" class="hover:underline">#...<%# Eval("Orden.ShortId") %></a>
                                                        </dd>
                                                    </dl>
                                                    <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                                                        <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Cliente</dt>
                                                        <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%# Eval("Cliente.NombreApellido") %></dd>
                                                    </dl>
                    
                                                    <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                                                        <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Hora</dt>
                                                        <dd class="mt-1.5 text-base font-semibold text-gray-900 dark:text-white"><%# Eval("Orden.HoraEntrega") %></dd>
                                                    </dl>
                    
                                                    <dl class="w-1/2 sm:w-1/4 lg:w-auto lg:flex-1">
                                                        <dt class="text-base font-medium text-gray-500 dark:text-gray-400">Estado</dt>
                                                        <dd class="me-2 mt-1.5 inline-flex items-center rounded bg-red-100 px-2.5 py-0.5 text-xs font-medium text-red-800 dark:bg-red-900 dark:text-red-300">
                                                            <svg class="me-1 h-3 w-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18 17.94 6M18 18 6.06 6" />
                                                            </svg>
                                                            Cancelled
                                                        </dd>
                                                    </dl>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    
                                </div>
                                <!-- Modal footer -->
                                <div class="flex items-center p-4 md:p-5 border-t border-gray-200 rounded-b dark:border-gray-600">
                                    <button data-modal-hide="dayOrders" type="button" class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Nueva Orden</button>
                                </div>
                            </div>
                        </div>
                    </div>
                     
                 </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

