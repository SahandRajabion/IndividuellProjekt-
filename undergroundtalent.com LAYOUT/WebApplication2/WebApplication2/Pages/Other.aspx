<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Other.aspx.cs" Inherits="WebApplication2.Pages.Other" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href='http://fonts.googleapis.com/css?family=Oswald' rel='stylesheet' type='text/css'>
    <div id="marginTitle">
    <h1>Övrigt</h1></div>

  <%-- Listview för videoklipp (hämtas via databas) --%>
 <asp:ListView ID="VideoListView" runat="server"
                ItemType="WebApplication2.Model.Video"
                SelectMethod="VideoListView_GetData"
                DataKeyNames="VideoID">
                <LayoutTemplate>
                    <table>

                        <%-- Platshållare för nya rader --%>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                       <div id="clear" style="clear: both;" >
                     <%--Datapager för Paging av innehåll på sidan--%>
                     <asp:DataPager ID="DataPager" runat="server" PageSize="9">
                        <Fields>
                             <asp:NextPreviousPagerField ShowFirstPageButton="False" FirstPageText=" Första " ShowNextPageButton="False" ShowPreviousPageButton="True" ButtonType="Button"  PreviousPageText="Föregående"/>
                            <asp:NumericPagerField ButtonType="Link" />
                            <asp:NextPreviousPagerField ShowLastPageButton="false" LastPageText=" Sista " ShowNextPageButton="True" ShowPreviousPageButton="False" ButtonType="Button"  NextPageText="Nästa"/>
                        </Fields>
                    </asp:DataPager>

                </LayoutTemplate>
                <ItemTemplate>
                <span class="videolisting"> 
                     <div id="titelposition">
            <asp:Label ID="Label2" runat="server" Text="<%# Item.Titel %>"></asp:Label>
                         </div>
            <a class="player"  href='<%#"../Videos/" + Item.VidName %>'></a>
        
            <%--Popup Ruta--%>
            <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="LinkButton1_Command"  CssClass="buttonClass"  Text="Klicka för att Kommentera"></asp:LinkButton>
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false">
            <div class="background"></div>
            <div class="popupWindow">
                <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="../css/images1/cancel.png" OnCommand="ImageButton1_Command" CssClass="cancel"/>
                <div id="buttonsfb">
                 <div class="fb-like" data-href='http://facebook.com/<%# Item.VidName %>' data-layout="standard" data-action="like" data-show-faces="true" layout="button_count"></div>
                <div class="fb-share-button" data-href="http://sahand11-001-site1.smarterasp.net/<%# Item.VidName %>" data-type="button"></div>
                </div>
               <a class="player"  href='<%#"../Videos/" + Item.VidName %>'></a>

                <div class="popCom">
              <div class="fb-comments" data-href="http://sahand11-001-site1.smarterasp.net/Videos/<%# Item.VidName %>" data-numposts="1" data-colorscheme="dark" data-width="400px" style="color: white"></div>

                </div>
            </div>
          </asp:Label>

                    </span>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td>Finns inte något under denna kategori för tillfället. Ladda gärna upp något :)
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>

            </asp:ListView>

    <%--Taget från http://flash.flowplayer.org/plugins/streaming/pseudostreaming.html--%>
    <%--Script för mediaspelaren--%>
    <script src="../FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        flowplayer("a.player", "../FlowPlayer/flowplayer-3.2.16.swf", {
            plugins: {
                pseudo: { url: "../FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
            },
            clip: { provider: 'pseudo', autoPlay: false, autoBuffering: true },
        });
    </script>
</asp:Content>
