function GetMyCarpools() {
    CallAPI("/api/carpool/getmine", "GET", null, function (e) {
        if (e.length > 0)
        LoadPartialView("/CarPool/_MyCarPools", "POST", e, "#MyCarpools", function (e) {
        });
    });
}

function JoinCarPool(id) {
    CallAPI("/api/carpool/join/" + id, "GET", null, function (e) {
        alert(e);
        reloadView();
        $('#myModal').modal('toggle');
    });
}

function LeaveCarPool(id) {
    CallAPI("/api/carpool/leave/" + id, "GET", null, function (e) {
        alert(e);
        reloadView();
    });
}

function reloadView() {
    $("#MyCarpools").empty();
    DoGrid();
    GetMyCarpools();
}

var gridActive = false;
function DoGrid() {
    if (!gridActive) {
        gridActive = true;
        $("#Grid").jqGrid({
            ajaxGridOptions: { type: "GET" },
            serializeGridData: function (postdata) {
                var filters = $("#filters").serializeObject();
                //for (var k in filters) {
                //    if (filters.hasOwnProperty(k)) {
                //        if (filters[k] == "")
                //            filters[k] = $("[name='" + k + "']").data("default");
                //    }
                //}
                $.extend(postdata, filters);
                return postdata;
            },
            url: '/api/carpool/get/',
            datatype: 'json',
            jsonReader: {
                root: "data",
                total: "total",
                page: "page",
                records: "records",
                repeatitems: false,
                id: "UserID",
            },
            colNames: ['Departure Time', 'Expected Arrival Time', 'Leaving From', 'Destination', 'Seats', 'Days'],
            colModel: [
                { name: 'departureTime', index: 'departureTime', width: 100, align: 'center', formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: 'H:i:s' } },
                { name: 'expectedArrivalTime', index: 'expectedArrivalTime', width: 100, align: 'center', formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: 'H:i:s' } },
                { name: 'origin', index: 'origin', width: 250 },
                { name: 'destination', index: 'destination', width: 250 },
                { name: 'seatsAvailable', index: 'seatsAvailable', width: 60, formatter: 'integer', align: 'center' },
                { name: 'daysAvaiable', index: 'daysAvaiable', width: 380 }
            ],
            rowNum: 20,
            width: null,
            autowidth: false,
            shrinkToFit: false,
            height: "40em",
            rowList: [20, 30, 40, 50, 60, 70, 80],
            pager: '#Pager',
            sortname: 'departureTime',
            sortorder: "asc",
            caption: "",
            onSelectRow: function (ids) {
                CarPoolModal(ids);
            }
        });

    } else {
        $('#Grid').trigger('reloadGrid');
    }
}