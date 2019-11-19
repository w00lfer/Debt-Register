 
const apiURL = "https://localhost:44379/api"
$(document).ready(() => { 
    $("#contactType").change(() => {
        if ($("#contactType option:selected").val() == 1 ) {           
            $(document).ready(() => {
                $.ajax({
                    type: "GET",
                    url: `${apiURL}/Account/UsersFullNames`,
                    success: (data) => populateContactNames(data)
                });
            });
        }
        if ($("#contactType option:selected").val() == 2 ) {
            $(document).ready( () =>
                $.ajax({
                    type: "GET",
                    url: `${apiURL}/Contact/1/ContactsFullNames`,
                    success: (data) => populateContactNames(data)
                })
            );
        }
    });
});

$(document).ready(function() {
    $("#contactSelect").change( () =>  $(".searchBtn").prop("disabled", false) );
});

$(document).ready(function() {
    $("#contactType").change( () => $(".searchBtn").prop("disabled", true) );
});

$("#addDebtButton").click( (e) => {
    e.preventDefault();
    let addDebtData;
    currentURL.indexOf("Borrow") >=0 ?
        addDebtData = {
            Name: $('#debtName').val(),
            Value: parseFloat($('#debtValue').val()),
            Description: $('#debtDescription').val(),
            DebtStartDate: new Date(),
            DebtEndDate: null,
            LenderId: parseInt($("#contactSelect option:selected").val()),
            IsLenderLocal: ($("#contactType option:selected").val() != 1),
            BorrowerId: 1,
            IsBorrowerLocal: false,
            IsPayed: $("#isPayedCheck").prop("checked")
        }
        :
        addDebtData = {
            Name: $('#debtName').val(),
            Value: parseFloat($('#debtValue').val()),
            Description: $('#debtDescription').val(),
            DebtStartDate: new Date(),
            DebtEndDate: null,
            LenderId: 1,
            IsLenderLocal: false,
            BorrowerId: parseInt($("#contactSelect option:selected").val()),
            IsBorrowerLocal:($("#contactType option:selected").val() != 1),
            IsPayed: $("#isPayedCheck").prop("checked")
        }
    $.ajax({
        type: 'POST',
        url: `${apiURL}/Debt/AddDebt`,
        dataType: 'json',
        data: JSON.stringify(addDebtData),
        contentType: 'application/json',
        success: () => alert("udalo sie dodac"),
        error: () => alert("nie udalo sie dodac")
    });
    return false;
});


function populateContactNames(data){ 
    var s = '<option value="-1" disabled selected>Please choose contact or user</option>'
    for (var i = 0; i < data.length; i++) {
        s += `<option value="${data[i].id}">${data[i].fullName}</option>`;
    }
     $("#contactSelect").html(s);
}