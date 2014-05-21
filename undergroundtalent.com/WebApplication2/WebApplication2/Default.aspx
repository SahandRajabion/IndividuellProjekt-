
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">



 
    

    
<link rel='stylesheet' id='style-css'  href='diapo.css' type='text/css' media='all'> 
<script type='text/javascript' src='scripts1/jquery.min.js'></script>
<!--[if !IE]><!--><script type='text/javascript' src='../scripts1/jquery.mobile-1.0rc2.customized.min.js'></script><!--<![endif]-->
<script type='text/javascript' src='scripts1/jquery.easing.1.3.js'></script> 
<script type='text/javascript' src='scripts1/jquery.hoverIntent.minified.js'></script> 
<script type='text/javascript' src='scripts1/diapo.js'></script> 

<script>
    $(function () {
        $('.pix_diapo').diapo();
    });

</script>



    

    <%--Slideshow av videoexempel--%>
    <div style="margin-left:320px; margin-bottom:10px;">
    
        <h1>Välkommen till upload.com</h1> 
        </div>
 
    
    	<div style="overflow:hidden; width:960px; margin: 100px auto; padding:0 20px;"> 
              
            <div class="pix_diapo">
                 
                  
                    
                    <div data-thumb="images1/thumbs/electro.png">
                        <iframe width="940" height="470" src="https://www.youtube.com/embed/86R-YywrvW0" data-fake="images1/slides/electro.png" frameborder="5" allowfullscreen></iframe>
                        
                    </div>
                    
                      
                    <div data-thumb="images1/thumbs/guitar.png">
                        <iframe width="940" height="470" src="http://www.youtube.com/embed/W1pYGgLA_Wo" data-fake="images1/slides/guitar.png" frameborder="0" allowfullscreen></iframe>
                       
                    </div>
                      
                    <div data-thumb="images1/thumbs/spose.png">
                        <iframe width="940" height="470" src="http://www.youtube.com/embed/YN104cEvtyE" data-fake="images1/slides/spose.png" frameborder="0" allowfullscreen></iframe>
                      
                    </div>
                      
                    <div data-thumb="images1/thumbs/inception.png">
                        <iframe width="940" height="470" src="http://www.youtube.com/embed/eTSQKFMwOS4" data-fake="images1/slides/inception.png" frameborder="0" allowfullscreen></iframe>
                       
                    </div>
                      
                    <div data-thumb="images1/thumbs/briz.png">
                        <iframe width="940" height="470" src="http://www.youtube.com/embed/i110Qn_AVgI" data-fake="images1/slides/briz.png" frameborder="0" allowfullscreen></iframe>
                       
                    </div>
                      
                    <div data-thumb="images1/thumbs/dahlia.png">
                        <iframe width="940" height="470" src="http://www.youtube.com/embed/QmliQehOY0c" data-fake="images1/slides/dahlia.png" frameborder="0" allowfullscreen></iframe>
                       
                    </div>
                      
                   
                    
                    
                    
                    
                    </div>
               </div>
                
      
    
   
   
</asp:Content>