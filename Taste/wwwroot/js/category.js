var datatable;

$(document).ready(function () {
    
    load_list();
});

function load_list() {
    
    dataTable = $('#DT-load').DataTable({
        "ajax": {
            "url": "/api/category",
            "type": "GET",
            "datatype": "JSON"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            { "data": "displayOrder", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                        <a href="/Admin/category/upsert?id=${data}" class="btn btn-success text-white" style ="cursor:pointer; width:100px;">   
                        <i class="fas fa-edit"></i></a>

                         <a  class="btn btn-danger text-white" style ="cursor:pointer; width:100px;" onclick="deleting('/api/category/${data}')">   
                        <i class="fas fa-trash"></i></a>
                        </div>
                    `;
                }, "width": "30%"
            }
        ],
        "width": "100%",
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
