const apiURL = "https://localhost:44379/api"

$(document).ready(() => showContacts());

$(document).ready( () => $(".addContact").click( () => createAddContactModal()));

// ADDING A CONTACT
$(document).ready(() => {
    $(document).on("click", "#btnAddContact", function(e) {
        if(validateContactFullName() && validateContactPhoneNumber())
        {
            let contact = {
                fullName : $("#contactFullName").val(),
                phoneNumber: $("#contactPhoneNumber").val()
            }
            $.ajax({
                type: 'POST',
                url: `${apiURL}/Contact/AddContact`,
                data: JSON.stringify(contact),
                contentType: 'application/json',
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                },
                success: () => {
                    alert("You have successfuly deleted a contact");
                    $(".btn-cancel-add-contact").click();
                    showContacts();
                },
                error: () => alert("Failed to delete a contact")
            })
        }
        e.preventDefault();
    })
});

$(document).ready( () => $("tbody").on("click", ".editContact", function(e) {
    createEditContactModal($(this).closest("tr"));
}));

// EDIT A CONTACT
$(document).ready( () => {
    $(document).on("click", "#btnEditContact", function(e) {
        let contactId = $("#formEditContact").attr("data-contact-id");
        let contact = {
            id: parseInt($("#formEditContact").attr("data-contact-id")),
            fullName : $("#contactFullName").val(),
            phoneNumber: $("#contactPhoneNumber").val()
        };
        $.ajax({
            type: 'PUT',
            url: `${apiURL}/Contact/${contactId}`,
            data: JSON.stringify(contact),
            contentType: 'application/json',
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            },
            success: () => {
                alert("You have successfuly edited a contact");
                $(".btn-cancel-edit-contact").click();
                showContacts();
            },
            error: () => alert("Failed to edited a contact")
        });
        e.preventDefault();
    });
});

// DELETING A CONTACT
$(document).ready( () => {
    $("tbody").on("click", '.deleteContact', function(e) {
        contactId = $(this).closest("tr").attr("data-contact-id");
        $.ajax({
            type: 'DELETE',
            url: `${apiURL}/Contact/${contactId}`,
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            },
            success: () => {
                alert("You have successfuly deleted a contact");
                showContacts();
            },
            error: () => alert("Failed to delete a contact")
        })
    });
});

function showContacts()
{
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Contact`,
        dataType: 'json',
        contentType: 'application/json',
        headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        success: (data) => populateContactTable(data),
        error:  () => alert("Failed to load resources to contact table, check your internet connection!")
    });
};

function populateContactTable(data)
{
    let tableBody = $("#contactsTableBody");
    let rows = ""
    for(let row in data)
    {
        rows += `<tr data-contact-id="${data[row].id}">
        <td class="tdContactFullName">${data[row].fullName}</td>
        <td class="tdContactPhoneNumber">${data[row].phoneNumber}</td>
        <td>
            <a href=\"#\" class=\"editContact"\ >Edit</a> 
            <a href=\"#\" class=\"deleteContact"\ >Delete</a>
        </td>
        </tr>`
    }
    tableBody.html(rows);
}
function createAddContactModal()
{
    var html = 
    `<div id="addContactModal" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h3>Add contact</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"> × </button>
                </div>
            <div class="modal-body">
                <form class="form" role="form" autocomplete="off" id="formAddContact" novalidate="" method="POST">
                    <div class="form-group">
                        <input type="" class="form-control form-control-lg"  id="contactFullName"
                            required="" placeholder="Contact fullname" />
                        <div class="invalid-feedback">Oops, you missed this one.</div>
                    </div>
                    <div class="form-group">
                        <input  class="form-control form-control-lg" id="contactPhoneNumber" required=""
                            placeholder="Contact phone number" />
                        <div class="invalid-feedback">Oops, you missed this one.</div>
                    </div>
                    <div class="form-group py-4">
                        <button class="btn btn-outline-secondary btn-lg btn-cancel-add-contact" data-dismiss="modal"aria-hidden="true">Cancel</button>
                        <button class="btn btn-secondary btn-lg float-right" id="btnAddContact">Add contact</button>
                    </div>
                </form>
            </div>
        </div>
    </div>`
    $("#addContactModalContainer").html(html);
    $("#addContactModal").modal();
}

function createEditContactModal(row)
{
    let contactFullName = $(row).children(".tdContactFullName").text();
    let contactPhoneNumber = $(row).children(".tdContactPhoneNumber").text();
    let contactId = $(row).attr("data-contact-id");
    var html = 
    `<div id="editContactModal" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h3>Add contact</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"> × </button>
                </div>
            <div class="modal-body">
                <form data-contact-id="${contactId}"class="form" role="form" autocomplete="off" id="formEditContact" novalidate="" method="POST">
                    <div class="form-group">
                        <input type="" class="form-control form-control-lg"  id="contactFullName" value="${contactFullName}"
                            required="" placeholder="Contact fullname" />
                        <div class="invalid-feedback">Oops, you missed this one.</div>
                    </div>
                    <div class="form-group">
                        <input  class="form-control form-control-lg" id="contactPhoneNumber" value="${contactPhoneNumber}"
                            required="" placeholder="Contact phone number" />
                        <div class="invalid-feedback">Oops, you missed this one.</div>
                    </div>
                    <div class="form-group py-4">
                        <button class="btn btn-outline-secondary btn-lg btn-cancel-edit-contact" data-dismiss="modal"aria-hidden="true">Cancel</button>
                        <button class="btn btn-secondary btn-lg float-right" id="btnEditContact">Edit contact</button>
                    </div>
                </form>
            </div>
        </div>
    </div>`
    $("#editContactModalContainer").html(html);
    $("#editContactModal").modal();
}

function validateContactFullName(){
    return $("#contactFullName").val().match(/^([\w]{3,})+\s+([\w\s]{3,})+$/i)
}
function validateContactPhoneNumber(){
    return $("#contactPhoneNumber").val().match(/^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$/);
}
