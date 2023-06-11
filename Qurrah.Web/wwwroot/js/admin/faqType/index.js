
function deleteType(id, question, description, confirmButtonText, cancelButtonText) {
    Swal.fire({
        title: question,
        text: description,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: confirmButtonText,
        cancelButtonText: cancelButtonText
    }).then((result) => {
        if (result.isConfirmed)
            $.ajax({
                url: `/Admin/FAQType/Delete/${id}`,
                type: "get",
                success: function (response) {
                    if (response.success)
                        location.href = '/Admin/FAQType/Index';
                },
                error: function (response) {
                    console.log('error occured');
                }
            });
    })
}