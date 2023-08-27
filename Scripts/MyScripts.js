$("#create").validate({
    rules: {
        Name_Product: "required",
        Price: {
            required: true,
            number: true,
        },
        Content: "required",
        Img: "required"
    },

    messages: {
        Content: "Please enter description product",
        Name_Product: "Please enter name product",
        Price: "Please enter a valid price",
        Img: "Please choose image"
    },
});

function ShowImgPreview(imgUploader, previewImg) {
    if (imgUploader.files && imgUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImg).attr('src', e.target.result);
        }
        reader.readAsDataURL(imgUploader.files[0]);
    }
}

$("#registration").validate({
    rules: {
        Phone: {
            required: true,
            number: true,
            minlength: 10,
            maxlength: 11

        },
        Password: {
            required: true,
            minlength: 6
        },

    },

    messages: {
        Phone: {
            required: "Please enter a valid phone number",
            minlength: "Your phone must be at least 10 number",
            maxlength: "Your phone number up to 11 numbers"
        },

        Password: {
            required: "Please provide a password",
            minlength: "Your password must be at least 6 characters long"
        },


    },
    submitHandler: function (form) {
        $.ajax({
            type: "POST",
            url: "/Customer/Login",
            data: $(form).serializeArray(),
            success: function (data) {
                if (data == "error") {
                    $("#pass").html('<font color = "Red">Phone or password wrong! Try Again</font>');
                }
                else if (data == "admin") {
                    window.location.href = "/Admin/AdminHome";
                }
                else {
                    window.location.href = "/Cart/Index";
                }
            },
            error: function () {
                console.log();
            }
        });
    }
});

$("#register").validate({
    rules: {
        Name: "required",
        Address: "required",
        Email: {
            required: true,
            email: true
        },
        Phone: {
            required: true,
            number: true,
            minlength: 10,
            maxlength: 11

        },
        Password: {
            required: true,
            minlength: 6
        },

    },

    messages: {
        Name: "Please enter your name",
        Address: "Please enter your address",
        Email: {
            required: "Please enter your email",
            email: "Enter the correct email format"
        },
        Phone: {
            required: "Please enter a valid phone number",
            minlength: "Your phone must be at least 10 number",
            maxlength: "Your phone number up to 11 numbers"
        },

        Password: {
            required: "Please provide a password",
            minlength: "Your password must be at least 6 characters long"
        },


    },
    submitHandler: function (form) {
        CheckNumber();
        $.ajax({
            type: "POST",
            url: "/Customer/Register",
            data: $(form).serializeArray(),
            success: function () {
            },
            error: function () {
                console.log();
            }
        });

    }
});
function CheckNumber() {
    $.post("/Customer/CheckPhoneNumber", {
        phonenumber: $("#Phone").val()
    },
        function (data) {
            if (data == "error") {
                window.location.href = "/Customer/Login"
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'That phone is exist',
                    showConfirmButton: false,
                    timer: 2500
                });
            }
        }
    )
}

