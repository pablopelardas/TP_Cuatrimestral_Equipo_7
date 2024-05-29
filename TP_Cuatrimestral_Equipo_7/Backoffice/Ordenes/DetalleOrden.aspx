<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="DetalleOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.DetalleOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">
    <a href="/Backoffice/Ordenes" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Detalles de la orden</h4>
    </a>
    <a href="EditarOrden.aspx?id=<%: orden.IdOrden %>" class="btn btn-primary">Editar contacto</a>
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
                <% foreach (var producto in orden.Productos) { %>
                    <div>
                        <div>
                            <span>Producto: </span>
                            <span><%: producto.Nombre %></span>
                        </div>
                        <div>
                            <span>Cantidad: </span>
                            <span><%: producto. %></span>
                        </div>
                        <div>
                            <span>Precio: </span>
                            <span><%: producto.Precio %></span>
                        </div>
                    </div>
                <%} %>
            </div>
        </div>
        <%-- DIV Ordenes  --%>
        <div>
            <div>
                <h3>Historial de Ordenes</h3>
                <button>Nueva orden</button>
            </div>
            <div>
                No hay ordenes agregadas
            </div>
            </div>
    <%} %>
</asp:Content>
