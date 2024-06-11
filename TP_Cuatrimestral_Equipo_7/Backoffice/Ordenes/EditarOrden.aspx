<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/LayoutTailwind.Master" AutoEventWireup="true" CodeBehind="EditarOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.EditarOrden"  ValidateRequest="false"%>

<%@ Register Src="~/Backoffice/Components/ComboBoxAutoComplete.ascx" TagPrefix="uc" TagName="ComboBoxAutoComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Css/chosen.css" rel="stylesheet" />
    <script src="/Js/jquery-3.7.1.min.js"></script>
    <script src="/Js/chosen.jquery.js" type="text/javascript"></script>
    <script src="https://cdn.tiny.cloud/1/valwbezytp23wuvlb68adt6hx9ggw67661q3p79cvj23ai0p/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script>

    <script type = "text/javascript" src = "https://unpkg.com/default-passive-events" ></script>
</asp:Content>

<%-- <asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server"> --%>
<%--     <%if (id != null) { %> --%>
<%--         <a href="DetalleOrden.aspx?id=<%: id %>" class="page-header--go-back"> --%>
<%--         <i class="fa-solid fa-arrow-left"></i> --%>
<%--         <h4>Editor de orden</h4> --%>
<%--     </a> --%>
<%--     <%} --%>
<%-- else { %> --%>
<%--     <a href="/Backoffice/Ordenes" class="page-header--go-back"> --%>
<%--         <i class="fa-solid fa-arrow-left"></i> --%>
<%--         <h4>Editor de orden</h4> --%>
<%--     </a> --%>
<%--     <%} %> --%>
<%-- </asp:Content> --%>

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
        <div class="do-lista">
            <asp:PlaceHolder ID="phDetalleOrden" runat="server"></asp:PlaceHolder>
        </div>
        <div class="do-detalle">
            <div>
                <span>Subtotal: </span>
                <span>$ <%: orden.Subtotal %></span>
            </div>
            <div>
                <span>Descuento (%): </span>
                <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control" OnTextChanged="TotalChanged"></asp:TextBox>
            </div>
            <div>
                <span>Costo envío / extra: </span>
                <asp:TextBox ID="txtCostoEnvio" runat="server" CssClass="form-control" OnTextChanged="TotalChanged"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnAplicarDescuento" runat="server" Text="Aplicar" CssClass="btn btn-primary" />
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

