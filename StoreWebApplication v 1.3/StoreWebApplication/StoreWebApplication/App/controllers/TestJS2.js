
'use strict'

app.controller('TestCtrl', function ($scope, $http) {

$scope.message = "Test";

//var TestCtrl = function ($scope, $http) {
    alert(1)
    $scope.SendData = function (Data) {
        var GetAll = new Object();
        GetAll.FirstName = Data.firstName;
        GetAll.SecondName = Data.lastName;
        GetAll.SecondGet = new Object();
        GetAll.SecondGet.Mobile = Data.mobile;
        GetAll.SecondGet.EmailId = Data.email;
        $http({
            //url: 'http://localhost:60102/NewRoute/firstCall',
            url: "../json/products.js",
            dataType: 'json',
            method: 'POST',
            data: GetAll,
            headers: {
                "Content-Type": "application/json"
            }
        }).success(function (response) {
            $scope.value = response;
            alert(response);
        })
            .error(function (error) {
                alert(error);
            });
    }
});  