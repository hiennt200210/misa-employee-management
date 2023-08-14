var buttonAdd = document.getElementById("button-add");
var popup = document.getElementById("popup");
var firstInput = document.getElementById("staff-id");
var buttonExit = document.getElementById("button-exit");
var buttonDestroy = document.getElementById("button-destroy");

buttonAdd.addEventListener("click", function () {
    popup.style.display = "flex";
    firstInput.focus();
});

buttonExit.addEventListener("click", function () {
    popup.style.display = "none";
});

buttonDestroy.addEventListener("click", function () {
    popup.style.display = "none";
});
