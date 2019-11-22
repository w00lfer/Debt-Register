const logoutApiURL = "https://localhost:44379/api/Account/Logout"
$(document).ready( () => {
    token = localStorage.getItem("token");
    if(token !== null)
    {
        const parseJwt = (token) => {
            try {
              return JSON.parse(atob(token.split('.')[1]));
            } catch (e) {
              return null;
            }
          };
          $(".username").text(parseJwt(token).unique_name);
    }
    else {
        alert("You are not logged in! Stop cheating")
        window.location.href="Homepage.html";
    }
});

$(document).ready( () => {
    $(".logout").click( () => {
        $.ajax({
            type: 'POST',
            url: logoutApiURL,
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            },
            success: () => {
                alert("You have logout successfuly, see you next time!")
                localStorage.removeItem("token");
                window.location.href = "Homepage.html";   
            },
            error: () => alert("Something went wrong")
        });    
    })
})