

//ajax
//$.ajax({
//    url: "https://localhost:44303/API/Employees/GetRegisterData"
//}).done((result) => {
//    console.log(result.results);
//    var text = "";
//    $.each(result.results, function (key, val) {
//        text += `<tr>
//                    <td>${key+1}</td>
//                    <td>${val.name}</td>
//                    <td>
//                        <button data-toggle="modal" data-target="#modalPoke" class="btn btn-primary" onclick="getDetails('${val.url}')">Detail</button>
//                    </td>
//                </tr>`;
//    });
//    console.log(text);
//    $(".tablePoke").html(text);

//const { title } = require("process");

//const { post } = require("jquery");

//}).fail((error) => {
//    console.log(error);
//})


$(document).ready(function () {
    $('#tPoke').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                text: '<i class="fa fa-files-o"> Copy</i>',
                className: 'btn btn-secondary',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"> Excel</i>',
                className: 'btn btn-success',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-csv"> CSV</i>',
                className: 'btn btn-primary',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"> PDF</i>',
                className: 'btn btn-danger',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print"> Print</i>',
                className: 'btn btn-dark',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
        ],
        'ajax': {
            'url': "https://localhost:44357/Employees/GetRegisterData",
            'dataType': 'json',
            'dataSrc': 'result'
        },
        'scrollX': true,
        'columns': [
            {
                'data': null,
                'render': (data, type, row, meta) => {
                    return (meta.row + 1);
                }
            },
            {
                'data': 'nik',
            },
            {
                
                'width': '200px',
                'data': null,
                'render': (data) => {
                    var fullName = data.firstName + " " + data.lastName;
                    const fullNameCapital = fullName.charAt(0).toUpperCase() + fullName.slice(1);
                    return (fullNameCapital);
                }
            },
            {
                'width': '150px',
                'data': 'phone'
            },
            {
                'data': 'email'
            },
            {
                'width': '100px',
                'data': 'birthDateStr'
            },
            {
                'width': '150px',
                'data': null,
                'render': (data) => {
                    return ("$" + data.salary)
                }
            },
            {
                'data': 'gender'
            },
            {
                'data': 'degree'
            },
            {
                'data': 'universityName'
            },
            {
                'data': null,
                'render': (data) => {
                    var rName = "";
                    $.each(data.role, function (key, val) {
                        if (data.role.length - 1 == key) {
                            rName += val + ".";
                        } else {
                            rName += val + ", ";
                        }
                    })

                    return (rName)
                }
            },
            {
                'data': null,
                'render': (data, type, row) => {
                    return `<button data-toggle="modal" data-target="#updateEmployee" type="button" class="btn btn-success fas fa-info" onclick="UpdateEmploye(${row["nik"]})"></button>
                        <button type = "button" class="btn btn-danger mt-2 fa fa-remove" onclick="DeleteEmployee(${row["nik"]})"></button>`
                },
                'bSortable': false
            }
        ]
    });
});

function DeleteEmployee(nik) {
    console.log(nik);
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '##ff0000',
        cancelButtonColor: '#d33',
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            var myTable = $('#tPoke').DataTable();
            $.ajax({
                url: "https://localhost:44357/Employees/DeleteRegisterData/" + nik,
                type: "DELETE",
                //contentType: "application/json;charset=utf-8",
            }).done((result) => {
                console.log(result);
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: "Delete Success",
                    type: 'success'
                });
                myTable.ajax.reload();

            }).fail((error) => {
                console.log(error);
                Swal.fire({
                    icon: 'failed',
                    title: 'Failed',
                    text: "Delete Failed",
                    type: 'failed'
                });
            })
        }
    })
}




function UpdateEmploye(nik) { //show detail
    $.ajax({
        url: "https://localhost:44303/Api/Universities"
    }).done((result) => {
        var option = "<option>Select:</option>";
        $.each(result, function (key, val) {
            option += `<option value="${val.universityID}">${val.name}</option>`
        });
        $("#universityUpdate").html(option);
    }).fail((error) => {
        console.log(error)
    })
    console.log(nik);
    $.ajax({
        url: "https://localhost:44357/Employees/GetRegisterByNIK/" + nik,
    }).done((result) => {
        console.log("GPA: " + result.gpa);
        $('input#nikEmployee').val(`${nik}`);
        $('input#firstNameUpdate').val(`${result.firstName}`);
        $('input#lastNameUpdate').val(`${result.lastName}`);
        $('input#emailUpdate').val(`${result.email}`);
        $('input#passwordUpdate').val(`${result.password}`);
        $('input#phoneUpdate').val(`${result.phone}`);
        $('input#birthDateUpdate').val(`${result.birthDateStr}`);
        $('input#salaryUpdate').val(`${result.salary}`);
        $('#genderUpdate').val(`${result.gender}`);
        $('#universityUpdate').val(`${result.universityID}`);
        $('#degreeUpdate').val(`${result.degree}`);
        $('input#gpaUpdate').val(`${result.gpa}`);

    }).fail((error) => {
        console.log(error)
    })
}



function submitData() {
    var obj = Object();
    obj.FirstName = $('#firstName').val();
    obj.LastName = $('#lastName').val();
    obj.Email = $('#email').val();
    obj.Password = $('#password').val();
    obj.Phone = $('#phone').val();
    obj.BirthDate = $('#birthDate').val();
    obj.Salary = parseInt($('#salary').val());
    obj.Gender = parseInt($('#gender').val());
    obj.UniversityID = parseInt($('#university').val());
    obj.Degree = $('#degree').val();
    obj.GPA = parseFloat($('#gpa').val());

    console.log(obj.GPA);
    //var objJson = JSON.stringify(obj);
    //console.log(objJson);

    var myTable = $('#tPoke').DataTable();

    $.ajax({
        url: "https://localhost:44357/Employees/Register",
        type: "POST",
        data: obj
    }).done((result) => {
        console.log(result)
        if (result.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: result.message,

            });
        }
        else
        {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: result.message,

            });
        }
        $('.modal#modalRegister').modal("hide");
        myTable.ajax.reload();

    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'failed',
            title: 'Failed',
            text: error.message,

        });
    })
}

function submitUpdate() {
    Swal.fire({
        title: 'Are you sure?',
        text: "Your data will be change!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '##ff0000',
        cancelButtonColor: '#d33',
        confirmButtonText: "Yes, change it!"
    }).then((result) => {
        if (result.isConfirmed) {
            var obj = Object();
            obj.NIK = $("#nikEmployee").val();
            obj.FirstName = $('#firstNameUpdate').val();
            obj.LastName = $('#lastNameUpdate').val();
            obj.Email = $('#emailUpdate').val();
            obj.Phone = $('#phoneUpdate').val();
            obj.BirthDate = $('#birthDateUpdate').val();
            obj.Salary = parseInt($('#salaryUpdate').val());
            obj.Gender = parseInt($('#genderUpdate').val());
            obj.UniversityID = parseInt($('#universityUpdate').val());
            obj.Degree = $('#degreeUpdate').val();
            obj.GPA = parseFloat($('#gpaUpdate').val());
            //var objJson = JSON.stringify(obj);
            //console.log(objJson);

            var myTable = $('#tPoke').DataTable();

            $.ajax({
                url: "Employees/UpdateRegister",
                type: "PUT",
                //contentType: "application/json;charset=utf-8",
                data: obj
            }).done((result) => {
                console.log(result)
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: result.message,
                    type: 'success'
                });
                myTable.ajax.reload();
                $('.modal#updateEmployee').modal("toggle");

            }).fail((error) => {
                console.log(error);
                Swal.fire({
                    icon: 'failed',
                    title: 'Failed',
                    text: error.message,
                    type: 'failed'
                });
            })

        }
    })
}

$('#formEmployee').submit(function (e) {
    e.preventDefault();

    // do ajax now
    submitData();
    $('#formEmployee').trigger("reset");


});


$('#updateEmployee').submit(function (e) {
    e.preventDefault();

    // do ajax now
    submitUpdate();
    $('#updateEmployee').trigger("reset");


});

//chart gender
$.ajax({
    url: "https://localhost:44303/API/Employees/GetRegisterData"
}).done((result) => {
    var male = result.result.filter((g) => {
        return g.gender == "Male";
    });
    var female = result.result.filter((g) => {
        return g.gender == "Female";
    });
    console.log(result);
    console.log("male");
    console.log(male);

    var options = {
        chart: {
            type: 'pie'
        },
        series: [male.length, female.length],
        labels: ['Male', 'Female']
    }

    var chart = new ApexCharts(document.querySelector("#chartGender"), options);

    chart.render();
})

//tampilan univ
function FormAdd() {
    $.ajax({
        url: "https://localhost:44357/Universities/GetAll"
    }).done((result) => {
        var option = "<option>Select:</option>";
        $.each(result, function (key, val) {
            option += `<option value="${val.universityID}">${val.name}</option>`
        });
        $("#university").html(option);

    }).fail((error) => {
        console.log(error)
    })
}


//chart Univ
$.ajax({
    url: "https://localhost:44357/Universities/GetAll"
}).done((result) => {
    console.log("result")
    console.log(result);
    var univName = new Array();
    $.each(result, function (key, val) {

        //console.log(val.name);
        univName.push(val.name);
    });
    GetChartUniv(univName);


}).fail((error) => {
    console.log("error")
    console.log(error)
})

function GetChartUniv(univName) {
    $.ajax({
        url: "https://localhost:44303/API/Employees/GetRegisterData"
    }).done((result) => {
        var employeeUniv = new Array();
        $.each(result.result, function (key, val) {
            var empUniv = val.universityName;
            employeeUniv.push(empUniv);
            var test = univName.filter((u) => {
                return u == empUniv;
            });
        });
        var univCL = new Array();
        console.log("Univ Name: " + univName);
        console.log("Employee Univ: " + employeeUniv);
        $.each(univName, function (key, val) {
            var univChart = employeeUniv.filter((u) => {
                return u == val;
            });
            console.log("test")
            console.log(univChart.length);
            univCL.push(univChart.length);

        })
        var options = {
            chart: {
                type: 'bar'
            },
            theme: {
                monochrome: {
                    enabled: true,
                    color: '#255aee',
                    shadeTo: 'light',
                    shadeIntensity: 0.65
                }
            },
            series: [{
                name: 'Universities',
                data: univCL
            }],
            xaxis: {
                categories: univName
            },
        }

        var chart = new ApexCharts(document.querySelector("#chartUniv"), options);

        chart.render();

    }).fail((error) => {
        console.log("error");
        console.log(error);
    })
}

function goBack() {
    window.history.back;
}

function LoginEmployee() {
    var obj = new Object();
    obj.Email = $("#emailEmployee").val();
    obj.Password = $("#passwordEmployee").val();
    //var objJson = JSON.stringify(obj);
    //console.log(objJson);
    $.ajax({
        url: "https://localhost:44357/Accounts/Login",
        //url: "Accounts/Login",
        type: "POST",
        //contentType: "application/json;charset=utf-8",
        data: obj

    }).done((result) => {
        if (result.status == 200) {
            Swal.fire({

                icon: 'success',
                title: 'Success',
                text: result.message,
                type: 'success'
            });
            setTimeout(function () {
                location.href = "https://localhost:44357/Employees";
            }, 3000);
        } else {
            Swal.fire({

                icon: 'error',
                title: 'Oops!',
                text: result.message,
                type: 'error'
            });
        }
    }).fail((error) => {
        Swal.fire({
            icon: 'failed',
            title: 'Failed',
            text: error.responseJSON.text,
            type: 'failed'
        });
    })
}

