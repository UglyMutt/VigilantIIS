/// <reference path="../angular.js" />
"use strict";
var testdata;
var vigilApp = angular.module('vigilApp', []);

vigilApp.service('RequestsService', function ($q, $http, $rootScope, $timeout) {
    var svc = this;
    svc.GetRequests = function() {
        $http.get("api/Requests").success(function (data, status, headers, config) {
            return data;
        }).error(function (data, status, headers, config) {
            alert("there was an error requesting data from server: " + status);
            return {};
        });
    };
});

vigilApp.controller('WelcomeController', ["RequestsService", function ($scope, RequestsService) {

    $scope.message = 'You are all going to die today.';
    $scope.requests = RequestsService.GetRequests();
    testdata = RequestsService.GetRequests();
}]);



