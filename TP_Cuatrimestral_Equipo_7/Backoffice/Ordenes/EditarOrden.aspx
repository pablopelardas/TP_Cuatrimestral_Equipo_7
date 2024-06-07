<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="EditarOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.EditarOrden"  ValidateRequest="false"%>

<%@ Register Src="~/Backoffice/Components/ComboBoxAutoComplete.ascx" TagPrefix="uc" TagName="ComboBoxAutoComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Css/chosen.css" rel="stylesheet" />
    <script src="/Js/jquery-3.7.1.min.js"></script>
    <script src="/Js/chosen.jquery.js" type="text/javascript"></script>
    <script src="https://cdn.tiny.cloud/1/valwbezytp23wuvlb68adt6hx9ggw67661q3p79cvj23ai0p/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">
    <%if (id != null) { %>
        <a href="DetalleOrden.aspx?id=<%: id %>" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Editor de orden</h4>
    </a>
    <%}
else { %>
    <a href="/Backoffice/Ordenes" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Editor de orden</h4>
    </a>
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- DIV Detalles del evento  --%>
    <div>
        <h3>Detalles del evento</h3>
        <div>
            <%if (id != null)
                {  %>
             <div>
                    <span>Orden #: </span>
                    <span><%: orden.IdOrden %></span>
            </div>
            <% }  %>
            <div class="my-2 d-flex gap-3">
                <label class="form-label">Tipo: </label>
                <asp:PlaceHolder ID="phComboBoxTipo" runat="server"></asp:PlaceHolder>
            </div>
            <div class="my-2 d-flex gap-3">
                <label class="form-label">Cliente: </label>
                <asp:PlaceHolder ID="phComboBoxCliente" runat="server"></asp:PlaceHolder>
            </div>
            <div>
                <label class="form-label">Fecha: <%: FechaSeleccionada != null ? FechaSeleccionada : ""%></label>
                <asp:PlaceHolder ID="phCalendario" runat="server"></asp:PlaceHolder>
            </div>
            
        </div>
    </div>
    <%-- DIV Tipo entrega  --%>
    <div>
        <h3>Datos de la entrega</h3>
        <div>
            <label class="form-label">Tipo de entrega</label>
            <asp:RadioButtonList ID="rbtnTipoEntrega" AutoPostBack="true" OnSelectedIndexChanged="rbtnTipoEntrega_SelectedIndexChanged" runat="server">
                <asp:ListItem Text="Retiro" Value="R"></asp:ListItem>
                <asp:ListItem Text="Delivery" Value="D"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            <label class="form-label">Hora de entrega: </label>
            <asp:TextBox CssClass="form-control" ID="txtHora" runat="server"></asp:TextBox>
        </div>
        <%if (orden.TipoEntrega == "D")
            { %>
        <div>
            <label class="form-label">Dirección</label>
            <asp:TextBox CssClass="form-control" ID="txtDireccion" runat="server"></asp:TextBox>
        </div>
        <%} %>
    </div>
    <%-- DIV DETALLE ORDEN --%>
    <div class="detalle-orden">
        <div class="do-header">
             <h3>Detalle</h3>
             <asp:Button ID="btnAgregarProducto" runat="server" OnClick="btnAgregarProducto_Click" />
        </div>
        <div class="do-lista">
            <%foreach (Dominio.Modelos.ProductoDetalleOrdenModelo detalle in orden.DetalleProductos)
                {  %>
            <div class="do-item">
                <div class="do-item-left">
                    <span class="do-item-left__categoria"><%: detalle.Producto.Categoria.Nombre %></span>
                    <span class="do-item-left__nombre"><%: detalle.Producto.Nombre %></span>
                    <span class="do-item-left__cantidad"><%: detalle.Cantidad %></span>
                    <span class="do-item-left__precio-unitario">$ <%: detalle.PrecioUnitarioActual %></span>
                </div>
                <div class="do-item-right">
                    <div>
                        <asp:LinkButton ID="btnEditarProducto" runat="server" OnClick="btnEditarProducto_Click"><i class="fa-solid fa-pencil"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnEliminarProducto" runat="server" OnClick="btnEliminarProducto_Click"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                    </div>
                    <div>
                        <span class="do-item-right__precio-total">$ <%: detalle.Subtotal%></span>
                    </div>
                </div>
            </div>
                <% } %>
        </div>
        <div class="do-detalle">
            <div>
                <span>Subtotal: </span>
                <span>$ <%: orden.Subtotal %></span>
            </div>
            <div>
                <span>Descuento (%): </span>
                <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div>
                <span>Costo envío / extra: </span>
                <asp:TextBox ID="txtCostoEnvio" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div>
                <span>Total: </span>
                <span>$ <%: orden.Total %></span>
            </div>
        </div>
    </div>

    <%-- DIV OBSERVACIONES --%>
    <div class="observaciones">
        <h3>Observaciones</h3>
        <div>
            <asp:TextBox TextMode="MultiLine" id="tiny" ClientIDMode="Static" runat="server" OnLoad="OnTinyLoad"></asp:TextBox>
        </div>
    </div>
    
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />



    <script type="text/javascript" language="javascript">
        function LoadTiny() {
            tinymce.init({
                selector: 'textarea#tiny',
                height: 500,
                plugins: [
                    'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
                    'anchor', 'searchreplace', 'visualblocks', 'code', 'fullscreen',
                    'insertdatetime', 'media', 'table', 'help', 'wordcount'
                ],
                toolbar: 'undo redo | blocks | ' +
                    'bold italic backcolor | alignleft aligncenter ' +
                    'alignright alignjustify | bullist numlist outdent indent | ' +
                    'removeformat',
                content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }'
            });
        }

        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    </script>
</asp:Content>

