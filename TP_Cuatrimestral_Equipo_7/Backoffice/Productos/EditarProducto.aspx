<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="EditarProducto.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Productos.EditarProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.tiny.cloud/1/valwbezytp23wuvlb68adt6hx9ggw67661q3p79cvj23ai0p/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%if (id != null)
        { %>
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

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="server">
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
    <div>
        <h3>Detalles de producto</h3>
        <div>
            <div>
                <label class="form-label">Nombre</label>
                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Descripcion</label>
                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Porciones</label>
                <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">HorasTrabajo</label>
                <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">TipoPrecio</label>
                <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">ValorPrecio</label>
                <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Categoria</label>
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                    <asp:ListItem Text="uno" Value="1"></asp:ListItem>
                    <asp:ListItem Text="dos" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>
    </div>

    <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
</asp:Content>
