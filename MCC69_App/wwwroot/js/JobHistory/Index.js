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
            url: "/JobHistory/GetAll",
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
                    return `${row.employee.firstName} ${row.employee.lastName}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.startDate}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.endDate}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.job.jobTitle}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.department.name}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `
                            <button type="button" class="btn fa fa-edit btn-default" data-toggle="modal" data-target="#editJobHistory" onclick="Edit('${row.id}')"></button>
                            <button type="button" class="btn fa fa-remove btn-default" onclick="Delete('${row.id}')"></button>
                           `;
                }
            }
        ]
    });
});

function Create() {
    let btn = document.getElementById("buttonAdd");
    btn.addEventListener("click", function (e) {
        e.preventDefault();
        let obj = new Object();
        obj.id = $("#addEmployeeId").val();
        obj.startDate = $("#addStartDate").val();
        obj.endDate = $("#addEndDate").val();
        obj.job_Id = $("#addJobId").val();
        obj.department_Id = $("#addDepartmentId").val();
        $.ajax({
            url: "/JobHistory/Post",
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
                $("#addJobHistory").modal("toggle");
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
        url: `/JobHistory/Get/${id}`,
        type: "GET"
    }).done((data) => {
        console.log(data);
        $("#employeeId").val(data.id);
        $("#startDate").val(data.startDate);
        $("#endDate").val(data.endDate);
        $("#jobId").val(data.job_Id);
        $("#departmentId").val(data.department_Id);
        let btn = document.getElementById("buttonEdit");
        btn.addEventListener("click", function (e) {
            e.preventDefault();
            let obj = new Object();
            obj.id = $("#employeeId").val();
            obj.startDate = $("#startDate").val();
            obj.endDate = $("#endDate").val();
            obj.job_Id = $("#jobId").val();
            obj.department_Id = $("#departmentId").val();
            $.ajax({
                url: "/JobHistory/Put",
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
                    $("#editJobHistory").modal("toggle");
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
                url: `/JobHistory/Delete/${id}`,
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