function ShowAddTokenModal() {
    $('.datepicker').flatpickr({ /* options here */ });
    $("#ShowAddTokenModal").modal("show");
};

function HideAddTokenModal() {
    $("#ShowAddTokenModal").modal("hide");
};


function expiresDatePicker() {
    $('#expiresDatePicker').datetimepicker({
        format: 'YYYY-MM-DD HH:mm:ss',
    });
};



function SaveToken() {
    $("#ShowAddTokenModal").modal("hide");

    var tokenName = $("#tokenName").val();
    var tokenDescription = $("#tokenDescription").val();
    var tokenExpires = $("#expiresDatePicker input").val();
    var isActive = $("#isActive").prop("checked");

    var dataToSend = {
        tokenName: tokenName,
        tokenDescription: tokenDescription,
        tokenExpires: tokenExpires,
        isActive: isActive
    };

    $.ajax({
        type: 'POST',
        url: '/SaveToken',
        data: JSON.stringify(dataToSend),
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
        },
        error: function (error) {
            console.error("Error:", error);
        }
    });



};

