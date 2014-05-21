<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Other.aspx.cs" Inherits="WebApplication2.Pages.Other" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


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

                     <%--Datapager för Paging av innehåll på sidan--%>
                     <asp:DataPager ID="DataPager" runat="server" PageSize="8">
                        <Fields>
                            <asp:NextPreviousPagerField ShowFirstPageButton="True" FirstPageText=" Första " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" />
                            <asp:NumericPagerField ButtonType="Link" />
                            <asp:NextPreviousPagerField ShowLastPageButton="True" LastPageText=" Sista " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" />
                        </Fields>
                    </asp:DataPager>

                </LayoutTemplate>
                <ItemTemplate>
                <span class="videolisting"> 

            <asp:Label ID="Label2" runat="server" Text="<%# Item.Titel %>"></asp:Label>
            <a class="player"  href='<%#"../Videos/" + Item.VidName %>'></a>
        
            <%--Popup Ruta--%>
            <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="LinkButton1_Command"   Text="Klicka för att Kommentera"></asp:LinkButton>
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false">
            <div class="background"></div>
            <div class="popupWindow">
                <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="../css/images1/cancel.png" OnCommand="ImageButton1_Command" CssClass="cancel"/>
                <div class="fb-like" data-href='http://facebook.com/<%# Item.VidName %>' data-layout="standard" data-action="like" data-show-faces="true" layout="button_count"></div>
                <div class="fb-share-button" data-href="http://localhost:3116/<%# Item.VidName %>" data-type="button"></div>

               <a class="player"  href='<%#"../Videos/" + Item.VidName %>'></a>

                <div class="popCom">
              <div class="fb-comments" data-href="http://localhost:3116/comments/<%# Item.VidName %>" data-numposts="1" data-colorscheme="dark" data-width="400px" style="color: white"></div>

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
