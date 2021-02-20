$(".addtocart").on("click", function () {
    var id = $(this).data("id");
    $.get("/Product/AddToCart", { productId: id }, function (data) {
        $("#allcount").text(data.count);
        $("#allprice").text(data.price);
    })
});
