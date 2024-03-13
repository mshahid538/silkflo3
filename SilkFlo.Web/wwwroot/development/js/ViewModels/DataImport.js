﻿/* eslint-disable no-restricted-globals */
/* eslint-disable no-undef */
/* eslint-disable no-unused-vars */
window.onload = function () {
    //$('#tstProgress').toast('show');
    //$('#tstSuccess').toast('show');

    //$('#tstProgress').toast('hide');
    //$('#tstSuccess').toast('hide');

    //var toastElement = document.querySelector('.toast');
    //if (toastElement) {
    //    toastElement.classList.add('show');
    //}
}

function ContinueWorking() {
    $('#tstProgress').toast('show');
}

function getStatus() {
    var intervalId = setInterval(function () {
        $.ajax({
            url: '/Data/Status',
            type: 'GET',
            success: function (data) {
                if (data) {
                    if (data.isCompleted) {
                        clearInterval(intervalId);
                        $('#tstProgress').toast('hide');
                        $('#tstSuccess').toast('show');
                        var tstbody = "<span><b>" + data.successCount + "</b></span> <strong>ideas have been added!</strong>" +
                            "<span><b>" + data.failedCount + "</b></span> <strong>ideas have been failed!</strong>";
                        $("#tstSuccessBody").html(tstbody);
                    }
                    else {
                        $('#tstProgress').toast('show');
                        getStatus();
                    }
                }
            }
        });
    }, 1000);
}


function showImportModal() {
    $("#UploadPipelineModal").modal("show");
};

function btnShowUploadPipelineMessageModal() {
    $("#ShowDataPipelineModal").modal("hide");
    $("#ShowUploadPipelineMessageModal").modal("show");
};
function btnShowUploadFileModal() {
    $("#ShowUploadPipelineMessageModal").modal("hide");
    $("#UploadPipelineModal").modal("show");
};


function btnShowFileData() {
    $("#ShowUploadPipelineMessageModal").modal("hide");

    $("#ShowDataPipelineModal").modal("show");
};



$('#ShowUpdateDeleteModal').click(function () {

    $("#UpdateDeleteModal").modal("show");
});

$('#btnCloseFileModal').click(function () {

    $("#UploadPipelineModal").modal("hide");
    location.reload();


});
function closeUploadPipelineModal() {

    $("#UploadPipelineModal").modal("hide");
};
function closeShowUploadPipelineMessageModal() {
    $("#ShowUploadPipelineMessageModal").modal("hide");
};

function closeShowDataPipelineModal() {
    $("#ShowDataPipelineModal").modal("hide");
};
function closeShowResultModal() {
    $("#ShowResultModal").modal("hide");
};

$('#btnCloseResultModal').click(function () {

    $("#ShowResultModal").modal("hide");
    location.reload();


});

$("#DownloadTemplate").on("click", function () {

    var baseUrl = window.location.origin;

    var filePath = '/IdeaTemplate/IdeasListTemplate.xlsx';

    var fullFilePath = baseUrl + filePath;

    var filePath = fullFilePath;

    var link = document.createElement('a');
    link.href = filePath;
    link.download = 'IdeasListTemplate.xlsx';

    document.body.appendChild(link);

    link.click();

    document.body.removeChild(link);
});

function btnTFilesUpload() {
    $("#InvalidFileTemplate").css("display", "none");

    var fileInput = document.getElementById('fileElem');

    if (fileInput.files.length === 0) {
        return;
    }

    var formData = new FormData();
    var file = fileInput.files[0];

    formData.append('File', file);

    $.ajax({
        type: 'POST',
        url: '/Data/Import',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.status == true) {
                var employeeData = response.data;
                $("#UploadPipelineModal").modal("hide");
                $("#EmptyFileText").css("display", "none");
                $("#duplicateIdeasText").css("display", "none");
                $("#InvalidFileTemplate").css("display", "none");

                $('#Coedata').empty();
                var x = 1;

                $.each(employeeData, function (index, row) {
                    var newRow = '<tr>';
                    newRow += '<td>' + x + '</td>';
                    x++;
                    for (var prop in row) {
                        if (row.hasOwnProperty(prop)) {
                            var cellContent = (row[prop] === null) ? "" : row[prop];
                            newRow += '<td  contenteditable="true">' + cellContent + '</td>';
                        }
                    }
                    newRow += '<td><span title="Delete"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash child-div btnDeleteRow" viewBox="0 0 16 16" style="color:red;"><path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6Z"></path><path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1ZM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118ZM2.5 3h11V2h-11v1Z"></path></svg></span></td>';
                   // newRow += '<td><button title="delete" class="btn btn-danger btnDeleteRow" style="background: transparent; border: none; color: red;"><i class="fas fa-trash-alt"></i></button></td>';

                    newRow += '</tr>';
                    $('#Coedata').append(newRow);
                });

                $("#ShowUploadPipelineMessageModal").modal("show");

            } else {
                $("#InvalidFileTemplate").text(response.message).css("display", "block");
                return;
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

$(document).on('click', '.btnDeleteRow', function () {
    $(this).closest('tr').remove();
});

function saveCOEData() {

    $("#duplicateIdeasText").css("display", "none");
    $("#emptyNamesText").css("display", "none");
    $("#emptyDescriptionText").css("display", "none");
    $("#exceedDescriptionText").css("display", "none");
    $("#exceedNameText").css("display", "none");
    $("#EmptyFileText").css("display", "none");

    var tableData = [];

    var hasDuplicates = false;
    var hasEmptyNames = false;
    var hasEmptyDescription = false;
    var exceedNameLength = false;
    var exceedDescriptionLength = false;


    $("#Coedatalist tbody tr").each(function () {
        var rowData = {};
        var $row = $(this);

        $row.find('td:first-child, td:nth-child(2), td:last-child').addBack().css('background-color', '');

        $row.find('td').each(function (index) {
            var columnName = $("#Coedatalist thead tr td").eq(index).text();

            var cellValue = $(this).text();

            rowData[columnName] = cellValue;
        });

        var name = rowData['Name'];
        var description = rowData['Description'];
        if (!name.trim()) {
            $row.find('td:first-child, td:nth-child(2), td:last-child').addBack().css('background-color', 'rgb(255, 228, 225)');
            hasEmptyNames = true;
        }
        else if (tableData.some(item => item['Name'] === name)) {
            $row.find('td:first-child, td:nth-child(2), td:last-child').addBack().css('background-color', 'rgb(255, 228, 225)');
            hasDuplicates = true;
        }


        else if (!description.trim()) {
            $row.find('td:first-child, td:nth-child(2), td:last-child').addBack().css('background-color', 'rgb(255, 228, 225)');
            hasEmptyDescription = true;
        }


        else if (description.length > 10000) {
            $row.find('td:first-child, td:nth-child(2), td:last-child').addBack().css('background-color', 'rgb(255, 228, 225)');
            exceedDescriptionLength = true;
        }

        else if (name.length > 100) {
            $row.find('td:first-child, td:nth-child(2), td:last-child').addBack().css('background-color', 'rgb(255, 228, 225)');
            exceedNameLength = true;
        }

        tableData.push(rowData);
    });

    if (tableData.length == 0) {
        $("#EmptyFileText").css("display", "block");
        return;
    }

    else if (hasDuplicates) {
        $("#duplicateIdeasText").css("display", hasDuplicates ? "block" : "none");
        return;
    }

    else if (hasEmptyNames) {
        $("#emptyNamesText").css("display", hasEmptyNames ? "block" : "none");
        return;
    }

    else if (hasEmptyDescription) {
        $("#emptyDescriptionText").css("display", hasEmptyDescription ? "block" : "none");
        return;
    }

    else if (exceedDescriptionLength) {
        $("#exceedDescriptionText").css("display", exceedDescriptionLength ? "block" : "none");
        return;
    }

    else if (exceedNameLength) {
        $("#exceedNameText").css("display", exceedNameLength ? "block" : "none");
        return;
    }

    $("#ShowDataPipelineModal").modal("hide");
    $("#ShowDataProcessingModal").modal("show");
    $('#loading-overlay').show();


    $.ajax({
        type: 'POST',
        url: '/Data/Save',
        data: JSON.stringify(tableData),
        contentType: 'application/json',
        success: function (response) {
            if (response.status == true) {
                $("#ShowDataProcessingModal").modal("hide");

                $('#loading-overlay').hide();
                $("#SuccessIdeaCounter").text(response.successCount);
                $("#FailedIdeaCounter").text(response.failedCount);


                $("#ShowResultModal").modal("show");


            } else {
                console.error(response.message);
            }
        },
        error: function (error) {
            hideLoading();

            console.log(error);
        }
    });
}


$("#performIdeaSearch").on("click", function () {
    var searchType = $("input[name='searchType']:checked").val();

    var searchText = $("#searchInput").val();

    $.ajax({
        url: '/api/ApisController/GetIdeaBySearch',
        type: 'Get',
        data: {
            searchType: searchType,
            searchText: searchText
        },
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.error(error);
        }
    });
});

function closeModalAndRefreshPage() {

    location.reload();
}
function continueWorking() {
    $("#ShowDataProcessingModal").modal("hide");
    console.log("ideas being adding");
}

function FinishPipelinemodals() {
    location.reload();
}

function onFileElemChange2() {
    $("#InvalidFileTemplate").css("display", "none");
    var fileInput = document.getElementById('fileElem');
    var fileLabel = document.getElementById('fileLabel');

    if (fileInput.files.length > 0) {
        fileLabel.innerText = fileInput.files[0].name;
    } else {
        fileLabel.innerText = "No file selected";
    }
}

function dropHandler(event) {
    event.preventDefault();
    var fileInput = document.getElementById('fileElem');
    var fileLabel = document.getElementById('fileLabel');

    if (event.dataTransfer.items) {
        if (event.dataTransfer.items[0].kind === 'file') {
            var file = event.dataTransfer.items[0].getAsFile();
            fileLabel.innerText = file.name;
        }
    }
}

function dragOverHandler(event) {
    event.preventDefault();
}