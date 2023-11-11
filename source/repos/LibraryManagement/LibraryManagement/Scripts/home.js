var myapp = angular.module('homeApp', ['ngRoute','ui.bootstrap'])
    .controller("homeController", ["$scope", '$http', '$interval', 'filterFilter', function ($scope, $http, $interval, $filterFilter) {

        $scope.country = '';

        $scope.SelectedCountry = function (country) {
            console.log("Country " + country);
        }

        $scope.Calculate = function () {
            var fromdate = $('#fromDate').val();
            var todate = $('#toDate').val();

            $.ajax({
                url: "../Home/CaluatePenalty",
                type: "POST",
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                data: { fromDate: fromdate, toDate: todate, Country: $scope.country },
                success: function (response) {
                    console.log(response);
                    alert(response);
                },
                error: function (err) {

                }
            });
        }

    }]);