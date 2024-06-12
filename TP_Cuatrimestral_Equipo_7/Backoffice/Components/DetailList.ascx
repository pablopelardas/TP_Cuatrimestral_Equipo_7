<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailList.ascx.cs" Inherits="TP_Cuatrimestral_Equipo_7.Backoffice.Components.DetailList" %>

<script type="text/javascript">
    var loading = false;
    var loader = document.getElementById('loader');

    function ShowLoader() {
        loader.classList.remove('hidden');
    }

    function HideLoader() {
        loader.classList.add('hidden');
    }

    function ShowModal() {
        console.log('ShowModal');

        if (loading) {
            return;
        }

        loading = true;
        ShowLoader();

        setTimeout(() => {
            const button = document.querySelector('[data-modal-toggle="<%:CustomID%>-modal"]');

            // show modal
            button.click();
            loading = false;
            HideLoader();
        }, 1000);

        // wait for modal to be rendered with observer
    }
</script>


<style>
    .modal-body .chzn-container.chzn-container-single, .modal-body .chzn-drop {
        width: 100% !important;
    }
    .modal-body .chzn-drop input{
        width: 100% !important;
    }
</style>

<%-- fullScreen loader with inline style --%>
<div id="loader" class="fixed inset-0 bg-gray-900 opacity-50 flex items-center justify-center hidden">
    <div role="status">
        <svg aria-hidden="true" class="w-8 h-8 text-gray-200 animate-spin dark:text-gray-600 fill-blue-600" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor" />
            <path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill" />
        </svg>
        <span class="sr-only">Loading...</span>
    </div>
</div>

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

<!-- Modal toggle -->
<button data-modal-target="<%:CustomID%>-modal" data-modal-toggle="<%:CustomID%>-modal" class="hidden" type="button">
</button>

<div id="<%:CustomID%>-modal" tabindex="-1" aria-hidden="true" class="hidden overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full">
    <div class="relative p-4 w-full max-w-2xl max-h-full">
        <!-- Modal content -->
        <div class="relative bg-white rounded-lg shadow dark:bg-gray-800">
            <!-- Modal header -->
            <div class="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
                <h3 class="text-xl font-semibold text-gray-900 dark:text-white"> <%:ModalTitle%>
                </h3>
                <button type="button" class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="<%:CustomID%>-modal">
                    <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6" />
                    </svg>
                    <span class="sr-only">Close modal</span>
                </button>
            </div>
            <!-- Modal body -->
            <div class="modal-body p-5">
                   <asp:Panel ID="pnlModalBody" runat="server"></asp:Panel>
            </div>
            <!-- Modal footer -->
            <div class="flex items-center p-4 md:p-5 border-t border-gray-200 rounded-b dark:border-gray-600">
                <asp:Button ID="btnCancelarModal" runat="server" Text="Cerrar" data-bs-dismiss="modal" CssClass="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" />
                <asp:Button ID="btnGuardarModal" runat="server" Text="Guardar" CssClass="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" />
            </div>
        </div>
    </div>
</div>


