<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Servizio_Manut.aspx.vb" Inherits="MANUTENZIONI_Servizio_Manut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>INTERVENTO DI MANUTENZIONE</title>
    <script type ="text/javascript" src ="funzioni.js" >function IMG1_onclick() {

}

</script>

</head>
<body bgcolor="#ffffff" text="#ede0c0">

<script type ="text/jscript" >
function CompletaData(e,obj) {
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
    <form id="form1" runat="server">
    <div style="text-align: left">
        <asp:Label ID="LBLINSEDIFICIO" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 16px; position: absolute; top: 128px"
            Width="48px">Edificio</asp:Label>
        <asp:DropDownList ID="DrLEdificio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 101; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 128px" TabIndex="5"
            Width="520px">
        </asp:DropDownList>
        <asp:Label ID="LBLINSCOMP" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 102; left: 16px; position: absolute; top: 96px">Complesso</asp:Label>
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 103; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 96px" TabIndex="5" Width="520px">
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 106; left: 8px; position: absolute; top: 224px"
            Width="41px">SERVIZIO</asp:Label>
        <asp:DropDownList ID="cmbTipoServizio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 107; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 248px" TabIndex="5"
            Width="520px">
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 108; left: 16px; position: absolute; top: 248px">Tipologia</asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="txtNote" runat="server" Style="left: 104px; position: absolute;
            top: 278px; z-index: 111;" TextMode="MultiLine" Width="515px" Height="31px" MaxLength="150"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 112; left: 16px; position: absolute; top: 278px">Descrizione</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 113; left: 13px; position: absolute; top: 322px">Data Inizio Interv.</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 114; left: 206px; position: absolute; top: 322px">Data Fine Interv.</asp:Label>
        <hr style="left: 58px; width: 598px; position: absolute; top: 232px; z-index: 115;" />
        <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="z-index: 117; left: 600px; position: absolute; top: 29px" ToolTip="Esci" />
        <asp:TextBox ID="TxtDataInizio" runat="server" Style="left: 104px; position: absolute;
            top: 322px; z-index: 118;" Width="80px" ToolTip="Data Inizio (dd/Mm/YYYY)" MaxLength="10"></asp:TextBox>
        <asp:TextBox ID="txtDatFine" runat="server" Style="left: 287px; position: absolute;
            top: 322px; z-index: 119;" Width="80px" ToolTip="Data Fine (dd/Mm/YYYY)" MaxLength="10"></asp:TextBox>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 120; left: 397px; position: absolute; top: 322px">Data Ordine</asp:Label>
        <asp:TextBox ID="txtDataOrdine" runat="server" Style="left: 457px; position: absolute;
            top: 322px; z-index: 121;" Width="80px" ToolTip="Data Ordine (dd/Mm/YYYY)" MaxLength="10"></asp:TextBox>
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 122; left: 13px; position: absolute; top: 381px">Costo Totale *</asp:Label>
        <asp:TextBox ID="txtCosto" runat="server" Style="left: 104px; position: absolute;
            top: 381px; z-index: 123;" Width="80px" MaxLength="14" CausesValidation="True"></asp:TextBox>
        <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="z-index: 124; left: 88px; position: absolute; top: 29px" ToolTip="Salva" />
        <asp:Label ID="LblRiepilogo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 125; left: 16px; position: absolute; top: 58px"
            Width="640px" Height="24px"></asp:Label>
        <asp:Label ID="LBLINSUNICOM" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 126; left: 16px; position: absolute; top: 160px"
            Width="72px">Unità Comune</asp:Label>
        <asp:DropDownList ID="cmbUnitaComune" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 127; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 160px" TabIndex="5"
            Width="520px">
        </asp:DropDownList>
        <asp:Label ID="LBLINSUNIIMMOB" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 128; left: 16px; position: absolute; top: 192px"
            Width="80px">Unità Immobiliare</asp:Label>
        <asp:DropDownList ID="cmbUnitaImmob" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 129; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 192px" TabIndex="5"
            Width="520px">
        </asp:DropDownList>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCosto"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 190px; position: absolute;
            top: 381px; z-index: 130;" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator>
       <a href="javascript:" onclick="history.go(document.getElementById('txtindietro').value);return false;"> <img id="IMG1" alt="Indietro" border="0" src="../NuoveImm/Img_Indietro.png" style="left: 24px;
            position: absolute; top: 29px; z-index: 131;"/></a> 
        <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 114px; position: absolute; top: 284px; background-color: transparent;"
            Width="4px">0</asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtDataInizio"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 189px; position: absolute;
            top: 323px; z-index: 133;" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
            Width="1px"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDatFine"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 372px; position: absolute;
            top: 322px; z-index: 134;" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
            Width="1px"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataOrdine"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 543px; position: absolute;
            top: 322px; z-index: 135;" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
            Width="1px"></asp:RegularExpressionValidator>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
            width: 674px; position: absolute; top: 0px">
            <tr>
                <td style="width: 670px; height: 514px; text-align: right">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
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
                    &nbsp;
                    <asp:DropDownList ID="cmbReversibile" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 101; left: 207px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 381px" TabIndex="5"
            Width="166px">
                        <asp:ListItem Value="0">A carico della propriet&#224;</asp:ListItem>
                        <asp:ListItem Value="1">Reversibile</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="lblRevers" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 102; left: 397px; position: absolute; top: 381px"
                        Visible="False">Nella quota</asp:Label>
                    <asp:TextBox ID="txtCostoRevers" runat="server" Style="z-index: 103; left: 457px;
                        position: absolute; top: 381px" Visible="False" Width="80px" MaxLength="14"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 104; left: 13px; position: absolute; top: 351px">Num. Documento</asp:Label>
                    <asp:Label ID="LblEdificiAssociati" runat="server" Font-Bold="False" Font-Names="Arial"
                        Font-Size="8pt" Font-Underline="True" ForeColor="Black" Style="left: 493px; position: absolute; top: 284px" Visible="False" Width="4px">Edifici Associati</asp:Label>
                    &nbsp;&nbsp;
                    <div style="border-top-width: thin; border-left-width: thin; border-left-color: gray;
                        left: 574px; border-bottom-width: thin; border-bottom-color: gray; overflow: auto;
                        width: 13px; border-top-color: gray; position: absolute; top: 286px; height: 23px;
                        text-align: left; border-right-width: thin; border-right-color: gray">
                        <asp:CheckBoxList ID="ListEdifci" runat="server" EnableTheming="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Height="25px" RepeatLayout="Flow" Style="z-index: 1;
                            left: 10px; top: 12px" Width="506px">
                        </asp:CheckBoxList></div>
                    <asp:Button ID="btnSelezionaTutto" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Style="left: 552px; position: absolute; top: 284px" Text="Seleziona/Deseleziona"
                        Visible="False" Width="3px" />
                    <asp:TextBox ID="txtNumDoc" runat="server" Style="z-index: 106; left: 104px; position: absolute;
                        top: 351px" ToolTip="dd/Mm/YYYY" Width="264px" MaxLength="20"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 107; left: 396px; position: absolute; top: 351px">Num. Fattura</asp:Label>
                    <asp:TextBox ID="txtNumFattura" runat="server" Style="z-index: 108; left: 457px;
                        position: absolute; top: 351px" ToolTip="dd/Mm/YYYY" Width="161px" MaxLength="20"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 10; left: 131px; position: absolute; top: 506px"
            Text="Label" Visible="False" Width="535px"></asp:Label>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCostoRevers"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 130; left: 544px;
            position: absolute; top: 382px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
            Width="1px"></asp:RegularExpressionValidator>
        <div style="z-index: 11; left: 12px; overflow: auto; width: 620px; position: absolute;
            top: 416px; height: 100px">
            <asp:RadioButtonList ID="RdbMillesimali" runat="server" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="596px">
            </asp:RadioButtonList></div>
        <asp:Label ID="LblMillesimali" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Font-Underline="True" ForeColor="Black" Style="z-index: 104; left: 12px; position: absolute;
            top: 401px" Visible="False">Tabelle Millesimali</asp:Label>
    
    </div>
    </form>
</body>
</html>
