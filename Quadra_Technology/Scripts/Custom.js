$(document).ready(function () {
    callApi(0);
});
var tablename = '#example';
$('.nav-list>li>a').click(
    function () {
        // var table = $(tablename).DataTable();
        let id = $(this).attr('id');
        $('#department_tempid').val(id);
        callApi(id)
    });
function callApi(id) {
    $(tablename).DataTable().destroy();
    var _baseUrl = window.location.host;
    //console.log(new Date(648147600000));
    var table = $(tablename).DataTable({
        "ajax": ({
            dataType: "json",
            url: "/Home/getStaff/" + id,
            type: 'POST'
        }),
        // "ajax": "Content/objects.txt",
        "columns": [
            { "data": "department" },
            { "data": "name" },
            {
                "data": "birthdate", render: function (data, type, row) {
                    if (data != null) {
                        return convertDate(data)
                    }
                    return '-'
                }

            },
            { "data": "address" },
            { "data": "phone" },
            { "data": "email" },
            { "data": "position" },
            {
                "data": "workingday", render: function (data, type, row) {
                    if (data != null) {
                        return convertDate(data)
                    }
                    return '-'
                }
            },
            {
                "data": "guid", render: function (data, type, row) {
                    return '<button onclick="addStaff()" class="addstaff fa fa-plus-circle btn btn-success" > </button>  <button id=' + data + ' onclick="Editdata($(this).attr(\'id\'))" class="edit fa fa-pencil btn btn-primary" aria-hidden="true"></button>  <button  id=' + data + ' class="del fa fa-trash-o btn btn-danger" aria-hidden="true" onclick="Deldata($(this).attr(\'id\'))"></button>'
                }
            }
        ]
    });
    return table;
}
//<i class="fa fa-plus-circle" aria-hidden="true"></i>
function convertDate(data) {

    data = data.substring(6, (data.length - 2));
    var d = new Date(parseInt(data)),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}
function addStaff() {
    let url = 'home/addstaff/';
    var strWindowFeatures = "width=1024,height=800,top=30,left=400";
    window.open(url, "popupWindow", strWindowFeatures);
}
function Editdata(id) {
    //window.open('home/EditForm/' + id, '_blank').focus();
    let url = 'home/EditForm/' + id;
    var strWindowFeatures = "width=1024,height=800,top=30,left=400";
    window.open(url, "popupWindow", strWindowFeatures);
}

function Deldata(id) {

    if (confirm('ต้องการลบข้อมูล ??')) {
        $.ajax({
            type: "POST",
            url: "/Home/Deldata?id=" + id,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (response) {
                console.log(response);
                let callid = $('#department_tempid').val();
                if (callid == '') { callid = 0 }
                callApi(callid)
            },
            failure: function (response) {
                alert("Failure " + response);
            },
            error: function (response) {
                cosole.log(response);
                //alert("Error : " + response);
            }
        });
    } else {
        return;
    }

}
