<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTailwind.Master" AutoEventWireup="true" CodeBehind="EditarContacto.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Contactos.EditarContacto"  ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Css/chosen.css" rel="stylesheet"/>

    <link rel="stylesheet" href="/Js/richtexteditor/rte_theme_default.css"/>
    <script type="text/javascript" src="/Js/chosen.jquery.js"></script>
    <script type="text/javascript" src="/Js/richtexteditor/rte.js"></script>
    <script type="text/javascript" src="/Js/richtexteditor/plugins/all_plugins.js"></script>
    
    <style>
        .modal-body .chzn-container.chzn-container-single, .modal-body .chzn-drop {
            /*width: 100%*/
        }
        .modal-body .chzn-drop input{
            /*width: 100% !important;*/
        }
        
        .rte-fixed.rte-floatpanel-paragraphop{
            display: none !important;
        }
    </style>
    
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>

    <% if (contacto != null) { %>
        <form action="#" class="mx-auto max-w-screen-xl px-4">
            <div class="mx-auto max-w-3xl px-4">
                <div class="flex-col">
                <% if (id != Guid.Empty)
                           { %>
                            <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl sm:mb-4">Editando Contacto #<%: contacto.Id %></h2>
                        <% }
                           else
                           { %>
                            <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl sm:mb-4">Nuevo Contacto</h2>
                        <% } %>
                        <a href="<%: redirect_to %>" class="flex align-items-center gap-2 text-gray-800 dark:text-gray-400 hover:text-gray-900 dark:hover:text-gray-200">
                            <svg class="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h14M5 12l4-4m-4 4 4 4" />
                            </svg>
                            <span>VOLVER
                            </span>
                        </a>
                </div>
                 <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="mt-6 space-y-4 border-b border-t border-gray-200 py-8 dark:border-gray-700 sm:mt-8">
                             <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Información General</h4>
                            <div class="w-full flex justify-between text-start flex-wrap gap-4">
                                <div class="w-full mt-3 sm:mt-0 sm:w-2/5 flex flex-col flex-wrap  ">
                                    <label id="lblTipoContacto" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Tipo<span class="text-red-500">*</span></label>
                                    <asp:DropDownList ID="ddTipoContacto" CssClass="chzn-select" AutoPostBack="True" AppendDataBoundItems="True" runat="server">
                                        <asp:ListItem Text="Seleccione una opción" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Cliente" Value="Cliente"></asp:ListItem>
                                        <asp:ListItem Text="Proveedor" Value="Proveedor"></asp:ListItem>
                                    </asp:DropDownList>
                                    <!-- add validation for ddCliente -->
                                </div>
                                <div class="w-full sm:w-2/5 flex flex-col flex-wrap  ">
                                    <label id="lblNombreYApellido" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Nombre y Apellido <span class="text-red-500">*</span></label>
                                    <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtNombreYApellido" runat="server"></asp:TextBox>
                                </div>
                                <div class="w-full sm:w-2/5 flex flex-col flex-wrap  ">
                                    <label id="lblCorreo" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Correo</label>
                                    <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtCorreo" runat="server"></asp:TextBox>
                                </div>
                                <div class="w-full sm:w-2/5 flex flex-col flex-wrap  ">
                                    <label id="lblTelefono" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Telefono</label>
                                    <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtTelefono" runat="server"></asp:TextBox>
                                </div>
                                <div class="w-full sm:w-2/5 flex flex-col flex-wrap  ">
                                    <label id="lblFuente" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Fuente<span class="text-red-500">*</span></label>
                                    <asp:DropDownList ID="ddFuente" CssClass="chzn-select" AutoPostBack="True" AppendDataBoundItems="True" runat="server">
                                        <asp:ListItem Text="Seleccione una opción" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Conocido" Value="Otro"></asp:ListItem>
                                        <asp:ListItem Text="Facebook" Value="Facebook"></asp:ListItem>
                                        <asp:ListItem Text="Instagram" Value="Instagram"></asp:ListItem>
                                        <asp:ListItem Text="Google" Value="Google"></asp:ListItem>
                                        <asp:ListItem Text="Referido" Value="Referido"></asp:ListItem>
                                        <asp:ListItem Text="Otro" Value="Otro"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="w-full sm:w-2/5 flex gap-4 flex-wrap  ">
                                    <div>
                                        <label id="lblDeseaRecibirCorreos" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Recibir correos</label>
                                        <asp:CheckBox ID="chkDeseaRecibirCorreos" runat="server" />
                                    </div>
                                    <div>
                                        <label id="lblDeseaRecibirWhatsapps" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Recibir whatsapps</label>
                                        <asp:CheckBox ID="chkDeseaRecibirWhatsapps" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="mt-6 space-y-4 border-b border-t border-gray-200 py-8 dark:border-gray-700 sm:mt-8">
                             <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Información Extra</h4>
                            <div class="w-full flex justify-between text-start flex-wrap gap-4">
                                <div class="w-full flex flex-col flex-wrap  ">
                                    <div class="max-w-full" id="contactoExtraEditor"></div>
                                    <asp:TextBox CssClass="hidden" id="txtContactoExtra" ClientIDMode="Static" runat="server" OnLoad="OnContactoExtraLoad"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="space-y-4 border-b border-gray-200 py-8 dark:border-gray-700">
                            <%-- <asp:PlaceHolder ID="phDetalleOrden" runat="server"></asp:PlaceHolder> --%>
                            <div class="flex justify-between items center mb-4">
                                <asp:Label ID="lblDirecciones" CssClass="text-lg font-semibold text-gray-900 dark:text-white" runat="server">Direcciones</asp:Label>
                                <asp:Button ID="btnAgregarDireccion" runat="server" Text="Agregar Dirección" CssClass="inline-flex items-center justify-center rounded-lg bg-primary-700 px-5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 cursor-pointer" type="button" OnClick="btnAgregarDireccion_Click"/>
                
                            </div>
                            <table class="w-full mt-5 text-left font-medium text-gray-900 dark:text-white ">
                                <thead class="">
                                    <tr>
                                        <th class="">Calle y Número</th>
                                        <th class="">Piso y Departamento</th>
                                        <th class="">Localidad</th>
                                        <th class="">Provincia</th>
                                    </tr>
                                </thead>
                                <tbody class="divide-y divide-gray-200 dark:divide-gray-800">
                                    <asp:Repeater runat="server" id="rptDirecciones">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="py-2 font-bold text-gray-900 dark:text-gray-400"><%# Eval("CalleNumero") %></td>
                    
                                                <td class="py-2 text-base font-bold text-gray-900 dark:text-gray-400 hidden md:table-cell"><%# Eval("Piso") %><%# Eval("Departamento") %></td>
                    
                                                <td class="py-2 text-base font-bold text-gray-900 dark:text-gray-400"><%# Eval("Localidad") %> (<%# Eval("CodigoPostal") %>)</td>
                                                
                                                <td class="py-2 text-base font-bold text-gray-900 dark:text-gray-400"><%# Eval("Provincia") %> </td>
                    
                                                <td class="py-2 ">
                                                    <div class="flex justify-end gap-2">
                                                        <button id="dropdownDefaultButton<%# Eval("IdDireccion") %>" data-dropdown-toggle="dropdown<%# Eval("IdDireccion") %>" class="text-white font-medium text-sm" type="button">
                                                            <svg class="w-2.5 h-2.5 ms-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
                                                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 4 4 4-4"/>
                                                            </svg>
                                                        </button>
                    
                                                        <!-- Dropdown menu -->
                                                        <div id="dropdown<%# Eval("IdDireccion") %>" class="z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow w-auto dark:bg-gray-700">
                                                            <ul class="py-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownDefaultButton">
                                                                <li>
                                                                    <asp:Button ID="btnEditarDireccion" runat="server" Text="Editar" CssClass="block w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white" OnClick="btnEditarDireccion_Click" CommandArgument='<%# Eval("IdDireccion") %>'/>
                                                                </li>
                                                                <li>
                                                                    <asp:Button ID="btnEliminarDireccion" runat="server" Text="Eliminar" CssClass="block w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white" OnClick="btnEliminarDireccion_Click" CommandArgument='<%# Eval("IdDireccion") %>'/>
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
                        <div class="space-y-4 border-b border-gray-200 py-8 dark:border-gray-700">
                            <div class="flex justify-between items center mb-4">
                                <asp:Label ID="lblEventos" CssClass="text-lg font-semibold text-gray-900 dark:text-white" runat="server">Eventos</asp:Label>
                                <asp:Button ID="btnAgregarEvento" runat="server" Text="Agregar Evento" CssClass="inline-flex items-center justify-center rounded-lg bg-primary-700 px-5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 cursor-pointer" type="button" OnClick="btnAgregarEvento_Click"/>
                
                            </div>
                            <table class="w-full mt-5 text-left font-medium text-gray-900 dark:text-white ">
                                <thead class="">
                                    <tr>
                                        <th class="">Tipo</th>
                                        <th class="">Fecha</th>
                                        <th class="">Descripción</th>
                                    </tr>
                                </thead>
                                <tbody class="divide-y divide-gray-200 dark:divide-gray-800">
                                    <asp:Repeater runat="server" id="rptEventos">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="py-2 font-bold text-gray-900 dark:text-gray-400"><%# Eval("TipoEvento.Nombre") %></td>
                    
                                                <td class="py-2 text-base font-bold text-gray-900 dark:text-gray-400 hidden md:table-cell"><%# ((DateTime)Eval("Fecha")).ToShortDateString() %></td>
                    
                                                <td class="py-2 text-base font-bold text-gray-900 dark:text-gray-400"><%# Eval("Descripcion") %></td>
                                                
                                                <td class="py-2 ">
                                                    <div class="flex justify-end gap-2">
                                                        <button id="dropdownDefaultButton<%# Eval("IdEvento") %>" data-dropdown-toggle="dropdown<%# Eval("IdEvento") %>" class="text-white font-medium text-sm" type="button">
                                                            <svg class="w-2.5 h-2.5 ms-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
                                                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 4 4 4-4"/>
                                                            </svg>
                                                        </button>
                    
                                                        <!-- Dropdown menu -->
                                                        <div id="dropdown<%# Eval("IdEvento") %>" class="z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow w-auto dark:bg-gray-700">
                                                            <ul class="py-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownDefaultButton">
                                                                <li>
                                                                    <asp:Button ID="btnEditarEvento" runat="server" Text="Editar" CssClass="block w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white" OnClick="btnEditarEvento_Click" CommandArgument='<%# Eval("IdEvento") %>'/>
                                                                </li>
                                                                <li>
                                                                    <asp:Button ID="btnEliminarEvento" runat="server" Text="Eliminar" CssClass="block w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white" OnClick="btnEliminarEvento_Click" CommandArgument='<%# Eval("IdEvento") %>'/>
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
                       <% if (id != Guid.Empty) {%>
                        <div class="space-y-4 py-8 dark:border-gray-700">
                            <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Justificación de la modificación <span class="text-red-500">*</span></h4>
                            <div>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtJustificacion" CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" Height="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvJustificacion" ControlToValidate="txtJustificacion" ErrorMessage="Debe justificar la modificación" Display="Dynamic" CssClass="text-red-500 text-sm" />
                                <asp:CustomValidator runat="server" ID="cvJustificacion" ControlToValidate="txtJustificacion" ErrorMessage="La justificación debe tener al menos 10 caracteres" Display="Dynamic" CssClass="text-red-500 text-sm" OnServerValidate="validateJustificacion"></asp:CustomValidator>
                            </div>
                        </div>
                        <% } %>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="flex justify-between pt-8">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="inline-flex items-center justify-center rounded-lg bg-gray-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-gray-700 dark:hover:bg-gray-800 dark:focus:ring-gray-800 cursor-pointer" OnClick="btnCancelar_Click"/>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="inline-flex items-center justify-center rounded-lg bg-primary-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 cursor-pointer" OnClick="btnGuardar_Click"/>
                </div>
            </div>
        </form>
        <!-- add prevent default y prevent propagation to click -->
        <div id="modalDireccion" data-modal-backdrop="static" tabindex="-1" aria-hidden="true" class="hidden overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full">
                <div class="relative p-4 w-full max-w-2xl max-h-full">
        
                    <!-- Modal content -->
                    <div class="px-4 relative bg-white rounded-lg shadow dark:bg-gray-800">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <!-- Modal header -->
                                <div class="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
                                    <h3 class="text-xl font-semibold text-gray-900 dark:text-white">
                                        <asp:Label runat="server" Text="Agregar Dirección" ID="lblTitleModalDireccion"></asp:Label>
                                    </h3>
                                    <button type="button" class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="modalDireccion">
                                        <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                                            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                                        </svg>
                                        <span class="sr-only">Close modal</span>
                                    </button>
                                </div>
                                <!-- Modal body -->
                                <div class="modal-body p-5">
                                    <div class="w-full flex justify-between text-start flex-wrap gap-4">
                                        <div class="w-full sm:w-2/5 flex flex-col flex-wrap  ">
                                            <label id="lblCalleYNumero" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Calle y Número <span class="text-red-500">*</span></label>
                                            <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtCalleYNumero" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="w-full sm:w-2/5 flex flex-col flex-wrap  ">
                                            <label id="lblLocalidad" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Localidad<span class="text-red-500">*</span></label>
                                            <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtLocalidad" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="w-full sm:w-2/5 flex flex-col flex-wrap  ">
                                            <label id="lblPiso" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Piso</label>
                                            <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtPiso" runat="server"></asp:TextBox>
                                        </div>                                        
                                        <div class="w-full sm:w-2/5 flex flex-col flex-wrap  ">
                                            <label id="lblDepartamento" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Departamento</label>
                                            <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtDepartamento" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="w-full sm:w-2/5 flex flex-col flex-wrap  ">
                                            <label id="lblCodigoPostal" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Codigo Postal</label>
                                            <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtCodigoPostal" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="w-full sm:w-2/5 flex flex-col flex-wrap  ">
                                            <label id="lblProvincia" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Provincia</label>
                                            <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtProvincia" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <!-- Modal footer -->
                                <div class="flex items-center p-4 md:p-5 border-t border-gray-200 rounded-b dark:border-gray-600">
        
                                    <asp:Button ID="btnCancelarModalDireccion" OnClick="HideModalDireccion" runat="server" Text="Cerrar" data-bs-dismiss="modal" CssClass="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"/>
                                    <asp:Button ID="btnGuardarModalDireccion" runat="server" Text="Guardar" CssClass="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" OnClick="onGuardarModalDireccion"/>
        
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnGuardarModalDireccion" EventName="Click"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
    
        <!-- Modal toggle -->
        <button data-modal-target="modalDireccion" data-modal-toggle="modalDireccion" class="hidden" type="button">
        </button>
    
        <%-- <!-- Modal toggle --> --%>
        <button data-modal-target="modalEvento" data-modal-toggle="modalEvento" class="hidden" type="button">
        </button>
    
     <div id="modalEvento" data-modal-backdrop="static" tabindex="-1" aria-hidden="true" class="hidden overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full">
                    <div class="relative xs:p-4 w-full max-w-2xl max-h-full">
            
                        <!-- Modal content -->
                        <div class="xs:px-4 relative bg-white rounded-lg shadow dark:bg-gray-800">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <!-- Modal header -->
                                    <div class="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
                                        <h3 class="text-xl font-semibold text-gray-900 dark:text-white">
                                            <asp:Label runat="server" Text="Agregar Evento" ID="lblTitleModalEvento"></asp:Label>
                                        </h3>
                                        <button type="button" class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="modalEvento">
                                            <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                                            </svg>
                                            <span class="sr-only">Close modal</span>
                                        </button>
                                    </div>
                                    <!-- Modal body -->
                                    <div class="modal-body xs:p-5">
                                        <div class="w-full flex justify-between text-start flex-wrap gap-4">
                                             <div class="w-full mt-3 flex flex-col flex-wrap  ">
                                                <label id="lblTipoEvento" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Tipo de evento <span class="text-red-500">*</span> </label>
                                                <asp:DropDownList ID="ddTipoEvento" CssClass="chzn-select"  AppendDataBoundItems="True" DataTextField="Nombre" DataValueField="IdTipoEvento" DataSourceID="odsTipoEvento" runat="server">
                                                    <asp:ListItem Text="Seleccione una opción" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="odsTipoEvento" runat="server" SelectMethod="ListarTipoDeEventos" TypeName="Negocio.Servicios.EventoServicio"></asp:ObjectDataSource>
                                            </div>
                                            <div class="w-full mt-3 flex flex-col flex-wrap justify-end">
                                                <label id="lblFechaEvento" runat="server" class="block mb-5 text-sm font-medium text-gray-900 dark:text-white">Fecha: <%: FechaEventoSeleccionada != null ? FechaEventoSeleccionada : "" %> <span class="text-red-500">*</span></label>
                                                <asp:PlaceHolder ID="phCalendarioEvento" runat="server"></asp:PlaceHolder>
                                            </div>
                                            <div class="w-full flex flex-col">
                                                <label id="lblDescEvento" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Descripción del evento</label>
                                                 <asp:TextBox runat="server" TextMode="MultiLine" ID="txtEventoDesc" CssClass="bg-gray-50 h-[24px] resize-none border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" Height="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Modal footer -->
                                    <div class="flex items-center gap-4 justify-between flex-wrap p-4 md:p-5 border-t border-gray-200 rounded-b dark:border-gray-600">
                                        <asp:Button ID="btnCancelarModalEvento" OnClick="HideModalEvento" runat="server" Text="Cerrar" data-bs-dismiss="modal" CssClass="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"/>
                                        <asp:Button ID="btnGuardarModalEvento" runat="server" Text="Guardar" CssClass="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" OnClick="OnGuardarModalEvento"/>
            
                                    </div>
                        
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGuardarModalDireccion" EventName="Click"></asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
    
    
    
    
        
    <% }%>
<script>
    function ShowModal(modalId) {
        const id = modalId.getAttribute('id');
        const button = document.querySelector(`[data-modal-toggle="${id}"]`);
        console.log(button)
        button.click();
    }
    
    function HideModal(modalId) {
        const id = modalId.getAttribute('id');
        const button = document.querySelector(`[data-modal-hide="${id}"]`);
        setTimeout(() => button.click(), 1);
    }
    
    
    function LoadContactoExtra() {
        console.log("Cargando editor de contacto extra");
        let options = {
            toolbar: "basic",
            editorResizeMode: "height",
            showPlusButton: false,
            showTagList: false,
            showStatistics: false,
            toggleBorder: true,
        };
        let rte = new RichTextEditor("#contactoExtraEditor", options);
        let tiny = document.getElementById("txtContactoExtra");
        rte.setHTMLCode(tiny.value);
        rte.attachEvent("change", function (e) {
            tiny.value = rte.getHTMLCode();
        });
        tiny.addEventListener("change", function (e) {
                rte.setHTMLCode("");
        });
    }
 
    </script>

</asp:Content>
