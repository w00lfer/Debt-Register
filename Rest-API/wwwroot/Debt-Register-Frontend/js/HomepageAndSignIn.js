 const apiURL = "https://localhost:44379/api";

/// sign up function
$('#signUpButton').click(function(e) {
    e.preventDefault();
    var signUpData = {
        Username: $('#username').val(),
        Email: $('#email').val(),
        Password: $('#password').val(),
        FullName: $('#fullname').val(),
        TelephoneNumber: $('#telephoneNumber').val()
    };
    $.ajax({
        type: 'POST',
        url: `${apiURL}/Account/Register`,
        dataType: 'json',
        data: JSON.stringify(signUpData),
        contentType: 'application/json',
        success: (data) => {
            alert(`Welcome, you have created account successfuly!`);
            localStorage.setItem("token", data.token);
            window.location.href = "Summary.html";  
        },
        error: () => alert("Data given is incorrect!")
    });
    return false;
});

$(".sign-in").click(() => createSignInModal());

/// sign in function
$(document).on("click", "#btnLogin", () => {
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
            success: (data) => {
                     alert(`signed in`);
                     localStorage.setItem("token", data.token);
                     window.location.href = "Summary.html";   
            },
            error: () => alert("you gave wrong credentials!")
        });
        
    }
});

$(document).on("click", "#signUp", () => {
    window.location.href = "SignUp.html";
 });

 function createSignInModal()
 {
     var html =
    `<div id="signInModal" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
           <div class="modal-content">
             <div class="modal-header">
                 <h3>Sign in</h3>
                 <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                     ×
                 </button>
             </div>
             <div class="modal-body">
                 <form class="form" role="form" autocomplete="off" id="formLogin" novalidate="" method="POST">
                     <div class="form-group">
                         <input type="text" class="form-control form-control-lg" name="uname1" id="loginUsername"
                             required="" placeholder="Username" />
                         <div class="invalid-feedback">Oops, you missed this one.</div>
                     </div>
                     <div class="form-group">
                         <input type="password" class="form-control form-control-lg" id="loginPassword" required=""
                             autocomplete="new-password" placeholder="Password" />
                         <div class="invalid-feedback">Enter your password too!</div>
                     </div>
                     <div class="custom-control custom-checkbox">
                         <input type="checkbox" class="custom-control-input" id="rememberMe" />
                         <label class="custom-control-label" for="rememberMe">Remember me on this
                             computer</label>
                     </div>
                     <div class="form-group py-4">
                         <button class="btn btn-outline-secondary btn-lg btn-cancel-sign-in" data-dismiss="modal"aria-hidden="true"> Cancel </button>
                         <button class="btn btn-secondary btn-lg float-right" id="btnLogin"> Login </button>
                     </div>
                 </form>
                 <div class="form-group">
                     <label class="label">Doesn't have you account?</label>
                     <button class="btn btn-outline btn-lg btn-secondary" id="signUp" data-dismiss="modal"
                         aria-hidden="true" href="SignUp.html">
                         Sign Up in Debt Register!
                     </button>
                 </div>
             </div>
         </div>
     </div>
    </div>`
    $(".sign-in-modal-container").html(html);
    $("#signInModal").modal();
 }