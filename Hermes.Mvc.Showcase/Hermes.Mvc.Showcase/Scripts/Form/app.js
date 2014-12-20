var app = angular.module('formApp', []);


app.controller('formController', function($scope) {
    $scope.title = "";
    $scope.firstname = "Daniel Luke This name is plainly too long";
    $scope.surname = "Le Pelley";
    $scope.age = 38;
});

app.controller('exchangeController', function ($scope, dataService) {
    $scope.count = 0;
    $scope.rate = null;

    var update = function () {
        dataService.getData(function (dataResponse) {
            $scope.rate = dataResponse[1].Rate;
            $scope.count += 1;
        });
    };

    setInterval(update, 5000);
});


app.factory('dataService', function ($http) {
    return {
        getData: function (callbackFunc) {
            $http({
                method: 'GET',
                url: 'https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(%22EURUSD%22%2C%22GBPUSD%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback='
            }).success(function (data) {
                callbackFunc(data.query.results.rate);

            }).error(function () {
                alert("error");
            });
        }
    };
});