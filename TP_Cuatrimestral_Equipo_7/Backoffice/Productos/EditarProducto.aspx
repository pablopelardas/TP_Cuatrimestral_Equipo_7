<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="EditarProducto.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Productos.EditarProducto" ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.tiny.cloud/1/valwbezytp23wuvlb68adt6hx9ggw67661q3p79cvj23ai0p/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">
    
    <%if (id != null) { %>
        <a href="DetalleProducto.aspx?id=<%: id %>" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Editor de producto</h4>
    </a>
    <%}
        else
        { %>
        <a href="/Backoffice/Productos" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Editor de producto</h4>
    </a>
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h3>Detalles de producto</h3>
        <div>
            <div>
                <label class="form-label">Nombre</label>
                <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Descripcion</label>
                <asp:TextBox CssClass="form-control" ID="txtDescripcion" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Porciones</label>
                <asp:TextBox CssClass="form-control" ID="txtPorciones" runat="server"></asp:TextBox>                
            </div>
            <div>
                <label class="form-label">HorasTrabajo</label>
                <asp:TextBox CssClass="form-control" ID="txtHorasTrabajo" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">TipoPrecio</label>
                <asp:TextBox CssClass="form-control" ID="txtTipoPrecio" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">ValorPrecio</label>
                <asp:TextBox CssClass="form-control" ID="txtValorPrecio" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Categoria</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control">
                <asp:ListItem Text="Uno" Value="1"></asp:ListItem>
                <asp:ListItem Text="Dos" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>
    </div>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />   
</asp:Content>
