<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reparatie.aspx.cs" Inherits="ICT4Rails_ASP.Reparatie" MasterPageFile="../MasterPage.Master" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">

    <div class="row jumbotron">
        <div class="col-md-4">
        <h4 class="form-signin-heading">Reparatie afronden</h4>
        <asp:Label ID="Label1" runat="server" Text="Tramnummer"/>
        <br/>
        <asp:DropDownList ID="ddlReparatieAfronden" class="btn btn-default dropdown-toggle" runat="server"/>
        <br/>
        <br/>
        <asp:Button ID="btnReparatieAfronden" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnReparatieAfronden_Click" />
    </div>

    <div class="col-md-4">
        <h4 class="form-signin-heading">Servicebeurt Toevoegen</h4>
        <asp:Label ID="Label2" runat="server" Text="Tramnummer"/>
        <br/>
        <asp:DropDownList ID="ddlServicebeurtToevoegenTramNummer" class="btn btn-default dropdown-toggle" runat="server"/>
        <br/>
        <asp:Label ID="Label3" runat="server" Text="Type"/>
        <br/>
        <asp:DropDownList ID="ddlServicebeurtToevoegenType" class="btn btn-default dropdown-toggle" runat="server">
            <asp:ListItem>GroteServiceBeurt</asp:ListItem>
            <asp:ListItem>KleineServiceBeurt</asp:ListItem>
        </asp:DropDownList>

        <br />

        <br/>
        <asp:Button ID="btnServicebeurtToevoegen" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnServicebeurtToevoegen_Click" />
    </div>
        </div>

    <div class="jumbotron">
        <h3 class="form-signin-heading">Reparatielijsten</h3>
        <asp:GridView ID="gvReparatieLijsten" CssClass="table table-hover table-striped" runat="server"></asp:GridView>
    </div>

</asp:Content>