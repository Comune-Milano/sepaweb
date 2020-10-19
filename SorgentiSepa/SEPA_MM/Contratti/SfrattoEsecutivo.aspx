<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SfrattoEsecutivo.aspx.vb" Inherits="Contratti_SfrattoEsecutivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body >
    <form id="form1" runat="server">
    
      
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        Style="position:absolute; z-index: 500; left: 9px; top: 18px;" 
                        ForeColor="#CC3300">Dati Sfratto Esecutivo</asp:Label>
        <table style="width: 100%; background-image: url('../NuoveImm/SfondoMascheraRubrica.jpg'); background-repeat: no-repeat; position: absolute; top: -1px; left: 1px;z-index:200" >
            <tr>
                <td >
                    </td>
                <td style="width: 115px">
                    &nbsp;</td>
                <td style="width: 112px">
                    &nbsp;</td>
                <td style="width: 112px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td >
                    &nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td style="width: 115px">
                    &nbsp;</td>
                <td style="width: 112px">
                    &nbsp;</td>
                <td style="width: 112px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 52px; top: 121px" Width="124px">Data Convalida Sfratto</asp:Label></td>
                <td style="width: 115px">
                    <asp:TextBox ID="txtDataConvSfratto" runat="server" Font-Names="Arial" Font-Size="10pt"
                        Width="85px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataConvSfratto"
                        ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px;
                        top: 158px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="2px"></asp:RegularExpressionValidator></td>
                <td style="width: 112px">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 52px; top: 121px" Width="115px">Sfratto esecutivo dal</asp:Label></td>
                <td style="width: 112px">
                    <asp:TextBox ID="txtDataEsecuzioneSfratto" runat="server" Font-Names="Arial" Font-Size="10pt"
                        Width="85px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataEsecuzioneSfratto"
                        ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px;
                        top: 158px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="2px"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 52px; top: 121px" Width="124px">Data Conferma F.P.</asp:Label></td>
                <td style="width: 115px; height: 26px;">
                    <asp:TextBox ID="TxtDataConfFP" runat="server" Font-Names="Arial" Font-Size="10pt" Width="85px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtDataConfFP"
                        ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px;
                        top: 158px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="2px"></asp:RegularExpressionValidator></td>
                <td style="width: 112px; height: 26px;">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 52px; top: 121px" Width="63px">Data Rinvio</asp:Label></td>
                <td style="width: 112px; height: 26px;">
                    <asp:TextBox ID="txtDataRinvioSfratto" runat="server" Font-Names="Arial" Font-Size="10pt"
                        Width="85px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataRinvioSfratto"
                        ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 2; left: 173px;
                        top: 158px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="2px"></asp:RegularExpressionValidator>&nbsp;</td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td style="width: 115px; height: 26px;">
                    &nbsp; &nbsp;</td>
                <td style="width: 112px; height: 26px;">
                    &nbsp; &nbsp;</td>
                <td style="width: 112px; height: 26px;">
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td >
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td style="width: 115px; height: 26px;">
                    &nbsp;</td>
                <td style="width: 112px; height: 26px;">
                    <asp:ImageButton 
                        ID="ImgButSave" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                
                Style="z-index: 102;  cursor: pointer; text-align: right; " 
                ToolTip="Salva"  TabIndex="23" 
                        
                        
                        onclientclick="alert('Attenzione, la memorizzazione dei dati sarà effettiva solo dopo aver premuto il pulsante SALVA della maschera principale! PerAnnullare, uscire senza salvare.');window.opener.document.form1.txtModificato.value = '1';" /></td>
                <td style="width: 112px; height: 26px;">
                    <asp:ImageButton ID="ImgButEsci" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                
                Style="z-index: 102;  cursor: pointer; text-align: right; " 
                ToolTip="Esci"  TabIndex="24" /></td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td style="width: 115px; height: 26px;">
                    &nbsp;</td>
                <td style="width: 112px; height: 26px;">
                    &nbsp;</td>
                <td style="width: 112px; height: 26px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td >
                    &nbsp;&nbsp;
                    &nbsp;</td>
                <td style="width: 115px; height: 26px;">
                    &nbsp;</td>
                <td style="width: 112px; height: 26px;">
                    &nbsp;</td>
                <td style="width: 112px; height: 26px;">
                    &nbsp;</td>
            </tr>
        </table>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Style="right: 406px; left: 8px; position: absolute; top: 159px"
                            Text="Label" Visible="False" Width="386px"></asp:Label>
        
    </form>
       <script type ="text/javascript" >

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

    <br />
    <br />
    <br />
                    <br />
                    <br />
                    <br />
                    <br />
    <br />
</body>
</html>
