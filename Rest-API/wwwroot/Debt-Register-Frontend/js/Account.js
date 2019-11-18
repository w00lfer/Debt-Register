const apiURL = "https://localhost:44379/api";

/// sign up function
$('#signUpButton').click(function(e) {
    e.preventDefault();
    var signUpData = {
        Username: $('#username').val(),
        Email: $('#email').val(),
        Password: $('#password').val(),
        FullName: $('#fullname').val()
    };
    $.ajax({
        type: 'POST',
        url: `${apiURL}/Account/Register`,
        dataType: 'json',
        data: JSON.stringify(signUpData),
        contentType: 'application/json',
        success: function(data) {
            alert(`Welcome, you have created account successfuly ${data}`);
            window.location.href = "Summary.html";  
        },
        error: function() {
            alert("Data given is incorrect!")
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
        event.preventDefault()
        var signInData = {
            Username: $('#loginUsername').val(),
            Password: $('#loginPassword').val(),
        };
        $.ajax({
            type: 'POST',
            url: `${apiURL}/Account/Login`,
            dataType: 'json',
            data: JSON.stringify(signInData),
            contentType: 'application/json',
            success: function(data) {
                    console.log(data);
                    alert(`signed in ${data}`);
                     
                     window.location.href = "Summary.html";   
            },
            error: function(){
               alert("you gave wrong credentials!"); 
            }
            
        });
        
    }
});

//// sign up redirect from sign in modal
//$("#signUp").click(function(event) {
//    window.location.href = "SignUp.html";
// })

$("#signUp").click(() => window.location.href = 'SignUp.html');