



app.service('productService', function ($http) {

    this.GetPhoneListByType = function (PrType) {
        
        return $http({

            method: "Get",
            url: "/GetData.aspx?q=2&TP=" + PrType, //get all products PrType==null
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            data = eval('(' + JSON.stringify(data) + ')');
            //alert(data);
            //alert('success' + JSON.stringify(data))
        }).error(function (data) {

            alert('failed' + JSON.stringify(data));

        });
    };

    this.GetHomePage = function (PrType) {

        return $http({

            method: "Get",
            url: "/GetData.aspx?q=6&TP=" + PrType, //get all products PrType==null
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            data = eval('(' + JSON.stringify(data) + ')');
            //alert(data);
            //alert('success' + JSON.stringify(data))
        }).error(function (data) {

            alert('failed' + JSON.stringify(data));

        });
    };

    this.getDealsList = function (Name, prTypeP, OS, Brand, MinPrice, MaxPrice) {
        if (!prTypeP) prTypeP = null;
        if (!MinPrice) MinPrice = null;
        if (!MaxPrice) MaxPrice = null;
        var OSArr = '[';
        if (!Name) Name = null;
        if (!OS) OSArr = null;
        else {
            var OSKeys = Object.values(OS);
            for (var i = 0; i <= OSKeys.length; i++) {
                if (OSKeys[i] !== 'false' && OSKeys[i] && OSKeys[i] != 'undefinded') OSArr += OSKeys[i] + ',';

            }
            OSArr += ']';
        }

        var BrdArr = '[';
        if (!Brand) BrdArr = null;
        else {
            var BrdKeys = Object.values(Brand); //{"2":1,"3":4} "2" obj key & 1 obj value

            for (var i = 0; i <= BrdKeys.length; i++)
                if (BrdKeys[i] !== 'false' && BrdKeys[i] && BrdKeys[i] != 'undefinded') BrdArr += BrdKeys[i] + ',';

            BrdArr += ']';
        }

        var jsonData = JSON.stringify(
            { n: Name, TP: prTypeP, OS: OSArr, Br: BrdArr, minP: MinPrice, maxP: MaxPrice }

        );

        // alert(jsonData);
        return $http({

            method: "GET",
            url: "/GetData.aspx?q=3&JD=" + jsonData, //send object params
            dataType: "json",
            header: { "Content-Type": "application/json; charset=utf-8" }

        }).success(function (response) {

            //alert('success' + JSON.stringify(response));


        }).error(function (error) {

            alert(param + 'error');
            $scope.error = error;
        });

    };

    this.GetPhonesByFilter = function (Name, prTypeP, OS, Brand, MinPrice, MaxPrice) {
       
        if (!MinPrice) MinPrice = null;
        if (!MaxPrice) MaxPrice = null;
        var OSArr = '[';
        if (!Name) Name = null;
        if (!OS) OSArr = null;
        else {
            var OSKeys = Object.values(OS);
            for (var i = 0; i <= OSKeys.length ; i++) {
                if (OSKeys[i] !== 'false' && OSKeys[i] && OSKeys[i] != 'undefinded') OSArr += OSKeys[i] + ',';

            }
            OSArr += ']';
        }

        var BrdArr = '[';
        if (!Brand) BrdArr = null;
        else {
            var BrdKeys = Object.values(Brand); //{"2":1,"3":4} "2" obj key & 1 obj value
            
            for (var i = 0; i <= BrdKeys.length; i++)            
                if (BrdKeys[i] !== 'false' && BrdKeys[i] && BrdKeys[i]!= 'undefinded') BrdArr += BrdKeys[i] + ',';
            
            BrdArr += ']';
        }
    
        var jsonData = JSON.stringify(
            { n: Name, TP: prTypeP, OS: OSArr, Br: BrdArr, minP: MinPrice, maxP:MaxPrice }
         
        );

       // alert(jsonData);
        return $http({

            method: "GET",
            url: "/GetData.aspx?q=4&JD=" + jsonData, //send object params
            dataType: "json",
            header: {"Content-Type": "application/json; charset=utf-8"}

           // param: jsonData
            //param: { n: name, TP: prTypeP, TT: prTypeT }

        }).success(function (response) {
         
            //alert('success' + JSON.stringify(response));


        }).error(function (error) {

            alert(param + 'error');
            $scope.error = error;
        });


    };


    this.GetPhoneItembyID = function (id) {

        return $http({

            method: "GET",
            url: "/GetData.aspx?q=5&id=" + id, 
            dataType: "json",
            header: { "Content-Type": "application/json; charset=utf-8" }

        }).success(function (response) {

           // alert('success' + JSON.stringify(response));


        }).error(function (error) {

            alert(param + 'error');
            $scope.error = error;
        });
    }


    this.GetPhoneFeatureInfo = function (FId,PId,F,T) {//Feature Id, Phone Id, F='memo', T=Tablet or phone since it is not coming from SQL yet.

   
        return $http({

            method: "GET",
            url: "/GetData.aspx?q=7&FId=" + FId + "&PId=" + PId + "&F=" + F+"&T="+T,
            dataType: "json",
            header: { "Content-Type": "application/json; charset=utf-8" }

        }).success(function (response) {

            //alert('success' + JSON.stringify(response));


        }).error(function (error) {

            alert(param + 'error');
            $scope.error = error;
        });
    }


    //stackoverflow.com/questions/20279725/angularjs-submit-form-with-checkboxes-via-ajax 

   // var Indata = { param: 'val1', .....}
   // $http({
     //   url: "time.php",
     //   method: "POST",
     //   params: Indata
   // })

});


