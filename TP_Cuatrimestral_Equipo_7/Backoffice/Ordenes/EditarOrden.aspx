<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="EditarOrden.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Contactos.EditarContacto"  ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.tiny.cloud/1/valwbezytp23wuvlb68adt6hx9ggw67661q3p79cvj23ai0p/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">

    <%if (id != null) { %>
        <a href="DetalleContacto.aspx?id=<%: id %>" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Editor de contacto</h4>
    </a>
    <%}
else { %>
    <a href="/Backoffice/Contactos" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Editor de contacto</h4>
    </a>
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- DIV Detalles de contacto  --%>
    <div>
        <h3>Detalles de contacto</h3>
        <div>
            <div>
                <label class="form-label">Tipo</label>
                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Cliente" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Proveedor" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                <label class="form-label">Nombre y Apellido</label>
                <asp:TextBox CssClass="form-control" ID="txtNombreApellido" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Correo</label>
                <asp:TextBox CssClass="form-control" ID="txtCorreo" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Telefono</label>
                <asp:TextBox CssClass="form-control" ID="txtTelefono" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Fuente</label>
                <asp:TextBox CssClass="form-control" ID="txtFuente" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Direccion</label>
                <asp:TextBox CssClass="form-control" ID="txtDireccion" runat="server"></asp:TextBox>
            </div>
            <div>
                <label class="form-label" for="chkDeseaRecibirCorreos">Desea recibir correos: </label>
                <asp:CheckBox CssClass="form-control" ID="chkDeseaRecibirCorreos" runat="server" />
            </div>
            <div>
                <label class="form-label" for="chkDeseaRecibirWhatsapps">Desea recibir whatsapps: </label>
                <asp:CheckBox CssClass="form-control" ID="chkDeseaRecibirWhatsapps" runat="server" />
            </div>
            
        </div>
    </div>
    <%-- DIV Informacion personal  --%>
    <div>
        <h3>Información personal</h3>
        <div>
            <asp:TextBox TextMode="MultiLine" id="tiny" ClientIDMode="Static" runat="server" OnLoad="OnTinyLoad"></asp:TextBox>
        </div>
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
