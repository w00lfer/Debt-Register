var isLoggedIn = false;
console.log("dsadsa");
$('#inp').click(function(e) {
    var myData = {
        Username: $('#username').val(),
        Email: $('#email').val(),
        Password: $('#password').val(),
        FullName: $('#fullname').val()
    };
    console.log(myData);
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
$("#signUp").click(function(event) {
    window.location.href = "SignUp.html";
})