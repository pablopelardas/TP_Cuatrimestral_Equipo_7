<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendario.ascx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Components.WebUserControl1" %>

<h1>
    ESTO ES UN CALENDARIO CUSTOm
</h1>

<asp:Calendar CssClass="calendario" ID="cldFecha" runat="server" BackColor="#FFFFFF" BorderColor="#A0D2DB"
    BorderWidth="1px" DayNameFormat="Full" Font-Names="Verdana" Font-Size="8pt" 
    ForeColor="#403233" ShowGridLines="True" OnDayRender="cldFecha_DayRender" OnSelectionChanged="cldFecha_SelectionChanged"
    OnVisibleMonthChanged="cldFecha_VisibleMonthChanged">
    <SelectedDayStyle BackColor="#FBFBFF" Font-Bold="True" ForeColor="#403233" />
    <TodayDayStyle BackColor="#A0D2DB" ForeColor="White" />
    <OtherMonthDayStyle ForeColor="#cbcbcb"/>
    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
    <DayHeaderStyle BackColor="#A0D2DB" Font-Bold="True" Height="1px" />
    <TitleStyle BackColor="#293c4f" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFFF" />
    <DayStyle CssClass="day"/>
</asp:Calendar>
<asp:Label ID="LabelAction" runat="server"></asp:Label>

<script type="text/javascript">
    // search for
</script>


<style>
    .calendario td.day .day-header{
        display: flex;
        justify-content: space-between;
        align-items: center;
    }


    .calendario td.day .event{
        display: flex;
        justify-content: space-between;
        align-items: center;
        flex-direction: column;
        font-size: 10px;
        border: 1px solid #A0D2DB;
        padding: 5px;
        margin: 10px;
    }

    .calendario td.day .event .event-title{
        font-weight: bold;
    }

    .calendario td.day .event .event-order{
        font-size: 10px;
    }
</style>


