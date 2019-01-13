

'use strict';

app.filter("jsDate", function () {

    return function (x) {
        if (x !== null)
            return new Date(parseInt(x.substr(6)));
        else
            return '';
    };


});

app.controller('employeesController', function (employeeService) {


    var vm = this;
    vm.message = "Micreez Team";

    var promiseGet = employeeService.GetEmployeesList();

    promiseGet.then(function (p1) {
        vm.employees = p1.data;
        var $TemplateHolder = document.getElementById("TemplateHolderDiv");
        var $myTemplate = '<div class="row TeamInfoHolder" >';

        //var $myRows = Math.ceil(vm.employees.length / 3);

        for (var i = 0; i < vm.employees.length; i++) {

            if (i % 4 != 0) {
               // $myTemplate += '<span class="col-sx-4">' + vm.employees[i].FirstName + '</span>';
                $myTemplate += '<div class="col-sm-3"><div class="post-item">' +
                    '<div class="post-featured-image">' +
                    '<img width="280" height="289" class="img-responsive imgCenter" src="' + vm.employees[i].ImageUrl +
                    '" class="wp-post-image" alt="">' +
                    '<div class="team-name-position">' +
                    '<p class="team-name">' + vm.employees[i].FirstName + vm.employees[i].LastName + '</p>' +
                    '<p class="team-position">' + vm.employees[i].Tittle +
                    '</p>' +
                    '</div>' +
                    '</div>' +
                    '<a href="' + vm.employees[i].Id +'" class="post-content">' +
                    '<p class="post-Descripton">' +
                    vm.employees[i].Description + '<br />';
                if (vm.employees[i].HiringDate != null) 
                    $myTemplate +=  Date(parseInt(vm.employees[i].HiringDate.substr(6)));

                else $myTemplate += '';
                $myTemplate += '<br />' + vm.employees[i].PhoneNumber +'</p ></a ></div ></div >'
         

            }
            else {
                $myTemplate += '</div>';
                $myTemplate += '<div class="row TeamInfoHolder" >';
                //$myTemplate += '<span class="col-sx-4">' + vm.employees[i].FirstName + '</span>';
                $myTemplate += '<div class="col-sm-3"><div class="post-item">' +
                    '<div class="post-featured-image">' +
                    '<img width="280" height="289" class="img-responsive imgCenter" src="' + vm.employees[i].ImageUrl +
                    '" class="wp-post-image" alt="">' +
                    '<div class="team-name-position">' +
                    '<p class="team-name">' + vm.employees[i].FirstName + vm.employees[i].LastName + '</p>' +
                    '<p class="team-position">' + vm.employees[i].Tittle +
                    '</p>' +
                    '</div>' +
                    '</div>' +
                    '<a href="'+ vm.employees[i].Id +'" class="post-content">' +
                    '<p class="post-Descripton">' +
                    vm.employees[i].Description + '<br />';
                if (vm.employees[i].HiringDate != null)
                    $myTemplate += Date(parseInt(vm.employees[i].HiringDate.substr(6)))
                else $myTemplate += '';
                $myTemplate += '<br />' + vm.employees[i].PhoneNumber + '</p ></a ></div ></div >'
            }
        }

       
        $TemplateHolder.innerHTML = $myTemplate;

    },

        function (errorP1) { alert('Failure Loading!' + errorP1) })





});


