<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="DetalleIngrediente.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ingredientes.DetalleIngrediente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">
    <a href="/Backoffice/Ingredientes" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Detalles ingrediente</h4>
    </a>
    <%if (ingrediente != null) {  %>
    <a href="EditarIngrediente.aspx?id=<%: ingrediente.IdIngrediente %>" class="btn btn-primary">Editar ingrediente</a>
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- DIV Detalles de contacto  --%>
    <div>
        <h3>Detalles ingrediente</h3>
        <div>
            <div>
                <span>Nombre: </span>
                <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Cantidad: </span>
                <asp:Label ID="lblCantidad" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Unidad de Medida: </span>
                <asp:Label ID="lblUnidadAbreviatura" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Costo: </span>
                <asp:Label ID="lblCosto" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Proveedor: </span>
                <asp:Label ID="lblProveedor" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
