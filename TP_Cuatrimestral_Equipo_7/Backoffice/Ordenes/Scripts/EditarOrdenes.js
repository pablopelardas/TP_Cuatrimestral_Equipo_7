function ShowModal() {
    console.log("Mostrando modal")
    const button = document.querySelector('[data-modal-toggle="agregarProductoModal"]');
    // setTimeout(() => button.click(), 1000);
    console.log(button)
    button.click();
}

function HideModal() {
    const button = document.querySelector('[data-modal-hide="agregarProductoModal"]');
    setTimeout(() => button.click(), 1);
}


function LoadTiny() {
    console.log("Cargando Tiny")
    let options = {
        toolbar: "basic",
        editorResizeMode: "height",
        showPlusButton: false,
        showTagList: false,
        showStatistics: false,
        toggleBorder: true,
    };
    let rte = new RichTextEditor("#tinyEditor", options);
    let tiny = document.getElementById("tiny");
    rte.setHTMLCode(tiny.value);
    rte.attachEvent("change", function (e) {
        tiny.value = rte.getHTMLCode();
    });
}

function pageLoad() {
        $(".chzn-select").chosen();
        $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        initFlowbite();
}

// var prm = Sys.WebForms.PageRequestManager.getInstance();
// if (prm != null) {
//     prm.add_endRequest(function (sender, e) {
//         if (sender._postBackSettings.panelsToUpdate != null) {
//             $(".chzn-select").chosen();
//             $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
//             console.log("Recargando")
//             // initFlowbite();
//         }
//     });
//     prm.add_beginRequest(function (sender, e) {
//         console.log("Comenzando")
//         // initFlowbite();
//        
//     });
// }



