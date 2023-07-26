$(document).ready(function () {
    $("#taskbookform").on("submit", function (e) {
        var inputName = $("#name").val();
        var inputAssignedfrom = $("#assignedfrom").val();
        var inputAssignedto = $("#assignedto").val();
        if (inputName.match(/^([a-zA-Z ]{3,})$/)) {
            if (inputAssignedfrom.match(/^([a-zA-Z ]{3,})$/)) {
                if (inputAssignedto.match(/^([a-zA-Z ]{3,})$/)) {
                    return;
                } else {
                    e.preventDefault();
                    alert("Entered assigned to contains number or whitespace! Please enter only characters");
                    return;
                }
            } else {
                e.preventDefault();
                alert("Entered assigned from contains number or whitespace! Please enter only characters");
                return;
            }
        } else {
            e.preventDefault();
            alert("Entered username contains number or whitespace! Please enter only characters");
            return;
        }
    });
});