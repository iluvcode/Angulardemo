
var MyApp = angular.module("MyApp", ['smart-table']);

MyApp.controller('AngularController', function ($scope, $http) {

    $scope.Count = 0;
  
    $http.get('http://localhost/AngularApp/Api/RegVendor/GetNominations?keyword=1').success(function (data) {
        $scope.Vendors = data;
        $scope.DisplayVendors = data;
    });
    $scope.ServiceClick = function (e) {

        var VendorCode = e;
        e = '#' + e;

        //$(e).load(webRoot + 'Vendors/_services?VendorCode=' + VendorCode);
    }
    //getVendors();

    //function getVendors() {
    //    VendorService.getVendors()
    //        .success(function (Vendors) {
    //            $scope.Vendors = Vendors;
    //            $scope.DisplayVendors = data;
    //        })
    //        .error(function (error) {
    //            $scope.status = 'Unable to load Vendor data: ' + error.message;

    //        });
    //}
});