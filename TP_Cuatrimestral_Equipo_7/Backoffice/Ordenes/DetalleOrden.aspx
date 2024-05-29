<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="DetalleOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.DetalleOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">
    <a href="/Backoffice/Ordenes" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Detalles de la orden</h4>
    </a>
    <%if (orden != null) {  %>
    <a href="EditarOrden.aspx?id=<%: orden.IdOrden %>" class="btn btn-primary">Editar orden</a>
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- DIV Detalles de evento  --%>
    <%if (orden != null) { %>
        <div>
            <h3>Detalles del evento</h3>
            <div>
                <div>
                    <span>Orden #: </span>
                    <span><%: orden.IdOrden %></span>
                </div>
                <div>
                    <span>Evento: </span>
                    <span><%: orden.TipoEvento %></span>
                </div>
                <div>
                    <span>Cliente: </span>
                    <a>
                        <span><%: orden.Cliente.NombreApellido %></span>
                    </a>
                </div>
                <div>
                    <span>Fecha: </span>
                    <span><%: orden.Fecha %></span>
                </div>
                <div>
                    <span>Estado: </span>
                    <%--<span><%: orden.Estado %></span>--%>
                    <span>Pendiente de pago</span>
                </div>
            </div>
        </div>
        <%-- DIV Tipo entrega  --%>
        <div>
            <h3>Tipo de entrega</h3>
            <div>
                <div>
                    <span>Tipo de entrega: </span>
                    <span><%: orden.TipoEntrega %></span>
                </div>
                <div>
                    <span>Correo: </span>
                    <span><%: orden.Cliente.Email%></span>
                </div>
                <div>
                    <span>Telefono: </span>
                    <span><%: orden.Cliente.Telefono %></span>
                </div>
                <div>
                    <span>Direccion: </span>
                    <span><%: orden.Cliente.Direccion %></span>
                </div>
            </div>
        </div>
        <%-- DIV Orden  --%>
        <div>
            <h3>Orden</h3>
            <div class="detalle-productos">
                <% foreach (var detalleProducto in orden.DetalleProductos) { %>
                    <div>
                        <div>
                            <span><%: detalleProducto.Producto.Categoria.Nombre %></span>
                            <span><%: detalleProducto.Producto.Nombre %></span>
                            <span><%: detalleProducto.Producto.Porciones %></span>
                        </div>
                        <div>
                            <span>Cantidad <%: detalleProducto.Cantidad %></span>
                            <span>Precio <%: detalleProducto.Producto.ValorPrecio %></span>
                            <span>Subtotal: <%:detalleProducto.Subtotal %></span>
                        </div>
                    </div>
                <%}; %>
            </div>
            <div>
                <div>
                    <span>Subtotal: </span>
                    <span><%: orden.Subtotal %></span>
                </div>
                <div>
                    <span>Descuento: </span>
                    <span><%: orden.DescuentoPorcentaje %></span>
                </div>
                <div>
                    <span>Incremento: </span>
                    <span><%: orden.IncrementoPorcentaje %></span>
                </div>
                <div>
                    <span>Total: </span>
                    <span><%: orden.Total %></span>
                </div>
            </div>
        </div>
    <%} %>
</asp:Content>
