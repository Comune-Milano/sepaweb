﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaContraenteUD.aspx.vb" Inherits="Contratti_SceltaContraenteUD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
	Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Scelta Contraente</title>
</head>
<body>
<script type="text/javascript">
    function Conferma() {
        

        if (document.getElementById('scelta').value !='-1') {
        
            if (document.getElementById('scelta').value == '0') {
                var sicuro = window.confirm('Confermi la scelta PERSONA FISICA?');
            }
            else {
                var sicuro = window.confirm('Confermi la scelta PERSONA GIURIDICA?');
            }
            
            if (sicuro == true) {
                document.getElementById('prosegui').value = '1';
            }
            else {
                document.getElementById('prosegui').value = '0';
            }
        }
        else {
            alert('Selezionare una tipologia!');
            document.getElementById('prosegui').value = '0';
        }
    }


</script>
    <form id="form1" runat="server" defaultbutton="ImgProcedi" 
    defaultfocus="txtCognome">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Seleziona 
                    Tipologia Contraente </strong></span><br />
                    <br />
                    <br />
                    <a href="../cf/codice.htm" target="_blank"></a><br />
                    &nbsp;<br />
                    &nbsp;<br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <asp:RadioButton ID="chGiuridica" runat="server" 
                        style="position:absolute; top: 149px; left: 60px;" Font-Bold="True" 
                        Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="2" 
                        Text="PERSONA GIURIDICA" onclick="document.getElementById('scelta').value = '1';"/>
                    <asp:RadioButton ID="chFisica" runat="server" 
                        style="position:absolute; top: 107px; left: 60px;" Font-Bold="True" 
                        Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="1" 
                        Text="PERSONA FISICA" onclick="document.getElementById('scelta').value = '0';"/>
                    <br />
                    <br />

                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="scelta" runat="server" Value="-1" />
                    <asp:HiddenField ID="prosegui" runat="server" Value="0" />
                    <br />

                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 515px; height: 20px;" 
                        TabIndex="3" onclientclick="Conferma();" />
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 666px; position: absolute; top: 515px" 
                        ToolTip="Home" TabIndex="4" />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; left: 48px; position: absolute;
                        top: 428px; height: 30px;" Visible="False" Width="506px"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

