$(function () {
    function registerEvents() {
        $('.btn-minus').on("click", function () {
            let id = $(this).data('id');
            let quantity = parseInt($('#txt_quantity_' + id).val()) - 1;
            updateCart(id, quantity);
        })
        $('.btn-plus').on("click", function () {
            let id = $(this).data('id');
            let quantity = parseInt($('#txt_quantity_' + id).val()) + 1;
            updateCart(id, quantity);
        })
        $('.btn-remove').on("click", function () {
            const id = $(this).data('id');
            updateCart(id, 0);
        })
    }

    function updateCart(id, quantity) {
        $.ajax({
            url: '/Cart/Update',
            type: "POST",
            data: {
                id: id,
                quantity: quantity
            },
            success: function (res) {
                window.location.reload();
            },
            error: function (err) {
                console.log(err);
            }
        })
    }

    function cartDetail() {
        let qty = document.querySelector("#txt_quantity_");
        $(".btn-plus-detail").click(function () {
            let quantity = parseInt($('#txt_quantity_').val()) + 1;
            qty.value = quantity;
        })
        $(".btn-minus-detail").click(function () {
            let quantity = parseInt($('#txt_quantity_').val()) - 1;
            if (quantity <= 1) {
                quantity = 1;
                qty.value = quantity;
            }
        });
    }
    function loadData() {
        $.ajax({
            url: '/Cart/CartBag',
            success: function (res) {
                $('#CartCount').html(res);
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
    function AddToCart() {
        $(".btn-add-cart").click(function () {
            $.ajax({
                url: "/Cart/AddCart",
                data: {
                    Id: $(this).data("id"),
                },             
                success: function (response) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Add cart success',
                        showConfirmButton: false,
                        timer: 4500
                    });                 
                    loadData();
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Add cart unsuccess',
                        text: 'Try Again',
                        showConfirmButton: false,
                        timer: 4500
                    });
                }
            });
        });
    }
    function addToCartDetail() {
        $("#add").click(function () {
            $.ajax({
                url: "/Cart/AddToCartDetail",
                data: {
                    Id: $(this).data('id'),
                    quantity: $("#txt_quantity_").val(),
                },
                success: function (response) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Add cart success',
                        showConfirmButton: false,
                        timer: 2500
                    });
                    loadData();
                },
                failure: function (respone) {
                    alert(respone);
                }
            });
        });
    }
    function hideOnload() {
        $('#cc').hide();
        $('#pD').hide();
    }
    function choosePayment() {
        hideOnload();
        $('#trans').change(function () {
            let ID = $(this).val();
            if (ID == "PayPal") {
                $('#cc').hide();
                $('#pD').show();
            } else if (ID == "COD") {
                $('#pD').hide();
                $('#cc').show();
            } else {
                hideOnload();
            }
            console.log(ID);
        });
    }
    function placeOrd() {
        $("#btnPlaceOrder").click(function () {
            let a = $('#form1').serialize();
            $.ajax({
                url: "/Paypal/GetDataPaypal",
                data: a,                   
                type: 'POST',
                success: function (data) {

                    /* window.location.href = "/Paypal/GetDataPaypal";*/
                }
            });
        });
        $("#btnPlaceOrder1").click(function () {          
            let a = $('#form1').serialize();
            $.ajax({
                url: "/Paypal/GetDataCod",
                data: a ,
                type: 'POST',
                success: function () {
                    Swal.fire({
                        icon: 'success',
                        title: 'Order Success',
                        text: 'Please wait a moment the system will send you a notification',
                        showConfirmButton: false,
                        timer: 2500
                    });
                    window.location.href = "/Paypal/GetDataCod";
                }
            });

        });
    }
    AddToCart();
    addToCartDetail();
    registerEvents();
    cartDetail();
    choosePayment();
    placeOrd();
})