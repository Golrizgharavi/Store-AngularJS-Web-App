

//$scope.id = $routeParams.id;
'use strict'

app.controller('tabletItemController', function ($routeParams, productService) {


    var vm = this;
    var MemoryItems = document.getElementsByClassName("MemItemInn");
    var ColorItems = document.getElementsByClassName("ColorItemImg");
    var ItemSliderContainer = document.getElementById("ItemSliderContainer");
    var PriceContainer = document.getElementById("PriceContainer");



    //alert($routeParams.id);

    var promiseGet = productService.GetPhoneItembyID($routeParams.id);


    promiseGet.then(function (p1) {
        vm.tabletItem = p1.data

        vm.message = "Welcome to " + vm.tabletItem.Name;

    },

        function (errorP1) { alert('Failure Loading!' + errorP1) });


    vm.SetSlider = function (obj) {
        var template = '<div class="mySlidesHolder">';

        template += (vm.tabletItem.Sale === 'True') ? '<span class="CircleOffer">SPECIAL OFFER</span>' : '';
        template += '<img class="mySlides img-responsive" src="' + obj.Img1 + '" style="width:350px">' +
            '<img class="mySlides img-responsive" src="' + obj.Img2 + '" style="width:350px">' +
            '<img class="mySlides img-responsive" src="' + obj.Img3 + '" style="width:350px">' +
            ' </div>' +
            '<ul class="ImgSelectionHolder">' +
            '<li class="ImgSelection col-xs-4"><img class="img-responsive ImgItem Selected" src="' + obj.Img1 + '" onclick="myImageSlider.MoveSlide(1)"></li>' +
            '<li class="ImgSelection col-xs-4"><img class="img-responsive ImgItem" src="' + obj.Img2 + '" onclick="myImageSlider.MoveSlide(2)"></li>' +
            '<li class="ImgSelection col-xs-4"><img class="img-responsive ImgItem" src="' + obj.Img3 + '" onclick="myImageSlider.MoveSlide(3)"></li>' +
            '</ul>';

        ItemSliderContainer.innerHTML = template;

        myImageSlider.ShowSlide(1);

    },
        vm.SetSelection = function (obj, index) {

            for (i = 0; i < obj.length; i++) {
                obj[i].className = obj[i].className.replace(" ItemSelected", "");
            }

            obj[index - 1].className += " ItemSelected";
        },
        vm.GetPhoneColor = function (CId, index) {
            // alert(Id + ' ' + vm.phoneItem.Id+' ' + index);
            vm.SetSelection(ColorItems, index);

            var GetPhoneColorResult = productService.GetPhoneFeatureInfo(CId, vm.tabletItem.Id, 'color',2);
        GetPhoneColorResult.then(function (p1) {

            vm.PhoneColor = p1.data;

            vm.SetSlider(vm.PhoneColor);
            //alert(JSON.stringify(p1.data));
            },

                function (errorP1) { alert('Failure Loading!' + errorP1) });

        },

        vm.GetPhoneMemory = function (MId, index) {

            vm.SetSelection(MemoryItems, index);
            //alert(MId)
            var GetPhoneColorResult = productService.GetPhoneFeatureInfo(MId, vm.tabletItem.Id, 'memo');
            GetPhoneColorResult.then(function (p1) {

                vm.TabletMemo = p1.data;
              
                var temp = '';

                if (vm.TabletMemo.Sale === 'True') {
                    temp += '<span class="ItemDetail ItemPrice"><h2><del>Price: $' + vm.TabletMemo.Price + '</del></h2></span>' +
                        '<span class="ItemDetail ItemPrice"><h2 class="RedSkin">Promotion: $' + vm.TabletMemo.DPrice + '</h2></span>'

                }
                else {
                    temp += '<span class="ItemDetail ItemPrice"><h2>Price: $' + vm.TabletMemo.Price + '</h2></span>';

                }
                PriceContainer.innerHTML = temp;
            },

                function (errorP1) { alert('Failure Loading!' + errorP1) });

        }

});


