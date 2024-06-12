﻿<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTailwind.Master" AutoEventWireup="true" CodeBehind="DetalleOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.DetalleOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- DIV Detalles de evento  --%>
    <% if (orden != null)
       { %>
        <form action="#" class="mx-auto max-w-screen-xl px-4 ">
            <div class="mx-auto max-w-3xl px-4">
                <div class="flex justify-between align-items-center">
                    <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl">Orden #<%: orden.IdOrden %></h2>
                    <a href="<%: redirect_to %>" class="flex align-items-center gap-2 text-gray-800 dark:text-white hover:text-primary-600 hover:dark:text-primary-600 ">
                        <svg class="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h14M5 12l4-4m-4 4 4 4"/>
                        </svg>
                        <span>
                            VOLVER
                        </span>
                    </a>
                </div>
                <a href="EditarOrden.aspx?id=<%: orden.IdOrden %>&redirect_to=DetalleOrden.aspx?id=<%: orden.IdOrden %>" class="inline-flex items-center justify-center mt-3 rounded-lg bg-primary-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800">Editar orden</a>
                <div class="mt-6 space-y-4 border-b border-t border-gray-200 py-8 dark:border-gray-700 sm:mt-8">
                    <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Información General</h4>
                    <dl class="w-full flex justify-between text-start flex-wrap flex-col lg:flex-row">
                        <div class="w-full mt-5 lg:mt-0 lg:w-1/3 flex flex-col flex-wrap">
                            <dt class=" text-base font-medium text-gray-900 dark:text-white">Cliente</dt>
                            <dd class=" mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: orden.Cliente.DatosDeContacto %></dd>
                        </div>
                        <div class="w-full mt-5 lg:mt-0 lg:text-center lg:w-1/3 flex flex-col flex-wrap lg:align-items-baseline">
                            <dt class="text-base font-medium text-gray-900 dark:text-white">Evento</dt>
                            <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: orden.Evento.DescripcionEventoOrden %></dd>
                        </div>
                        <div class="w-full mt-5 lg:mt-0 lg:text-right lg:w-1/3 flex flex-col flex-wrap">
                            <dt class=" text-base font-medium text-gray-900 dark:text-white">Entrega</dt>
                            <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: orden.DetalleEntrega %></dd>
                        </div>

                    </dl>
                </div>
                <div class="space-y-4 border-b py-8 dark:border-gray-700">
                    <h4 class="text-lg font-semibold flex flex-wrap text-gray-900 dark:text-white">
                        <span class="mr-4">Información de la orden</span>
                        <div class="relative <%: orden.Estado.PillClass %> !pr-8">
                            <%: orden.Estado.Nombre %>
                            <div class="absolute top-[6px] right-3">
                                <button id="dropdownEstadoButton" data-dropdown-toggle="dropdownEstado" class="text-gray-100 font-medium text-sm" type="button">
                                    <svg class="w-2.5 h-2.5 ms-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
                                        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 4 4 4-4"/>
                                    </svg>
                                </button>
                                <!-- Dropdown menu -->
                                <div id="dropdownEstado" class="z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow w-auto dark:bg-gray-700">
                                    <ul class="py-4 px-2 flex flex-col gap-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownDefaultButton">
                                        <asp:PlaceHolder runat="server" ID="phEstados"></asp:PlaceHolder>
                                    </ul>
                                </div>
                            </div>
                        </div>

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
                            Pagos
                            <span class="<%: orden.EstadoPago.PillClass %> ml-4"><%: orden.EstadoPago.Nombre %></span>
                        </div>
                        <asp:Button CssClass="inline-flex items-center justify-center rounded-lg bg-primary-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 cursor-pointer" ID="btnAgregarPago" runat="server" Text="Agregar Pago"></asp:Button>
                        <%-- <button type="button" data-modal-target="billingInformationModal" data-modal-toggle="billingInformationModal">Abrir modal</button> --%>
                    </h4>

                    <div class="flex justify-between">

                        <dl>
                            <dt class="text-base mb-3 font-medium text-gray-900 dark:text-white">Total</dt>
                            <dd class="text-base font-normal text-gray-900 dark:text-white">$<%: orden.Total %></dd>
                        </dl>
                        <dl>
                            <dt class="text-base mb-3 font-medium text-gray-900 dark:text-white">Pagado</dt>
                            <dd class="text-base font-normal text-gray-900 dark:text-white">$<%: 0 %></dd>
                            <%-- <dd class="text-base font-normal text-gray-900 dark:text-white">$<%: orden.Pagado %></dd> --%>
                        </dl>
                        <dl>
                            <dt class="text-base mb-3 font-medium text-gray-900 dark:text-white">Restante</dt>
                            <dd class="text-base font-normal text-gray-900 dark:text-white">$<%: orden.Total %></dd>
                            <%-- <dd class="text-base font-normal text-gray-900 dark:text-white">$<%: orden.Restante %></dd> --%>
                        </dl>
                    </div>
                </div>

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
                            <button type="submit" class="mt-4 flex w-full items-center justify-center rounded-lg bg-primary-700  px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300  dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 sm:mt-0">
                                <svg class="w-6 h-6 text-white " aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 4h1.5L8 16m0 0h8m-8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm.75-3H7.5M11 7H6.312M17 4v6m-3-3h6"/>
                                </svg>
                                Generar lista de compras
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    <% } %>
</asp:Content>