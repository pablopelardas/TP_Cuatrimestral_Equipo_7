<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/LayoutTailwind.Master" AutoEventWireup="true" CodeBehind="EditarOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.EditarOrden" ValidateRequest="false"%>
<%@ Import Namespace="System.Web.Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Css/chosen.css" rel="stylesheet"/>
    <script src="/Js/jquery-3.7.1.min.js"></script>
    <script src="/Js/chosen.jquery.js" type="text/javascript"></script>
    <script src="https://cdn.tiny.cloud/1/valwbezytp23wuvlb68adt6hx9ggw67661q3p79cvj23ai0p/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script>


    <style type="text/tailwindcss">
        @layer base {
            .radioEntrega input[type="radio"] {
                @apply my-3 w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600 !important;
            }
            
            .radioEntrega label {
                @apply my-3 ms-2 text-sm font-medium text-gray-900 dark:text-gray-300 !important;
            }
            
        }
    </style>
    <style>
        .modal-body .chzn-container.chzn-container-single, .modal-body .chzn-drop {
            width: 100% !important;
        }
        .modal-body .chzn-drop input{
            width: 100% !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<% if (orden != null)
   { %>
    <form action="#" class="mx-auto max-w-screen-xl px-4">
        <div class="mx-auto max-w-3xl px-4">
            <div class="flex justify-between align-items-center">
                <% if (id != Guid.Empty)
                   { %>
                    <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl">Editando Orden #<%: orden.IdOrden %></h2>
                <% }
                   else
                   { %>
                    <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl">Nueva Orden</h2>
                <% } %>
                <a href="<%: redirect_to %>" class="flex align-items-center gap-2 text-gray-800 dark:text-white hover:text-primary-600 hover:dark:text-primary-600 ">
                    <svg class="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h14M5 12l4-4m-4 4 4 4"/>
                    </svg>
                    <span>
                        VOLVER
                    </span>
                </a>
            </div>
            <div class="mt-6 space-y-4 border-b border-t border-gray-200 py-8 dark:border-gray-700 sm:mt-8">
                <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Información General</h4>
                <div class="w-full flex justify-between text-start flex-wrap gap-4">
                    <div class="w-full mt-3 lg:mt-0 lg:w-2/5 flex flex-col flex-wrap  ">
                        <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Cliente</label>
                        <asp:PlaceHolder ID="phComboBoxCliente" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="w-full mt-3 lg:mt-0 lg:w-2/5 flex flex-col flex-wrap  ">
                        <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Tipo de evento: </label>
                        <asp:PlaceHolder ID="phComboBoxTipo" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="w-full mt-3 flex flex-col flex-wrap justify-end">
                        <label class="block mb-5 text-sm font-medium text-gray-900 dark:text-white">Fecha: <%: FechaSeleccionada != null ? FechaSeleccionada : "" %></label>
                        <asp:PlaceHolder ID="phCalendario" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="w-full mt-5 lg:mt-0 lg:w-2/5 flex flex-col flex-wrap">
                        <label class="block mb-5 text-sm font-medium text-gray-900 dark:text-white">Tipo de entrega: </label>
                        <asp:RadioButtonList CssClass="radioEntrega" ID="rbtnTipoEntrega" AutoPostBack="true" OnSelectedIndexChanged="rbtnTipoEntrega_SelectedIndexChanged" runat="server">
                            <asp:ListItem Text="Retira" Value="R"></asp:ListItem>
                            <asp:ListItem Text="Delivery" Value="D"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="w-full mt-3 lg:mt-0 lg:w-2/5 flex flex-col flex-wrap">
                        <div class="w-full mb-3">
                            <label for="inputHora" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Hora: </label>
                            <div class="relative">
                                <div class="absolute inset-y-0 end-0 top-0 flex items-center pe-3.5 pointer-events-none">
                                    <svg class="w-4 h-4 text-gray-500 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 24 24">
                                        <path fill-rule="evenodd" d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v4a1 1 0 0 0 .293.707l3 3a1 1 0 0 0 1.414-1.414L13 11.586V8Z" clip-rule="evenodd"/>
                                    </svg>
                                </div>
                                <input type="time" id="inputHora" class="bg-gray-50 h-[24px] border leading-none border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" value="15:00" required runat="server"/>
                            </div>
                        </div>
                    </div>
                     <% if (orden.TipoEntrega == "D")
                       { %>
                        <div class="w-full mt-3 flex flex-col flex-wrap justify-end MAP">
                                   
                            <label class="block mb-5 text-sm font-medium text-gray-900 dark:text-white">Direccion</label>
                            <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtDireccion"  runat="server"></asp:TextBox>
                            <% if (!string.IsNullOrEmpty(orden.DireccionEntrega.GoogleUrl))
                               { %>
                                <a href="<%: orden.DireccionEntrega.GoogleUrl %>" target="_blank" class="w-20 text-xs mt-3 font-medium text-gray-900 dark:text-white">Abrir en maps</a>
                            <% } %>
                            
                        </div>
                    <% } %>

                </div>
            </div>
            <div class="space-y-4 border-b border-gray-200 py-8 dark:border-gray-700">
                <%-- <asp:PlaceHolder ID="phDetalleOrden" runat="server"></asp:PlaceHolder> --%>
                <div class="flex justify-between items center mb-4">
                    <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Detalle de la orden</h4>
                    <asp:Button runat="server" Text="Agregar Producto" CssClass="inline-flex items-center justify-center rounded-lg bg-primary-700 px-5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 cursor-pointer" type="button" OnClick="btnAgregarProducto_Click"/>

                </div>
                <table class="w-full mt-5 text-left font-medium text-gray-900 dark:text-white md:table-fixed">
                    <thead class="">
                    <tr>
                        <th class="py-4">Producto</th>
                        <th class="p-4 text-center hidden md:table-cell">Precio</th>
                        <th class="p-4 text-right">Subtotal</th>
                        <th class="p-2 text-right"></th>
                    </tr>
                    </thead>

                    <tbody class="divide-y divide-gray-200 dark:divide-gray-800">

                    
                    <asp:Repeater runat="server" id="rptDetalleOrden">
                        <ItemTemplate>
                            <tr>
                                <td class="whitespace nowrap py-4">
                                    <div class="flex items center gap-4">
                                        <a href="#" class="flex gap-4 align-items-center">
                                            <div class="flex items center aspect-square w-20 shrink-0 relative">
                                                <div class="w-36 fill-primary-600 category-svg">
                                                    <object type="image/svg+xml" data="<%# Eval("Producto.Categoria.ImagenPath") %>"></object>
                                                </div>
                                                <div class="absolute bg-primary-300  w-6 h-6 rounded-full text-primary-900 text-center place-content-center text-sm top-1/2 right-4"><%# Eval("Cantidad") %></div>
                                            </div>
                                            <a href="#" class="content-center hover:underline"><%# Eval("Producto.Nombre") %></a>
                                        </a>
                                    </div>
                                </td>
                            
                                <td class="p-4 text-center text-base font-bold text-gray-900 dark:text-white hidden md:table-cell">$<%# Eval("PrecioUnitarioActual") %></td>
                            
                                <td class="p-4 text-right text-base font-bold text-gray-900 dark:text-white">$<%# Eval("Subtotal") %></td>
                            
                                <td class="p-4 text-right">
                                    <div class="flex justify-end gap-2">
                                        <button id="dropdownDefaultButton<%# Eval("Producto.IdProducto") %>" data-dropdown-toggle="dropdown<%# Eval("Producto.IdProducto") %>" class="text-white font-medium text-sm" type="button">
                                            <svg class="w-2.5 h-2.5 ms-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
                                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 4 4 4-4"/>
                                            </svg>
                                        </button>
                                        
                                        <!-- Dropdown menu -->
                                        <div id="dropdown<%# Eval("Producto.IdProducto") %>" class="z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow w-auto dark:bg-gray-700">
                                            <ul class="py-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownDefaultButton">
                                                <li>
                                                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="block w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white" OnClick="btnAbrirModalEditar_Click" CommandArgument='<%# Eval("Producto.IdProducto") %>'/>
                                                </li>
                                                <li>
                                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="block w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white" OnClick="btnEliminarProducto_Click" CommandArgument='<%# Eval("Producto.IdProducto") %>'/>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="space-y-4 border-b border-gray-200 pt-8 pb-4 dark:border-gray-700">
                <h4 class="text-xl font-semibold text-gray-900 dark:text-white">Resumen</h4>
                <div class="space-y-4">
                    <div class="space-y-2">
                        <dl class="flex items-center justify-between gap-4">
                            <dt class="text-gray-500 dark:text-gray-400">Subtotal</dt>
                            <dd class="text-base font-medium text-gray-900 dark:text-white">$<%: orden.Subtotal %></dd>
                        </dl>

                        <dl class="flex items-center justify-between gap-4">
                            <dt class="text-gray-500 dark:text-gray-400">Descuento %</dt>
                            <asp:TextBox ID="txtDescuento" runat="server" TextMode="Number" min="0" max="100" CssClass="bg-gray-50 h-[24px] w-[75px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" OnTextChanged="TotalChanged"></asp:TextBox>
                        </dl>

                        <dl class="flex items-center justify-between gap-4">
                            <dt class="text-gray-500 dark:text-gray-400">Costo Envío / Extras</dt>
                            <asp:TextBox ID="txtCostoEnvio" TextMode="Number" min="0" runat="server" CssClass="bg-gray-50 h-[24px] w-[75px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" OnTextChanged="TotalChanged"></asp:TextBox>
                        </dl>
                        <dl class="flex items-center justify-end">
                            <asp:Button ID="btnAplicarDescuento" runat="server" Text="Aplicar" CssClass="px-3 py-2 text-xs font-medium text-center text-white bg-primary-700 rounded-lg hover:bg-primary-800 focus:ring-4 focus:outline-none focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 cursor-pointer"/>
                        </dl>
                    </div>

                    <dl class="flex items-center justify-between gap-4 border-t border-gray-200 pt-2 dark:border-gray-700">
                        <dt class="text-lg font-bold text-gray-900 dark:text-white">Total</dt>
                        <dd class="text-lg font-bold text-gray-900 dark:text-white">$<%: orden.Total %></dd>
                    </dl>
                </div>
            </div>
            <div class="space-y-4 py-8 dark:border-gray-700">
                <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Información Extra</h4>
                <div>
                    <asp:TextBox TextMode="MultiLine" id="tiny" ClientIDMode="Static" runat="server" OnLoad="OnTinyLoad"></asp:TextBox>
                </div>
            </div>
            <div class="flex justify-between">
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="inline-flex items-center justify-center rounded-lg bg-gray-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-gray-700 dark:hover:bg-gray-800 dark:focus:ring-gray-800 cursor-pointer" OnClick="btnCancelar_Click"/>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="inline-flex items-center justify-center rounded-lg bg-primary-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 cursor-pointer" OnClick="btnGuardar_Click"/>
            </div>
        </div>
    </form>

    <%-- fullScreen loader with inline style --%>
    <div id="loader" class="fixed inset-0 bg-gray-900 opacity-50 flex items-center justify-center hidden">
        <div role="status">
            <svg aria-hidden="true" class="w-8 h-8 text-gray-200 animate-spin dark:text-gray-600 fill-blue-600" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor"/>
                <path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill"/>
            </svg>
            <span class="sr-only">Loading...</span>
        </div>
    </div>

    <!-- Modal toggle -->
    <button data-modal-target="agregarProductoModal" data-modal-toggle="agregarProductoModal" class="hidden" type="button">
    </button>

    <div id="agregarProductoModal" tabindex="-1" aria-hidden="true" class="hidden overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full">
        <div class="relative p-4 w-full max-w-2xl max-h-full">
            <!-- Modal content -->
            <div class="relative bg-white rounded-lg shadow dark:bg-gray-800">
                <!-- Modal header -->
                <div class="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
                    <h3 class="text-xl font-semibold text-gray-900 dark:text-white">
                        Modificar Detalle
                    </h3>
                    <button type="button" class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="agregarProductoModal">
                        <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                        </svg>
                        <span class="sr-only">Close modal</span>
                    </button>
                </div>
                <!-- Modal body -->
                <div class="modal-body p-5">
                    <div class="w-full flex justify-between text-start flex-wrap gap-4">
                        <div class="w-full mt-3 lg:mt-0 lg:w-2/5 flex flex-col flex-wrap">
                            <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Producto</label>
                            <asp:PlaceHolder ID="phComboBoxProducto" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="w-full mt-3 lg:mt-0 lg:w-2/5 flex flex-col flex-wrap">
                            <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Cantidad</label>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" TextMode="Number" min="1"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- Modal footer -->
                <div class="flex items-center p-4 md:p-5 border-t border-gray-200 rounded-b dark:border-gray-600">
                    <asp:Button ID="btnCancelarModal" runat="server" Text="Cerrar" data-bs-dismiss="modal" CssClass="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"/>
                    <asp:Button ID="btnGuardarModal" runat="server" Text="Guardar" CssClass="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" OnClick="OnAceptarAgregarProducto"/>
                </div>
            </div>
        </div>
    </div>
<% } %>


<script type="text/javascript">
        var loading = false;
        var loader = document.getElementById('loader');
        
    
        function ShowLoader() {
            loader.classList.remove('hidden');
        }
    
        function HideLoader() {
            loader.classList.add('hidden');
        }
    
        function ShowModal() {
    
            if (loading) {
                return;
            }
    
            loading = true;
            ShowLoader();
    
            setTimeout(() => {
                const button = document.querySelector('[data-modal-toggle="agregarProductoModal"]');
                // show modal
                button.click();
                loading = false;
                HideLoader();
            }, 1000);
    
            // wait for modal to be rendered with observer
        }
    </script>

<script>
    let autocomplete;
    function initAutocomplete(){
        autocomplete = new google.maps.places.Autocomplete(document.getElementById('txtDireccion'), {
            fields: ['place_id', 'formatted_address', 'geometry', 'name', 'url'],
            componentRestrictions: { country: ['AR'] },
        });
        google.maps.event.addDomListener(document.getElementById('txtDireccion'), 'keydown', function(e) { 
            if (e.keyCode == 13) 
            { 
                e.preventDefault(); 
            }
        });
        google.maps.event.addListener(autocomplete, 'place_changed', function() {
            let place = autocomplete.getPlace();
            __doPostBack('getPlaceDetails', JSON.stringify({
                lat: place.geometry.location.lat(),
                lng: place.geometry.location.lng(),
                formatted_address: place.formatted_address,
                placeId: place.place_id,
                url: place.url,
                name: place.name
            }));
        });
    }
</script>
<script async defer
    src="https://maps.googleapis.com/maps/api/js?key=<%: WebConfigurationManager.AppSettings["ApiKey:GoogleMaps"]%>&loading=async&libraries=places&callback=initAutocomplete">
</script>

<script type="text/javascript" language="javascript">
        function LoadTiny() {
            tinymce.init({
                selector: 'textarea#tiny',
                height: 500,
                plugins: [
                    'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
                    'anchor', 'searchreplace', 'visualblocks', 'code', 'fullscreen',
                    'insertdatetime', 'media', 'table', 'help', 'wordcount'
                ],
                toolbar: 'undo redo | blocks | ' +
                    'bold italic backcolor | alignleft aligncenter ' +
                    'alignright alignjustify | bullist numlist outdent indent | ' +
                    'removeformat',
                content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }',
              skin: 'oxide-dark',
              content_css: 'dark'
            });
        }

        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    </script>
</asp:Content>