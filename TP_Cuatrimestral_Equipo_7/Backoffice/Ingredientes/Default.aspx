<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ingredientes.Default" %>
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
        </div>
        <div class="tbl-body">
            <% foreach (Dominio.Modelos.IngredienteModelo ingrediente in ingredientes) { %>
                <a class="tbl-row" href="DetalleIngrediente.aspx?id=<%: ingrediente.IdIngrediente %>">
                    <div class="tbl-cell">
                        <span><%: ingrediente.Nombre %></span>
                    </div>
                    <div class="tbl-cell">
                        <span><%: ingrediente.Cantidad %></span>
                    </div>
                    <div class="tbl-cell">
                        <span><%: ingrediente.Unidad.Nombre %></span>
                    </div>
                    <div class="tbl-cell">
                        <span><%: ingrediente.Costo %></span>
                    </div>
                </a>
            <% } %>
        </div>
    </div>
</asp:Content>
