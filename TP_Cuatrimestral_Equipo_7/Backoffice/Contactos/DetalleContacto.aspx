<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="DetalleContacto.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Contactos.DetalleContacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">
    <a href="/Backoffice/Contactos" class="page-header--go-back">
        <i class="fa-solid fa-arrow-left"></i>
        <h4>Detalles de contacto</h4>
    </a>
    <a href="EditarContacto.aspx?id=<%: contacto.Id %>" class="btn btn-primary">Editar contacto</a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- DIV Detalles de contacto  --%>
    <div>
        <h3>Detalles de contacto</h3>
        <div>
            <div>
                <span>Tipo: </span>
                <asp:Label ID="lblTipo" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Nombre Completo: </span>
                <asp:Label ID="lblNombreApellido" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Correo: </span>
                <asp:Label ID="lblCorreo" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Telefono: </span>
                <asp:Label ID="lblTelefono" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Fuente: </span>
                <asp:Label ID="lblFuente" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Direccion: </span>
                <asp:Label ID="lblDireccion" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Desea recibir correos: </span>
                <asp:Label ID="lblDeseaRecibirCorreos" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <span>Desea recibir whatsapps: </span>
                <asp:Label ID="lblDeseaRecibirWhatsapps" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <%-- DIV Informacion personal  --%>
    <div>
        <h3>Información personal</h3>
        <asp:Literal ID="litInformacionPersonal" runat="server" />
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
</asp:Content>
