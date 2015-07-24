/// <reference path="../angular.js" />
"use strict";
var testdata;
var vigilApp = angular.module('vigilApp', ['ui.bootstrap']);

vigilApp.service('RequestsService', function ($q, $http, $rootScope, $timeout) {

    var svc = this;

    svc.GetRequests = function () {

        var promise = $http.get("api/Requests").success(function (data, status, headers, config) {
            return data;
        }).error(function (data, status, headers, config) {
            alert("there was an error requesting data from server: " + status);
            return {};
        });

        return promise;
    };

    svc.DeleteRequest = function (request) {

        var promise = $http.delete('api/Requests/' + request._id).success(function (result) {
            return true;
        }).error(function(data)
        {
            return false;
        });

        return promise;
    };

});

vigilApp.controller('WelcomeController', ['$scope', 'RequestsService', '$modal', function ($scope, RequestsService, $modal) {

        $scope.message = 'Hey Buddy!';
        $scope.requestSort = 'siteName';
        $scope.reverseSort = false;
        

        RequestsService.GetRequests().then(function (response) {
            $scope.requests = response.data;
        });

        $scope.sort = function (request) {

            if ($scope.orderProp == 'requestedOn') {
                return new Date(request.requestedOn);
            }
            return request[$scope.orderProp];
        }

        $scope.openForEdit = function (request) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'requestEditModal.html',
                controller: 'requestDetailsModalController',
                size: 'medium',
                resolve: {
                    request: function () {
                        return request;
                    }
                }

            });

            modalInstance.result.then(function (selectedItem) {
                //success: do nothing, model already changed
            }, function (originalRequest) {
                $scope.requests[$scope.requests.indexOf(request)] = originalRequest;
            });
        };

        $scope.delete = function (request) {

            RequestsService.DeleteRequest(request).then(function (success) {
                if (success) {
                    $scope.requests.splice($scope.requests.indexOf(request), 1);
                }
            });
        };

}]);

vigilApp.controller('requestDetailsModalController', function ($scope, $modalInstance, $http, request) {

    $scope.originalRequest = angular.copy(request);
    $scope.editRequest = request;

    $scope.save = function () {
        //todo: save thru service
        $modalInstance.close();
    };

    $scope.cancel = function () {

        $modalInstance.dismiss($scope.originalRequest);
    };
});



