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
                alert(data)
            }
        }); 

        return false;
    })