var datatable;

$(document).ready(function () {
    
    load_list();
});

function load_list() {
    
    dataTable = $('#DT-load').DataTable({
        "ajax": {
            "url": "/api/MenuItem",
            "type": "GET",
            "datatype": "JSON"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "foodType.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                        <a href="/Admin/MenuItem/upsert?id=${data}" class="btn btn-success text-white" style ="cursor:pointer; width:100px;">   
                        <i class="fas fa-edit"></i></a>

                         <a  class="btn btn-danger text-white" style ="cursor:pointer; width:100px;" onclick="deleting('/api/MenuItem/${data}')">   
                        <i class="fas fa-trash"></i></a>
                        </div>
                    `;
                }, "width": "30%"
            }
        ],
        "width": "100%",
        "order": [[2, "asc"]],
        "language": {
            "emptyTable": "No data found"
        }
        



    });
}

function deleting(url){

    swal({
        "title": "Are you sure you want to delete?",
        "text": "You will not be able to restore data!",
        "icon": "warning",
        "buttons": true,
        "dangerMode": true

    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
