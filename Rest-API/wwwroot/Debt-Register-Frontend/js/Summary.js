const apiURL = "https://localhost:44379/api"

/// AJAX TO GET LAST DEBTS FROM API
$(document).ready(function(){
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/1/LastBorrowed`,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
            populateLastDebtsTable( document.getElementById("lastBorrowedDebtsTableBody"), data)
        }
    });
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/1/LastLent`,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
            populateLastDebtsTable( document.getElementById("lastLentDebtsTableBody"), data)
        }
    });
    
});

function populateLastDebtsTable(tableBody, data){
    let myData = data;
    for(let row in data)
    {
        let tableRow = tableBody.insertRow();
        startDateCell = tableRow.insertCell();
        startDateCell.innerHTML = new Date(data[row].debtStartDate).toLocaleDateString();
        valueCell = tableRow.insertCell();
        valueCell.innerHTML = data[row].value;
        descriptionCell = tableRow.insertCell();
        descriptionCell.innerHTML = data[row].description;
        payedCell = tableRow.insertCell();
        payedCell.innerHTML = data[row].payed == 'true';
        borrowerCell = tableRow.insertCell();
        borrowerCell.innerHTML = data[row].borrowerId;
    }
}