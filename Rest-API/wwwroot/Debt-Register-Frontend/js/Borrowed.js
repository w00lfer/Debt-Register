const apiURL = "https://localhost:44379/api"
showLastDebts()
$(document).ready(function(){
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/1/Borrowed`,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
            showLastDebts();
        },
        error:  function(){
            alert("Failed to load resources to last debts tables, check your internet connection!");
        }
    });
});
//-!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ON PAGE LOAD TO POPULATE DEFAULT TABLE
function showLastDebts()
{
    $(".last-and-to-person .highlighted").removeClass("highlighted");
    $('#showLastDebts').addClass("highlighted");
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/1/Borrowed`,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
            populateLastDebtsTable( document.getElementById("borrowedDebtsTableBody"), data)
        },
        error:  function(){
            alert("Failed to load resources to last debts tables, check your internet connection!");
        }
    });
}
function showDebtsToPerson()
{
    $(".last-and-to-person .highlighted").removeClass("highlighted");
    $('#showDebtsToPerson').addClass("highlighted");
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/1/Borrowed`,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
            //populateLastDebtsTable( document.getElementById("borrowedDebtsTableBody"), data)
        },
        error:  function(){
            alert("Failed to load resources to last debts tables, check your internet connection!");
        }
    });
}

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


