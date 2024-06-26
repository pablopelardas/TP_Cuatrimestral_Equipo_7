﻿<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTailwind.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Productos.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- DIV Detalles de evento  --%>
    <% if (producto != null)
        { %>
    <form action="#" class="mx-auto max-w-screen-xl px-4 ">
        <div class="mx-auto max-w-3xl px-4">
            <div class="flex justify-between align-items-center">
                <h2 class="text-xl font-semibold text-gray-900 dark:text-white sm:text-2xl"><%: producto.Nombre %></h2>
                <a href="<%: redirect_to %>" class="flex align-items-center gap-2 text-gray-800 dark:text-white hover:text-primary-600 hover:dark:text-primary-600 ">
                    <svg class="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h14M5 12l4-4m-4 4 4 4" />
                    </svg>
                    <span>VOLVER
                    </span>
                </a>
            </div>
            <a href="Editarproducto.aspx?id=<%: producto.IdProducto %>&redirect_to=Detalleproducto.aspx?id=<%: producto.IdProducto %>" class="inline-flex items-center justify-center mt-3 rounded-lg bg-primary-700 px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800">Editar producto</a>
            <div class="mt-6 space-y-4 border-b border-t border-gray-200 py-8 dark:border-gray-700 sm:mt-8">
                <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Información General</h4>
                <dl class="w-full flex justify-between text-start flex-wrap flex-col lg:flex-row">
                    <div class="w-full mt-5 flex flex-col flex-wra">
                        <dt class=" text-base font-medium text-gray-900 dark:text-white">Nombre</dt>
                        <dd class=" mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: producto.Nombre %></dd>
                    </div>
                    <div class="mt-5 flex flex-col flex-wrap">
                        <dt class="text-base font-medium text-gray-900 dark:text-white">Categoria</dt>
                        <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: producto.Categoria.Nombre %></dd>
                    </div>
                </dl>
            </div>
            <div class="mt-6 sm:mt-8">
                <div class="relative overflow-x-auto border-b border-gray-200 dark:border-gray-800">

                    <table class="w-full mt-5 text-left font-medium text-gray-900 dark:text-white md:table-fixed">
                        <thead class="">
                            <tr>
                                <th class="text-lg font-semibold text-gray-900 dark:text-white">Receta</th>
                                <th class="p-4 text-right">Subtotal</th>
                            </tr>
                        </thead>

                        <tbody class="divide-y divide-gray-200 dark:divide-gray-800">

                            <%foreach (var item in Recetas)
                                { %>
                            <tr>
                                <td class="whitespace nowrap py-4">
                                    <div class="flex items center gap-4">
                                        <a href="#" class="flex gap-4 align-items-center">
                                            <div class="flex items-center aspect-square w-20 shrink-0 relative">
                                                <div class="w-36 fill-primary-600 category-svg">
                                                    <object type="image/svg+xml" data="<%: item.Receta.Categoria.ImagenPath %>"></object>
                                                </div>

                                                <div class="absolute bg-primary-300  w-6 h-6 rounded-full text-primary-900 text-center place-content-center text-sm top-1/2 right-4">
                                                    <%: item.Cantidad %>
                                                </div>
                                            </div>
                                            <a href="/Backoffice/Recetas/DetalleReceta.aspx?id=<%: item.Receta.IdReceta %>" class="content-center hover:underline"><%: item.Receta.Nombre %></a>
                                        </a>
                                    </div>
                                </td>
                                <td class="p-4 text-right text-base font-bold text-gray-900 dark:text-white">$<%: item.Receta.CostoTotal.ToString("F2") %></td>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>
                    </div>
                    <table class="w-full mt-5 text-left font-medium text-gray-900 dark:text-white md:table-fixed">
                        <thead class="">
                            <tr>
                                <th class="text-lg font-semibold text-gray-900 dark:text-white">Suministro</th>
                                <th class="p-4 text-right">Subtotal</th>
                            </tr>
                        </thead>

                        <tbody class="divide-y divide-gray-200 dark:divide-gray-800">

                            <%foreach (var item in Suministros)
                                { %>
                            <tr>
                                <td class="whitespace nowrap py-4">
                                    <div class="flex items center gap-4">
                                        <a href="#" class="flex gap-4 align-items-center">
                                            <div class="flex items-center aspect-square w-20 shrink-0 relative">
                                                <div class="w-36 fill-primary-600 category-svg">
                                                    <object type="image/svg+xml" data="<%: item.Suministro.Categoria.ImagenPath %>"></object>
                                                </div>

                                                <div class="absolute bg-primary-300  w-6 h-6 rounded-full text-primary-900 text-center place-content-center text-sm top-1/2 right-4">
                                                    <%: item.Cantidad %>
                                                </div>
                                            </div>
                                            <a href="/Backoffice/Recetas/DetalleReceta.aspx?id=<%: item.Suministro.IdSuministro %>" class="content-center hover:underline"><%: item.Suministro.Nombre %></a>
                                        </a>
                                    </div>
                                </td>
                                <td class="p-4 text-right text-base font-bold text-gray-900 dark:text-white">$<%: item.Suministro.Costo.ToString("F2") %></td>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>
                
                <div class="mt-6 space-y-4 border-b border-t border-gray-200 py-8 dark:border-gray-700 sm:mt-8">
                    <h4 class="text-lg font-semibold text-gray-900 dark:text-white">Detalle</h4>
                    <div class="w-full flex flex-wrap justify-between">
                        <div class="w-full lg:w-1/2 flex flex-col space-y-4">
                            <dl class="w-full">
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Porciones</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: producto.Porciones %></dd>
                                </div>
                            </dl>
                            <dl class="w-full">
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Horas de Trabajo</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: producto.HorasTrabajo %></dd>
                                </div>
                            </dl>
                            <dl class="w-full">
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Tipo de Precio</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: producto.TipoPrecio %></dd>
                                </div>
                            </dl>

                            <%if(producto.TipoPrecio == "Margen") {%>
                            <dl class="w-full">
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Overhead</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"> <%: producto.ValorPrecio %></dd>
                                </div>
                            </dl>
                            <%} %>
                            <dl class="w-full">
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Recetas</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400">$<%: producto.ListaCompra.TotalIngredientes%></dd>
                                </div>
                            </dl>
                            <dl class="w-full">
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Suministros</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400">$<%: producto.ListaCompra.TotalSuministros%></dd>
                                </div>
                            </dl>
                        </div>
                        <div class="w-full lg:w-1/2 flex flex-col space-y-4">
                            <dl class="w-full">
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Costo Total</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: producto.Costo.ToString("F2") %></dd>
                                </div>
                            </dl>
                            <dl class="w-full">
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Costo por Porcion</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: (producto.Costo / producto.Porciones).ToString("F2") %></dd>
                                </div>
                            </dl>
                            <dl class="w-full">
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Precio de Venta</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: producto.Precio.ToString("F2") %></dd>
                                </div>
                            </dl>
                            <dl class="w-full">
                                <div class="mt-5 flex flex-col flex-wrap">
                                    <dt class="text-base font-medium text-gray-900 dark:text-white">Ganancia Bruta</dt>
                                    <dd class="mt-1 text-base font-normal text-gray-500 dark:text-gray-400"><%: (producto.Precio - producto.Costo).ToString("F2") %></dd>
                                </div>
                            </dl>
                        </div>
                    </div>
                </div>

                <div class="mt-4 space-y-6">
                    <h4 class="text-xl font-semibold text-gray-900 dark:text-white">Resumen</h4>

                    <div class="space-y-4">
                        <div class="space-y-2">
                            <dl class="flex items-center justify-between gap-4">
                                <dt class="text-gray-500 dark:text-gray-400">Costo</dt>
                                <dd class="text-base font-medium text-gray-900 dark:text-white">$<%: producto.Costo %></dd>
                            </dl>
                        </div>

                        <dl class="flex items-center justify-between gap-4">
                            <dt class="text-gray-500 dark:text-gray-400">Precio</dt>
                            <dd class="text-base font-medium text-gray-900 dark:text-white">$<%: producto.Precio %></dd>
                        </dl>

                    </div>

                    <div class="gap-4 sm:flex sm:items-center">
                        <a href="./Default.aspx" type="button" class="w-full text-center rounded-lg  border border-gray-200 bg-white px-5  py-2.5 text-sm font-medium text-gray-900 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:outline-none focus:ring-4 focus:ring-gray-100 dark:border-gray-600 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white dark:focus:ring-gray-700">Volver al dashboard</a>
                        <button type="submit" class="mt-4 flex w-full items-center justify-center rounded-lg bg-primary-700  px-5 py-2.5 text-sm font-medium text-white hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300  dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 sm:mt-0">
                            <svg class="w-6 h-6 text-white " aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 4h1.5L8 16m0 0h8m-8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm.75-3H7.5M11 7H6.312M17 4v6m-3-3h6" />
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

