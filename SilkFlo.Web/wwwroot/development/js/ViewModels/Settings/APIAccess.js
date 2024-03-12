/* eslint-disable no-undef */
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
    //$("#ShowAddTokenModal").modal("hide");

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
            if (response && response.isSucceed) {
                $("#ShowAddTokenModal").modal("hide");
                $("#apiAccessKeyGen").modal("show");
                $("#valueToShow").val(response.result);

                // Close modal when close button is clicked
                $("#akbtnclose").click(function () {
                    $("#apiAccessKeyGen").modal("hide");

                    $.ajax({
                        type: 'GET',
                        url: '/GetTokens',
                        contentType: 'application/json',
                        success: function (response) {
                            if (response && response.isSucceed) {
                                // Get the table body element
                                var tableBody = document.getElementById("accessKeytbl");
                                response.result.forEach(function (item, index) {
                                    var row = document.createElement("tr");

                                    // Create table cells and append them to the row
                                    var indexCell = document.createElement("td");
                                    indexCell.textContent = (index + 1) + ".";
                                    row.appendChild(indexCell);

                                    var usernameCell = document.createElement("td");
                                    usernameCell.textContent = item.name;
                                    row.appendChild(usernameCell);

                                    var keyCell = document.createElement("td");
                                    keyCell.textContent = item.sessionKey;
                                    row.appendChild(keyCell);

                                    var descriptionCell = document.createElement("td");
                                    descriptionCell.textContent = item.description;
                                    row.appendChild(descriptionCell);

                                    var dateCell = document.createElement("td");
                                    dateCell.textContent = item.expirationDate;
                                    row.appendChild(dateCell);

                                    var isActiveCell = document.createElement("td");
                                    isActiveCell.textContent = item.isActive;
                                    row.appendChild(isActiveCell);

                                    // Append the row to the table body
                                    tableBody.appendChild(row);
                                });
                            }
                        },
                        error: function (error) { }
                    });
                });

                // Copy text to clipboard when copy button is clicked
                $("#copyBtn").click(function () {
                    // Get the text to copy
                    var textToCopy = $("#valueToShow").text();

                    // Create a temporary textarea element
                    var $temp = $("<textarea>");
                    $("body").append($temp);
                    $temp.val(textToCopy).select();

                    // Copy the text to clipboard
                    document.execCommand("copy");

                    // Remove the temporary textarea
                    $temp.remove();
                });
            }
            else {

            }
        },
        error: function (error) {
            console.error("Error:", error);
        }
    });



};

function akbtncloseClick() {
    $("#apiAccessKeyGen").modal("hide");

    $.ajax({
        type: 'GET',
        url: '/GetTokens',
        contentType: 'application/json',
        success: function (response) {
            if (response && response.isSucceed) {
                // Get the table body element
                var tableBody = document.getElementById("accessKeytbl");
                response.result.forEach(function (item, index) {
                    var row = document.createElement("tr");

                    // Create table cells and append them to the row
                    var indexCell = document.createElement("td");
                    indexCell.textContent = (index + 1) + ".";
                    row.appendChild(indexCell);

                    var usernameCell = document.createElement("td");
                    usernameCell.textContent = item.name;
                    row.appendChild(usernameCell);

                    var keyCell = document.createElement("td");
                    keyCell.textContent = item.sessionKey;
                    row.appendChild(keyCell);

                    var descriptionCell = document.createElement("td");
                    descriptionCell.textContent = item.description;
                    row.appendChild(descriptionCell);

                    var dateCell = document.createElement("td");
                    dateCell.textContent = item.expirationDate;
                    row.appendChild(dateCell);

                    var isActiveCell = document.createElement("td");
                    isActiveCell.textContent = item.isActive;
                    row.appendChild(isActiveCell);

                    // Append the row to the table body
                    tableBody.appendChild(row);
                });
            }
        },
        error: function (error) { }
    });
}

function copyBtnClick() {
    // Get the text to copy
    var textToCopy = $("#valueToShow").text();

    // Create a temporary textarea element
    var $temp = $("<textarea>");
    $("body").append($temp);
    $temp.val(textToCopy).select();

    // Copy the text to clipboard
    document.execCommand("copy");

    // Remove the temporary textarea
    $temp.remove();
}

function getToken() {
    $.ajax({
        type: 'POST',
        url: '/GetTokens',
        contentType: 'application/json',
        success: function (response) {
            if (response && response.isSucceed) {
                // Get the table body element
                var tableBody = document.getElementById("accessKeytbl");
                response.result.forEach(function (item, index) {
                    var row = document.createElement("tr");

                    // Create table cells and append them to the row
                    var indexCell = document.createElement("td");
                    indexCell.textContent = (index + 1) + ".";
                    row.appendChild(indexCell);

                    var usernameCell = document.createElement("td");
                    usernameCell.textContent = item.name;
                    row.appendChild(usernameCell);

                    var keyCell = document.createElement("td");
                    keyCell.textContent = item.sessionKey;
                    row.appendChild(keyCell);

                    var descriptionCell = document.createElement("td");
                    descriptionCell.textContent = item.description;
                    row.appendChild(descriptionCell);

                    var dateCell = document.createElement("td");
                    dateCell.textContent = item.expirationDateString;
                    row.appendChild(dateCell);

                    var isActiveCell = document.createElement("td");
                    isActiveCell.textContent = item.isActive === true ? 'Yes' : 'No';
                    row.appendChild(isActiveCell);

                    // Append the row to the table body
                    tableBody.appendChild(row);
                });
            }
        },
        error: function (error) { }
    });
}

