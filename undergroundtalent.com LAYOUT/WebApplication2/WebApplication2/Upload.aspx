<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="WebApplication2.Upload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <link href='http://fonts.googleapis.com/css?family=Oswald' rel='stylesheet' type='text/css'>
     <div id="marginTitle">
    <h1>Ladda upp</h1></div><br />
    <asp:Label ID="LabelStatus" text="" runat="server" />

  <div id="info">
    <asp:Label ID="infoupload" runat="server" >
 
<p>Innan du laddar upp en video, kontrollera att den inte överstiger:</p> <br / />
<p>- 25MB i storlek</p>  <br />
<p>Rekommenderade videofiler:</p> 
<p>- Format: mp4 (H264). </p> 
<p>- Upplösning: 320x240, 640x480 eller 1280x720</p> 
      </asp:Label>  <br />
   </div>
    
     <div id="all">
   
          <%--Funktions knappar--%>
         
      <asp:FileUpload ID="Uploadfiles" runat="server" /><br />
    

    
        <asp:Label ID="title" runat="server" Text="Ange Titel:"></asp:Label>
        <asp:TextBox ID="Titel" runat="server" Text='' MaxLength="30" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="En Titel måste anges." ControlToValidate="Titel" Display="None" ></asp:RequiredFieldValidator> <br />
         
    
    <%--DropdownList innehållande kategorivalen hämtade från databasen--%>
   <asp:Label ID="Label3" runat="server" Text="Ange Kategori:"></asp:Label>
     <asp:DropDownList ID="DropDownCategory" runat="server"
                    SelectMethod="DropDownCategory_GetData"
                    DataTextField="Kategori"
                    DataValueField="KategoriID"/>  
                   <div style="clear: both;" /> 


      


   <asp:Button ID="UploadButton" Text="Ladda upp" runat="server" onclick="buttonUpload_Click" /> 

    <asp:Label ID="Label5" runat="server" >
    <div id="border"> 
         <asp:Label ID="Label4" runat="server" >
 
<p>För borttagning, markera <br/>en eller flera videoklipp <br/>och tryck på knappen nedan:</p> <br/>
  <br />

      </asp:Label>  <br />
    <asp:Button ID="DeleteButton" Text="Ta bort" runat="server" onclick="buttonDelete_Click" CausesValidation="false" /></div> </asp:Label>

     <div id="labelloginfirst" >
    <asp:Label ID="Label1" style="color: Red;"  runat="server" text="&nbsp;" ></asp:Label>
   </div>
    
     <asp:Label ID="StatusLogin" text="" runat="server" />
    <div id="success">
    <asp:Label ID="Success" text="" runat="server" Visible="false" CssClass="success"/></div>
    
   <%--Infogning av loading bild för upload--%>
  <div class="loading" align="center">
   Laddar. Vänligen vänta.<br />
    <br />
    <img src="../css/images1/Loading.gif" id="loading" />
</div>

    
    

                  

        <%--Validering--%>
    <asp:RegularExpressionValidator ID="FileUploadRegularExpressionValidator" runat="server" ErrorMessage="Filen måste vara av formaten avi, mp4, wmv, MOV, mpeg." ControlToValidate="Uploadfiles" Display="None" ValidationExpression=".*.(avi|mp4|wmv|MOV)"></asp:RegularExpressionValidator>
    <asp:RequiredFieldValidator ID="FileUploadRequiredFieldValidator" runat="server" ErrorMessage="Du måste välja minst en fil för uppladdning." Display="None" ControlToValidate="Uploadfiles"></asp:RequiredFieldValidator>
   <div id="summary">
     <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors" />
    </div>
    <div id="positions"></div>
      <div id="Div1"></div>



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
          
             <%--Checkboxrutor för Delete--%>
            
            <div id="titelposition">
            <asp:Label ID="Label2" runat="server" Text="<%# Item.Titel %>"></asp:Label>
                </div>
            <a class="player"  href='<%#"../Videos/" + Item.VidName %>'></a>
        <asp:CheckBox special="<%# Item.VidName %>" ID="Delete" Text="Delete" runat="server" />
        <asp:HyperLink ID="LinkButton" runat="server" Text="Redigera" CssClass="buttonClassredigera" NavigateUrl='<%# GetRouteUrl("edit", new { id = Item.VideoID }) %>' CausesValidation="false"/>
                       
          

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
 

    <%--Taget från http://flash.flowplayer.org/plugins/streaming/pseudostreaming.html--%>
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
   
    
    
    
     <script>
         $(document).ready(function () {
             var $Successmessagefade = $("#MainContent_Success");
             if ($Successmessagefade.length) {
                 setTimeout(function () {
                     $Successmessagefade.fadeOut();
                 }, 5000);
             }
         });

    </script>

      <script>
          $(document).ready(function () {
              var $Successmessagefade = $("#MainContent_LabelStatus");
              if ($Successmessagefade.length) {
                  setTimeout(function () {
                      $Successmessagefade.fadeOut();
                  }, 5000);
              }
          });

    </script>


</asp:Content>







  


  