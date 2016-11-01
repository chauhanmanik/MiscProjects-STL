(function () {
    "use strict";

    angular.module("appStl")
        .controller("DoubleBookingEntryCtrl", DoubleBookingEntryCtrl);

    function DoubleBookingEntryCtrl($http) {
        var vm = this;
        vm.data = [];

        vm.testMessage = "";

        vm.currentDate = new Date();
        vm.calOpen = function () {
            vm.calPopup.opened = true;
        };

        vm.calPopup = { opened: false };
        vm.calOpen = function () { vm.calPopup.opened = true; };

        //Get data from Api
        $http.get("/api/DoubleBookingEntry")
             .then(function (response) {
                 //Success
                 angular.copy(response.data, vm.data);
             }), function () {
                 vm.alertMessage = "Failed during data retrieval!";
             };

        //Post
        vm.submitForm = function () {

            vm.data.result.selectedAuthority = vm.data.selectedAuthority;
            vm.data.result.selectedSearchClerk = vm.data.selectedSearchClerk;
            vm.data.result.bookingDate = vm.currentDate;
            
            $http.post("/api/DoubleBookingEntry", vm.data.result)
                        .then(function (response) {
                            //Success
                            vm.alertMessage = "Data saved!";
                            vm.data.selectedAuthority = {};
                            vm.data.selectedSearchClerk = {};
                            vm.data.bookingDate = new Date();
                        }), function () {
                            //failure
                            vm.alertMessage = "Failed during saving the data!";
                        };
        };
    }
})();