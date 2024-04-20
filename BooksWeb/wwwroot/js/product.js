var dataTable;

dataTable = $('#tblData').DataTable();

//function loadDataTable() {
//    dataTable = $('#tblData').DataTable({
//        "ajax": { URL: '/admin/product/getall' },
//        "columns": [
//            { data: 'title', "width": "15%"},
//            { data: 'isbn', "width": "15%"},
//            { data: 'listPrice', "width": "15%"},
//            { data: 'author', "width": "15%"},
//            { data: 'category.name', "width": "15%"}
//        ]
//    });
//}

//loadDataTable();

function DeleteConfirm(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    $('#tblData').DataTable().ajax.reload();
                    toastr.success(data.message)
                }
            })
            
        }
        
    });
    $('#tblData').DataTable();

}