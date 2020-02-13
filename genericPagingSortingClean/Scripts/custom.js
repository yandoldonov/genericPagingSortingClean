
function grayOutArea() {
    $('#partialDataContainer').block({
        css: {
            backgroundColor: 'transparent',
            border: 'none'
        },
        message: '<div class="spinner-grow text-muted"></div>',
        baseZ: 1500,
        overlayCSS: {
            backgroundColor: '#FFFFFF',
            opacity: 0.7,
            cursor: 'wait'
        }
    });
}

function unGrayOutArea() {
    $('#partialDataContainer').unblock();
}

function pullQueryVariants() {
    $('#filterDataForm').block({
        css: {
            backgroundColor: 'transparent',
            border: 'none'
        },
        message: '<div class="spinner-grow text-muted"></div>',
        baseZ: 1500,
        overlayCSS: {
            backgroundColor: '#FFFFFF',
            opacity: 0.7,
            cursor: 'wait'
        }
    });

    var _selectedProperty = document.getElementById("selectedProperty").value;

    updateQueryVariantsFromDatabase(_selectedProperty);
}

function updateQueryVariantsFromDatabase(_selectedProperty) {
    var procemessage = "<option value='0'> Please wait...</option>";
    $("#selectedQuetyOption").html(procemessage).show();
    var url = "/" + document.getElementById("genericTypeName").value + "/updateQueryVariantsFromDatabase/";

    $.ajax({
        url: url,
        data: { selectedProperty: _selectedProperty },
        cache: false,
        type: "POST",
        success: function (data) {
            var markup;
            for (var x = 0; x < data.length; x++) {

                markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
            }
            $("#selectedQuetyOption").html(markup).show();
            $('#filterDataForm').unblock();
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
}