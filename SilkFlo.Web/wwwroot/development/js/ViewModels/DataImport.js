window.onload = function () {
    $('#tstProgress').toast('show');
    $('#tstSuccess').toast('show');

    //$('#tstProgress').toast('hide');
    //$('#tstSuccess').toast('hide');

    //var toastElement = document.querySelector('.toast');
    //if (toastElement) {
    //    toastElement.classList.add('show');
    //}
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
                            newRow += '<td  contenteditable="true">' + row[prop] + '</td>';
                        }
                    }

                    newRow += '<td><button class="btn btn-danger btnDeleteRow" style="background: transparent; border: none; color: red;"><i class="fas fa-trash-alt"></i></button></td>';


                    newRow += '</tr>';
                    $('#Coedata').append(newRow);
                });

                $("#ShowUploadPipelineMessageModal").modal("show");

            } else {
                $("#InvalidFileTemplate").css("display", "block");
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

    var tableData = [];

    var hasDuplicates = false;


    $("#Coedatalist tbody tr").each(function () {
        var rowData = {};
        var $row = $(this);

        $row.find('td').each(function (index) {
            var columnName = $("#Coedatalist thead tr td").eq(index).text();

            var cellValue = $(this).text();

            rowData[columnName] = cellValue;
        });

        var name = rowData['Name'];
        if (tableData.some(item => item['Name'] === name)) {
            $row.css('background-color', 'rgb(255, 228, 225)');
            hasDuplicates = true;
        }

        tableData.push(rowData);
    });

    if (tableData.length == 0) {
        $("#EmptyFileText").css("display", "block");
        return;
    }

    if (hasDuplicates) {
        $("#duplicateIdeasText").css("display", hasDuplicates ? "block" : "none");
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

function FinishPipelinemodals() {
    location.reload();
}

function onFileElemChange2() {
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