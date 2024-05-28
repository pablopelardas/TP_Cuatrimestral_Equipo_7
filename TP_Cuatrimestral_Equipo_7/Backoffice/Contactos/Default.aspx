<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Contactos.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <% foreach (Dominio.Modelos.ContactoModelo contacto in contactos) { %>
                <a class="tbl-row" href="DetalleContacto.aspx?id=<%: contacto.Id %>">
                    <div class="tbl-cell">
                        <span>
                            <% if (contacto.Rol == "Proveedor") { %>
                                <i class="fa-solid fa-truck"></i>
                            <% } else { %>
                                <i class="fa-solid fa-user"></i>
                            <% } %>
                        </span>
                        <span><%: contacto.NombreApellido %></span>
                    </div>
                    <div class="tbl-cell">
                        <span><%: contacto.Email %></span>
                    </div>
                    <div class="tbl-cell">
                        <span><%: contacto.Telefono %></span>
                    </div>
                </a>
            <% } %>
        </div>
    </div>
</asp:Content>
