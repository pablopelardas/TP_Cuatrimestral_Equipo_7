<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendario.ascx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Components.Calendario" %>

<div class="">
    <div class="flex justify-center mb-5">
        <asp:DropDownList ID="ddlYear" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm  border-s-gray-100 dark:border-s-gray-700 border-s-2 focus:ring-blue-500 focus:border-blue-500 block p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" AutoPostBack="True" OnSelectedIndexChanged="Set_Calendar">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlMonth" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Set_Calendar">
        </asp:DropDownList>
    </div>

    <asp:Calendar CssClass="calendario" ID="cldFecha" runat="server" DayNameFormat="Full"
        OnDayRender="cldFecha_DayRender"
        OnVisibleMonthChanged="cldFecha_VisibleMonthChanged">
    </asp:Calendar>
</div>




<style>
    .calendario td.day .day-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
    }


    .calendario td.day .event {
        display: flex;
        justify-content: space-between;
        align-items: center;
        flex-direction: column;
        font-size: 10px;
        border: 1px solid #A0D2DB;
        padding: 5px;
        margin: 10px;
    }

        .calendario td.day .event .event-title {
            font-weight: bold;
        }

        .calendario td.day .event .event-order {
            font-size: 10px;
        }
</style>


