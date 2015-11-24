$(document).ready(function () { /*code here*/
    
    $.post('GetCreatedCarPools', {}, function (data) {
        // TODO: Navigate away...
        var message = data.Message;

        var parsedData = eval("(" + JSON.stringify(data)+ ")");
        //alert(JSON.stringify(data) + " " + parsedData[0]["Id"]);

        if (data != null) {
            loaddata(parsedData);
        } else {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text(message);
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 10000);


        }

    }).fail(function (data, status, err) {
        var $alert = $("#error");
        var $p = $alert.find("p");
        $p.text('No Created CarPools');
        $alert.removeClass('hidden');

        /*setTimeout(function () {
            $p.text('');
            $alert.addClass('hidden');
        }, 10000);*/

    });

    function loaddata(data) {

        var useremail = $("#myDiv").data('value');

        //alert(" sess " + useremail);

        //alert("in load");
        for (i = 0; i < data.length; i++) {
            //data[0]["Id"]

            var div1 = document.createElement("div");
            div1.setAttribute("class","carpool-entry row row-margin");

            var div2 = document.createElement("div");
            div2.setAttribute("class","carpool-owner");

            var h4 = document.createElement("h4");
            h4.innerHTML = data[i]["Owner"];

            var h5 = document.createElement("h5");
            //put names of members in the carpool here
            if (data[i]["Members"] != null) {
                var members = data[i]["Members"].split(",");
                for (h = 0; h < members.length; h++) {
                    h5.innerHTML = h5.innerHTML + "<br>" + members[h];
                }
            }

            div2.appendChild(h4);
            div2.appendChild(h5);

            div1.appendChild(div2);
            //alert("in load 1");

            // *******************************
            var div3 = document.createElement("div");
            div3.setAttribute("class","carpool-detail"); 

            var h4 = document.createElement("h4");
            h4.innerHTML = data[i]["Origin"]+" "+data[i]["Departure"];

            div3.appendChild(h4);

            div1.appendChild(div3);

            //alert("in load 2");
            // *******************************
            var div4 = document.createElement("div");
            div4.setAttribute("class","carpool-detail"); 

            var h4 = document.createElement("h4");
            h4.innerHTML = data[i]["Destination"]+" "+data[i]["Arrival"];

            div4.appendChild(h4);

            div1.appendChild(div4);

            //alert("in load 3");
            // *******************************
            var div5 = document.createElement("div");
            div5.setAttribute("class","carpool-detail"); 

            var h4 = document.createElement("h4");
            h4.innerHTML = data[i]["Days"];

            div5.appendChild(h4);

            div1.appendChild(div5);

            //alert("in load 4");

            // *******************************
            var div6 = document.createElement("div");
            div6.setAttribute("class","carpool-join"); 

            if (data[i]["Members"] != null) {
                var remaining = parseInt(data[i]["Seats"]) - data[i]["Members"].split(",").length;
            } else {
                remaining = parseInt(data[i]["Seats"]);
            }
           
            var h4 = document.createElement("label");
            if (remaining == 0){
                h4.setAttribute("class","btn btn-primary remaining-seats-none");
            }else{
                h4.setAttribute("class","btn btn-primary remaining-seats");
            }
            h4.innerHTML = remaining + " Remaining";

            div6.appendChild(h4);

            //alert("in load 5");
            // ------------
            var button = document.createElement("button");
            button.setAttribute("class","created-carpool btn btn-primary"); 
            button.setAttribute("type", "button");
            button.innerHTML = "Created";
            
            //alert("in load 6");
            div6.appendChild(button);
           
            div1.appendChild(div6);
            
            //alert("in load 7");
            //lastly add the div to the page
            document.getElementById("createdcarpools").appendChild(div1);
   
        }
    }

    function joined(members, member) {
        var j = false;
        for (k = 0; k < members.length; k++) {
            alert(member+" "+members[i]);
            if (member == members[k]) {
                j = true;
            }
        }
        
        return j;
    }

});