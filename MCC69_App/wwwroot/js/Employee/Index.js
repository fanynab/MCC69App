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
            url: "/Employee/GetAll",
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
                    return `${row.firstName} ${row.lastName}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.email}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.phoneNumber}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.hireDate}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `Rp.${row.salary},-`;
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
                    return `${row.manager_Id}`;
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
                            <button type="button" class="btn fa fa-edit btn-default" data-toggle="modal" data-target="#editEmployee" onclick="Edit('${row.id}')"></button>
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
        obj.firstName = $("#addFirstName").val();
        obj.lastName = $("#addLastName").val();
        obj.email = $("#addEmail").val();
        obj.phoneNumber = $("#addPhoneNumber").val();
        obj.hireDate = $("#addHireDate").val();
        obj.salary = $("#addSalary").val();
        obj.job_Id = $("#addJobId").val();
        obj.manager_Id = $("#addManagerId").val();
        obj.department_Id = $("#addDepartmentId").val();
        $.ajax({
            url: "/Employee/Post",
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
                $("#addEmployee").modal("toggle");
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
        url: `/Employee/Get/${id}`,
        type: "GET"
    }).done((data) => {
        console.log(data);
        $("#firstName").val(data.firstName);
        $("#lastName").val(data.lastName);
        $("#email").val(data.email);
        $("#phoneNumber").val(data.phoneNumber);
        $("#hireDate").val(data.hireDate);
        $("#salary").val(data.salary);
        $("#jobId").val(data.job_Id);
        $("#managerId").val(data.manager_Id);
        $("#departmentId").val(data.department_Id);
        let btn = document.getElementById("buttonEdit");
        btn.addEventListener("click", function (e) {
            e.preventDefault();
            let obj = new Object();
            obj.id = id;
            obj.firstName = $("#firstName").val();
            obj.lastName = $("#lastName").val();
            obj.email = $("#email").val();
            obj.phoneNumber = $("#phoneNumber").val();
            obj.hireDate = $("#hireDate").val();
            obj.salary = $("#salary").val();
            obj.job_Id = $("#jobId").val();
            obj.manager_Id = $("#managerId").val();
            obj.department_Id = $("#departmentId").val();
            $.ajax({
                url: "/Employee/Put",
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
                    $("#editEmployee").modal("toggle");
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
                url: `/Employee/Delete/${id}`,
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