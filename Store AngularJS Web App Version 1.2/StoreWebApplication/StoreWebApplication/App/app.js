

var app = angular.module('myApp', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {

 
        $routeProvider
            .when('/', {
                templateUrl:'views/home.html',
                controller:'homeController'

            })

            .when('/products', {
                templateUrl: 'views/products.html',
                controller: 'productsController'

            })

            .when('/phone', {
                templateUrl: 'views/phone.html',
                controller: 'phoneController'

            })

            .when('/tablet', {
                templateUrl: 'views/tablet.html',
                controller: 'tabletController'

            })

            .when('/deals', {
                templateUrl: 'views/deals.html',
                controller: 'dealsController'

            })

            .when('/contact', {
                templateUrl: 'views/contact.html',
                controller: 'contactController'

            })
            .when('/createPhone', {
                templateUrl: 'views/createPhone.html',
                controller: 'createPhoneController'

            })
            .when('/employees', {
                templateUrl: 'views/employees.html',
                controller: 'employeesController'

            })
            .when('/updatePhone', {
                templateUrl: 'views/updatePhone.html',
                controller: 'updatePhoneController'

            })

            .when('/phone/:id', {
                templateUrl: 'views/phoneItem.html',
                controller: 'phoneItemController'
            })

            .when('/tablet/:id', {
                templateUrl: 'views/tabletItem.html',
                controller: 'tabletItemController'
            })

            .when('/test2', {
                templateUrl: 'views/Test2.html',
                controller: 'TestCtrl'

            })
            

    }])

    .controller('mainController', function ($scope) {
      
        $scope.message = "main content";
    })