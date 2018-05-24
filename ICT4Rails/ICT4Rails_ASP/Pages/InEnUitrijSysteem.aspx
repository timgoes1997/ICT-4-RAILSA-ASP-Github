<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InEnUitrijSysteem.aspx.cs" Inherits="ICT4Rails_ASP.InEnUitrijSysteem" MasterPageFile="../MasterPage.Master" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">

    <div class="row jumbotron">
        <div class="col-md-4">
            <h4 class="form-signin-heading">Tram invoeren</h4>
            <asp:Label ID="Label1" runat="server" Text="Tramnummer" />
            <br />
            <asp:DropDownList ID="ddlTramInvoerenTramnummer" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnTramInvoeren" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnTramInvoeren_Click" />
        </div>

        <div class="col-md-4">
            <h4 class="form-signin-heading">Tramstatus aanpassen</h4>
            <asp:Label ID="lbUnused2" runat="server" Text="Tramnummer"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlTramstatusAanpassenTramnummer" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
            <br />
            <asp:Label ID="lbUnused3" runat="server" Text="Status"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlTramstatusAanpassenStatus" class="btn btn-default dropdown-toggle" runat="server">
                <asp:ListItem>vervuild</asp:ListItem>
                <asp:ListItem>defect</asp:ListItem>
                <asp:ListItem>beschikbaar</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnTramstatusAanpassen" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnTramstatusAanpassen_Click" />
        </div>
    </div>

    <div class="jumbotron">
        <h3 class="form-signin-heading">Trams</h3>
        <asp:GridView ID="gvTrams" CssClass="table table-hover table-striped" runat="server"></asp:GridView>
    </div>

</asp:Content>
