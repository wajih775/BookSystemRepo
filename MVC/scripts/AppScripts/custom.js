
function doAjaxRequest(type, url, parameterData, datatype, async) {
    debugger
    var result = "";
    $.ajax({
        type: type,
        url: url,
        data: parameterData,
        contentType: "application/json;",
        async: async,
        datatype: datatype,
        success: function (data) {
            result = data;
        },
        failure: function (data) {
            result = data;
        }
    });
    
    return result;
}

function showWarning(msg, divName, action) {
    $("#" + divName).html("<p><strong>WARNING: </strong>" + msg + "</p>");
    $("#" + divName).slideToggle(500, function () {
        $("#" + divName).fadeOut(6222);
    });
}

function showSuccess(msg, divName, action) {
    $("#" + divName).html("<p><strong>Message: </strong>" + msg + "</p>");
    $("#" + divName).slideToggle(500, function () {
        $("#" + divName).fadeOut(6222);
    });

    if (action == "delete") {
        setTimeout(function () {
            location.reload();
        }, 1000);
    }
}

function doAjaxSubmit(divName, msgDivNameSuccess, msgDivNameWarning, actionMsg) {
    debugger
    $('#' + divName).delegate('form', 'submit', function () {
        debugger
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#' + divName).find('.field-validation-error').text('');
                    if (result.status == true) {
                        showSuccess(result.message, msgDivNameSuccess, actionMsg);
                    } else {
                        showWarning(result.message, msgDivNameWarning, actionMsg);
                    }
                } else {
                    $('#' + divName).html(result);
                }
            }
        });
        return false;
    });
}

function fillDropDown(selectId, autoData, data, url, textProperty, valueProperty, initalValue) {
    if (initalValue != null) {
        $("#" + selectId).append($('<option>').text(initalValue).attr('value', ""));
    }
    if (autoData == true) {
        data = doAjaxRequest('post', url, '', 'json', false);
        for (var i = 0; i < data.length; i++) {
            $("#" + selectId).append($('<option>').text(data[i][textProperty]).attr('value', data[i][valueProperty]));
        }
    } else if (autoData == false) {
        for (var i = 0; i < data.length; i++) {
            $("#" + selectId).append($('<option>').text(data[i][textProperty]).attr('value', data[i][valueProperty]));
        }
    }
}



