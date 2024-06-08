<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailList.ascx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Components.DetailList" %>

<script type="text/javascript">
    function ShowModal() {
        setTimeout(() => {
            document.querySelector("#<%:CustomID%>-trigger").click();
        }, 1);
    }
</script>

<style>
    .modal-body .chzn-container.chzn-container-single, .modal-body .chzn-drop {
        min-width : 300px !important; /* or any value that fits your needs */
    }
    .modal-body .chzn-drop input{
        width: 100% !important;
    }
</style>

<div class="detail-list">
    <div class="detail-list__header">
        <asp:PlaceHolder ID="phListHeader" runat="server" />
    </div>
    <div class="detail-list__main">
        <asp:PlaceHolder ID="phList" runat="server" />
    </div>
    <div class="detial-list__footer">
        <asp:PlaceHolder ID="phListFooter" runat="server" />
    </div>
</div>


<!-- Button trigger modal -->
<button type="button" id="<%:CustomID%>-trigger" style="display: none;" data-bs-toggle="modal" data-bs-target="#<%:CustomID%>-modal">
</button>

<!-- Modal -->
<div class="modal fade" id="<%:CustomID%>-modal" tabindex="-1" aria-labelledby="<%:CustomID%>-modalTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
    <div class="modal-content">
        <div class="modal-header">
        <h1 class="modal-title fs-5" id="<%:CustomID%>-modalTitle"><%:CustomID%></h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body position-relative " style="min-height: 300px">
            <asp:Panel ID="pnlModalBody" runat="server"></asp:Panel>
        </div>
        <div class="modal-footer">
        <asp:Button ID="btnCancelarModal" runat="server" Text="Cerrar" data-bs-dismiss="modal" CssClass="btn btn-secondary" />
        <asp:Button ID="btnGuardarModal" runat="server" Text="Guardar" CssClass="btn btn-primary" />
        </div>
    </div>
    </div>
</div>

