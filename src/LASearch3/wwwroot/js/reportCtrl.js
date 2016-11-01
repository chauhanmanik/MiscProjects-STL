(function () {
    "use strict";

    angular.module("appStl")
          .controller("ReportCtrl", ReportCtrl);


    function ReportCtrl($http, $filter) {
        var vm = this;
        vm.data = [];
        vm.bookingDate = new Date();

        //Calender
        vm.calOpen = function () {
            vm.calPopup.opened = true;
        };
        vm.calPopup = { opened: false };
        vm.calOpen = function () { vm.calPopup.opened = true; };

        //Get data by month
        vm.filterData = function () {
            $http({
                url: "/api/DoubleBookingReport",
                method: 'get',
                params: { param1: vm.bookingDate.toDateString() }
            }).then(function (response) {
                //Success
                angular.copy(response.data, vm.data);
            }), function () {
                //Fail
                vm.alertMessage = "Failed during data retrieval!";
            };
        };

        //Get data from Api
        $http.get("/api/DoubleBookingReport")
             .then(function (response) {
                 //Success
                 angular.copy(response.data, vm.data);
             }), function () {
                 //Fail
                 vm.alertMessage = "Failed during data retrieval!";
             };
    };

})();
