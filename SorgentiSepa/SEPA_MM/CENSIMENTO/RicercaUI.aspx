<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaUI.aspx.vb" Inherits="CENSIMENTO_RicercaUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
<head id="Head1" runat="server">
    <title>Ricerca U.I.</title>
    </head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="btnCerca">
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 164px">Edifcio</asp:Label>
        <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 164px" TabIndex="3"
            Width="510px">
        </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 104px">Complesso</asp:Label>
        <asp:DropDownList ID="DrLComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 104px" TabIndex="1"
            Width="510px">
        </asp:DropDownList>
    <div>
        &nbsp; &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 538px; position: absolute; top: 422px" 
            ToolTip="Home" TabIndex="17" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 406px; position: absolute; top: 422px" 
            ToolTip="Avvia Ricerca" TabIndex="16" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
        <table style="left: 0px;
            position: absolute; top: 0px; z-index: 1;">
            <tr>
                <td style="width: 670px; z-index: 200;">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        U.I.</strong></span><br />
                    <br />
            <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                
                            
                        Style="z-index: 100; left: 44px; position: absolute; top: 76px; right: 586px; width: 46px;">Zona</asp:Label>
            <asp:DropDownList ID="cmbZona" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
                            
                            
                            
                        Style="border: 1px solid black; z-index: 10; left: 97px; position: absolute; top: 72px" TabIndex="2"
                Width="80px">
            </asp:DropDownList>
                    <br />
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
                        Style="z-index: 100; left: 256px; position: absolute; top: 87px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 40px; position: absolute; top: 134px">Ascensore</asp:Label>
                    <asp:DropDownList ID="cmbAscensore" runat="server" AutoPostBack="True" BackColor="White"
                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                        border-top: black 1px solid; z-index: 111; left: 96px; border-left: black 1px solid;
                        border-bottom: black 1px solid; position: absolute; top: 134px" TabIndex="2"
                        Width="80px">
                    </asp:DropDownList>
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
                        Style="z-index: 100; left: 256px; position: absolute; top: 148px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
                    <br />
                        <img id="Img13" alt="Aiuto Ricerca per Denominazione Edificio" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
                            src="IMMCENSIMENTO/Search_24x24.png" style="left: 606px; cursor: pointer; position: absolute;
                            top: 161px" />
                    <br />
                    <br />
                    <br />
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 41px; position: absolute; top: 197px">Indirizzo</asp:Label>
                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 441px; position: absolute; top: 197px" Width="12px">Scala</asp:Label>
                    <asp:DropDownList ID="cmbScala" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 467px; position: absolute; top: 195px; right: 217px;" 
                        TabIndex="6" Width="66px">
                    </asp:DropDownList>
        <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        
                        Style="border: 1px solid black; z-index: 111; left: 96px; position: absolute; top: 195px; right: 333px;" TabIndex="4"
            Width="239px">
        </asp:DropDownList>
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 340px; position: absolute; top: 197px">Civico</asp:Label>
        <asp:DropDownList ID="cmbInterno" runat="server" BackColor="White" Font-Names="ARIAL"
            Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 551px; position: absolute; top: 195px" 
                        TabIndex="7" Width="55px">
        </asp:DropDownList>
    
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 535px; position: absolute; top: 198px">Int.</asp:Label>
                    <br />
                    <br />
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 41px; position: absolute; top: 229px">Tipologia</asp:Label>
                    <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 370px; position: absolute; top: 226px; height: 14px"
                        Width="58px">Disponibilità*</asp:Label>
                    <br />
                    <asp:DropDownList ID="cmbSedeTerr" runat="server" BackColor="White" Font-Names="arial"
                        Font-Size="9pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 546px; position: absolute; top: 243px" 
                        TabIndex="8" Width="230px">
                    </asp:DropDownList>
                        <br />
                    <asp:DropDownList ID="DrLDisponib" runat="server" BackColor="White" Font-Names="arial"
                        Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                        z-index: 111; left: 370px; border-left: black 1px solid; border-bottom: black 1px solid;
                        position: absolute; top: 243px" TabIndex="8" Width="164px">
                    </asp:DropDownList>
                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 542px; position: absolute; top: 226px; height: 14px"
                        Width="80px">Sede Territoriale</asp:Label>
                    <asp:DropDownList ID="cmbZonaOsmi" runat="server" BackColor="White" Font-Names="arial"
                        Font-Size="9pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 546px; position: absolute; top: 285px" 
                        TabIndex="8" Width="230px">
                    </asp:DropDownList>
                    <br />
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 10px; position: absolute; top: 479px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
        <div id="Tipologia" style="z-index: 200; left: 86px; width: 270px; position: absolute; top: 227px; height: 203px; background-color: #FFFFFF;
            ">
            <table style="width: 97%; height: 199px;">
                <tr>
                    <td style="text-align: left; vertical-align: top">
                        <div style="height: 191px; overflow: auto; width: 252px;">
                            <asp:CheckBoxList ID="chkListTipologie" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" Width="80%" TabIndex="15">
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    
                    <br />
                    <br />
                    <br />
        <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 369px; position: absolute; top: 195px; right: 217px;" 
                        TabIndex="5" Width="70px">
        </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="TextBox1" runat="server" />
                        <asp:Button ID="btnSelezionaTutto" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 5; left: 236px; position: absolute; top: 434px" Text="Seleziona/Deseleziona"
                            Width="119px" TabIndex="5" />
                    <br />
                </td>
            </tr>
        </table>
        <div id="AiutoRicerca" 
            
            
            
            
            
            
            
            
            style="z-index: 200; left: 303px; width: 302px; position: absolute; top: 144px; height: 225px; background-color: transparent; ">
            <br />
            <div style="width: 180px; height: 68px; background-color: silver; vertical-align: top; text-align: left;">
                <table style="width: 301px; background-color: silver" cellpadding="1" cellspacing="1">
                    <tr>
                        <td style="vertical-align: top; width: 166px; height: 20px; text-align: left">
                            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="112px">Denominazione Edificio</asp:Label></td>
                        <td style="vertical-align: baseline; width: 27px; height: 20px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 166px; height: 20px; text-align: left">
                            <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                ToolTip="Approssimare la ricerca per questo indirizzo" Width="233px"></asp:TextBox></td>
                        <td style="vertical-align: baseline; width: 27px; height: 20px; text-align: left">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/text_view.png"
                                Style="z-index: 111; left: 246px; top: 50px" ToolTip="Cerca Per Approssimazione" /></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 166px; height: 104px; text-align: left">
                            <asp:Label
                                    ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label><br />
                            <div style="left: 5px; overflow: auto; width: 256px; top: 87px; height: 109px">
                                <asp:RadioButtonList ID="ListEdifci" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Width="233px">
                                </asp:RadioButtonList></div>
                        </td>
                        <td style="vertical-align: baseline; width: 27px; height: 104px; text-align: left">
                            <br />
                            <br />
                            <br />
                            <br />
                            
                            <br />
                            <br />
                            <asp:ImageButton ID="BtnConferma" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/Next_24x24.png"
                                Style="z-index: 111; left: 268px; top: 190px" ToolTip="Conferma" 
                                onclientclick="myOpacity.toggle();" /></td>
                    </tr>
                </table>
            </div>
        </div>
    
    </div>
            <script type="text/javascript">
                myOpacity = new fx.Opacity('AiutoRicerca', { duration: 200 });
                //myOpacity.hide();
                if (document.getElementById('TextBox1').value != '2') {
                    myOpacity.hide(); ;
                }
            </script>


        <asp:DropDownList ID="cmbCondominio" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
            
            Style="border: 1px solid black; z-index: 111; left: 370px; position: absolute; top: 284px; width: 64px;" 
            TabIndex="9">
        </asp:DropDownList>

        <asp:Label ID="lblhandicap" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 469px; position: absolute; top: 269px; height: 14px"
                        Width="65px">Per Handicap</asp:Label>
                        <asp:DropDownList ID="cmbHandicap" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
            
            Style="border: 1px solid black; z-index: 111; left: 469px; position: absolute; top: 284px; width: 64px;" 
            TabIndex="10">
        </asp:DropDownList>
        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 370px; position: absolute; top: 309px; height: 14px"
                        Width="58px">Dest. uso</asp:Label>

        <asp:DropDownList ID="DrlDestUso" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
            
            Style="border: 1px solid black; z-index: 111; left: 370px; position: absolute; top: 326px; width: 164px; height: 20px;" 
            TabIndex="11" Width="164px">
        </asp:DropDownList>
        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 543px; position: absolute; top: 309px; height: 14px"
                        Width="58px">Progr.Eventi</asp:Label>
                                                <asp:DropDownList ID="DrlProgrEventi" 
            runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
            
            Style="border: 1px solid black; z-index: 111; left: 546px; position: absolute; top: 325px; width: 164px; height: 20px;" 
            TabIndex="12" Width="164px">
        </asp:DropDownList>
        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
            Style="z-index: 100; left: 370px; position: absolute; top: 356px; height: 14px; width: 74px;">Sup.Netta Da</asp:Label>
        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            
            Style="z-index: 100; left: 370px; position: absolute; top: 268px; right: 747px;">Condominio</asp:Label>

            <asp:TextBox ID="TxtSupDa" runat="server" MaxLength="10" Style="left: 370px;
            position: absolute; top: 372px; right: 1133px;z-index:111;" Width="56px" TabIndex="13"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
        runat="server" ControlToValidate="TxtSupDa"
            ErrorMessage="!!!" Font-Bold="True" Style="left: 436px; position: absolute;
            top: 372px; height: 17px; width: 16px;" ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$" 
        ToolTip="Inserire un valore con decimale a precisione doppia"></asp:RegularExpressionValidator>
        <asp:Label ID="lblDa" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
            
            
            Style="z-index: 100; left: 464px; position: absolute; top: 356px; height: 14px; width: 74px;">Sup.Netta A</asp:Label>
            <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
            
            
            
            
            
            Style="z-index: 100; left: 547px; position: absolute; top: 356px; height: 14px; width: 146px;">Rendita Catastale presente</asp:Label>
            <asp:DropDownList ID="cmbRendita" 
            runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
            
            Style="border: 1px solid black; z-index: 500; left: 546px; position: absolute; top: 372px; width: 50px; height: 20px;" 
            TabIndex="12" Width="50px">
        </asp:DropDownList>
            <asp:TextBox ID="TxtSupA" runat="server" MaxLength="10" Style="left: 463px;
            position: absolute; top: 372px; right: 1040px; z-index:111;" Width="56px" 
            TabIndex="14"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
        runat="server" ControlToValidate="TxtSupA"
            ErrorMessage="!!!" Font-Bold="True" Style="left: 532px; position: absolute;
            top: 372px; height: 17px; width: 16px;" ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$" 
        ToolTip="Inserire un valore con decimale a precisione doppia"></asp:RegularExpressionValidator>
        <p>
                    <asp:Label ID="Label20" runat="server" Font-Bold="False" 
                Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 542px; position: absolute; top: 269px; height: 14px"
                        Width="80px">Zona OSMI</asp:Label>
                    </p>
    </form>
</body>
</html>
