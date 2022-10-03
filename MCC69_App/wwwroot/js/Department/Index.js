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
            url: "/Department/GetAll",
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
                    return `${row.location.streetAddress}, ${row.location.city}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `
                            <button type="button" class="btn fa fa-edit btn-default" data-toggle="modal" data-target="#editDepartment" onclick="Edit('${row.id}')"></button>
                            <button type="button" class="btn fa fa-remove btn-default" onclick="Delete('${row.id}')"></button>
                           `;
                }
            }
        ]
    });
});

function Create() {
    console.log("test")
    let btn = document.getElementById("buttonAdd");
    btn.addEventListener("click", function (e) {
        e.preventDefault();
        let obj = new Object();
        obj.name = $("#addName").val();
        obj.location_Id = $("#addLocationId").val();
        $.ajax({
            url: "/Department/Post",
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
                $("#addDepartment").modal("toggle");
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
        url: `/Department/Get/${id}`,
        type: "GET"
    }).done((data) => {
        console.log(data);
        $("#editName").val(data.name);
        $("#editLocationId").val(data.location_Id);
        let btn = document.getElementById("buttonEdit");
        btn.addEventListener("click", function (e) {
            e.preventDefault();
            let obj = new Object();
            obj.id = id;
            obj.name = $("#editName").val();
            obj.location_Id = $("#editLocationId").val();
            $.ajax({
                url: "/Department/Put",
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
                    $("#editDepartment").modal("toggle");
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
                url: `/Department/Delete/${id}`,
                type: "DELETE"
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