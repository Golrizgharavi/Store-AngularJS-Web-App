'use strict'

app.controller('tabletController', function (productService) {

    var vm = this;
    vm.message = "Welcome to Tablet Page";
    var $TemplateHolder = document.getElementById("ListContainerDiv");
    var $ObjTemplateHolder = $('#ListContainerDiv');


    var promiseGet = productService.GetPhoneListByType(2);

    vm.SetTemplate = function (myObjs) {
        var $myTemplate = "";
        for (var i = 0; i < myObjs.length; i++) {

            if (i % 4 != 0) {
                $myTemplate += '<div class="col-sm-3"><div class="post-item">' +
                    '<div class="post-featured-image">' +
                    '<img width="289" height="289" class="img-responsive imgCenter" src="' + myObjs[i].ImgUrl +
                    '" class="wp-post-image" alt="">' +
                    '<div class="Item-detail">' +
                    '<p class="Item-name">' + myObjs[i].Name + '</p>' +
                    '<p class="Item-price">$' + myObjs[i].Price + ' | ' + myObjs[i].Brand +
                    '</p>' +
                    '</div>' +
                    '</div>' +
                    '<a href="#/tablet/' + myObjs[i].Id + '" class="post-content" target="_blank">' +
                    '<p class="post-Descripton">' + myObjs[i].Summery

                $myTemplate += (myObjs[i].Sale == 'True') ? '<span class="ColorBold">On Sale </span>' : '';
                $myTemplate += '</p ></a >';

                $myTemplate += (myObjs[i].Sale === 'True') ? '<span class="CircleOffer">SPECIAL OFFER</span>' : '';

                $myTemplate += '</div ></div >';


            }
            else {
                $myTemplate += '</div>';
                $myTemplate += '<div class="row ItemInfoHolder" >';

                $myTemplate += '<div class="col-sm-3"><div class="post-item">' +
                    '<div class="post-featured-image">' +
                    '<img width="289" height="289" class="img-responsive imgCenter" src="' + myObjs[i].ImgUrl +
                    '" class="wp-post-image" alt="">' +
                    '<div class="Item-detail">' +
                    '<p class="Item-name">' + myObjs[i].Name + '</p>' +
                    '<p class="Item-price">$' + myObjs[i].Price + ' | ' + myObjs[i].Brand +
                    '</p>' +
                    '</div>' +
                    '</div>' +
                    '<a href="#/tablet/' + myObjs[i].Id + '" class="post-content" target="_blank">' +
                    '<p class="post-Descripton">' + myObjs[i].Summery;

                $myTemplate += (myObjs[i].Sale == 'True') ? '<span class="ColorBold">On Sale </span>' : '';
                $myTemplate += '</p ></a >';

                $myTemplate += (myObjs[i].Sale === 'True') ? '<span class="CircleOffer">SPECIAL OFFER</span>' : '';

                $myTemplate += '</div ></div >';

            }
        }

        
        $ObjTemplateHolder.animate({ 'opacity': 0 }, 400, function () {
            $(this).html($myTemplate).animate({ 'opacity': 1 }, 400);
        });
    };


    promiseGet.then(function (p1) {
        vm.tablets = p1.data;
        //alert(JSON.stringify(vm.tablets.Brds));
        vm.SetTemplate(vm.tablets.DataArr);

    },

        function (errorP1) { alert('Failure Loading!' + errorP1) });



    vm.SearchPhoneByFilter = function () {

        if (vm.minPrice > vm.maxPrice) {
            alert("Minimum price should be less than maximum price! :)");

        } else {
            //alert(JSON.stringify(vm.Brd));
            var GetSearchResult = productService.GetPhonesByFilter(vm.Name, 2, vm.OS, vm.Brd, vm.minPrice, vm.maxPrice);
            GetSearchResult.then(function (p1) {
               // alert(JSON.stringify(p1.data));
                vm.filteredTablets= p1.data;
                if (vm.filteredTablets.length == 0)

                    $ObjTemplateHolder.animate({ 'opacity': 0 }, 400, function () {
                        $(this).html('<div class="alertMsg">OpPsSs!! There is no match! :)</div>').animate({ 'opacity': 1 }, 400);
                    });
                else
                    vm.SetTemplate(vm.filteredTablets);

            },

                function (errorP1) { alert('Failure Loading!' + errorP1) });
        }

    };

    vm.resetForm = function () {
  
        vm.Name = '';
        vm.OS = false;
        vm.Brd = false;
        vm.minPrice = '';
        vm.maxPrice = '';
        vm.SetTemplate(vm.tablets.DataArr);
    };

});



app.filter("jsDate", function () {

    return function (x) {
        if (x !== null)
            return new Date(parseInt(x.substr(6)));
        else
            return '';
    };


});