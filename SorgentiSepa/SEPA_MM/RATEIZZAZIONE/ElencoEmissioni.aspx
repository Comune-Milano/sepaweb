<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoEmissioni.aspx.vb" Inherits="Contratti_ElencoSimulazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
	Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Elenco Simulazioni</title>
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server" defaultbutton="btnAnnulla" 
    defaultfocus="Label3">
    <div>
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="Label1" runat="server" Text="Elenco Emissioni Rateizzazioni"></asp:Label>
                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                            Style="z-index: 101; left: 661px; position: absolute; top: 515px" 
                        ToolTip="Home" TabIndex="2" />
                    </strong></span>
                    <br />
                    <br />
                    <table width="90%">
                        <tr>
                            <td style="width: 3px">
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 28px;
                                    position: static; top: 203px" Width="100%" TabIndex="1"></asp:Label></td>
                        </tr>
                    </table>
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
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="DaAnnullare" runat="server" />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    
</body>

</html>
