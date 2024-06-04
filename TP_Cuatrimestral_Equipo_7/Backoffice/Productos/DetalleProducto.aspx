<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Productos.DetalleProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="/Backoffice/Producto" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Detalles producto</h4>
    </a>
    <%if (producto != null)
        {  %>
    <a href="EditarProducto.aspx?id=<%: producto.IdProducto %>" class="btn btn-primary">Editar producto</a>
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="server">
     <div>
     <h3>Detalles producto</h3>
     <div>
         <div>
             <span>Nombre: </span>
             <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
         </div>
         <div>
             <span>Descripcion: </span>
             <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label>
         </div>
         <div>
             <span>Porciones: </span>
             <asp:Label ID="lblPorciones" runat="server" Text=""></asp:Label>
         </div>
         <div>
             <span>HorasTrabajo: </span>
             <asp:Label ID="lblHorasTrabajo" runat="server" Text=""></asp:Label>
         </div>
         <div>
             <span>Tipo Precio: </span>
             <asp:Label ID="lblTipoPrecio" runat="server" Text=""></asp:Label>
         </div>
         <div>
             <span>Valor Precio: </span>
             <asp:Label ID="lblValorPrecio" runat="server" Text=""></asp:Label>
         </div>
         <div>
             <span>Categoria: </span>
             <asp:Label ID="lblCategoria" runat="server" Text=""></asp:Label>
         </div>
     </div>
     </div>
</asp:Content>
