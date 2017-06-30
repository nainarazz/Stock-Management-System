//code adapted from a post by  Shridhar Sharma
//http://www.c-sharpcorner.com/UploadFile/092589/implementing-modal-pop-up-in-mvc-application/

$(function () {
    $.ajaxSetup({ cache: false });

    $(document).on("click", "a[data-modal]", function (e) {

        $('#myModalContent').load(this.href, function () {
            
            $.validator.unobtrusive.parse($('form'));

            $('#myModal').modal({
                keyboard: true
            }, 'show');
            bindForm(this);
        });
        return false;
    });
});

function bindForm(dialog) {
    $('form', dialog).submit(function () {

        var table = $('#dt').DataTable();

        if ($(this).valid()) {

            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    if (result.success) {
                        $('#myModal').modal('hide');                                                
                        table.ajax.reload();
                    } else {
                        $('#myModalContent').html(result);
                        bindForm(dialog);
                    }
                }
            });
            return false;
        }        
    });

}