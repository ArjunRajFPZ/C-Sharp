$(document).ready(function () {
    $("#registerForm").on("submit", function (e) {
        var inputName = $("#Name").val();
        var inputAddress = $("#Address").val();
        var inputEmail = $("#Email").val();
        var inputPassword = $("#Password").val();
        var inputConfirmPassword = $("#ConfirmPassword").val();
        if (inputName.match(/^([a-zA-Z ]{3,})$/)) {
            if (inputAddress.match(/^([a-zA-Z0-9., ]{6,})$/)) {
                if (inputEmail.match(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/)) {
                    if (inputPassword == inputConfirmPassword) {
                        return;
                    } else {
                        e.preventDefault();
                        alert("Entered password and confirm password do not match! Please check again.");
                        return;
                    }
                } else {
                    e.preventDefault();
                    alert("Entered email is not in correct format! Please try again.");
                    return;
                }
            } else {
                e.preventDefault();
                alert("Entered address contain less than 6 letters or contains special charactes! Please try again.");
                return;
            }
        } else {
            e.preventDefault();
            alert("Entered name must contain atleast 3 letters and should not be numbers or whitespace! Please try again.");
            return;
        }
    });
});