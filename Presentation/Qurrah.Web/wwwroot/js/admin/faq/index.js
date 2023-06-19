
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
                url: `/Admin/FAQ/Delete/${id}`,
                type: "get",
                success: function (response) {
                    if (response.success)
                        location.href = '/Admin/FAQ/Index';
                },
                error: function (response) {
                    console.log('error occured');
                }
            });
    })
}