var xmlHttp = null;

function GetCustomerInfo() {
    var CustomerNumber = document.getElementById("p").value;
    var Url = "https://localhost:44379/weatherforecast";

    xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = ProcessRequest;
    xmlHttp.open("GET", Url, true);
    xmlHttp.send(null);
}

function ProcessRequest() {
    if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
        if (xmlHttp.responseText == "Not found") {
            document.getElementById("p").value = "Not found";
            document.getElementById("p").value = "";
        }
        else {
            var info = eval("(" + xmlHttp.responseText + ")");
            console.log(info[1].date);
            // No parsing necessary with JSON!        
            document.getElementById("p").innerText = info[1].date;
            document.getElementById("p").innerText = info[1].date;
        }
    }
}
GetCustomerInfo();