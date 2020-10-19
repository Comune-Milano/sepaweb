<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Int_Manutenzione.aspx.vb" Inherits="MANUTENZIONI_Int_Manutenzione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>INTERVENTO DI MANUTENZIONE</title>
    <script type ="text/javascript" src ="funzioni.js" ></script>

</head>
<body bgcolor="#ffffff" text="#ede0c0">

    <form id="form1" runat="server">
    <div style="text-align: left">
        <asp:Label ID="LBLINSEDIFICIO" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 16px; position: absolute; top: 107px"
            Width="48px" TabIndex="-1">Edificio</asp:Label>
        <asp:DropDownList ID="DrLEdificio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 101; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 107px" TabIndex="1"
            Width="520px">
        </asp:DropDownList>
        <asp:Label ID="LBLINSCOMP" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 102; left: 16px; position: absolute; top: 78px" TabIndex="-1">Complesso</asp:Label>
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 103; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 78px" Width="520px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbTipoIntervento" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 104; left: 396px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 249px" TabIndex="5"
            Width="228px">
        </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 105; left: 321px; position: absolute; top: 249px" Width="74px">Tipo Intervento</asp:Label>
        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 106; left: 8px; position: absolute; top: 224px"
            Width="160px" TabIndex="-1">INTERVENTO DI MANUTENZIONE</asp:Label>
        <asp:DropDownList ID="cmbTipoServizio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 107; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 248px" TabIndex="4"
            Width="212px">
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 108; left: 16px; position: absolute; top: 248px" TabIndex="-1">Tipo Servizio</asp:Label>
        <asp:DropDownList ID="cmbArticolo" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 109; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 281px" TabIndex="6"
            Width="212px">
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 110; left: 16px; position: absolute; top: 281px" TabIndex="-1">Articolo</asp:Label>
        <asp:TextBox ID="txtNote" runat="server" Style="left: 396px; position: absolute;
            top: 281px; z-index: 111;" TextMode="MultiLine" Width="223px" Height="32px" MaxLength="150" TabIndex="7"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 112; left: 321px; position: absolute; top: 281px">Descrizione</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 113; left: 13px; position: absolute; top: 322px" TabIndex="-1">Data Inizio Interv.</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 114; left: 206px; position: absolute; top: 322px">Data Fine Interv.</asp:Label>
        <hr style="left: 168px; width: 488px; position: absolute; top: 232px; z-index: 115;" />
        <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            
            Style="z-index: 117; left: 600px; position: absolute; top: 29px; height: 12px;" 
            ToolTip="Esci" TabIndex="17" />
        <asp:TextBox ID="TxtDataInizio" runat="server" Style="left: 104px; position: absolute;
            top: 322px; z-index: 118;" Width="80px" ToolTip="Data inizio (dd/Mm/YYYY)" MaxLength="10" TabIndex="8"></asp:TextBox>
        <asp:TextBox ID="txtDatFine" runat="server" Style="left: 287px; position: absolute;
            top: 322px; z-index: 119;" Width="80px" ToolTip="Data Fine (dd/Mm/YYYY)" MaxLength="10" TabIndex="9"></asp:TextBox>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 120; left: 397px; position: absolute; top: 322px">Data Ordine</asp:Label>
        <asp:TextBox ID="txtDataOrdine" runat="server" Style="left: 457px; position: absolute;
            top: 322px; z-index: 121;" Width="80px" ToolTip="Data Ordine (dd/Mm/YYYY)" MaxLength="10" TabIndex="10"></asp:TextBox>
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 122; left: 13px; position: absolute; top: 379px" TabIndex="-1">Costo Totale*</asp:Label>
        &nbsp;
        <asp:TextBox ID="txtCosto" runat="server" Style="left: 104px; position: absolute;
            top: 379px; z-index: 123;" Width="80px" MaxLength="14" ToolTip="Costo ToT" TabIndex="13"></asp:TextBox>
        <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="z-index: 124; left: 88px; position: absolute; top: 29px" ToolTip="Salva" TabIndex="16" />
        <asp:Label ID="LblRiepilogo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 125; left: 16px; position: absolute; top: 50px"
            Width="640px" Height="24px"></asp:Label>
        <asp:Label ID="LBLINSUNICOM" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 126; left: 16px; position: absolute; top: 136px"
            Width="72px" TabIndex="-1">Unità Comune</asp:Label>
        <asp:DropDownList ID="cmbUnitaComune" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 127; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 136px" TabIndex="2"
            Width="520px">
        </asp:DropDownList>
        <asp:Label ID="LBLINSUNIIMMOB" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 128; left: 16px; position: absolute; top: 166px"
            Width="80px" TabIndex="-1">Unità Immobiliare</asp:Label>
        <asp:DropDownList ID="cmbUnitaImmob" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 129; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 165px" TabIndex="3"
            Width="520px">
        </asp:DropDownList>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCosto"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 190px; position: absolute;
            top: 379px; z-index: 130;" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator>
       <a href="javascript:" onclick="history.go(document.getElementById('txtindietro').value);return false;"> <img id="IMG1" alt="Indietro" border="0" src="../NuoveImm/Img_Indietro.png" style="left: 24px;
            position: absolute; top: 29px; z-index: 131;" /></a> 
        <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 567px; position: absolute; top: 290px; background-color: transparent;"
            Width="1px">0</asp:TextBox>
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
            width: 674px; position: absolute; top: 0px; z-index: 2;">
            <tr>
                <td style="width: 670px; height: 515px; text-align: right">
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
                    <asp:Label ID="lblImpianti" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 128; left: 16px; position: absolute; top: 193px"
                        TabIndex="-1" Width="33px">Impianti</asp:Label>
                    <asp:DropDownList ID="cmbImpianti" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 129; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 194px" TabIndex="3"
            Width="520px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
        <asp:Label ID="LBLeuro" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 122; left: 541px; position: absolute; top: 381px" 
                        TabIndex="-1" Visible="False">€.</asp:Label>
                    <br />
                    &nbsp;
                    <asp:DropDownList ID="cmbReversibile" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 101; left: 207px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 379px" TabIndex="14"
            Width="168px">
                        <asp:ListItem Value="0">A carico della propriet&#224;</asp:ListItem>
                        <asp:ListItem Value="1">Reversibile</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="lblRevers" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 102; left: 399px; position: absolute; top: 379px"
                        Visible="False">Nella quota</asp:Label>
                    <asp:TextBox ID="txtCostoRevers" runat="server" Style="z-index: 103; left: 457px;
                        position: absolute; top: 377px; width: 78px;" Visible="False" 
                        MaxLength="14" TabIndex="15"></asp:TextBox>
                    <br />
        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" 
                        Style="z-index: 122; left: 190px; position: absolute; top: 382px; height: 14px; width: 6px;" 
                        TabIndex="-1">€.</asp:Label>
                    <br />
                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 104; left: 13px; position: absolute; top: 351px" TabIndex="-1">Num. Documento</asp:Label>
                    <asp:Label ID="LblEdificiAssociati" runat="server" Font-Bold="False" Font-Names="Arial"
                        Font-Size="8pt" Font-Underline="True" ForeColor="Black" Style="left: 400px; position: absolute; top: 286px" Visible="False">Edifici Associati</asp:Label>
                    &nbsp;&nbsp;
                    <div style="border-top-width: thin; border-left-width: thin; border-left-color: gray;
                        left: 585px; border-bottom-width: thin; border-bottom-color: gray; overflow: auto;
                        width: 21px; border-top-color: gray; position: absolute; top: 287px; height: 18px;
                        text-align: left; border-right-width: thin; border-right-color: gray">
                        <asp:CheckBoxList ID="ListEdifci" runat="server" EnableTheming="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Height="25px" RepeatLayout="Flow" Style="z-index: 1;
                            left: 10px; top: 12px" Width="37px">
                        </asp:CheckBoxList></div>
                    <asp:Button ID="btnSelezionaTutto" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Style="left: 505px; position: absolute; top: 285px" Text="Seleziona/Deseleziona"
                        Visible="False" Width="14px" />
                    <asp:TextBox ID="txtNumDoc" runat="server" Style="z-index: 106; left: 104px; position: absolute;
                        top: 351px;" ToolTip="Numero Documento" MaxLength="20" 
                        TabIndex="11" Width="264px"></asp:TextBox>
                    <br />
        <asp:Label ID="lblMillesimali" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Font-Underline="True" ForeColor="Black" Style="z-index: 104; left: 12px; position: absolute;
            top: 399px" Visible="False" TabIndex="-1">Tabelle Millesimali</asp:Label>
                    &nbsp;
                    <br />
                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" 
                        Style="z-index: 107; left: 396px; position: absolute; top: 351px">Num. Fattura</asp:Label>
                    <asp:TextBox ID="txtNumFattura" runat="server" Style="z-index: 108; left: 457px;
                        position: absolute; top: 351px;" ToolTip="Numero Fattura" 
                        MaxLength="20" TabIndex="12" Width="163px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:ImageButton ID="btnDettaglio" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/Search_24x24.png"
                        Style="z-index: 111; left: 641px; position: absolute; top: 415px" ToolTip="Dettagli"
                        Visible="False" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 10; left: 11px; position: absolute; top: 514px"
            Text="Label" Visible="False" Width="535px"></asp:Label>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCostoRevers"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 130; left: 560px;
            position: absolute; top: 378px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
            Width="1px"></asp:RegularExpressionValidator>
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
        <br />
        <br />
        <br />
        <br />
        <div style="z-index: 11; left: 12px; width: 628px; position: absolute; top: 415px;
            height: 101px; overflow: auto;">
            <asp:Label ID="lblTitle" runat="server" Font-Names="Courier New" Font-Size="8pt" 
                ForeColor="Black" 
                Text="&nbsp;&nbsp;&nbsp;TIPOLOGIA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DESCRIZIONE&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DESCRIZIONE TABELLA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;COD.EDIF.&nbsp;&nbsp;&nbsp;&nbsp;DEN.EDIFICIO" 
                Width="1086px" Font-Bold="True" Visible="False"></asp:Label>
            <asp:RadioButtonList ID="RdbMillesimali" runat="server" Font-Names="Courier New" Font-Size="8pt"
                ForeColor="Black" Width="1198px" style="left: 84px; top: 620px">
            </asp:RadioButtonList></div>
    
    </div>
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
</body>
</html>
