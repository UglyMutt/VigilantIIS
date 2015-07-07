/// <reference path="../angular.js" />

var vigilApp = angular.module('vigilApp', []);

vigilApp.controller('WelcomeController', function ($scope) {

    $scope.message = 'You are all going to die today.';
});