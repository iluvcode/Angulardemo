<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemPage.aspx.cs" Inherits="AngularApp.ItemPage" %>

<!DOCTYPE html>


<head runat="server">
    <title>Personalize your product</title>
   <%-- <script src="Scripts/jquery-1.12.4.js"></script>--%>
    <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css">
  <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css">
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
  <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.2.1/angular.min.js"></script>
    <script src="Scripts/Gallery/owl.carousel.js"></script>
    <link href="CCS/Gallery/owl.carousel.css" rel="stylesheet" />
    <link href="CCS/Gallery/owl.theme.css" rel="stylesheet" />
    <link href="CCS/Gallery/owl.transitions.css" rel="stylesheet" />
     <style type="text/css">
         #gallery {
             width: 500px;
         }
         #gallery .item{
             margin: 3px;
         }
         #gallery .item img{
             display: block;
             width: 100px;
             height: 100px;
          
         }
      
     </style>
</head>
<body>
    
    <div ng-app ="MyApp2">
        <div ng-controller="AngularController">
            <div>
                <div id="productImage">
                    <img height="500" width="500" src ='/images/Tshirt-front.jpg'>
                     <div items-drag id="uploadImage" style="display: none; position: absolute; top: 150px; left: 150px">
                         <img src="" resizable on-resize="resize($evt, $ui)" width="150" height="150" id="image"  />
	                                     <div ng-show="w">{{w}}:{{h}}px</div>
                     </div>
                    <div ng-repeat="item in Texts" items-drag style="position: absolute; top: 250px; left: 150px">
                      <div resizable on-resize="resize($evt, $ui)" style="width: 100px; height: 50px">{{item}} </div>
                </div>
                </div>
                <br/>
                <div id="gallery"></div>
            </div>
            <div>
                
                <div class="upload-button">Upload Image</div>
                <input class="file-upload" type="file" accept="image/*" style="display: none"/>
                <br/>
                <br/>
                <textarea ng-model="Name"></textarea>
                <button ng-click="addText()">Add Personalization Text</button>
            </div>
        </div>
    </div>
    
</body>
<script type="text/javascript">
    var galleryImages = ["/images/Tshirt-front.jpg", "/images/Tshirt-back.jpg"];

    $(document).ready(function() {
        for (var i = 0; i < galleryImages.length; i++) {
            var img = "<div class='item'><img src = '" + galleryImages[i] + "' ></div>";
            $("#gallery").append(img);
        }

        $("#gallery").owlCarousel({
            loop: true,
            margin: 10,
            nav: true,
        });

        $('.item').click(function () {
           var imgsrc = $(this).children('img').attr('src');
            $("#productImage").html("<img height='500' width='500' src = '" + imgsrc + "'>");

        });
       
        //start image upload
        var readURL = function (input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#image').attr('src', e.target.result);
                    $('#uploadImage').css("display", "block");
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
        //End image upload
    });

    var MyApp = angular.module("MyApp2", []);
    MyApp.controller('AngularController', function ($scope, $http) {
        $scope.Name = "Add Text here";
        $scope.Count = 0;
        $scope.Texts = [];
        //This is for the API call
        //$http.get('http://localhost/RegVendor/Api/RegVendor/GetNominations?keyword=1').success(function (data) {
        //    $scope.Vendors = data;
        //    $scope.DisplayVendors = data;
        //});
        //$scope.resizeFunction = function() {
        //    //Add code here to save the size of the div
        //};

        $scope.resize = function (evt, ui) {
            //console.log (evt,ui);
            $scope.w = ui.size.width;
            $scope.h = ui.size.height;
        }

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

    //MyApp.directive('resizable', function () {
    //    return {
    //        restrict: 'A',
    //        scope: {
    //            callback: '&onResize'
    //        },
    //        link: function postLink(scope, elem, attrs) {
    //            elem.resizable();
    //            elem.on('resizestop', function (evt, ui) {
    //                if (scope.callback) { scope.callback(); }
    //            });
    //        }
    //    };
    //});

    MyApp.directive('resizable', function () {
        return {
            restrict: 'A',
            scope: {
                callback: '&onResize'
            },
            link: function postLink(scope, elem, attrs) {
                elem.resizable({ handles: "all" });
                elem.on('resize', function (evt, ui) {
                    scope.$apply(function () {
                        if (scope.callback) {
                            scope.callback({ $evt: evt, $ui: ui });
                        }
                    });
                });
            }
        };
    });
</script>  

 <style>
     div[resizable] {
        border: 1px solid Black;
        width: 50%;
    }
    

    .center {
        position: absolute;
        width: 50px;
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
 </style>  


