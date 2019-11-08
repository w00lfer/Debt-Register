
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
        console.log("lol");
        event.preventDefault()
        var myData = {
            Username: $('#loginUsername').val(),
            Password: $('#loginPassword').val(),
        };
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44379/api/User/Login',
            dataType: 'json',
            data: JSON.stringify(myData),
            contentType: 'application/json',
            success: function(data) {
                     alert("signed in");
                     window.location.href = "Summary.html";   
            },
            error: function(){
               alert("you gave wrong credentials"); 
            }
            
        });
        
    }
});

// sign up redirect from sign in modal
$("#signUp").click(function(event) {
    window.location.href = "SignUp.html";
 })
