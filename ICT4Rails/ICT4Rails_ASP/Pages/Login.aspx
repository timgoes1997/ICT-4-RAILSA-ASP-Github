<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ICT4Rails_ASP.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery-2.1.4.min.js"></script>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <title></title>
</head>
<body>
    <div class="container">
        <form id="formLogin" class="form-signin" runat="server">
            <h2 class="form-signin-heading">Please sign in</h2>
            <label for="tbGebruikersnaam" class="sr-only">Gebruikersnaam</label>
            <asp:TextBox ID="tbGebruikersnaam" runat="server" class="form-control" placeholder="Gebruikersnaam" CssClass="form-control" />

            <label for="tbWachtwoord" class="sr-only">Password</label>
            <asp:TextBox vk_11d55="subscribed" type="password" class="form-control" placeholder="Password" ID="tbWachtwoord" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvWachtwoordInvoeren" runat="server" ControlToValidate="tbWachtwoord" ErrorMessage="U moet een wachtwoord invoeren om te kunnen inloggen!" ForeColor="Red"></asp:RequiredFieldValidator>

            <asp:Button class="btn btn-lg btn-primary btn-block" type="submit" ID="btnLogin" runat="server" Text="Inloggen" OnClick="btnLogin_Click" />
            <asp:Label ID="lblInlogMislukt" runat="server" ForeColor="Red" Text="Het inloggen is mislukt, mogelijk heb je een verkeerd wachtwoord/email ingevult of is je account nog niet geactiveerd!" Visible="False"></asp:Label>
        </form>
    </div>
</body>
</html>