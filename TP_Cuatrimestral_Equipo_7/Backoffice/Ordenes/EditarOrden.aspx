<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="EditarOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Ordenes.EditarOrden"  ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <div>
                <label class="form-label">Tipo</label>
                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Cumpleaños" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Bautimo" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Casamiento" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Aniversario" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Baby Shower" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Religioso" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Corporativo" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Otro" Value="8"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                <label class="form-label">Cliente</label>
                <%--<asp:TextBox CssClass="form-control" ID="txtNombreApellido" runat="server"></asp:TextBox>--%>
                <%-- buscador clientes --%>
            </div>
            <div>
                <label class="form-label">Fecha</label>
                <%-- datepicker --%>
                <%--<asp:TextBox CssClass="form-control" ID="txtCorreo" runat="server"></asp:TextBox>--%>
            </div>
            
        </div>
    </div>
    <%-- DIV Tipo entrega  --%>
    <div>
        <h3>Datos de la entrega</h3>
        <div>
            <label class="form-label">Tipo de entrega</label>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                <asp:ListItem Text="Retiro" Value="1"></asp:ListItem>
                <asp:ListItem Text="Delivery" Value="2"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            <label class="form-label">Hora de entrega: </label>
            <asp:TextBox CssClass="form-control" ID="txtHora" runat="server"></asp:TextBox>
        </div>
        <%if (orden.TipoEntrega == "Delivery")
            { %>
        <div>
            <label class="form-label">Dirección</label>
        </div>
        <%} %>
    </div>
    <%-- DIV Fechas importantes  --%>
    <div>
        <div>
            <h3>Fechas importantes</h3>
            <button>Nueva fecha</button>
        </div>
        <div>
            No hay fechas importantes agregadas
        </div>
    </div>

<%--    <div>
        <asp:TextBox TextMode="MultiLine" id="tiny" ClientIDMode="Static" runat="server" OnLoad="OnTinyLoad"></asp:TextBox>
    </div>--%>
    

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
    </script>
</asp:Content>
