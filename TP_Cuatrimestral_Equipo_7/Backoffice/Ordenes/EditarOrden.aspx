<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/LayoutTailwind.Master" AutoEventWireup="true" CodeBehind="EditarOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.EditarOrden"  ValidateRequest="false"%>

<%@ Register Src="~/Backoffice/Components/ComboBoxAutoComplete.ascx" TagPrefix="uc" TagName="ComboBoxAutoComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Css/chosen.css" rel="stylesheet" />
    <script src="/Js/jquery-3.7.1.min.js"></script>
    <script src="/Js/chosen.jquery.js" type="text/javascript"></script>
    <script src="https://cdn.tiny.cloud/1/valwbezytp23wuvlb68adt6hx9ggw67661q3p79cvj23ai0p/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script>

    <script type = "text/javascript" src = "https://unpkg.com/default-passive-events" ></script>
    
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
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%if (orden != null)
        {  %>
    <form action="#" class="mx-auto max-w-screen-xl px-4">
        <div class="mx-auto max-w-3xl px-4">
            <div class="flex justify-between align-items-center">
                <%if (id != Guid.Empty)
                    {  %>
                <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl">Editando Orden #<%: orden.IdOrden %></h2>
                <% } else { %>
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
                        <label class="block mb-5 text-sm font-medium text-gray-900 dark:text-white">Fecha: <%: FechaSeleccionada != null ? FechaSeleccionada : ""%></label>
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
                                            <path fill-rule="evenodd" d="M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12Zm11-4a1 1 0 1 0-2 0v4a1 1 0 0 0 .293.707l3 3a1 1 0 0 0 1.414-1.414L13 11.586V8Z" clip-rule="evenodd" />
                                        </svg>
                                    </div>
                                    <input type="time" id="inputHora" class="bg-gray-50 border leading-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" min="09:00" max="22:00" value="00:00" required runat="server" />
                                </div>
                        </div>
                        <% if (orden.TipoEntrega == "D"){%>
                        <div class="w-full">
                            <label for="txtDireccion" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Direccion</label>
                            <asp:TextBox CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ID="txtDireccion" runat="server"></asp:TextBox>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>
            <div class="space-y-4 border-b border-gray-200 py-8 dark:border-gray-700">
                <div class="w-full flex justify-between text-start flex-wrap gap-4">
                    <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Detalle de la orden</h4>
                </div>
                <asp:PlaceHolder ID="phDetalleOrden" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </form>

    <%-- DIV DETALLE ORDEN --%>
    <div class="detalle-orden">
        <div class="do-lista">
            <%-- <asp:PlaceHolder ID="phDetalleOrden" runat="server"></asp:PlaceHolder> --%>
        </div>
        <div class="do-detalle">
            <div>
                <span>Subtotal: </span>
                <span>$ <%: orden.Subtotal %></span>
            </div>
            <div>
                <span>Descuento (%): </span>
                <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control" OnTextChanged="TotalChanged"></asp:TextBox>
            </div>
            <div>
                <span>Costo envío / extra: </span>
                <asp:TextBox ID="txtCostoEnvio" runat="server" CssClass="form-control" OnTextChanged="TotalChanged"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnAplicarDescuento" runat="server" Text="Aplicar" CssClass="btn btn-primary" />
            </div>
            <div>
                <span>Total: </span>
                <span>$ <%: orden.Total %></span>
            </div>
        </div>
    </div>

    <%-- DIV OBSERVACIONES --%>
    <div class="observaciones">
        <h3>Observaciones</h3>
        <div>
            <asp:TextBox TextMode="MultiLine" id="tiny" ClientIDMode="Static" runat="server" OnLoad="OnTinyLoad"></asp:TextBox>
        </div>
    </div>
    
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
        
        <% } %>

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
                content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }'
            });
        }

        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    </script>
</asp:Content>

