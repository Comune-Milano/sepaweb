<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DateCompensi.aspx.vb" Inherits="Contabilita_DateCompensi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            width: 773px;
            height: 534px;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat">
    <form id="form1" runat="server">
        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
            ForeColor="#660000" Text="Ricerca Condomini per Inquilino" Width="758px" 
            TabIndex="9"></asp:Label>
        <p>
        <strong><span style="font-size: 10pt; color: #801f1c; font-family: Arial">
            <asp:Label ID="lblSottotitolo" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt"
                ForeColor="#0066FF" Style="z-index: 10; left: 16px; width: 449px; top: 56px; height: 19px; position: absolute;"
                
            
            Text="Definire l'intervallo di tempo per eseguire l'operazione"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 50px; position: absolute; top: 101px" Width="19px">Dal</asp:Label>
            &nbsp;
                        <asp:Label ID="Label12" runat="server" Font-Bold="False" 
                Font-Names="Arial" Font-Size="8pt"
                            
                Style="z-index: 102; left: 51px; position: absolute; top: 148px" Width="6px">al</asp:Label>
            &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
            
                Style="z-index: 106; left: 532px; position: absolute; top: 272px; bottom: 291px;" 
                ToolTip="Esci" TabIndex="4" />
        <asp:ImageButton ID="btnCalcola" runat="server" ImageUrl="IMMCONTABILITA/Img_Calcola.png"
            
                Style="z-index: 106; left: 448px; position: absolute; top: 271px; bottom: 316px;" 
                ToolTip="Calcola" TabIndex="3" />
            </span></strong>
        </p>
    		<script type="text/javascript">
            function CompletaData(e, obj) {
                // Check if the key is a number
                var sKeyPressed;

                sKeyPressed = (window.event) ? event.keyCode : e.which;

                if (sKeyPressed < 48 || sKeyPressed > 57) {
                    if (sKeyPressed != 8 && sKeyPressed != 0) {
                        // don't insert last non-numeric character
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
                else {
                    if (obj.value.length == 2) {
                        obj.value += "/";
                    }
                    else if (obj.value.length == 5) {
                        obj.value += "/";
                    }
                    else if (obj.value.length > 9) {
                        var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                        if (selText.length == 0) {
                            // make sure the field doesn't exceed the maximum length
                            if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            }
                        }
                    }
                }
            }
    </script>
        <p>
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 334px" Visible="False" Width="525px"></asp:Label>
            <asp:DropDownList ID="cmbMeseInizio" runat="server" 
                            style="position:absolute; left: 81px; top: 100px; width: 140px;">
            </asp:DropDownList><asp:DropDownList ID="cmbMeseFine" runat="server" 
                            style="position:absolute; left: 81px; top: 148px; width: 140px;">
            </asp:DropDownList>
            <asp:DropDownList ID="cmbAnnoFine" runat="server"  Style="left: 241px;
                position: absolute; top: 148px" TabIndex="1" Width="110px">
            </asp:DropDownList>
            <asp:DropDownList ID="cmbAnnoInizio" runat="server"  Style="left: 241px;
                position: absolute; top: 100px" TabIndex="1" Width="110px">
            </asp:DropDownList>
        </p>
    </form>
    		</body>
</html>
