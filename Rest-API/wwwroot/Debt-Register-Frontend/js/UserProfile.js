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
$("#saveProfileInfo").click( (e) => {
    if($("#userFullNameInfo").prop("disabled") === false && validateUserFullName())
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
                console.log(JSON.stringify({UserPhoneNumber: $("#userFullNameInfo").val()}))
                console.log($("#userFullNameInfo").val());
                alert(`You have changed fullname successfuly`);            
                populateForm();
            },
            error: () => {
                alert("Data given is incorrect!")
                console.log($("#userFullNameInfo").val());
            }
        });
    }
    else if($("#userPhonenumberInfo").prop("disabled") === false && validatePhonenumber())
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
    $("#userFullNameInfo").val(userCurrentFullName);
    $("#userPhonenumberInfo").val(userCurrentPhoneNumber);
    e.preventDefault();
});

$("#btnChangePassword").click((e) => {
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

            alert("You have changed password successfuly")
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
    return $("#newPasswordInfo") === $("#confirmNewPasswordInfo");
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
            console.log(data); 
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