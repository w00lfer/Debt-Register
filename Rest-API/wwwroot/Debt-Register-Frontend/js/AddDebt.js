 
const apiURL = "https://localhost:44379/api"
$(document).ready(() => { 
    $("#contactType").change(() => {
        if ($("#contactType option:selected").val() == 1 ) {           
            $(document).ready(() => {
                $.ajax({
                    type: "GET",
                    url: `${apiURL}/Account/UsersFullNames`,
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    },
                    success: (data) => populateContactNames(data)
                });
            });
        }
        if ($("#contactType option:selected").val() == 2 ) {
            $(document).ready( () =>
                $.ajax({
                    type: "GET",
                    url: `${apiURL}/Contact/ContactsFullNames`,
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    },
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
    var currentURL = window.location.pathname;
    let addDebtData;
    if(currentURL.indexOf("Borrow") >=0)
    {
        addBorrowedDebtData = {
            Name: $('#debtName').val(),
            Value: parseFloat($('#debtValue').val()),
            Description: $('#debtDescription').val(),
            DebtStartDate: new Date(),
            LenderId: parseInt($("#contactSelect option:selected").val()),
            IsLenderLocal: ($("#contactType option:selected").val() != 1),
            IsPayed: $("#isPayedCheck").prop("checked")
        };
        $.ajax({
            type: 'POST',
            url: `${apiURL}/Debt/AddBorrowedDebt`,
            data: JSON.stringify(addBorrowedDebtData),
            contentType: 'application/json',
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            },
            success: () => alert("udalo sie dodac"),
            error: () => alert("nie udalo sie dodac")
        });
    }
    else
    {
        addLentDebtData = {
            Name: $('#debtName').val(),
            Value: parseFloat($('#debtVal   ue').val()),
            Description: $('#debtDescription').val(),
            DebtStartDate: new Date(),
            BorrowerId: parseInt($("#contactSelect option:selected").val()),
            IsBorrowerLocal:($("#contactType option:selected").val() != 1),
            IsPayed: $("#isPayedCheck").prop("checked")
        };
        $.ajax({
            type: 'POST',
            url: `${apiURL}/Debt/AddLentDebt`,
            data: JSON.stringify(addLentDebtData),
            contentType: 'application/json',
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            },
            success: () => alert("udalo sie dodac"),
            error: () => alert("nie udalo sie dodac")
        });
    }
    
    return false;
});

$(document).ready( () =>{
    $(".cancel-add-borrowed-debt").click( (e) => {
        window.location.href = "Borrowed.html";
        e.preventDefault();
    });
});
$(document).ready( () =>{
    $(".cancel-add-lent-debt").click( (e) => {
         window.location.href = "Lent.html";
         e.preventDefault();
        });
});
function populateContactNames(data){ 
    var s = '<option value="-1" disabled selected>Please choose contact or user</option>'
    for (var i = 0; i < data.length; i++) {
        s += `<option value="${data[i].id}">${data[i].fullName}</option>`;
    }
     $("#contactSelect").html(s);
}