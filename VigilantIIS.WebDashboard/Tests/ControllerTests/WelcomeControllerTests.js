/// <reference path="C:\Projects\GitHub\VigilantIIS\VigilantIIS.WebDashboard\Scripts/jasmine-2.3.4/jasmine.js" />
/// <reference path="C:\Projects\GitHub\VigilantIIS\VigilantIIS.WebDashboard\Scripts/angular.js" />
/// <reference path="C:\Projects\GitHub\VigilantIIS\VigilantIIS.WebDashboard\Scripts/angular-mocks.js" />
/// <reference path="C:\Projects\GitHub\VigilantIIS\VigilantIIS.WebDashboard\Scripts/app/app.js" />

describe("WelcomeController", function () {


    var $scope;

    // load our angular app
    beforeEach(module('vigilApp'));

    // load angular dependencies
    beforeEach(inject(function ($rootScope, $controller) {
        $scope = $rootScope.$new();
        $controller('WelcomeController', { $scope: $scope });
    }));

    describe("Home page", function () {
        it("Displays welcome message", function () {

            // TODO: Add controller test here
            expect($scope.message).toBe('You are all going to die today.');
        });
    });
});