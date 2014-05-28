<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="WebApplication2.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title> </title>
    <link href ="~/Content/style.css" rel="stylesheet"/>
</head>
<body>
    <div id="allerrorcontent">
    <h1>System Fel</h1>
     <asp:image id="img" runat="server" imageurl="css/images1/error.jpg" />
    <p>Vi Beklagar att ett fel inträffade och inte kunde hantera din förfrågan försök gärna igen.<br /> Skulle det inte fungera, försök igen vid senare tidpunkt. </p>

        <div id="tillbaka">
    <a href="Default.aspx">Tillbaka Till Förstasidan</a>
        </div>
        </div>

</body>
</html>
</asp:Content>
