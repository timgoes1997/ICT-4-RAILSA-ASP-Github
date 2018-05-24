
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Beheer.aspx.cs" Inherits="ICT4Rails_ASP.Beheer" MasterPageFile="../MasterPage.Master" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">

    <div class="jumbotron">
        <h3 class="form-signin-heading">Remise
        </h3>
        <asp:Button ID="btnStopTimer" class="btn btn-default" runat="server" Text="Stop de timer" OnClick="btnStopTimer_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="btnStartTimer" class="btn btn-default" runat="server" Text="Start de timer" OnClick="btnStartTimer_Click" />
        <br />
        <asp:Timer ID="timerTimer" runat="server" OnTick="indeelTimer_Tick" Interval="2000">
        </asp:Timer>
        <asp:ScriptManager ID="smTimer" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upTimer" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lbTimerPlaceholder" runat="server" Text="0"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div style="overflow-x:auto; ">
        <asp:GridView ID="gvRemise" CssClass="table" runat="server" OnSelectedIndexChanged="gvRemise_SelectedIndexChanged">
            
        </asp:GridView>
            </div>
        <br/>
        <asp:Label ID="lbSectorBezet" runat="server" Text="Sector is bezet"></asp:Label>
        <br/>
        <asp:Label ID="lbSectorGeblokkeerd" runat="server" Text="Sector is geblokkeerd"></asp:Label>
        <br/>
        <asp:Label ID="lbSectorLeeg" runat="server" Text="Sector staat leeg"></asp:Label>
        <br/>
        <asp:Label ID="lbSectorBestaatNiet" runat="server" Text="Sector bestaat niet"></asp:Label>
        <br />
        <asp:Label ID="lbSectorNietBeschikbaar" runat="server" Text="Sector niet beschikbaar"></asp:Label>
        <br />
        <asp:Label ID="lbSectorGereserveerd" runat="server" Text="Sector gereserveerd"></asp:Label>
    &nbsp;&nbsp;&nbsp;
    </div>

    <div class="row jumbotron">
        <div class="col-md-4">
            <h4 class="form-signin-heading">Tram toevoegen</h4>
            <br />
            <asp:Label ID="lbUnused1" runat="server" Text="Nummer"></asp:Label>
            <br />
            <asp:TextBox ID="tbTramToevoegen" class="form-control textbox-width" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lbUnused2" runat="server" Text="Vertrektijd"></asp:Label>
            <br />
            HH (bijv. 23)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; MM (bijv. 59)<br />
            <asp:TextBox ID="tbTramToevoegenVertrektijdUren" runat="server"></asp:TextBox>
            :<asp:TextBox ID="tbTramToevoegenVertrektijdMinuten" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lbUnused3" runat="server" Text="Type"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlTramToevoegen" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnTramToevoegen" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnTramToevoegen_Click" />
        </div>

        <div class="col-md-4">
            <h4 class="form-signin-heading">Tram verwijderen</h4>
            <br />
            <asp:Label ID="lbUnused5" runat="server" Text="Tramnummer"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlTramVerwijderen" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnTramVerwijderen" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnTramVerwijderen_Click" />
        </div>

        <div class="col-md-4">
            <h4 class="form-signin-heading">Tram verplaatsen</h4>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Tramnummer"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlTramVerplaatsenTram" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Spoor"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlTramVerplaatsenSpoor" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTramVerplaatsenSpoor_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Sector"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlTramVerplaatsenSector" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnTramVerplaatsen" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnTramVerplaatsen_Click" />
        </div>
    </div>

    <div class="row jumbotron">
        <div class="col-md-4">
            <h4 class="form-signin-heading">Tramstatus aanpassen</h4>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Tramnummer"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlTramStatusAanpassenTramnummer" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Status"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlTramStatusAanpassenStatus" class="btn btn-default dropdown-toggle" runat="server">
                <asp:ListItem>vervuild</asp:ListItem>
                <asp:ListItem>defect</asp:ListItem>
                <asp:ListItem>beschikbaar</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnTramStatusAanpassen" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnTramStatusAanpassen_Click" />
        </div>

        <div class="col-md-4">
            <h4 class="form-signin-heading">Spoor reserveren</h4>
            <asp:Label ID="Label6" runat="server" Text="Spoor"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlSpoorReserverenSpoor" class="btn btn-default dropdown-toggle" runat="server" OnSelectedIndexChanged="ddlSectorReserverenSpoor_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="Label8" runat="server" Text="Tramnummer"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlSpoorReserverenTram" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnSpoorReserveren" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnSpoorReserveren_Click"/>
        </div>

        <div class="col-md-4">
            <h4 class="form-signin-heading">Sector blokkeren/deblokkeren</h4>
            <asp:Label ID="Label7" runat="server" Text="Spoor"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlSectorBlokkerenSpoor" AutoPostBack="True" class="btn btn-default dropdown-toggle" runat="server" OnSelectedIndexChanged="ddlSectorBlokkerenSpoor_SelectedIndexChanged"></asp:DropDownList>
            <br/>
            <asp:Label ID="Label9" runat="server" Text="Sector"></asp:Label>

            <br />
            <asp:DropDownList ID="ddlSectorBlokkerenSector" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnSectorBlokkerenDeblokkeren" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnSectorBlokkerenDeblokkeren_Click1" />
        </div>

        <div class="col-md-4">
            <br/>
            <br/>
        <h4 class="form-signin-heading">Spoor blokkeren/deblokkeren</h4>
        <asp:Label ID="Label10" runat="server" Text="Spoor"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlSpoorBlokkeren" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnSpoorBlokkerenDeblokkeren" class="btn btn-default" runat="server" Text="Bevestig" OnClick="btnSpoorBlokkerenDeblokkeren_Click" />
    </div>
    </div>

    <div class="jumbotron">
        <h3 class="form-signin-heading">Reserveringen</h3>
        <asp:GridView ID="gvReserveringen" CssClass="table table-hover table-striped" runat="server"></asp:GridView>
    </div>

</asp:Content>
