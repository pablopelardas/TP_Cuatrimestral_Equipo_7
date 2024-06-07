<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendario.ascx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Components.Calendario" %>

<script type="text/javascript">
    function ShowPopup(title, body) {
        console.log('SHOW POPUP')
        setTimeout(() => {
            document.querySelector("#modalTrigger").click();
        }, 1);
    }
</script>

<asp:Calendar CssClass="calendario" ID="cldFecha" runat="server" BackColor="#FFFFFF" BorderColor="#A0D2DB"
    BorderWidth="1px" DayNameFormat="Full" Font-Names="Verdana" Font-Size="8pt" 
    ForeColor="#403233" ShowGridLines="True" OnDayRender="cldFecha_DayRender"
    OnVisibleMonthChanged="cldFecha_VisibleMonthChanged">
    <SelectedDayStyle BackColor="#e3e3e3" Font-Bold="True" ForeColor="#403233" />
    <TodayDayStyle BackColor="#A0D2DB" ForeColor="#403233"/>
    <OtherMonthDayStyle ForeColor="#cbcbcb"/>
    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
    <DayHeaderStyle BackColor="#A0D2DB" Font-Bold="True" Height="1px" />
    <TitleStyle BackColor="#293c4f" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFFF" />
    <DayStyle CssClass="day"/>
</asp:Calendar>

<button style="display:none" type="button" id="modalTrigger" data-bs-toggle="modal" data-bs-target="#dayOrders"></button>

<!-- Modal -->
<div class="modal fade" id="dayOrders" tabindex="-1" aria-labelledby="dayOrdersTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="dayOrdersTitle">Modal title</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
          <asp:Repeater ID="rptEventos" runat="server">
              <ItemTemplate>
                  <div class="event">
                      <div class='event-order'> <%# Eval("Orden.IdOrden") %></div>
                      <a class='event' href='./Ordenes/DetalleOrden.aspx?id= <%# Eval("Orden.IdOrden") %>&redirect_back=/Backoffice/Dashboard.aspx'>
                            <div class='event-title'><%# Eval("Descripcion") %></div>
                      </a>
                  </div>
              </ItemTemplate>
            </asp:Repeater>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>


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


