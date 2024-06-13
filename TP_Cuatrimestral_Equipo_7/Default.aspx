<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-3xl underline">Dashboard</h1>
    <header>
       
    </header>
    <columns>
        <asp:Button ID="btnVerProductos" runat="server" Text="Ver productos" />
    </columns>
    <columns>
        <asp:Button ID="btnGenerarPedido" runat="server" Text="Generar pedido" />
    </columns>

</asp:Content>