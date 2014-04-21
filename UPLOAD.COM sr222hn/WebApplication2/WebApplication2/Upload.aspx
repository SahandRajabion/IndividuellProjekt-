<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="WebApplication2.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/jquery.fancybox-1.3.4/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <script src="http://slideshow.triptracker.net/slide.js" type="text/javascript">

    </script>    <script type="text/javascript">
                     // For more options, go to http://fancybox.net/api

                     $(document).ready(function () {
                         $("a[rel=example_group]").fancybox({
                             'transitionIn': 'none',
                             'transitionOut': 'none',
                             'titlePosition': 'inside',
                             'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
                                 return '<span id="fancybox-title-inside">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
                             }
                         });
                     });
    </script>

   

    <link href="Scripts/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        

    </style>
</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
      <h2>Upload Photos</h2><br />
    <asp:FileUpload ID="fuUpload" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnUpload" Text="Upload" runat="server" onclick="btnUpload_Click" /><br /><br />
    <asp:Label ID="lblStatus" style="color: Red;" text="&nbsp;" runat="server" />
    <asp:Button ID="btnDelete" Text="Delete" runat="server" onclick="btnDelete_Click" /><br /><br />

    <br /><br />
    
 <%--   <asp:Repeater ID="rptrUservids" runat="server">
        <ItemTemplate>
            <span class="saucer" style="float: left; padding: 15px; ">
            <asp:ImageButton OnCommand="vidUser_Command" CommandArgument="<%# Container.DataItem %>" ImageUrl="<%# Container.DataItem %>" ID="imgUserPhoto" style="width: 100px; height: 100px;" runat="server" alt="mm" /><br />
           
            </span>
        </ItemTemplate>
    </asp:Repeater>--%>
    <div style="clear: both;" />
     


    
    <asp:DataList ID="DataList1" Visible="true" runat="server"  AutoGenerateColumns="false"
        RepeatColumns="2" CellSpacing="5">
        <ItemTemplate>
            
            <hr />
            <a class="player" style="height: 300px; width: 300px; display: block" >
            </a>
            <asp:CheckBox special="<%# Container.DataItem %>" ID="cbDelete" Text="Delete" runat="server" /><br />
            
        </ItemTemplate>
    </asp:DataList>






     <script src="FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        flowplayer("a.player", "FlowPlayer/flowplayer-3.2.16.swf", {
            plugins: {
                pseudo: { url: "FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
            },
            clip: { provider: 'pseudo', autoPlay: false },
        });
    </script>
</asp:Content>
