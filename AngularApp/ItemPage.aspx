﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemPage.aspx.cs" Inherits="AngularApp.ItemPage" %>

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
            width: 700px;
            height: auto;

        }

            #gallery .item {
                width: 400px;
            }

                #gallery .item img {
                    display: block;
                    width: 150px;
                    height: 150px;
                   
                }
    </style>
</head>
<body>

    <div ng-app="MyApp2">
        <div ng-controller="AngularController">
            <div class="angular-main-container">
                <div class="angular-wrapper1">
                    <div id="productImage">
                        <img class="mainproductImage" src='/images/Tshirt-front.jpg'>
                        <div items-drag id="uploadImage" style="display: none; position: absolute; top: 150px; left: 635px">
                            <img src="" resizable on-resize="resize($evt, $ui)" width="150" height="150" id="image" />
                        </div>
                        <br />
                        <div ng-repeat="item in Texts" items-drag style="position: absolute; top: 320px; left: 670px">
                            <div resizable on-resize="resize($evt, $ui)" style="width: auto; height: auto"><font ng-style="selectedColor" face="{{selectedFont}}">{{item.name}}</font></div>
                        </div>
                    </div>
                    <div class="angular-wrapper3">
                        <div id="gallery" class="angular-gallery"></div>
                    </div>
                     <%-- <div ng-show="w">{{w}}:{{h}}px</div>--%>
                </div>


                <div class="angular-wrapper2">
                    <div class="angular-description">Men's Basic T-Shirt</div>
                    <div class="angular-itemprice">${{ItemPrice}} per shirt</div>

                    <div class="agular-heading">Pick your size:</div>
                    <select>
                        <option value="small">Small</option>
                        <option value="medium">Medium</option>
                        <option value="large">Large</option>
                        <option value="xlarge">Xtra Large</option>
                    </select>
                    <br />
                    <div class="agular-heading">Quantity: </div>
                    <select>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                    </select>
                    <br />
                    <button type="button" class ="angular-add-to-cart">Add to Cart</button>
                    
                    <div class="agular-heading">Customize It!</div>

                    <div class="angular-upload-button">Upload Image</div>
                    <input class="file-upload" type="file" accept="image/*" style="display: none" />


                    <textarea ng-model="Name"></textarea>
                    <button type="button" class="angular-personalization-button " ng-click="addText()">Add Personalization Text</button>
                    <div class="agular-heading">$1 per character</div>
                    <br />
                    <select ng-model="fontDropdown"
                        ng-options="font as font.label for font in fonts"
                        ng-change="change(fontDropdown)">
                        <option value="">Select font</option>
                    </select>
                    <select ng-model="colorDropdown"
                        ng-options="color as color.label for color in colors"
                        ng-change="changeColor(colorDropdown)">
                        <option value="">Select color</option>
                    </select>
                    

                    <br />
                
                    <div id="imagePreviewDiv" style="display: none">
                        <img src="" id="addedImagePreview" style="height: 50px; width: 50px" />
                        <button type="button" class="angular-delete-image" onclick="RemoveImage()">Delete</button>
                    </div>
                    <div ng-if="Texts.length > 0">
                        <div class="agular-heading">Personalizations added on the Product</div>    
                        
                        <div class="angular-repeat-text" ng-repeat="item in Texts">
                            <div class ="angular-edit-text">
                                <font ng-style="selectedColor" face="{{selectedFont}}"><span id="{{item.id}}span">{{item.name}}</span></font>
                                <input type="text" id="{{item.id}}textarea" style="display: none" class="angular-personalize-text"></input>
                            
                            <button type="button" class="angular-edit-button " id="{{item.id}}edit" ng-click="edit(item)">Edit</button>
                            <button type="button" class="angular-save-button" id="{{item.id}}save" style="display: none" ng-click="addChangedText(item)">Save</button>
                            <button type="button" class="angular-delete-button" ng-click="remove(item)">Delete</button>
                        </div>
</div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</body>
<script type="text/javascript">
    var galleryImages = ["/images/Tshirt-front.jpg", "/images/Tshirt-back.jpg"];

    $(document).ready(function () {
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
            //$("#productImage").html("<img height='500' width='500' src = '" + imgsrc + "'>");
            $(".mainproductImage").attr("src", imgsrc);

        });

        //start image upload
        var readURL = function (input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#image').attr('src', e.target.result);
                    $('#addedImagePreview').attr('src', e.target.result);
                    $('#uploadImage').css("display", "block");
                    $('#imagePreviewDiv').css('display', "block");
                }
                reader.readAsDataURL(input.files[0]);
            }
        }


        $(".file-upload").on('change', function () {
            readURL(this);
        });

        $(".angular-upload-button").on('click', function () {
            $(".file-upload").click();
        });
        //End image upload
    });


    function RemoveImage() {
        $('#uploadImage').css("display", "none");
        $('#imagePreviewDiv').css('display', "none");
    }

    var MyApp = angular.module("MyApp2", []);
    MyApp.controller('AngularController', function ($scope, $http) {
        $scope.Name = "Add Text here";
        $scope.Count = 0;
        $scope.Texts = [];
        $scope.ItemPrice = 14;
        //This is for the API call
        //$http.get('http://localhost/RegVendor/Api/RegVendor/GetNominations?keyword=1').success(function (data) {
        //    $scope.Vendors = data;
        //    $scope.DisplayVendors = data;
        //});
        //$scope.resizeFunction = function() {
        //    //Add code here to save the size of the div
        //};

        $scope.fonts = [
               {
                   value: 'Arial',
                   label: 'Arial'
               },
               {
                   value: 'Tahoma',
                   label: 'Tahoma'
               },
                {
                    value: 'Verdana',
                    label: 'Verdana'
                },
                {
                    value: 'Webdings',
                    label: 'Webdings'
                },
                {
                    value: 'Georgia',
                    label: 'Georgia'
                },
                {
                    value: 'Impact',
                    label: 'Impact'
                },
                {
                    value: 'Marlett',
                    label: 'Marlett'
                }
        ];

        $scope.colors = [
              {
                  value: 'Red',
                  label: 'Red'
              },
              {
                  value: 'black',
                  label: 'Black'
              },
               {
                   value: 'green',
                   label: 'Green'
               },
               {
                   value: 'blue',
                   label: 'Blue'
               },
               {
                   value: 'yellow',
                   label: 'Yellow'
               }
        ];

        $scope.selectedFont = '';
        $scope.change = function (option) {
            $scope.selectedFont = option.value;
        }

        $scope.changeColor = function(option) {
            $scope.selectedColor = {
                "color": option.value
         }
        }

        $scope.resize = function (evt, ui) {
            //console.log (evt,ui);
            $scope.w = ui.size.width;
            $scope.h = ui.size.height;
        }

        $scope.addText = function () {
            var counter = $scope.Texts.length + 1;
            $scope.Texts.push({ id: counter, name: $scope.Name });
            $scope.ItemPrice = $scope.ItemPrice + $scope.Name.length;
            //$scope.Texts.push($scope.id,$scope.Name);
        }

        $scope.remove = function (item) {
            var index = $scope.Texts.indexOf(item);
            $scope.Texts.splice(index, 1);
            $scope.ItemPrice = $scope.ItemPrice - item.name.length;
        }

        $scope.edit = function (item) {
            var inputId = "#" + item.id + "textarea";
            $(inputId).css("display", "block");
            $(inputId).val(item.name);

            var spanId = "#" + item.id + "span";
            $(spanId).hide();

            var savebuttonId = "#" + item.id + "save";
            $(savebuttonId).css("display", "block");

            var editbuttonId = "#" + item.id + "edit";
            $(editbuttonId).hide();

        }

        $scope.addChangedText = function (item) {
            var index = $scope.Texts.indexOf(item);

            var inputId = "#" + item.id + "textarea";
            var changedText = $(inputId).val();
            item.name = changedText;
            $scope.Texts[index] = item;

            $(inputId).css("display", "none");
            var spanId = "#" + item.id + "span";
            $(spanId).show();

            var savebuttonId = "#" + item.id + "save";
            $(savebuttonId).css("display", "none");

            var editbuttonId = "#" + item.id + "edit";
            $(editbuttonId).show();
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

    .angular-wrapper1 {
        float: left;
        margin-left: 0px;
        margin-right: 50px;
        width: 430px;
        height: 100%;
    }
        .angular-wrapper2 {
        float: left;
        margin-left: 50px;
        margin-right: 0px;
        width: 430px;
        height: 100%;
    }

       .angular-wrapper3 {
        float: left;
        margin-left: 0px;
        margin-right: 50px;
        width: 100%;
        height: auto;
    }
    .angular-main-container {
       display : block;
       padding-top: 10px;
     margin: auto;
     max-width: 1000px;
     
}
     .angular-gallery {
     display : block;
       padding-top: 10px;
     margin: auto;
     max-width: 900px;
     }

    input, select, textarea {
    background-color: #fff;
    border-color: #e0e0e0;
    -webkit-border-radius: 3px;
    border-radius: 3px;
    -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06);
    box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06);
    border-width: 1px;
    border-style: solid;
    color: #474e57;
    font-size: 16px;
    font-weight: 400;
    padding: 10px;
    margin-top: 6px; 
    margin-bottom: 6px; 
    width: 100%;
}

    .angular-personalization-button {
    background-color: #ff6600; /* Orange */
    border: none;
    color: white;
    padding: 15px 32px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    margin-bottom: 4px;
    width: 430px;
   -webkit-border-radius: 3px;
    border-radius: 3px;
    -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06);
}
    .angular-personalization-button:hover {
        background-color :#ff3300;
        letter-spacing: 1px;
        }

    .angular-add-to-cart {
        background-color: #4CAF50; /* Green */
        border: none;
        color: white;
        padding: 15px 12px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin-bottom: 4px;
        width: 430px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
        -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06);
    }
        .angular-add-to-cart:hover {
        background-color :#329b00;
        letter-spacing: 1px;
        }

    .mainproductImage {
        height: 500px;
        width: 500px;
        float: left; 
    }

     .draggable {
        position: relative;
        display: -moz-inline-stack;
        display: inline-block;
        vertical-align: top;
        zoom: 1;
        *display: inline;
        cursor: hand;
        cursor: pointer;
    }

      .angular-upload-button {
        background-color: #0099cc; /* Blue */
        border: none;
        color: white;
        padding: 15px 5px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin-bottom: 4px;
        margin-top: 4px; 
        width: 430px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
        -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06);
        cursor: pointer;
}
      .angular-upload-button:hover {
        background-color :#0066cc;
        letter-spacing: 1px;
        
        }
      .angular-delete-button{
        background-color: #f00; /* Red */
        border: none;
        color: white;
        padding: 5px 0px;
        text-align: center;
        text-decoration: none;
        display: inline;
        font-size: 16px;
        margin: 0px 0px 0px 5px;
        width: 75px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
       -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06);
        float: right;
}
      .angular-delete-button:hover {
        background-color :#990000;
        letter-spacing: 1px;
        
        }
      .angular-save-button{
        background-color: #f00; /* Red */
        border: none;
        color: white;
        padding: 5px 0px;
        text-align: center;
        text-decoration: none;
        display: inline;
        font-size: 16px;
        margin: 0px 0px 0px 5px;
        width: 75px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
       -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06);
        float: right;
}

        .angular-save-button:hover {
        background-color :#990000;
        letter-spacing: 1px;
        }


      .angular-edit-button{
        background-color: #f00; /* Red */
        border: none;
        color: white;
        padding: 5px 0px;
        text-align: center;
        text-decoration: none;
        display: inline;
        font-size: 16px;
        margin: 0px 0px 0px 5px;
        width: 75px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
       -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06);
        float: right;
}

      .angular-edit-button:hover {
        background-color :#990000;
        letter-spacing: 1px;
        }

      .angular-delete-image{
        background-color: #f00; /* Red */
        border: none;
        color: white;
        padding: 5px 0px;
        text-align: center;
        text-decoration: none;
        display: inline;
        font-size: 16px;
        margin: 5px 0px 0px 5px;
        width: 75px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
        -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06);
        float: right;
}

      .angular-delete-image:hover {
        background-color :#990000;
        letter-spacing: 1px;
        }

    .profile-pic {
        max-width: 200px;
        max-height: 200px;
        display: block;
    }

    .file-upload {
        display: none;
    }
     .agular-heading {
       font-weight: 700;
       font-size: 14px;
       color: #474e57;}

     .angular-description {
       font-weight: 700;
       font-size: 22px;
       color: #0099cc; /* Blue*/}

     .angular-itemprice{
       font-weight: 700;
       font-size: 18px;
       color: #4CAF50; /*Green*/}

    .angular-repeat-text {
    font-size:14px;
    height: auto;
    width:430px;
    word-wrap:break-word;
    padding-top: 10px;
    margin-top: 12px;
   
     }
    .angular-personalize-text {
    padding: 10px;
    margin: 10px 0px 10px 0px;
    width: 430px;
    }


    .angular-edit-text{ 
    position: relative;
    width: 430px;
    height: auto;
    padding-bottom: 5px;
    margin: 10px 0px 10px 0px;
    }

    </style>
