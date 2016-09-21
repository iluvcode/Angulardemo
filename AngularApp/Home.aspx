<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="AngularApp.Home" %>

<!DOCTYPE html>
<script src="Scripts/jquery-1.12.4.js"></script>
<script src="Scripts/angular.js"></script>
<script src="Scripts/jquery-ui-1.12.0.js"></script>
<link href="Scripts/jquery-ui.min.css" rel="stylesheet" />


<div ng-app="MyApp2">

    <div ng-controller="AngularController">
        Details:
   <textarea ng-model="Name"> </textarea>
   <button ng-click="addText()">Add</button>
        <br />
        <br />
        Color:

     <%--   //This is for the API Call--%>
        <%-- <select ng-model="DisplayVendors" 
        ng-options="item.VendorCode as item.LegalName for item in Vendors">
    <option value="">Select Vendors</option>
</select>--%>
        <select ng-model="data.DisplayVendors" ng-options="item.id as item.name for item in data.Vendors">
            <option value="">Select Vendors</option>
        </select>
        
         <div ng-repeat="item in Texts" items-drag>
                <div resizable on-resize="resizeFunction()">{{item}} </div>
             <%--<asp:Label ID="Label1" runat="server" Text="" ng-bind="item"></asp:Label>--%>
              
        </div>
        
       <%--//http://jsfiddle.net/EE63X/257/--%>
       <asp:Image runat="server" ID="itemImage"/>
        <div id="iamge-reizediv"  items-drag>
            <img id="image" class="profile-pic" src="" />
         </div>
        <div class="upload-button">Upload Image</div>
        <input class="file-upload" type="file" accept="image/*"/>
    </div>
</div>


<script>
    var MyApp = angular.module("MyApp2", []);
    var degrees = 90;
    MyApp.controller('AngularController', function ($scope, $http) {
        $scope.Name = "Enter your details on the shirt";
        $scope.Count = 0;
        $scope.Texts = [];
        //This is for the API call
        //$http.get('http://localhost/RegVendor/Api/RegVendor/GetNominations?keyword=1').success(function (data) {
        //    $scope.Vendors = data;
        //    $scope.DisplayVendors = data;
        //});
        $scope.resizeFunction = function() {
            //Add code here to save the size of the div
        };
    
        $scope.addText = function () {
            $scope.Texts.push($scope.Name);
        }
        $scope.data = {
            DisplayVendors: null,
            Vendors: [
                { id: '1', name: 'Option A' },
                { id: '2', name: 'Option B' },
                { id: '3', name: 'Option C' }
            ]
        };
        //http://www.erol.si/2014/07/ng-repeat-with-draggable-or-how-to-correctly-use-angularjs-with-jquery-ui/
        
        //$scope.ServiceClick = function (e) {

        //    var VendorCode = e;
        //    e = '#' + e;

        //    $(e).load(webRoot + 'Vendors/_services?VendorCode=' + VendorCode);
        //}
        //getVendors();

        //function getVendors() {
        //    VendorService.getVendors()
        //        .success(function (Vendors) {
        //            $scope.Vendors = Vendors;

        //        })
        //        .error(function (error) {
        //            $scope.status = 'Unable to load Vendor data: ' + error.message;

        //        });
        //}
    });

    MyApp.directive('itemsDrag', function () {
        return {
            link: function (scope, element, attrs) {
                element.draggable();

                scope.$on('$destroy', function () {
                    element.off('**');
                });
            }
        };
    });

    MyApp.directive('resizable', function () {
        return {
            restrict: 'A',
            scope: {
                callback: '&onResize'
            },
            link: function postLink(scope, elem, attrs) {
                elem.resizable();
                elem.on('resizestop', function (evt, ui) {
                    if (scope.callback) { scope.callback(); }
                });
            }
        };
    });
   
    $('#iamge-reizediv').resizable();
</script>

<script type="text/javascript">
    $(document).ready(function () {


        var readURL = function (input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#image').attr('src', e.target.result);
                    alert("yes");
                }

                reader.readAsDataURL(input.files[0]);
            }
        }


        $(".file-upload").on('change', function () {
            readURL(this);
        });

        $(".upload-button").on('click', function () {
            $(".file-upload").click();
        });
    });
</script>

<style>
    
  .upload-button {
    padding: 4px;
    border: 1px solid black;
    border-radius: 5px;
    display: block;
    float: left;
}

.profile-pic {
    max-width: 200px;
    max-height: 200px;
    display: block;
}

.file-upload {
    display: none;
}

    div[resizable] {
        border: 1px solid Black;
        width: 50%;
    }
    

    .center {
        position: absolute;
        width: 100px;
        height: 50px;
        top: 50%;
        left: 50%;
        margin-left: -50px; /* margin is -0.5 * dimension */
        margin-top: -25px;
    }

    ​ .draggable {
        position: relative;
        display: -moz-inline-stack;
        display: inline-block;
        vertical-align: top;
        zoom: 1;
        *display: inline;
        cursor: hand;
        cursor: pointer;
    }
    
</style>
