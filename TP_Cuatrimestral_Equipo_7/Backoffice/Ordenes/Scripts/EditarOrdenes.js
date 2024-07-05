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


function LoadInfoExtra() {
    let options = {
        toolbar: "basic",
        editorResizeMode: "height",
        showPlusButton: false,
        showTagList: false,
        showStatistics: false,
        toggleBorder: true,
    };
    let rte = new RichTextEditor("#infoExtraEditor", options);
    let tiny = document.getElementById("txtInfoExtra");
    rte.setHTMLCode(tiny.value);
    rte.attachEvent("change", function (e) {
        tiny.value = rte.getHTMLCode();
    });
}


 



