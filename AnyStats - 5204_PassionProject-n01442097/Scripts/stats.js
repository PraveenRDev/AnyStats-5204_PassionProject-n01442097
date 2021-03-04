function GetDynamicTextBox(value) {
    return '<input type="text" class="form-control" required placeholder="x-value" name="XValue"/><span class="input-group-btn" style="width:0px;"></span><input type="number" class="form-control" name="YValue" placeholder="y-value" step="0.001" required/><span class="input-group-btn" style="width:10px;"></span><input type="button" class="form-control" onclick="RemoveTextBox(this)" value="Remove" />'
}

function AddTextBox() {
    var div = document.createElement("DIV");
    div.classList.add("input-group")
    div.innerHTML = GetDynamicTextBox("");
    document.getElementById("divCont").appendChild(div);
}

function RemoveTextBox(div) {
    document.getElementById("divCont").removeChild(div.parentNode);
}


