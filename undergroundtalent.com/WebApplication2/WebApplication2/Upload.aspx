<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="WebApplication2.Upload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Upload Videos</h2><br />
    
    <%--Funktions knappar--%>
    <asp:FileUpload ID="Uploadfiles" runat="server" />
    <asp:Button ID="UploadButton" Text="Upload" runat="server" onclick="buttonUpload_Click" />
    
    
        <asp:Label ID="title" runat="server" Text="Ange Titel:"></asp:Label>
        <asp:TextBox ID="Titel" runat="server" Text='' MaxLength="30" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="En Titel måste anges." ControlToValidate="Titel" Display="None" ></asp:RequiredFieldValidator> 
         



    <asp:Label ID="Label1" style="color: Red;" runat="server" text="&nbsp;" ></asp:Label>
    <asp:Label ID="StatusLogin" text="" runat="server" />
    <asp:Label ID="Success" text="" runat="server" Visible="false" CssClass="success"/>
    <asp:Label ID="LabelStatus" text="" runat="server" />


    <%--DropdownList innehållande kategorivalen hämtade från databasen--%>
    <asp:DropDownList ID="DropDownCategory" runat="server"
                    SelectMethod="DropDownCategory_GetData"
                    DataTextField="Kategori"
                    DataValueField="KategoriID"/>  
                   <div style="clear: both;" /> <br />

    
    <asp:Button ID="DeleteButton" Text="Delete" runat="server" onclick="buttonDelete_Click" CausesValidation="false" />
    

                  

        <%--Validering--%>
    <asp:RegularExpressionValidator ID="FileUploadRegularExpressionValidator" runat="server" ErrorMessage="Filen måste vara av formaten avi, mp4, wmv, MOV, mpeg." ControlToValidate="Uploadfiles" Display="None" ValidationExpression=".*.(avi|mp4|wmv|MOV)"></asp:RegularExpressionValidator>
    <asp:RequiredFieldValidator ID="FileUploadRequiredFieldValidator" runat="server" ErrorMessage="Du måste välja minst en fil för uppladdning." Display="None" ControlToValidate="Uploadfiles"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors" />




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
          
             <%--Checkboxrutor för Delete--%>
            <asp:CheckBox special="<%# Item.VidName %>" ID="Delete" Text="Delete" runat="server" /><br />
            
            <asp:Label ID="Label2" runat="server" Text="<%# Item.Titel %>"></asp:Label>
            <a class="player"  href='<%#"../Videos/" + Item.VidName %>'></a>
        
                         <asp:HyperLink ID="LinkButton" runat="server" Text="Redigera" NavigateUrl='<%# GetRouteUrl("edit", new { id = Item.VideoID }) %>' CausesValidation="false"/>

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
                            <td>Videoklipp saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>


         

            </asp:ListView>
 

     <%--Script för mediaspelaren--%>
     <script src="../../FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        flowplayer("a.player", "../../FlowPlayer/flowplayer-3.2.16.swf", {
            plugins: {
                pseudo: { url: "../../FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
            },
            clip: { provider: 'pseudo', autoPlay: false, autoBuffering: true },
        });
    </script>
</asp:Content>







  


  