
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
        url: 'https://localhost:44379/api/Account/Register',
        dataType: 'json',
        data: JSON.stringify(myData),
        contentType: 'application/json',
        success: function(data) {
        },
        error: function() {
            alert("123")
        }
    });
    return false;
});

/// sign in function
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
            url: 'https://localhost:44379/api/Account/Login',
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

//// sign up redirect from sign in modal
//$("#signUp").click(function(event) {
//    window.location.href = "SignUp.html";
// })

$("#signUp").click(() => window.location.href = 'SignUp.html');