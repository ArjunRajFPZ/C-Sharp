$(document).ready(function () {
    $("#userform").on("submit", function (e) {
        var inputName = $("#name").val();
        var inputaddress = $("#address").val();
        var inputemail = $("#inputemail").val();
        if (inputName.match(/^([a-zA-Z ]{3,})$/)) {
            if (inputaddress.match(/^([a-zA-Z0-9., ]{3,})$/)) {
                if (inputemail.match(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/)) {
                    return;
                } else {
                    e.preventDefault();
                    alert("Entered email is not correct!");
                    return;
                }
            } else {
                e.preventDefault();
                alert("Entered address contains only whitespace or special Characters!");
                return;
            }
        } else {
            e.preventDefault();
            alert("Entered username contains number or whitespace! Please enter only characters");
            return;
        }
    });
});