const apiURL = "https://localhost:44379/api"

/// AJAX TO GET LAST DEBTS FROM API
$(document).ready(function(){
    /// POPULATES LAST 5 BORROWED DEBTS
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/LastBorrowed`,
        dataType: 'json',
        contentType: 'application/json',
        headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        success: function(data) {
            populateLastDebtsTable($("#lastBorrowedDebtsTableBody"), data)
        },
        error:  function(){
            alert("Failed to load resources to last debts tables, check your internet connection!");
        }
    });
    /// POPULATES LAST 5 LENT DEBTS
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/LastLent`,
        dataType: 'json',
        contentType: 'application/json',
        headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        success: function(data) {
            populateLastDebtsTable($("#lastLentDebtsTableBody"), data);
        },
        error:  function(){
            alert("Failed to load resources to last debts tables, check your internet connection!");
        }
    });
    
});

function populateLastDebtsTable(tableBody, data){
    let rows  = "";
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
    $(tableBody).html(rows);
}