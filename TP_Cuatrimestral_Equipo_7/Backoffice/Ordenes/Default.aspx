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
        .tbl-header .tbl-filter.tbl-header--right {
            justify-content: flex-start;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">
    <h4>Ordenes</h4>
    <a href="EditarOrden.aspx" class="btn btn-primary">Nueva orden</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="tbl-header">
            <div class="tbl-filter tbl-header--right">
            <span>Período</span>
            <asp:DropDownList ID="ddlMes" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
                <asp:ListItem Value="1">Enero</asp:ListItem>
                <asp:ListItem Value="2">Febrero</asp:ListItem>
                <asp:ListItem Value="3">Marzo</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Mayo</asp:ListItem>
                <asp:ListItem Value="6">Junio</asp:ListItem>
                <asp:ListItem Value="7">Julio</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Septiembre</asp:ListItem>
                <asp:ListItem Value="10">Octubre</asp:ListItem>
                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                <asp:ListItem Value="12">Diciembre</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged">
            </asp:DropDownList>
            </div>
            <div class="tbl-filter">
            <i class="fa-solid fa-filter"></i>
            <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                <asp:ListItem Value="0">Todas</asp:ListItem>
                <asp:ListItem Value="1">Sin pagos</asp:ListItem>
                <asp:ListItem Value="2">Reservadas</asp:ListItem>
                <asp:ListItem Value="3">Parcialmente Pagadas</asp:ListItem>
                <asp:ListItem Value="4">Completamente Pagadas</asp:ListItem>
                <asp:ListItem Value="5">Completadas</asp:ListItem>
                <asp:ListItem Value="6">Canceladas</asp:ListItem>
            </asp:DropDownList>
            </div>
        </div>
        <div class="tbl-body">
            <% foreach (Dominio.Modelos.OrdenModelo orden in ordenes) { %>
                <a class="tbl-row tbl-order-row" href="DetalleOrden.aspx?id=<%: orden.IdOrden %>">
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
