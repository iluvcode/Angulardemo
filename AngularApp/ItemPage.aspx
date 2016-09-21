<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemPage.aspx.cs" Inherits="AngularApp.ItemPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personalize your product</title>
    <script src="Scripts/jquery-1.12.4.js"></script>
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
    
    <div>
        <div id="productImage"><img height="500" width="500" src ='/images/Tshirt-front.jpg'></div>
        <div id="gallery"></div>
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
       

    });


   
    

</script>    

</html>
