<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaOccupante.aspx.vb" Inherits="CALL_CENTER_RicercaOccupante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>

<head id="Head1" runat="server">
    <title>Ricerca U.i. per Occipante</title>
    <style type="text/css">
        .CssMaiuscolo { TEXT-TRANSFORM: uppercase;}

    
        .style3
        {
            width: 352px;
            height: 10px;
        }
        .style4
        {
            width: 27px;
            height: 10px;
        }
        .style5
        {
            width: 352px;
            height: 18px;
        }
        .style6
        {
            width: 27px;
            height: 18px;
        }
    
    
    </style>
</head>
<body >
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="btnCerca">
    <div>
        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 33px; position: absolute; top: 134px">Cognome</asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            Style="z-index: 100; left: 33px; position: absolute; top: 100px; right: 1225px;">Edificio/Via</asp:Label>
        <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" 
            BorderWidth="1px" CssClass="CssMaiuscolo" Style="z-index: 102;
            left: 99px; position: absolute; top: 131px" Font-Names="arial" 
            Font-Size="10pt" Width="173px" TabIndex="1"></asp:TextBox>
        &nbsp;&nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 798px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Occupante</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                <img id="Img1" alt="Aiuto Ricerca per Denominazione Complesso" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
                    src="Immagini/Search_24x24.png" style="left: 699px; cursor: pointer; position: absolute;
                    top: 100px" /><br />
                    <br />
                    <br />
                        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" 
                        Style="left: 15px; position: absolute; top: 440px; width: 749px;" Text="Label"
                            Visible="False"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                         <div id="AiutoRicerca" 
                        style="z-index: 200; left: 299px; width: 402px; position: absolute;
                            top: 104px; height: 365px; background-color: transparent; visibility: hidden;">
                            <br />
                            <div style="width: 60px; height: 136px; background-color: silver">
                                <table style="width: 395px; height: 335px; background-color: silver">
                                    <tr>
                                        <td class="style3" style="vertical-align: top; text-align: left">
                                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" 
                                                Width="190px">Denominazione Via</asp:Label>
                                        </td>
                                        <td class="style4" style="vertical-align: baseline; text-align: left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5" style="vertical-align: top; text-align: left">
                                            <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                                ToolTip="Approssimare la ricerca per questo indirizzo" Width="343px"></asp:TextBox>
                                        </td>
                                        <td class="style6" style="vertical-align: top; text-align: left">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/text_view.png"
                                                Style="z-index: 111; left: 246px; top: 50px" ToolTip="Cerca Per Approssimazione" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; width: 352px; height: 104px; text-align: left" >
                                            <asp:Label ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label>
                                            <br />
                                            <div style="left: 5px; overflow: auto; width: 365px; top: 87px; height: 243px">
                                                <asp:RadioButtonList ID="ListEdifci" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="Black" Width="340px">
                                                </asp:RadioButtonList>
                                            </div>
                                        </td>
                                        <td style="vertical-align: bottom; width: 27px; height: 104px; text-align: left"
                                            valign="bottom">
                                            <asp:ImageButton ID="BtnConferma" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/Next_24x24.png"
                                                OnClientClick="myOpacity.toggle();" Style="z-index: 111; left: 268px; top: 190px"
                                                ToolTip="Conferma" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

    <asp:DropDownList ID="cmbEdificio" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="10pt" Style="border: 1px solid black; z-index: 111;
        left: 99px; position: absolute; top: 102px; width: 600px;" TabIndex="1">
    </asp:DropDownList>
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
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 667px; position: absolute; top: 505px" 
            TabIndex="7" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 525px; position: absolute; top: 505px" 
            TabIndex="6" ToolTip="Avvia Ricerca" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 33px; position: absolute; top: 164px">Nome</asp:Label>
        <asp:TextBox ID="txtNome" runat="server" CssClass="CssMaiuscolo" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 99px; position: absolute; top: 161px"
            TabIndex="2" Width="173px"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            
            
            Style="z-index: 100; left: 33px; position: absolute; top: 195px; height: 14px; width: 63px;">Cod. Fiscale</asp:Label>
        <asp:TextBox ID="txtCF" runat="server" CssClass="CssMaiuscolo" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 99px; position: absolute; top: 192px"
            TabIndex="3" Width="173px"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            
            Style="z-index: 100; left: 33px; position: absolute; top: 226px;  height: 15px;">Rag. Sociale</asp:Label>
        <asp:TextBox ID="txtRS" runat="server" CssClass="CssMaiuscolo" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 99px; position: absolute; top: 223px"
            TabIndex="4" Width="173px"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            
            
            Style="z-index: 100; left: 33px; position: absolute; top: 256px; width: 23px;">P.Iva</asp:Label>
        <asp:TextBox ID="txtIva" runat="server" CssClass="CssMaiuscolo" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 99px; position: absolute; top: 254px"
            TabIndex="5" Width="173px"></asp:TextBox>
    
        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"         
            Style="z-index: 100; left: 15px; position: absolute; top: 64px; height: 16px; width: 504px;">Inserire gli estremi per identificare il titolare del contratto di locazione</asp:Label>
    
    </div>
                    <asp:HiddenField ID="TextBox1" runat="server" />

    </form>
        <script type="text/javascript">

            myOpacity = new fx.Opacity('AiutoRicerca', { duration: 200 });
            //myOpacity.hide();

            if (document.getElementById('TextBox1').value != '2') {
                myOpacity.hide(); ;
            }
    </script>
</body>
</html>
