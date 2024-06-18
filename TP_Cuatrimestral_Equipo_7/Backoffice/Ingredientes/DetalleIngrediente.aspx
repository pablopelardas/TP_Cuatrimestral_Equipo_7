<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTailwind.Master" AutoEventWireup="true" CodeBehind="DetalleIngrediente.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ingredientes.DetalleIngrediente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- DIV Detalles de evento  --%>
    <% if (ingrediente != null)
        { %>
    <form action="#" class="mx-auto max-w-screen-xl px-4 ">
        <div class="mx-auto max-w-3xl px-4">
            <div class="flex justify-between align-items-center">
                <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl"><%: ingrediente.Nombre %></h2>
                <a href="<%: redirect_to %>" class="flex align-items-center gap-2 text-gray-800 dark:text-white hover:text-primary-600 hover:dark:text-primary-600 ">
                    <svg class="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h14M5 12l4-4m-4 4 4 4" />
                    </svg>
                    <span>VOLVER
                    </span>
                </a>
            </div>
            <a href="EditarIngrediente.aspx?id=<%: ingrediente.IdIngrediente %>&redirect_to=DetalleIngrediente.aspx?id=<%: ingrediente.IdIngrediente %>" class="inline-flex items-center justify-center mt-3 rounded-lg bg-primary-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800">Editar ingrediente</a>
            <div class="mt-6 space-y-4 border-b border-t border-gray-200 py-8 dark:border-gray-700 sm:mt-8">
                <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Información General</h4>
                <dl class="w-full flex justify-between text-start flex-wrap flex-col lg:flex-row">
                    <div class="w-full mt-5 flex flex-col flex-wra">
                        <dt class=" text-base font-medium text-gray-900 dark:text-white">Nombre</dt>
                        <dd class=" mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: ingrediente.Nombre %></dd>
                    </div>
                    <div class="w-full mt-5 flex flex-col flex-wra">
                        <dt class=" text-base font-medium text-gray-900 dark:text-white">Cantidad</dt>
                        <dd class=" mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: ingrediente.Cantidad %></dd>
                    </div>
                    <div class="w-full mt-5 flex flex-col flex-wra">
                        <dt class=" text-base font-medium text-gray-900 dark:text-white">Costo</dt>
                        <dd class=" mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: ingrediente.Costo %></dd>
                    </div>
                    <div class="w-full mt-5 flex flex-col flex-wra">
                        <dt class=" text-base font-medium text-gray-900 dark:text-white">Costo Normalizado</dt>
                        <dd class=" mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: ingrediente.CostoNormalizado %></dd>
                    </div>
                    <div class="mt-5 flex flex-col flex-wrap">
                        <dt class="text-base font-medium text-gray-900 dark:text-white">Proveedor</dt>
                        <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: ingrediente.Proveedor %></dd>
                    </div>
                </dl>
            </div>
            <div class="mt-6 sm:mt-8">
                    <div class="gap-4 sm:flex sm:items-center">
                        <a href="./Default.aspx" type="button" class="w-full text-center rounded-lg  border border-gray-200 bg-white px-5  py-2.5 text-sm font-medium text-gray-900 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:outline-none focus:ring-4 focus:ring-gray-100 dark:border-gray-600 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white dark:focus:ring-gray-700">Volver al dashboard</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <% } %>
</asp:Content>
