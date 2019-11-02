
/// sign up function
$('#signUpButton').click(function(e) {
    e.preventDefault();
    console.log("lol");
    var myData = {
        Username: $('#username').val(),
        Email: $('#email').val(),
        Password: $('#password').val(),
        FullName: $('#fullname').val()
    };
    $.ajax({
        type: 'POST',
        url: 'https://localhost:44379/api/User/Register',
        dataType: 'json',
        data: JSON.stringify(myData),
        contentType: 'application/json',
        success: function(data) {
            alert(data);
        }
    });
    return false;
});

/// log in function
$("#btnLogin").click(function(event) {
    if ($("#formLogin")[0].checkValidity() === false) {
    event.preventDefault()
    event.stopPropagation()
    }
    else
    {
        event.preventDefault();
        window.location.href = "Summary.html";
    }
});

// sign up redirect from sign in modal
$("#signUp").click(function(event) {
    window.location.href = "SignUp.html";
 })
