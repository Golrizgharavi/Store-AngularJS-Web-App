
'use strict'

app.controller('phoneController', function (productService) {

    var vm = this;
    vm.message = "Welcome to Phone Page";
    var $TemplateHolder = document.getElementById("PhoneListContainerDiv");
    var $ObjTemplateHolder = $('#PhoneListContainerDiv');


    var promiseGet = productService.GetPhoneListByType(1);

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
                    '<a href="#/phone/' + myObjs[i].Id + '" class="post-content" target="_blank">' +
                    '<p class="post-Descripton">' + myObjs[i].Summery

                $myTemplate += (myObjs[i].Sale == 'True') ? '<span class="ColorBold">On Sale </span>' : '';
                $myTemplate += '</p ></a >';

                $myTemplate += (myObjs[i].Sale === 'True') ? '<span class="CircleOffer">SPECIAL OFFER</span>' : '' ;

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
                    '<a href="#/phone/' + myObjs[i].Id + '" class="post-content" target="_blank">' +
                    '<p class="post-Descripton">' + myObjs[i].Summery;

                $myTemplate += (myObjs[i].Sale == 'True') ? '<span class="ColorBold">On Sale </span>' : '';
                //$myTemplate += (vm.phones[i].Available == 'True') ? '| Available' : '';
                $myTemplate += '</p ></a >';

                $myTemplate += (myObjs[i].Sale === 'True') ? '<span class="CircleOffer">SPECIAL OFFER</span>' : '';

                $myTemplate += '</div ></div >';

            }
        }


        //$TemplateHolder.innerHTML = $myTemplate;
       
        $ObjTemplateHolder.animate({ 'opacity': 0 }, 400, function () {
            $(this).html($myTemplate).animate({ 'opacity': 1 }, 400);
        });
    };


    promiseGet.then(function (p1) {
        vm.phones = p1.data;  
        //alert(JSON.stringify(vm.phones.Brds));
        vm.SetTemplate(vm.phones.DataArr);

    },

        function (errorP1) { alert('Failure Loading!' + errorP1) });



    vm.SearchPhoneByFilter = function () {

        if (vm.minPrice > vm.maxPrice) {
            alert("Minimum price should be less than maximum price! :)");

        } else {
            //alert(JSON.stringify(vm.Brd));
            var GetSearchResult = productService.GetPhonesByFilter(vm.Name, 1, vm.OS, vm.Brd, vm.minPrice, vm.maxPrice);
            GetSearchResult.then(function (p1) {

                vm.filteredPhones = p1.data;
                if (vm.filteredPhones.length == 0) 

                    $ObjTemplateHolder.animate({ 'opacity': 0 }, 400, function () {
                        $(this).html('<div class="alertMsg">OpPsSs!! There is no match! :)</div>').animate({ 'opacity': 1 }, 400);
                    });
                else
                 vm.SetTemplate(vm.filteredPhones);

            },

                function (errorP1) { alert('Failure Loading!' + errorP1) });
        }
        
    };

    vm.resetForm = function () {
       // vm.FrmPhoneSearchFilter.$setPristine();
        vm.Name = '';
        vm.OS = false;
        vm.Brd = false;
        vm.minPrice = '';
        vm.maxPrice = '';
        vm.SetTemplate(vm.phones.DataArr);
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





