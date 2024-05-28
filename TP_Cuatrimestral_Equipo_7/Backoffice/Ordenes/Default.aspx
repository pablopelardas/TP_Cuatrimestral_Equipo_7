<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tbl-order-row{
            display: flex;
            justify-content: space-between;
            text-decoration: none;
        }
        .tbl-order-cell {
            display: flex;
            flex-direction: column;
            justify-content: center;
            margin-left: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">
    <h4>Contactos</h4>
    <a href="EditarContacto.aspx" class="btn btn-primary">Nuevo contacto</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="tbl-header">
            <div class="tbl-search">
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar..." AutoPostBack="True" OnTextChanged="txtBuscar_TextChanged"></asp:TextBox>
            </div>
            <div class="tbl-filter">
            <i class="fa-solid fa-filter"></i>
            <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                <asp:ListItem Value="0">Todos</asp:ListItem>
                <asp:ListItem Value="1">Proveedores</asp:ListItem>
                <asp:ListItem Value="2">Clientes</asp:ListItem>
            </asp:DropDownList>
            </div>
        </div>
        <div class="tbl-body">
            <% foreach (Dominio.Modelos.OrdenModelo orden in ordenes) { %>
                <a class="tbl-row tbl-order-row" href="DetalleContacto.aspx?id=<%: orden.IdOrden %>">
                    <div class="tbl-cell">
                        <span>
                            <i class="fa-solid fa-tags"></i>
                        </span>
                        <div class="tbl-order-cell">
                            <span><%: $"#{orden.IdOrden} - {orden.Fecha.ToShortDateString()} @ {orden.Fecha.TimeOfDay}" %></span>
                            <span><span class="contact-name"><%: orden.Cliente.NombreApellido %></span><span class="event-type">(<%: orden.TipoEvento %>)</span></span>
                        </div>
                    </div>
                    <div class="tbl-cell tbl-cell--end">
                        <span>$<%: orden.Total %></span>
                    </div>
                </a>
            <% } %>
        </div>
    </div>
</asp:Content>
