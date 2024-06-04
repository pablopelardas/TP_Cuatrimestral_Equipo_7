<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Productos.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Productos</h4>
    <a href="EditarProducto.aspx" class="btn btn-primary">Nuevo Producto</a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="server">
    <div class="container">
        <div class="tbl-header">
            <div class="tbl-search">
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar..." AutoPostBack="True" OnTextChanged="txtBuscar_TextChanged"></asp:TextBox>
            </div>
        </div>
        <div class="tbl-body">
            <% foreach (Dominio.Modelos.ProductoModelo producto in productos) { %>
            <a class="tbl-row" href="DetalleProducto.aspx?id=<%: producto.IdProducto %>">
                <div class="tbl-cell">
                    <span><%: producto.Nombre %></span>
                </div>
                <div class="tbl-cell">
                    <span><%: producto.Descripcion %></span>
                </div>
                <div class="tbl-cell">
                    <span><%: producto.Porciones%></span>
                </div>
                <div class="tbl-cell">
                    <span><%: producto.HorasTrabajo%></span>
                </div>
                <div class="tbl-cell">
                    <span><%: producto.TipoPrecio%></span>
                </div>
                <div class="tbl-cell">
                    <span><%: producto.ValorPrecio%></span>
                </div>
                <div class="tbl-cell">
                    <span><%: producto.Categoria %></span>
                </div>
            </a>
            <% } %>
        </div>
    </div>
</asp:Content>