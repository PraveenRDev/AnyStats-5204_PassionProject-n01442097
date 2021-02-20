function GetDynamicTextBox(value) {
    return '<input type="text" required placeholder="x-value" name="XValue"/><input type="number" name="YValue" placeholder="y-value" step="0.001" required/><input type="button" onclick="RemoveTextBox(this)" value="Remove" />'
}

function AddTextBox() {
    var div = document.createElement("DIV");
    div.innerHTML = GetDynamicTextBox("");
    document.getElementById("divCont").appendChild(div);
}

function RemoveTextBox(div) {
    document.getElementById("divCont").removeChild(div.parentNode);
}


