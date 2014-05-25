<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditVideo.aspx.cs" Inherits="WebApplication2.Pages.EditVideo" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="marginTitle">
    <h1>Redigera</h1></div>

    
        <%-- Formview för videoklipp (hämtas via databas) --%>
     <asp:FormView ID="VideoEditFormView" runat="server"
                ItemType="WebApplication2.Model.Video"
                SelectMethod="VideoEditFormView_GetItem"
                DataKeyNames="VideoID"
                DefaultMode="Edit"
                UpdateMethod="VideoEditFormView_UpdateItem">
   
                <EditItemTemplate>
                     
                <div id ="positionedit">

           <a class="player"  href='<%#"../../Videos/" + Item.VidName %>'></a>

                </div>
                  
                    <div id="titelboxedit">
                      <asp:Label ID="Label2" runat="server" Text="Rubrik"></asp:Label>
           <asp:TextBox ID="Titel" runat="server" Text="<%# BindItem.Titel %>" MaxLength="30" /><br>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="En Titel måste anges." ControlToValidate="Titel" Display="None" ></asp:RequiredFieldValidator>
           <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors" /> </div>
                    
                        
    <%--DropdownList innehållande kategorivalen hämtade från databasen--%>
    <asp:DropDownList ID="DropDownCategory" runat="server"
                    SelectMethod="DropDownCategory_GetData"
                    DataTextField="Kategori"
                    SelectedValue="<%# BindItem.KategoriID %>"
                    DataValueField="KategoriID"/>  <br /><br />
                   

                         
             <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Spara" CssClass="buttonClassSpara" CausesValidation="false"  />
                         <asp:HyperLink ID="LinkButton" runat="server" Text="Avbryt" CssClass="buttonClassAvbryt" NavigateUrl='<%# GetRouteUrl("upload") %>' CausesValidation="false"/>
         

               
                      
           </EditItemTemplate>
               
            </asp:FormView>
        <script src="../../../FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        flowplayer("a.player", "../../../FlowPlayer/flowplayer-3.2.16.swf", {
            plugins: {
                pseudo: { url: "../../../FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
            },
            clip: { provider: 'pseudo', autoPlay: false, autoBuffering: true },
        });
    </script>

</asp:Content>
