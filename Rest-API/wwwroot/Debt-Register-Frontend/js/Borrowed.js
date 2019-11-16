const apiURL = "https://localhost:44379/api"

// ON PAGE LOAD TO POPULATE DEFAULT TABLE
$(document).ready(function(){
    showBorrowedDebts();
});

function highlightAndShowTable(category, tableBody)
{
    $(".highlighted").removeClass("highlighted"); // removes highlight class from previous element
    $(category).addClass("highlighted"); // highlights desired element
    $("table[style=''] tbody").html(""); // deletes elements from previous table body
    $("table[style='']").attr("style","display:none"); // hides table 
    $(tableBody).attr("style", ""); // shows desired table
}

function showBorrowedDebts()
{
    highlightAndShowTable($("#borrowedDebts"), $("#borrowedDebtsTable"))
    $("#contactFinder").attr("style", "display:none");
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/1/Borrowed`,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
            populateBorrowedDebtsTable(data)
        },
        error:  function(){
            alert("Failed to load resources to last debts tables, check your internet connection!");
        }
    });
}

$(document).ready(function() { 
    $("#borrowedDebtsFromLender").click(function(){
        highlightAndShowTable($("#borrowedDebtsFromLender"), $("#borrowedDebtsFromLenderTable"));
        $("#contactFinder").attr("style", ""); 
    });
});

$(document).ready(function() { 
    $("#borrowedDebts").click(function(){
        showBorrowedDebts();
    });
});

$(document).ready(function() {
    $("#contactSelect").change( () =>  $(".searchBtn").prop("disabled", false) );
});

$(document).ready(function() {
    $("#contactType").change( () => $(".searchBtn").prop("disabled", true) );
});

/// POPULATES USER/CONTACT SELECT
$(document).ready(function() { 
    $("#contactType").change(function() {
        if ($("#contactType option:selected").val() == 1 ) {           
            $(document).ready(function() {
                $.ajax({
                    type: "GET",
                    url: `${apiURL}/Account/UsersFullNames`,
                    success: function(data) {
                        var s = '<option value="-1" disabled selected>Please choose contact or user</option>'
                        for (var i = 0; i < data.length; i++) {
                            s += `<option value="${data[i].id}">${data[i].fullName}</option>`;
                        }
                        $("#contactSelect").html(s);
                    }
                });
            });
        }
        if ($("#contactType option:selected").val() == 2 ) {
            $(document).ready(function() {
                $.ajax({
                    type: "GET",
                    url: `${apiURL}/Contact/1/ContactsFullNames`,
                    success: function(data) {
                        var s = '<option value="-1" disabled selected>Please choose contact or user</option>'
                        for (var i = 0; i < data.length; i++) {
                            s += `<option value="${data[i].id}">${data[i].fullName}</option>`;
                        }
                        $("#contactSelect").html(s);
                    }
                });
            });
        }
    });
});
function showBorrowedDebtsFromLender() // activates after search click
{
    let isLocal = $("#contactType") != 1;
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/1/BorrowedFromLender/${$("#contactSelect option:selected").val()}/${isLocal}`,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
            populateBorrowedDebtsFromLenderTable(data)
        },
        error:  function(){
            alert("Failed to load resources to last debts tables, check your internet connection!");
        }
    });
}

function populateBorrowedDebtsTable(data){
    console.log(data);
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


