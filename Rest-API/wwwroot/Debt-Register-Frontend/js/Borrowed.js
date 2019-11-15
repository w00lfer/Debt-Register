const apiURL = "https://localhost:44379/api"

$(document).ready(function(){
    showLastDebts();
});
//-!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ON PAGE LOAD TO POPULATE DEFAULT TABLE

function highlightAndShowTable(category, tableBody)
{
    $(".highlighted").removeClass("highlighted"); // removes highlight class from previous element
    $(category).addClass("highlighted"); // highlights desired element
    $("table[style=''] tbody").html(""); // deletes elements from previous table body
    $("table[style='']").attr("style","display:none"); // hides table 
    $(tableBody).attr("style", ""); // shows desired table
}
function showLastDebts()
{
    highlightAndShowTable($("#showLastDebts"), $("#borrowedDebtsTable"))
    $("#contactFinder").attr("style", "display:none");
  //  $(".last-and-to-person .highlighted").removeClass("highlighted");
    //$('#showLastDebts').addClass("highlighted");
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/1/Borrowed`,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
            populateLastDebtsTable(data)
        },
        error:  function(){
            alert("Failed to load resources to last debts tables, check your internet connection!");
        }
    });
}
function showDebtsToPersonTable()
{
    highlightAndShowTable($("#showDebtsToPerson"), $("#borrowedDebtsToPersonTable"))
    $("#contactFinder").attr("style", ""); 
}
function showDebtsToPerson()
{
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Debt/${$("#contactInput").val()}/Lent`,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
            populateDebtsFromPerson(data)
        },
        error:  function(){
            alert("Failed to load resources to last debts tables, check your internet connection!");
        }
    });
}

function populateLastDebtsTable(data){
    let tableBody = document.getElementById("borrowedDebtsTableBody")
    tableBody.innerHTML = "";
    for(let row in data)
    {
        let tableRow = tableBody.insertRow();
        startDateCell = tableRow.insertCell();
        startDateCell.innerHTML = new Date(data[row].debtStartDate).toLocaleDateString();
        valueCell = tableRow.insertCell();
        valueCell.innerHTML = data[row].value;
        descriptionCell = tableRow.insertCell();
        descriptionCell.innerHTML = data[row].name;
        payedCell = tableRow.insertCell();
        payedCell.innerHTML = data[row].payed == 'true';
        borrowerCell = tableRow.insertCell();
        borrowerCell.innerHTML = data[row].contactFullName;
        editCell = tableRow.insertCell();
        tableRow.setAttribute("data-debt-id", data[row].id);

    }
}

function populateDebtsFromPerson(data){
    tableBody = document.getElementById("borrowedDebtsToPersonTableBody");
    tableBody.innerHTML = "";
    for(let row in data)
    {
        let tableRow = tableBody.insertRow();
        startDateCell = tableRow.insertCell();
        startDateCell.innerHTML = new Date(data[row].debtStartDate).toLocaleDateString();
        valueCell = tableRow.insertCell();
        valueCell.innerHTML = data[row].value;
        descriptionCell = tableRow.insertCell();
        descriptionCell.innerHTML = data[row].name;
        payedCell = tableRow.insertCell();
        payedCell.innerHTML = data[row].payed == 'true';
        editCell = tableRow.insertCell();
        tableRow.setAttribute("data-debt-id", data[row].id)
        
    }

    $("#contactType").change(function() {
        if ($("#contactType option:selected").val() != -1)
        {
            $.ajax({
                type: 'GET',
                url: `${apiURL}/User/${$("#contactInput").val()}/Lent`,
                dataType: 'json',
                contentType: 'application/json',
                success: function(data) {
                    populateDebtsFromPerson(data)
                },
                error:  function(){
                    alert("Failed to load resources to last debts tables, check your internet connection!");
                }
            });
        }
    })
}


