


app.service('employeeService', function ($http) {


    this.GetEmployeesList = function () {

        return $http({

            method: "Get",
            url: "/GetData.aspx?q=1",
            headers: { 'Content-Type': 'application/json' },

        }).success(function (data) {

            //alert('success'+ JSON.stringify(data));

        }).error(function (data) {

            alert('Failed'+JSON.stringify(data));


        })
    }





})