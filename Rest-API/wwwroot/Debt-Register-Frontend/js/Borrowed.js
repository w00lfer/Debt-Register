const apiURL = "https://localhost:44379/api"

// ON PAGE LOAD TO POPULATE DEFAULT TABLE
$(document).ready( () => showBorrowedDebts());

$("#addDebt").click( () => window.location.href = 'AddBorrowedDebt.html');

function highlightAndShowTable(category, table)
{
    $(".highlighted").removeClass("highlighted"); // removes highlight class from previous element
    $(category).addClass("highlighted"); // highlights desired element
    $("table[style=''] tbody").html(""); // deletes elements from previous table body
    $(".my-container-content div").attr("style","display: none;"); // hides table 
    $(table).attr("style", ""); // shows desired table
}

function showBorrowedDebts()
{
    highlightAndShowTable($("#borrowedDebts"), $("#borrowedDebtsContainer"))
    $("#contactFinder").attr("style", "display:none");
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/1/Borrowed`,
        dataType: 'json',
        contentType: 'application/json',
        success: (data) => populateBorrowedDebtsTable(data),
        error: () => alert("Failed to load resources to last debts tables, check your internet connection!")
    });
}

$(document).ready(() => { 
    $("#borrowedDebtsFromLender").click( () => {
        highlightAndShowTable($("#borrowedDebtsFromLender"), $("#borrowedDebtsFromLenderContainer"));
        $("#contactFinder").attr("style", ""); 
    });
});

$(document).ready(() => { 
    $("#borrowedDebts").click( () => showBorrowedDebts());
});

$(document).ready(() => {
    $("#contactSelect").change( () =>  $(".searchBtn").prop("disabled", false) );
});

$(document).ready(() => {
    $("#contactType").change( () => $(".searchBtn").prop("disabled", true) );
});

$(document).ready(function() {
    $(".searchBtn").click( () => showBorrowedDebtsFromLender());
})
/// POPULATES USER/CONTACT SELECT
$(document).ready(() => { 
    $("#contactType").change(() => {
        if ($("#contactType option:selected").val() == 1 ) {           
            $(document).ready(() => {
                $.ajax({
                    type: "GET",
                    url: `${apiURL}/Account/UsersFullNames`,
                    success: (data) => populateBorrowerNames(data)
                });
            });
        }
        if ($("#contactType option:selected").val() == 2 ) {
            $(document).ready( () =>
                $.ajax({
                    type: "GET",
                    url: `${apiURL}/Contact/1/ContactsFullNames`,
                    success: (data) => populateBorrowerNames(data)
                })
            );
        }
    });
});

function showBorrowedDebtsFromLender() // activated after search click
{
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/1/BorrowedFromLender/${$("#contactSelect option:selected").val()}/${$("#contactType").val() != 1}`,
        dataType: 'json',
        contentType: 'application/json',
        success: (data) => populateBorrowedDebtsFromLenderTable(data),
        error:  () => alert("Failed to load resources to last debts tables, check your internet connection!")
    });
}

function populateBorrowedDebtsTable(data){
    let tableBody = $("#borrowedDebtsTableBody");
    let rows = ""
    for(let row in data)
    {
        rows += `<tr data-debt-id="${data[row].id}">
        <td>${new Date(data[row].debtStartDate).toLocaleDateString()}</td>
        <td>${data[row].value}</td>
        <td>${data[row].name}</td>
        <td>${data[row].isPayed === true}</td>
        <td>${data[row].contactFullName}</td>
        </tr>`
    }
    tableBody.html(rows);
}

function populateBorrowedDebtsFromLenderTable(data){
    let tableBody = $("#borrowedDebtsFromLenderTableBody");
    let rows = ""
    for(let row in data)
    {
        rows += `<tr data-debt-id="${data[row].id}">
        <td>${new Date(data[row].debtStartDate).toLocaleDateString()}</td>
        <td>${data[row].value}</td>
        <td>${data[row].name}</td>
        <td>${data[row].isPayed === true}</td>
        </tr>`
    }
    tableBody.html(rows);
}

function populateBorrowerNames(data){ 
    var s = '<option value="-1" disabled selected>Please choose contact or user</option>'
    for (var i = 0; i < data.length; i++) {
        s += `<option value="${data[i].id}">${data[i].fullName}</option>`;
    }
     $("#contactSelect").html(s);
}


