<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTailwind.Master" AutoEventWireup="true" CodeBehind="EditarIngrediente.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ingredientes.EditarIngrediente" ValidateRequest="false" %>

<%@ Import Namespace="System.Web.Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Css/chosen.css" rel="stylesheet" />

    <%-- <script src="https://cdn.tiny.cloud/1/valwbezytp23wuvlb68adt6hx9ggw67661q3p79cvj23ai0p/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script> --%>

    <link rel="stylesheet" href="/Js/richtexteditor/rte_theme_default.css" />
    <script type="text/javascript" src="/Js/chosen.jquery.js"></script>
    <script type="text/javascript" src="/Js/richtexteditor/rte.js"></script>
    <script type="text/javascript" src="/Js/richtexteditor/plugins/all_plugins.js"></script>

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

            .modal-body .chzn-drop input {
                width: 100% !important;
            }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (ingrediente != null)
        { %>

    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>

    <form action="#" class="mx-auto max-w-screen-xl px-4">
        <div class="mx-auto max-w-3xl px-4">
            <div class="flex justify-between align-items-center">
                <% if (id != Guid.Empty)
                    { %>
                <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl">Editando <%: ingrediente.Nombre %></h2>
                <% }
                    else
                    { %>
                <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl">Nuevo Ingrediente</h2>
                <% } %>
                <a href="<%: redirect_to %>" class="flex align-items-center gap-2 text-gray-800 dark:text-white hover:text-primary-600 hover:dark:text-primary-600 ">
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
                                <label id="lblNombre" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Nombre <span class="text-red-500">*</span></label>
                                <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtNombre" runat="server"></asp:TextBox>
                            </div>
                            <div class="w-full mt-3 sm:mt-0 sm:w-2/5 flex flex-col flex-wrap  ">
                                <label id="lblUnidad" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Unidad<span class="text-red-500">*</span> </label>
                                <asp:DropDownList ID="ddUnidad" CssClass="chzn-select" AutoPostBack="True" AppendDataBoundItems="True" DataTextField="Nombre" DataValueField="Id" DataSourceID="odsUnidad" runat="server">
                                    <asp:ListItem Text="Seleccione una opción" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="odsUnidad" runat="server" SelectMethod="Listar" TypeName="Negocio.Servicios.UnidadMedidaServicio"></asp:ObjectDataSource>
                            </div>
                            <div class="w-full mt-3 sm:mt-0 sm:w-2/5 flex flex-col flex-wrap  ">
                                <label id="Label1" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Cantidad<span class="text-red-500">*</span></label>
                                <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtCantidad" runat="server"></asp:TextBox>
                            </div>
                            <div class="w-full mt-3 sm:mt-0 sm:w-2/5 flex flex-col flex-wrap  ">
                                <label id="Label2" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Costo<span class="text-red-500">*</span></label>
                                <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtCosto" runat="server"></asp:TextBox>
                            </div>
                            <div class="w-full mt-3 sm:mt-0 sm:w-2/5 flex flex-col flex-wrap  ">
                                <label id="Label3" runat="server" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Proveedor<span class="text-red-500">*</span></label>
                                <asp:TextBox CssClass="bg-gray-50 h-[24px] border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" ClientIDMode="Static" ID="txtProveedor" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="space-y-4 py-8 dark:border-gray-700">
                        </div>
                        <div class="flex justify-between">
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="inline-flex items-center justify-center rounded-lg bg-gray-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-gray-700 dark:hover:bg-gray-800 dark:focus:ring-gray-800 cursor-pointer" OnClick="btnCancelar_Click" />
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="inline-flex items-center justify-center rounded-lg bg-primary-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 cursor-pointer" OnClick="btnGuardar_Click" />
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>

    <% } %>


    <script type="text/javascript">

        function ShowModal() {
            console.log("Mostrando modal")
            const button = document.querySelector('[data-modal-toggle="agregarIngredienteModal"]');
            // setTimeout(() => button.click(), 1000);
            console.log(button)
            button.click();
        }

        function HideModal() {
            const button = document.querySelector('[data-modal-hide="agregarIngredienteModal"]');
            setTimeout(() => button.click(), 1);
        }


        function LoadTiny() {
            console.log("Cargando Tiny")
            let options = {
                toolbar: "basic",
                editorResizeMode: "height",
                showPlusButton: false,
                showTagList: false,
                showStatistics: false,
                toggleBorder: true,
            };
            let rte = new RichTextEditor("#tinyEditor", options);
            let tiny = document.getElementById("tiny");
            rte.setHTMLCode(tiny.value);
            rte.attachEvent("change", function (e) {
                tiny.value = rte.getHTMLCode();
            });
        }

        function pageLoad() {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

            initFlowbite();
        }
    </script>

</asp:Content>
