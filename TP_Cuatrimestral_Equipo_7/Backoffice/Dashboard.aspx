<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutNegocio.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Dashboard" %>
<%@ Register Src="~/Backoffice/Components/Calendario.ascx" TagPrefix="uc" TagName="Calendario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Dashboard</h1>

    <uc:Calendario ID="Calendario1" runat="server" />

</asp:Content>
