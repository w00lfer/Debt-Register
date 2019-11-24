const apiURL = "https://localhost:44379/api"

// ON PAGE LOAD TO POPULATE DEFAULT TABLE
$(document).ready( () => showBorrowedDebts());

$("#addDebt").click( () => window.location.href = 'AddBorrowedDebt.html');


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

$(document).ready(function() {
    $(".searchBtn").click( () => showBorrowedDebtsFromLender());
})

/// POPULATES USER/CONTACT SELECT
$(document).ready(() => { 
    $("#contactType").change(() => {
        if ($("#contactType option:selected").val() == 1 ) {     // users       
            $(document).ready(() => {
                $.ajax({
                    type: "GET",
                    url: `${apiURL}/Account/UsersFullNames`,
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    },
                    success: (data) => populateBorrowerNames(data),
                    error:  () => alert("cannot get list of users")
                });
            });
        }
        if ($("#contactType option:selected").val() == 2 ) { // contacts
            $(document).ready( () =>
                $.ajax({
                    type: "GET",
                    url: `${apiURL}/Contact/ContactsFullNames`,
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    },
                    success: (data) => populateBorrowerNames(data),
                    error : () => alert("cannot get list of contacts")
                })
            );
        }
    });
});

// DELETE DEBT
$(document).ready( () => {
    $("tbody").on("click", '.delete', function(e) {
        debtId = $(this).closest("tr").attr("data-debt-id");
        $.ajax({
            type: 'DELETE',
            url: `${apiURL}/Debt/${debtId}`,
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            },
            success: () => {
                alert("You have successfuly deleted a debt");
                showBorrowedDebts();
            },
            error: () => alert("Failed to delete a debt")
        })
    });
});

// GET DEBT FOR VIEW
$(document).ready( () => {
    $("tbody").on("click", '.view', function()  {
        debtId = $(this).closest("tr").attr("data-debt-id");
        $.ajax({
            type: 'GET',
            url: `${apiURL}/Debt/ViewDebt/${debtId}`,
            dataType: 'json',
            contentType: 'application/json',
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            },
            success: (data) => {
                createViewDebtModal(data)
            },
            error: () => alert("Failed to view a debt")
        })
    });
});

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
    localStorage.getItem("token");
    highlightAndShowTable($("#borrowedDebts"), $("#borrowedDebtsContainer"))
    $("#contactFinder").attr("style", "display:none");
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/Borrowed`,
        dataType: 'json',
        contentType: 'application/json',
        headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        success: (data) => populateBorrowedDebtsTable(data),
        error: () => alert("Failed to load resources to last debts tables, check your internet connection!")
    });
}

function showBorrowedDebtsFromLender() // activated after search click
{
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/BorrowedFromLender/${$("#contactSelect option:selected").val()}/${$("#contactType").val() != 1}`,
        dataType: 'json',
        contentType: 'application/json',
        headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`
        },
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
        <td>
            <a href=\"#\" class=\"view"\ >View</a>
            <a href=\"#\" class=\"delete"\ >Delete</a>
        </td>
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
        <td>
            <a href=\"#\" class=\"view"\ >View</a>
            <a href=\"#\" class=\"delete"\ >Delete</a>
        </td>
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

function createViewDebtModal(debtInfo){
    var html =
    `<div id="viewDebtModal" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
           <div class="modal-content">
             <div class="modal-header">
                <h3> ${debtInfo.name}</h3>
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                    ×
                </button>
             </div>
             <div class="modal-body">
                <form class="form" role="form" autocomplete="off" id="formEditContact" novalidate="" method="POST">
                    <div class="form-group">
                        <h5>Date: ${debtInfo.debtStartDate}</h5>
                        <h5>Lender: ${debtInfo.contactFullName}</h5>
                        <h5>Value: ${debtInfo.value}</h5>
                        <h5>Is payed? ${debtInfo.payed === true ? "yes" : "no"}</h5>
                    </div>
                    <div class="form-group">
                        <h6>Description: ${debtInfo.description}</h6> 
                    </div>
                    <div class="form-group py-4">
                        <button class="btn btn-outline-secondary btn-lg btn-cancel-edit-contact float-right" data-dismiss="modal"aria-hidden="true">Cancel</button>
                    </div>
                </form>
                    
             </div>
         </div>
     </div>
    </div>`
    $(".view-debt-modal-container").html(html);
    $("#viewDebtModal").modal();
}

