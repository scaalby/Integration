var App = angular.module("App", ['ngRoute', 'xeditable']);

App.run(function (editableOptions) {
    editableOptions.theme = 'bs3';
});

App.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/Index', {
            templateUrl: 'partials/Index.html'
        })
        .when('/CorporationsSearch', {
            templateUrl: 'partials/CorporationsSearch.html',
            controller: 'corporationsCtrl'
        })
        .when('/AddCorporation', {
            templateUrl: 'partials/AddCorporation.html',
            controller: 'addCtrl'
        })
        .when('/RealEstatesSearch/:corporationId', {
            templateUrl: 'partials/RealEstatesSearch.html',
            controller: 'RealEstateSearchCtrl'
        })
        .otherwise({
            redirectTo: '/Index'
        });

    $locationProvider.html5Mode(true);
});

App.controller('RealEstateSearchCtrl', function ($scope, $http, $routeParams) {
    $scope.params = {
        code: $scope.Code,
        state: $scope.State,
        city: $scope.City,
        street: $scope.Street,
        zip: $scope.Zip
    }

    function reset() {
        $scope.params = {};
    };

    $scope.searchRE = function () {

        var config = {
            params: $scope.params
        }

        $http.get("http://192.168.3.68:59536/Example/GetRealEstates/" + $routeParams.corporationId, config)
            .success(function (data) {
                if (data == "null") {
                    $scope.realEstates = {
                        name: '',
                        nation: ''
                    };
                    alert("No results found");
                } else {
                    $scope.realEstates = data;
                }
            })
            .error(function () {
                alert("Error! Retry");
            });
        reset();
    };
});

App.controller("corporationsCtrl", function ($scope, $http) {
    $scope.params = {
        name: $scope.Name,
        nation: $scope.Nation
    }
    $scope.searchCorp = function () {
        var config = {
            params: $scope.params
        }

        $http.get("http://192.168.3.68:59536/Example/GetCorporations", config)
            .success(function (data) {
                $scope.corporations = data;
            })
            .error(function () {
                alert("Error! Retry");
            })
    };

    $scope.deleteCorporation = function (corpId, index) {
        $http.delete("http://192.168.3.68:59536/Example/DeleteCorporation/" + corpId)
            .success(function () {
                $scope.corporations.splice(index, 1);
                alert("Corporation deleted correctly!");
            })
            .error(function () {
                alert("Error! Retry");
            })
    }

    $scope.editCorporation = function (corp) {
        var params = {
            name: corp.Name,
            nation: corp.Nation
        }
        $http.put("http://192.168.3.68:59536/Example/UpdateCorporation/" + corp.Id, params)
            .success(function () {
                alert("Corporation edited correctly!");
            })
            .error(function () {
                alert("Error! Retry");
            })
    }
});

App.controller("addCtrl", function ($scope, $http) {
    $scope.params = {
        name: $scope.Name,
        nation: $scope.Nation
    };

    function reset() {
        $scope.params = {};
    };

    $scope.add = function (formId) {
        $http.post("http://192.168.3.68:59536/Example/AddCorporation", $scope.params)
            .success(function (data) {
                $scope.corporations = data;
                alert("Corporation added correctly!");
            })
            .error(function () {
                alert("Error! Retry");
            })
        reset();
    };
});
