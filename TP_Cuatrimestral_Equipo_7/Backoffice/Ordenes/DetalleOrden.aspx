<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="DetalleOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.DetalleOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .status-pill{
            display: inline-block;
            padding: 5px 10px;
            margin-right: 10px;
            border-radius: 20px;
            color: #333;
        }

        .status-pill--warning{
            background-color: #f0ad4e5e;
        }

        .status-pill--success{
            background-color: #5cb85c5e;
        }

        .status-pill--danger{
            background-color: #d9534f5e;
        }

        .status-pill--default{
            background-color: #5bc0de5e;
        }
    </style>
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
                    <span>Estados: </span>
                    <span class="<%: orden.Estado.PillClass %>" ><%: orden.Estado.Nombre %></span>
                    <span class="<%: orden.EstadoPago.PillClass %>"><%: orden.EstadoPago.Nombre %></span>
                </div>
            </div>
        </div>
        <%-- DIV Tipo entrega  --%>
        <div>
            <h3>Datos de la entrega</h3>
            <div>
                <div>
                    <span>Tipo de entrega: </span>
                    <span><%: orden.TipoEntrega %></span>
                </div>
                <div>
                    <span>Hora de entrega: </span>
                    <span><%: orden %></span>
                </div>
                <div>
                    <span>Correo: </span>
                    <span><%: orden.Cliente.Email%></span>
                </div>
                <div>
                    <span>Telefono: </span>
                    <span><%: orden.Cliente.Telefono %></span>
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
                    <span>Costo envío / Extra: </span>
                    <span><%: orden.CostoEnvio %></span>
                </div>
                <div>
                    <span>Total: </span>
                    <span><%: orden.Total %></span>
                </div>
            </div>
        </div>
    <%} %>
</asp:Content>
