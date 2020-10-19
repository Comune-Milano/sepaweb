<%@ Page Language="VB" AutoEventWireup="false" CodeFile="start.aspx.vb" Inherits="AutoCompilazione_start" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Compilazione domanda ERP</title>
</head>
<body background="Immagini/Sfondo.gif">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: center; border-right: #cc0000 1px solid; width: 11%;">
                    <img src="Immagini/Milano.gif" /></td>
                <td style="border-bottom: #cc0000 1px solid; text-align: center; width: 91%; border-left-width: 1px; border-left-color: #cc0000;">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center; border-right: #cc0000 1px solid; border-top: #cc0000 1px solid; width: 11%;" valign="top">
                    <table cellpadding="0" cellspacing="0" style="width: 59px">
                        <tr>
                            <td>
                                <img src="Immagini/LogoComune.gif" /></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="background-color: #dadada; width: 176px;">
                        <tr>
                            <td style="width: 689px; height: 19px;">
                                <span style="font-size: 11pt; color: #cc0000; font-family: Arial"><strong>Funzioni</strong></span></td>
                            <td background="Immagini/TabAltoDx.gif" style="width: 80px; font-size: 12pt; font-family: Times New Roman; height: 19px;">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px; height: 19px; text-align: left;">
                                <span style="font-size: 8pt; font-family: Arial">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/AutoCompilazione/Recupera.aspx" Target="_top" TabIndex="60">Recupera il numero di una Domanda inserita.</asp:HyperLink></span></td>
                            <td style="height: 19px; width: 95px;">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px">
                                &nbsp;&nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px" align="left">
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Names="arial" Font-Size="8pt"
                                    NavigateUrl="~/AutoCompilazione/Cancella.aspx" Target="_top" TabIndex="70">Cancella Domanda</asp:HyperLink></td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px">
                                &nbsp;<strong><span style="font-size: 11pt; color: #cc0000; font-family: Arial">Link
                                    Utili</span></strong></td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px; text-align: left; height: 19px;">
                                &nbsp;</td>
                            <td style="width: 95px; height: 19px;">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px; text-align: left;">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px; text-align: left;">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px; text-align: left; height: 19px;">
                                &nbsp;</td>
                            <td style="width: 95px; height: 19px;">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px; text-align: left">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px">
                                &nbsp;&nbsp;
                            </td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 50px">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px; text-align: left">
                                <asp:HyperLink ID="HyperLink8" runat="server" Font-Names="arial" Font-Size="8pt"
                                    NavigateUrl="~/Portale.aspx" Target="_top">Torna alla pagina principale</asp:HyperLink></td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 50px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px; text-align: left">
                                &nbsp;
                            </td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 50px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 689px; text-align: left">
                                &nbsp;</td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 50px">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 91%; font-size: 12pt; font-family: Times New Roman; text-align: left;" align="center" valign="top">
                    <table width="100%">
                        <tr>
                            <td style="background-image: url(Immagini/BarraSfondo.gif); text-align: left;" valign="middle">
                                <span style="font-size: 14pt; color: #393a3a"> Compilazione Domanda
                                    di Bando E.R.P.<br>
                                    &nbsp;&nbsp;</span></td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;
                    <table width="80%">
                        <tr>
                            <td>
                                <span style="font-family: Arial"><span style="font-size: 10pt">Inserire i dati richiesti
                                    indicando il numero dei componenti il nucleo familiare.<br />
                                </span>&nbsp;</span><br />
                                <span style="font-family: Arial"><span style="font-size: 10pt"><strong>Nota bene</strong>:
                                    è possibile inserire al massimo 10 componenti per nucleo familiare (compreso il
                                    dichiarante). Eventuali altri componenti potranno essere aggiunti in fase di formalizzazione
                                    della domanda presso gli uffici del Comune.
                                    <br />
                                    <br />
                                    <font face="Verdana" size="2"><span style="font-size: 10pt; font-family: Arial">I dati
                                        inseriti potranno essere successivamente modificati, &nbsp;integrati o cancellati.</span></font>
                                    <br />
                                </span></span>
                            </td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <br />
                    <table cellpadding="0" cellspacing="0" width="80%">
                        <tr>
                            <td align="left" style="width: 18%">
                                &nbsp;<asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="12pt" Style="left: 19px;
            position: static; top: 220px; z-index: 100;" Text="Codice Fiscale del Dichiarante" Width="235px"></asp:Label></td>
                            <td align="left">
        <asp:TextBox ID="txtCF" runat="server" Font-Names="arial" Font-Size="12pt" MaxLength="16"
            Style="left: 271px; position: static; top: 218px; z-index: 104;" TabIndex="1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 18%">
                            </td>
                            <td  width="40%" align="left">
        <asp:Label ID="lblErrore" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
            Style="left: 19px; position: static; top: 246px; z-index: 109;" Visible="False" Width="410px" TabIndex="20"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 18%">
                                &nbsp;<asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="12pt" Style="left: 19px;
            position: static; top: 278px; z-index: 101;" Text="Cognome del Dichiarante" Width="179px"></asp:Label></td>
                            <td  width="40%" align="left">
        <asp:TextBox ID="txtCognome" runat="server" Font-Names="arial" Font-Size="12pt" MaxLength="25"
            Style="left: 271px; position: static; top: 275px; z-index: 105;" Width="294px" TabIndex="2"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 18%">
                                &nbsp;<asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="12pt" Style="left: 19px;
            position: static; top: 310px; z-index: 102;" Text="Nome del Dichiarante" Width="184px"></asp:Label></td>
                            <td align="left"  width="40%">
        <asp:TextBox ID="txtNome" runat="server" Font-Names="arial" Font-Size="12pt" MaxLength="25"
            Style="left: 271px; position: static; top: 306px; z-index: 106;" Width="294px" TabIndex="3"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 18%">
                            </td>
                            <td  width="40%" align="left">
        <asp:Label ID="Label5" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
            Style="left: 20px; position: static; top: 334px; z-index: 111;" Visible="False" Width="400px" TabIndex="30"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 18%">
                                &nbsp;<asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="12pt" Style="left: 20px;
            position: static; top: 360px; z-index: 103;" Text="Numero di Componenti nel nucleo" Width="235px"></asp:Label></td>
                            <td align="left"  width="40%">
        <asp:TextBox ID="txtNumero" runat="server" Font-Names="arial" Font-Size="12pt" MaxLength="2"
            Style="left: 271px; position: static; top: 357px; z-index: 107;" Width="48px" TabIndex="4">1</asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumero"
                                    ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d+$"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 18%; text-align: left;">
                                <span style="font-size: 10pt; font-family: Arial">&nbsp;(Compreso il Dichiarante)</span></td>
                            <td  width="40%" align="left">
        <asp:Label ID="lblErrore1" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
            Style="left: 20px; position: static; top: 385px; z-index: 110;" Visible="False" Width="400px" TabIndex="40"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 18%; height: 19px;">
                                &nbsp;
                            </td>
                            <td align="left" width="40%" style="height: 19px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 18%">
                            </td>
                            <td align="center"  width="40%">
        <asp:Button ID="btnValidaCF" runat="server" Style="left: 497px; position: static;
            top: 414px; z-index: 108;" Text="Prosegui" TabIndex="5" /></td>
                        </tr>
                        <tr>
                            <td style="width: 18%" align="left">
        </td>
                            <td align="right">
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
        <asp:Label ID="lblErroreGen" runat="server" Font-Names="ARIAL" Font-Size="10pt" ForeColor="Red"
            Style="left: 20px; position: static; top: 447px; z-index: 112;" Visible="False" Width="635px" TabIndex="50"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
        <br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    </form>
</body>
</html>

