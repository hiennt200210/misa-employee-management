var buttonAdd = document.getElementById("button-add");
var popup = document.getElementById("popup");
var inputId = document.getElementById("input-id");
var inputName = document.getElementById("input-name");
var selectUnit = document.getElementById("select-unit");
var buttonExit = document.getElementById("button-exit");
var buttonCancel = document.getElementById("button-cancel");

buttonAdd.addEventListener("click", function () {
    popup.style.display = "flex";
    inputId.focus();
});

buttonExit.addEventListener("click", function () {
    popup.style.display = "none";
});

buttonCancel.addEventListener("click", function () {
    popup.style.display = "none";
});

inputId.addEventListener("blur", function () {
    if (inputId.value == null || inputId.value == "") {
        inputId.classList.add("m-input-error");
    } else {
        inputId.classList.remove("m-input-error");
    }
});

inputName.addEventListener("blur", function () {
    if (inputName.value == null || inputName.value == "") {
        inputName.classList.add("m-input-error");
    } else {
        inputName.classList.remove("m-input-error");
    }
});

selectUnit.addEventListener("blur", function () {
    if (selectUnit.value == 1) {
        selectUnit.classList.add("m-input-error");
    }
});