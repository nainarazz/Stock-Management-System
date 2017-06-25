$(document).ready(function () {

    var table = $("#dt").DataTable({

        processing: true,
        serverSide: true,

        ajax: {
            url: "Category/GetCategory",
            dataSrc: "category"
        },

        columns: [

            {
                data: "CategoryName"
            },

            {
                data: "CategoryDescription"
            },

            {
                data: "Id",
                render: function (data, type, category) {

                    var linkAddCategory = "<a data-modal= ' ' href='/category/new' data-category-id= " + data + "'><span class='fa fa-plus' style='color:green;'></span></a>";
                    var linkEdit = "<a data-modal= ' ' href= '/category/edit/" + data + "'><span class='fa fa-pencil'></span></a>";
                    var linkDelete = "<button style='cursor:pointer;' class= 'btn-link js-delete' data-category-id=" + data + "><span class='fa fa-trash' style='color:red;'></button>";
                    return linkAddCategory + " | " + linkEdit + " | " + linkDelete;

                }
            }
        ]
    });

    $("#dt").on("click", ".js-delete", function () {

        var button = $(this);

        bootbox.confirm("Are you sure you want to delete this Category?", function (result) {
            if (result) {
                $.ajax({
                    url: "/Category/Delete",
                    data: { Id: button.attr("data-category-id") },
                    method: "DELETE",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();

                    }

                });
            }

        });
    });


});