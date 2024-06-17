<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComboBoxAutoComplete.ascx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Components.ComboBoxAutoComplete" %>


<%-- <asp:PlaceHolder ID="phComboBox" runat="server"></asp:PlaceHolder> --%>

<asp:DropDownList ID="ddAutocomplete" CssClass="chzn-select" AppendDataBoundItems="True" runat="server">
    <asp:ListItem Text="Seleccione una opción" Value=""></asp:ListItem>
</asp:DropDownList>
<asp:PlaceHolder ID="phDataSource" runat="server"></asp:PlaceHolder>


<script type="text/javascript" language="javascript">
    function InitChosen() {

    }
</script>