<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComboBoxAutoComplete.ascx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Components.ComboBoxAutoComplete" %>


<link href="/Css/chosen.css" rel="stylesheet" />
<script src="/Js/jquery-3.7.1.min.js"></script>
<script src="/Js/chosen.jquery.js" type="text/javascript"></script>

<asp:PlaceHolder ID="phComboBox" runat="server"></asp:PlaceHolder>

<script type="text/javascript" language="javascript">
    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
</script>