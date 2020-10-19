<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoApplicazioniAU.aspx.vb" Inherits="ANAUT_ElencoApplicazioniAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elenco Applicazioni A.U.</title>
    <style type="text/css">
        #elenco
        {
            height: 382px;
            width: 644px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                    &nbsp;Elenco Applicazioni A.U.</strong></span><br />
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
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <p>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 101; left: 556px; position: absolute; top: 471px" TabIndex="8" 
                ToolTip="Home" />
        </p>
                            <div id="elenco" 
        style="overflow: scroll; position:absolute; top: 50px;">
                    <asp:Label ID="lblElenco" runat="server" Font-Names="arial" Font-Size="10pt"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Names="arial" Font-Size="8pt" 
                        ForeColor="Red"></asp:Label>
    </form>

</body>
</html>