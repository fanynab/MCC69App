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
                    return `${row.job_Id}`;
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
                    return `${row.department_Id}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `
                            <button type="button" class="btn fa fa-edit btn-default" data-toggle="modal" data-target="#ModalData" onclick="Edit('${row.id}')"></button>
                            <button type="button" class="btn fa fa-remove btn-default" onclick="confirmDelete('tEmployees','Employees',${row.id})"></button>
                           `;
                }
            }
        ]
    });
});