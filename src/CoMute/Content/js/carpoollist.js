(function (root, $) {
    
    var carpools_created = [
        { 
            departure: new Date(), 
            arrival: new Date(), 
            origin: "Harare", 
            destination: "JBurg", 
            days: ["Monday", "Tuesday", "Friday"] ,
            notes: "some notes",
            seats: ["BL", "BM"]
        },
        { 
            departure: new Date(), 
            arrival: new Date(), 
            origin: "Harare", 
            destination: "JBurg", 
            days: ["Monday", "Tuesday", "Friday"] ,
            notes: "some notes",
            seats: ["BL", "BM"]
        },
        { 
            departure: new Date(), 
            arrival: new Date(), 
            origin: "Harare", 
            destination: "JBurg", 
            days: ["Monday", "Tuesday", "Friday"] ,
            notes: "some notes",
            seats: ["BL", "BM"]
        }];
    
    function td(val){
        return "<th>" + val + "</th>"
    };
    
	
    var carpool_table = "";
    carpools_created.forEach(function(carpool){
        carpool_table = carpool_table + "<tr>";
        carpool_table = carpool_table + td(carpool.origin);
        carpool_table = carpool_table + td(carpool.destination);
        carpool_table = carpool_table + td(carpool.departure);
        carpool_table = carpool_table + td(carpool.arrival);
        carpool_table = carpool_table + td(carpool.days);
        carpool_table = carpool_table + td(carpool.notes);
        carpool_table = carpool_table + td(carpool.seats);
        carpool_table = carpool_table + "</tr>";
    });
    $('#car-pools-created').html(carpool_table);
})(window, jQuery);