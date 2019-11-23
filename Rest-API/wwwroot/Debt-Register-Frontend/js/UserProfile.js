const apiURL = "https://localhost:44379/api";

$(document).ready(() => populateForm());
var userCurrentFullName;
var userCurrentPhoneNumber;

$("#userFullNameInfoEdit").click( () => {
    allowEditOnlyOneInput($("#userFullNameInfo"), $("#userPhonenumberInfo"));
    showFormButtons();
});
$("#userPhonenumberInfoEdit").click( () => {
    allowEditOnlyOneInput($("#userPhonenumberInfo"), $("#userFullNameInfo"));
    showFormButtons();
});

//EDIT FULLNAME OR PHONENUMBER
$("#saveProfileInfo").click( (e) => {
    if($("#userFullNameInfo").prop("disabled") === false && validateUserFullName()) // EDIT FULLNAME
    {
        $.ajax({
            type: 'POST',
            url: `${apiURL}/Account/ChangeUserFullName`,
            data: {"userFullName" : $("#userFullNameInfo").val()},
            contentType: 'application/x-www-form-urlencoded',
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            },
            success: () => {
                alert(`You have changed fullname successfuly`);            
                populateForm();
            },
            error: () => {
                alert("Data given is incorrect!")
                console.log($("#userFullNameInfo").val());
            }
        });
    }
    else if($("#userPhonenumberInfo").prop("disabled") === false && validatePhonenumber()) // EDIT PHONENUMBER
    {
        $.ajax({
            type: 'POST',
            url: `${apiURL}/Account/ChangePhoneNumber`,
            data: {"userPhoneNumber" : $("#userPhonenumberInfo").val()},
            contentType: 'application/x-www-form-urlencoded',
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            },
            success: () => {
                alert(`You have changed phonenumber successfully`);
                populateForm();            
            },
            error: () => alert("Data given is incorrect!")
        });
    }
    disableAnyEditableInput();
    hideFormButtons();
    e.preventDefault();
    });

$("#cancelEditProfileInfo").click((e) => {
    disableAnyEditableInput();
    hideFormButtons();
    $("#userFullNameInfo").val(userCurrentFullName); // GOES BACK TO OLD VALUE IN CASE IF USER CANCELS EDITING BUT GAVE INPUT 
    $("#userPhonenumberInfo").val(userCurrentPhoneNumber); // GOES BACK TO OLD VALUE IN CASE IF USER CANCELS EDITING BUT GAVE INPUT 
    e.preventDefault();
});

$(".changePassword").click(() => {
    createChangePasswordModal()
});

// CHANGE PASSWORD
$(document).on("click", "#btnChangePassword", (e) => {
    if(validateChangePasswordForm())
    {
        let changePasswordData = {
            CurrentPassword:  $("#currentPasswordInfo").val(),
            NewPassword: $("#newPasswordInfo").val()
        }
        $.ajax({
            type: 'POST',
            url: `${apiURL}/Account/ChangePassword`,
            data: JSON.stringify(changePasswordData),
            contentType: 'application/json',
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            },
            success: () => {
            alert("You have changed password successfuly");
            $(".btn-cancel-change-password").click();
        },
            error: () => alert("Failed to get profile info")
        });
    }
    else alert("Your new password and confirmed new password are different!");
    e.preventDefault();
});
function validateUserFullName(){
    return $("#userFullNameInfo").val().match(/^([\w]{3,})+\s+([\w\s]{3,})+$/i)
}
function validatePhonenumber(){
    return $("#userPhonenumberInfo").val().match(/^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$/);
}
function validateChangePasswordForm(){
    return $("#newPasswordInfo").val() === $("#confirmNewPasswordInfo").val();
}
function showFormButtons(){
    $("#saveProfileInfo").prop("hidden", false);
    $("#cancelEditProfileInfo").prop("hidden", false);
}
function hideFormButtons(){
    $("#saveProfileInfo").prop("hidden", true);
    $("#cancelEditProfileInfo").prop("hidden", true);
}
function allowEditOnlyOneInput(inputToEdit, inputToDisable)
{
    $(inputToEdit).prop("disabled", false);
    $(inputToDisable).prop("disabled", true);
}
function disableAnyEditableInput()
{
    $("#userProfileForm input").prop("disabled", true);
}
function populateForm()
{
    $.ajax({
        type: 'GET',
        url: `${apiURL}/Account/UserProfile`,
        dataType: 'json',
        contentType: 'application/json',
        headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        success: (data) => { 
            $("#usernameInfo").val(data.userName);
            $("#userEmailInfo").val(data.email);
            $("#userFullNameInfo").val(data.fullName);
            data.phoneNumber != null ? $("#userPhonenumberInfo").val(data.phoneNumber) : $("#userPhonenumberInfo").val("You haven't given phone number yet") ;
            userCurrentFullName = $("#userFullNameInfo").val();
            userCurrentPhoneNumber = $("#userPhonenumberInfo").val();
        },
        error: () => alert("Failed to get profile info")
    });
}
function createChangePasswordModal()
{
    var html = 
    `<div id="changePasswordModal" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h3>Change password</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×
                    </button>
                </div>
                <div class="modal-body">
                    <form class="form" role="form" autocomplete="off" id="formChangePassword" novalidate="" method="POST">
                        <div class="form-group">
                            <input type="password" class="form-control form-control-lg"  id="currentPasswordInfo"
                                required="" placeholder="Current password" />
                            <div class="invalid-feedback">Oops, you missed this one.</div>
                        </div>
                        <div class="form-group">
                            <input type="password" class="form-control form-control-lg" id="newPasswordInfo" required=""
                                autocomplete="new-password" placeholder="New password" />
                            <div class="invalid-feedback">Enter your password too!</div>
                        </div>
                        <div class="form-group">
                            <input type="password" class="form-control form-control-lg" id="confirmNewPasswordInfo" required=""
                                autocomplete="new-password" placeholder="Confirm new password" />
                            <div class="invalid-feedback">Enter your password too!</div>
                        </div>
                        <div class="form-group py-4">
                            <button class="btn btn-outline-secondary btn-lg btn-cancel-change-password" data-dismiss="modal"aria-hidden="true">Cancel</button>
                            <button class="btn btn-secondary btn-lg float-right" id="btnChangePassword">Change</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>`
    $("#changePasswordModalContainer").html(html);
    $("#changePasswordModal").modal();
}