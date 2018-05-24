<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Schoonmaak.aspx.cs" Inherits="ICT4Rails_ASP.Schoonmaak" MasterPageFile="../MasterPage.Master" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">

    <div class="row jumbotron">
        <div class="col-md-4">
            <h4 class="form-signin-heading">Schoonmaak afronden</h4>
            <asp:Label ID="Label1" runat="server" Text="Tramnummer" />
            <br />
            <asp:DropDownList ID="ddlSchoonmaakAfrondenTramnummer" class="btn btn-default dropdown-toggle" runat="server" />
            <br />
            <br />
            <asp:Button ID="btnSchoonmaakAfronden" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnSchoonmaakAfronden_Click" />
        </div>

        <div class="col-md-4">
            <h4 class="form-signin-heading">Schoonmaak Toevoegen</h4>
            <asp:Label ID="Label2" runat="server" Text="Tramnummer" />
            <br />
            <asp:DropDownList ID="ddlSchoonmaakToevoegenTramNummer" class="btn btn-default dropdown-toggle" runat="server" />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Schoonmaaktype" />
            <br />
            <asp:DropDownList ID="ddlSchoonmaakToevoegenSchoonmaakType" class="btn btn-default dropdown-toggle" runat="server" >
                <asp:ListItem>GroteSchoonmaakBeurt</asp:ListItem>
                <asp:ListItem>KleineSchoonmaakBeurt</asp:ListItem>
            </asp:DropDownList>
            <br />
            
            <br />
            <asp:Button ID="btnSchoonmaakToevoegen" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnSchoonmaakToevoegen_Click" />
        </div>
    </div>
    
    <div class="jumbotron">
            <h3 class="form-signin-heading">Schoonmaaklijsten</h3>
            <asp:GridView ID="gvSchoonmaakLijsten" CssClass="table table-hover table-striped" runat="server"></asp:GridView>
        </div>

</asp:Content>



