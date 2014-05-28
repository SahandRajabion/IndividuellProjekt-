
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href='http://fonts.googleapis.com/css?family=Oswald' rel='stylesheet' type='text/css'>
      <div id="marginTitle">
    <h1>Välkommen till Undergroundtalent.com</h1></div>

 
    

    
<link rel='stylesheet' id='style-css'  href='diapo.css' type='text/css' media='all'> 





      <%--Taget från css för video på startsidan från diapo--%>
      <%--videoexempel--%>
   
 
    
    	<div style="overflow:hidden; width:960px; margin: 100px auto; padding:0 20px;"> 
              
            <div class="pix_diapo">
                 
                  
                    
                    <div data-thumb="images1/thumbs/electro.png">
                        <iframe width="940" height="470" src="https://www.youtube.com/embed/86R-YywrvW0" data-fake="images1/slides/electro.png"  frameborder="5" allowfullscreen></iframe>
                        
                    </div>
                    
                   
                      
                   
                    
                    
                    
                    
                    </div>
               </div>
                
      
    
   
   
</asp:Content>