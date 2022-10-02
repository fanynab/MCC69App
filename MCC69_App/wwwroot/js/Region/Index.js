$(document).ready(function () {
    $("#dataTable").DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                text: '',
                className: 'buttonHide fa fa-copy btn-default',
                exportOptions: { orthogonal: 'export' }
            },
            {
                extend: 'excelHtml5',
                text: '',
                className: 'buttonHide fa fa-download btn-default',
                exportOptions: { orthogonal: 'export' }
            },
        ],
        "ajax": {
            url: "/Region/GetAll",
            type: "GET",
            dataSrc: "",
            dataType: "JSON"
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.name}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `
                            <button type="button" class="btn fa fa-edit btn-default" data-toggle="modal" data-target="#modalRegion" onclick="Edit('${row.id}')"></button>
                            <button type="button" class="btn fa fa-remove btn-default" onclick="Delete('${row.id}')"></button>
                           `;
                }
            }
        ]
    });
});

function Create() {
    let btn = document.getElementById("buttonRegion");
    btn.addEventListener("click", function (e) {
        e.preventDefault();
        let obj = new Object();
        obj.name = $("#inputName").val();
        $.ajax({
            url: "/Region/Post",
            type: "POST",
            data: obj
        }).done((result) => {
            console.log(result);
            if (result == 200) {
                Swal.fire(
                    'Good Job!',
                    'Your data has been saved.',
                    'success'
                )
                $("#modalRegion").modal("toggle");
                $('#dataTable').DataTable().ajax.reload();
            }
            else if (result == 400) {
                Swal.fire(
                    'Watch Out!',
                    'Duplicate Data!',
                    'error'
                )
            }
        }).fail((error) => {
            console.log(error);
        })
    })
}

function Edit(id) {
    $.ajax({
        url: `/Region/Get/${id}`,
        type: "GET"
    }).done((data) => {
        console.log(data);
        $("#inputName").val(data.name);
        let btn = document.getElementById("buttonRegion");
        btn.addEventListener("click", function (e) {
            e.preventDefault();
            let obj = new Object();
            obj.id = id;
            obj.name = $("#inputName").val();
            $.ajax({
                url: "/Region/Put",
                type: "PUT",
                data: obj
            }).done((result) => {
                console.log(result);
                if (result == 200) {
                    Swal.fire(
                        'Good Job!',
                        'Your data has been saved.',
                        'success'
                    )
                    $("#modalRegion").modal("toggle");
                    $('#dataTable').DataTable().ajax.reload();
                }
                else if (result == 400) {
                    Swal.fire(
                        'Watch Out!',
                        'Duplicate Data!',
                        'error'
                    )
                }
            }).fail((error) => {
                console.log(error);
            })
        })
    }).fail((error) => {
        console.log(error);
    })
}

function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/Region/Delete/${id}`,
                type: "DELETE",
            }).done((result) => {
                if (result == 200) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    $('#dataTable').DataTable().ajax.reload();
                }
                else if (result == 400) {
                    Swal.fire(
                        'Error!',
                        'Your data failed to delete',
                        'error'
                    )
                }
            }).fail((error) => {
                console.log(error);
            })
        }
    })
}