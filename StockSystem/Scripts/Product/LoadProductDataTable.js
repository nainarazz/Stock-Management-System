$(document).ready(function () {

    var table = $("#dt").DataTable({

        processing: true,
        serverSide: true,

        ajax: {
            url: "Product/GetProducts",
            dataSrc: "product"
        },
        
        columns: [

            {
                data: "ReferenceId",                                                                    
            },

            {
                data: "Category.CategoryName"
            },

            {
                data: "ItemName"
            },

            {
                data: "ItemUnit"
            },

            {
                data: "StockLevel"
            },

            {
                data: "UnitPrice"
            },

            {
                data: "ReferenceId",
                render: function (data,type,product) {
                    
                    var linkAddStock = "<a data-modal= ' ' href='/product/new' data-product-id= " + data + "'><span class='fa fa-plus' style='color:green;'></span></a>";
                    var linkRemoveStock = "<a href='#' data-product-id= " + data + "'><span class='fa fa-minus' style='color:red;'></span></a>";
                    var linkEdit = "<a data-modal= ' ' href= '/product/edit/" + data + "'><span class='fa fa-pencil'></span></a>";                    
                    var linkDelete = "<button style='cursor:pointer;' class= 'btn-link js-delete' data-product-id=" + data + "><span class='fa fa-trash' style='color:red;'></button>";
                    return linkAddStock + " | " + linkRemoveStock + " | " + linkEdit + " | " + linkDelete;

                }
            }
        ]
    });

    $("#dt").on("click", ".js-delete", function () {

        var button = $(this);

        bootbox.confirm("Are you sure you want to delete this product?", function (result) {
            if (result) {
                $.ajax({
                    url: "/Product/Delete",
                    data: { ReferenceId: button.attr("data-product-id") },
                    method: "DELETE",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();

                    }

                });
            }

        });
    });


});