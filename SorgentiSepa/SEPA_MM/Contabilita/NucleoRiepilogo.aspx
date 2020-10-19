<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NucleoRiepilogo.aspx.vb" Inherits="Contabilita_NucleoRiepilogo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nucleo Familiare</title>
</head>
<body >
    <form id="frmRiep" runat="server" >
    <div>
        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;
        <table style="left: 0px; 
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="left: 0px; 
                    width: 800px; position: absolute; top: 0px; height: 483px">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp;<asp:Label ID="LblIntestazione"
                            runat="server" 
                        Text="Composizione del Nucleo Familiare in base all'ultima Anagrafe Utenza"></asp:Label></span></strong><br />
<div style="left: 13px; width: 781px; position: absolute; top: 57px; height: 416px">
    <table width="100%">
        <tr>
            <td>
                &nbsp;<asp:Label ID="LblTitolo" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="10pt" ForeColor="Black" Text="NUCLEO FAMILIARE" Width="199px"></asp:Label>
                <br />
                &nbsp;
                <table width="100%">
                    <tr>
                        <td style="vertical-align: top; height: 41px;">
                            <asp:Label ID="TBL_NUCLEO" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                Width="100%" TabIndex="24"></asp:Label></td>
        </tr>
    </table>
    </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    &nbsp;
</div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<br />
                    &nbsp;
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 6px; position: absolute; top: 467px;"
                        Text="Label" Visible="False" Width="719px"></asp:Label>
                    <br />
                    <br />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="IdUtenzaCompNucleo" runat="server" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
