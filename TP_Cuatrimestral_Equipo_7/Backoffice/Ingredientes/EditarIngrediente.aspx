<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="EditarIngrediente.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ingredientes.EditarIngrediente"  ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.tiny.cloud/1/valwbezytp23wuvlb68adt6hx9ggw67661q3p79cvj23ai0p/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">

    <%if (id != null) { %>
        <a href="DetalleIngrediente.aspx?id=<%: id %>" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Editor de ingrediente</h4>
    </a>
    <%}
else { %>
    <a href="/Backoffice/Ingredientes" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Editor de ingrediente</h4>
    </a>
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- DIV Detalles de contacto  --%>
    <div>
        <h3>Detalles de ingrediente</h3>
        <div>
            <div>
                <label class="form-label">Nombre</label>
                <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Cantidad</label>
                <asp:TextBox CssClass="form-control" ID="txtCantidad" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Unidad</label>
                <asp:DropDownList ID="ddlUnidad" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div>
                <label class="form-label">Costo</label>
                <asp:TextBox CssClass="form-control" ID="txtCosto" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Proveedor</label>
                <asp:TextBox CssClass="form-control" ID="txtProveedor" runat="server"></asp:TextBox>
            </div>
            
        </div>
    </div>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
</asp:Content>
