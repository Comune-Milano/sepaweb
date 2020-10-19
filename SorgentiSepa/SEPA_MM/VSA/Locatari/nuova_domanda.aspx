<%@ Page Language="VB" AutoEventWireup="false" CodeFile="nuova_domanda.aspx.vb" Inherits="VSA_Locatari_nuova_domanda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nuova Domanda</title>
</head>
<body>
    <form id="form1" runat="server" style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; height: 597px; top: 0px; left: 0px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="LBLid" runat="server" />
        <asp:HiddenField ID="LBLcodUI" runat="server" />
        <asp:HiddenField ID="LBLintest" runat="server" />
        <asp:HiddenField ID="LBLintest2" runat="server" />
        <asp:HiddenField ID="LBLcodContr" runat="server" />
        <asp:HiddenField ID="LBLcodContr2" runat="server" />
        <asp:HiddenField ID="LBLid2" runat="server" />
        <asp:HiddenField ID="LBLcompEXTRA" runat="server" />
        <asp:HiddenField ID="HiddenLBLsaldo2" runat="server" />
        <asp:HiddenField ID="confermaRat" runat="server" Value="0" />
        <table style="position: absolute; left: 13px; width: 800px; top: 11px;" cellpadding="0"
            cellspacing="0">
            <tr>
                <td width="680px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Gestione
                    Locatari - Nuova Domanda</strong></span><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td width="700px">&nbsp
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                    Text="Selezionare la tipologia, la modalità di presentazione e l'intestatario della domanda che si intende effettuare"
                    Width="615px"></asp:Label>
                    <asp:Image ID="ImgElencoDom" runat="server" onclick="ElencoIstanze()" alt="ElencoDomande"
                        ImageUrl="~/NuoveImm/Info_ElencoDom.png" Style="cursor: pointer" ToolTip="Visualizza eventuali istanze presentate nel periodo considerato" />
                </td>
                <td>&nbsp
                </td>
            </tr>
            <tr>
                <td>&nbsp
                </td>
                <td>&nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <table cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDescrizione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                            Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>&nbsp
                </td>
            </tr>
            <tr>
                <td>&nbsp
                </td>
                <td>&nbsp
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Tipologia domanda"
                                            Width="180px"></asp:Label>
                                    </td>
                                    <td width="490px">
                                        <asp:DropDownList ID="cmbTipo" runat="server" Width="510px" AutoPostBack="True" TabIndex="1"
                                            Font-Names="arial" Font-Size="9pt">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblMotivo" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Motivo di present. della richiesta"
                                            Visible="False" Width="180px"></asp:Label>
                                    </td>
                                    <td width="490px">
                                        <asp:DropDownList ID="cmbMotivo" runat="server" Width="510px" Visible="False" AutoPostBack="True"
                                            TabIndex="2" Font-Names="arial" Font-Size="9pt">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblModR" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Modalità richiesta"
                                            Width="180px" Visible="False"></asp:Label>
                                    </td>
                                    <td width="490px">
                                        <asp:DropDownList ID="cmbModRichiesta" runat="server" Width="150px" TabIndex="3"
                                            Font-Names="arial" Font-Size="9pt" AutoPostBack="True" Visible="False">
                                            <%--                                        <asp:ListItem Value="0">di persona</asp:ListItem>
                                        <asp:ListItem Value="1">a mezzo posta</asp:ListItem>
                                        <asp:ListItem Value="2">verifica d'ufficio</asp:ListItem>
                                        <asp:ListItem Value="3">sindacati</asp:ListItem>
                                        <asp:ListItem Value="4">altro</asp:ListItem>
                                            --%>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblAltro" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Descrizione Modalità"
                                            Visible="False" Width="180px"></asp:Label>
                                    </td>
                                    <td width="490px">
                                        <asp:TextBox ID="txtAltro" runat="server" Visible="False" Width="500px" TabIndex="4"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblSindac" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Sindacato"
                                            Visible="False" Width="180px"></asp:Label>
                                    </td>
                                    <td width="490px">
                                        <asp:DropDownList ID="cmbSindacato" runat="server" Width="510px" TabIndex="5" Font-Names="arial"
                                            Font-Size="9pt" Visible="False">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblDataEvento" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data evento"
                                            Visible="False" Width="180px"></asp:Label>
                                    </td>
                                    <td width="110px">
                                        <asp:TextBox ID="txtDataEvento" runat="server" MaxLength="10" Visible="False" AutoPostBack="True"
                                            Width="100px" TabIndex="6"></asp:TextBox>
                                    </td>
                                    <td width="100px">&nbsp;
                                    </td>
                                    <td width="105px">&nbsp;
                                    </td>
                                    <td width="140px">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblNotifica" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data notifica"
                                            Visible="False" Width="180px"></asp:Label>
                                    </td>
                                    <td width="110px">
                                        <asp:TextBox ID="txtDataNotifica" runat="server" MaxLength="10" Visible="False" AutoPostBack="True"
                                            Width="100px" TabIndex="6"></asp:TextBox>
                                    </td>
                                    <td width="100px">&nbsp;
                                    </td>
                                    <td width="105px">&nbsp;
                                    </td>
                                    <td width="140px">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <%--<td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtDataPr" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>--%>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblDataPr" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data di presentazione"
                                            Visible="False" Width="180px"></asp:Label>
                                    </td>
                                    <td width="110px">
                                        <asp:TextBox ID="txtDataPr" runat="server" TabIndex="7" Visible="False" AutoPostBack="True"
                                            MaxLength="10" Width="100px"></asp:TextBox>
                                    </td>
                                    <td width="100px">
                                        <asp:Label ID="lblAU" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Anag. utenza"
                                            Visible="False" Width="91px"></asp:Label>
                                    </td>
                                    <td width="105px">
                                        <asp:DropDownList ID="cmbAU" runat="server" AutoPostBack="True" Font-Names="arial"
                                            Font-Size="8pt" TabIndex="9" Visible="False" Width="135px" BackColor="#FFFFCC">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="160px">
                                        <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Anno reddito"
                                            Visible="False" Width="157px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblanno" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Anno reddituale"
                                            Visible="False" Width="180px"></asp:Label>
                                    </td>
                                    <td width="110px">
                                        <asp:TextBox ID="txtAnnoReddito" runat="server" MaxLength="4" ToolTip="Anno reddituale a disposizione"
                                            Visible="False" Width="100px" TabIndex="8" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td width="100px">
                                        <asp:Label ID="lblVerificaAnno" runat="server" Font-Names="Arial" Font-Size="9pt"
                                            Text="Verifica Reddito" Visible="False" Width="91px"></asp:Label>
                                    </td>
                                    <td width="105px">
                                        <asp:DropDownList ID="cmbAnniRedd" runat="server" AutoPostBack="True" Enabled="False"
                                            Font-Names="arial" Font-Size="8pt" TabIndex="9" Visible="False" Width="135px"
                                            BackColor="#FFFFCC">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="175px">
                                        <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Anno reddito"
                                            Visible="False" Width="157px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblInizio" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Inizio Validità"
                                            Visible="False" Width="180px"></asp:Label>
                                    </td>
                                    <td width="110px">
                                        <asp:TextBox ID="txtDataIn" runat="server" Width="100px" ReadOnly="True" Visible="False"
                                            TabIndex="10" BackColor="#FFFFCC" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                    <td width="100px">
                                        <asp:Label ID="lblFine" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Fine Validità"
                                            Visible="False" Width="91px"></asp:Label>
                                    </td>
                                    <td width="105px">
                                        <asp:TextBox ID="txtDataFine" runat="server" Width="100px" ReadOnly="True" Visible="False"
                                            TabIndex="11" BackColor="#FFFFCC" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                    <td width="175px">
                                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Anno reddito"
                                            Visible="False" Width="157px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblDom" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Intestatario domanda"
                                            Width="180px" Visible="False"></asp:Label>
                                    </td>
                                    <td width="490px">
                                        <asp:DropDownList ID="cmbComponenti" runat="server" Width="510px" TabIndex="12" Font-Names="arial"
                                            Font-Size="9pt" Visible="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Image ID="imgAggComp" runat="server" ImageUrl="~/ANAUT/img/ImgAdd.png" Visible="False"
                                            onclick="apriRicercaComp()" ToolTip="Aggiungi Componente" Style="width: 16px; cursor: pointer;" />
                                    </td>
                                    <td>
                                        <asp:Image ID="imgRicercaComp" runat="server" onclick="apriRicercaComponenti();"
                                            alt="RicercaComponente" ImageUrl="../../Condomini/Immagini/Search_16x16.png"
                                            ToolTip="Ricerca Componente" Style="cursor: pointer" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblIntestRU" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Intestatario contratto"
                                            Width="180px" Visible="False"></asp:Label>
                                    </td>
                                    <td width="490px">
                                        <asp:DropDownList ID="cmbIntestRU" runat="server" Width="510px" TabIndex="12" Font-Names="arial"
                                            Font-Size="9pt" Visible="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp
                                    </td>
                                    <td>&nbsp
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td width="180px">
                                        <asp:Label ID="lblUIscambio" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Codice UI oggetto dello scambio"
                                            Width="180px" Visible="False"></asp:Label>
                                    </td>
                                    <td width="490px">
                                        <asp:TextBox ID="txtUIscambio" runat="server" Width="200px" ToolTip="Inserire codice UI con cui effettuare il cambio"
                                            Visible="False" AutoPostBack="True"></asp:TextBox>&nbsp
                                    <asp:Image ID="imgRicercaUI" runat="server" onclick="apriRicerca()" alt="RicercaUI"
                                        ImageUrl="../../CENSIMENTO/IMMCENSIMENTO/Search_24x24.png" Style="cursor: pointer"
                                        Visible="False" Width="20px" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl1" runat="server" Font-Names="Arial" Font-Size="9pt" Text="1° Dichiarante:"
                                            Width="167px" Visible="False" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:Label ID="lblSaldo" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Saldo Attuale"
                                            Width="167px" Visible="False"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:TextBox ID="txtSaldo" runat="server" Width="90px" BackColor="#CCFFFF" Visible="False"
                                            ReadOnly="True" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align: top; text-align: right;">
                                        <asp:Label ID="lblImporto" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Importo Accertato"
                                            Width="100px" Visible="False"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top; text-align: right;">
                                        <asp:TextBox ID="txtImporto" runat="server" Width="90px" BackColor="#CCFFFF" Visible="False"
                                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align: top; text-align: right;">
                                        <asp:Label ID="lblNumRate" runat="server" Font-Names="Arial" Font-Size="9pt" Text="N.ro Rate"
                                            Width="70px" Visible="False"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top; text-align: right;">
                                        <asp:TextBox ID="txtNumRate" runat="server" Width="90px" BackColor="#CCFFFF" Visible="False"
                                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <%--<table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsgDebito" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="#801F1C"
                                        Visible="False" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <asp:Label ID="lblAltreDom" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                        ForeColor="black" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl2" runat="server" Font-Names="Arial" Font-Size="9pt" Text="2° Dichiarante:"
                                            Width="167px" Visible="False" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:Label ID="lblSaldo2" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Saldo Attuale"
                                            Width="167px" Visible="False"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:TextBox ID="txtSaldo2" runat="server" Width="90px" BackColor="#CCFFFF" Visible="False"
                                            ReadOnly="True" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align: top; text-align: right;">
                                        <asp:Label ID="lblImporto2" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Importo Accertato"
                                            Width="100px" Visible="False"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top; text-align: right;">
                                        <asp:TextBox ID="txtImporto2" runat="server" Width="90px" BackColor="#CCFFFF" Visible="False"
                                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align: top; text-align: right;">
                                        <asp:Label ID="lblNumRate2" runat="server" Font-Names="Arial" Font-Size="9pt" Text="N.ro Rate"
                                            Width="70px" Visible="False"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top; text-align: right;">
                                        <asp:TextBox ID="txtNumRate2" runat="server" Width="90px" BackColor="#CCFFFF" Visible="False"
                                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <%--<table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsgDebito" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="#801F1C"
                                        Visible="False" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <asp:Label ID="lblAltreDom" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                        ForeColor="black" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="670px">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <table width="670px">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMess" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="#801F1C"
                                            Visible="False" Font-Bold="True"></asp:Label>
                                        <asp:Label ID="lblMess0" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                            ForeColor="#801F1C" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp
                                    </td>
                                </tr>
                                <%-- <tr>
                                <td width="100%">
                                    <asp:Label ID="lblAltreDom" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                        ForeColor="black" Visible="False"></asp:Label>
                                </td>
                            </tr>--%>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../../NuoveImm/Img_Procedi.png"
                                ToolTip="Procedi" TabIndex="13" Style="position: absolute; top: 527px; left: 573px;" />
                            <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="../../NuoveImm/Img_EsciCorto.png"
                                OnClientClick="javascript:window.close();" TabIndex="14" Style="position: absolute; top: 527px; left: 669px;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:ImageButton ID="btnAggComp" runat="server" ImageUrl="../../NuoveImm/Img_Procedi.png"
                        ToolTip="Procedi" Style="display: none;" />
                    <asp:ImageButton ID="btnConfermaRAT" runat="server" ImageUrl="../../NuoveImm/Img_Procedi.png"
                        ToolTip="" Style="display: none;" />
                </td>
            </tr>
        </table>
    </form>
</body>
<script language="javascript" type="text/javascript">

    // Funzione javascript per l'inserimento in automatico degli slash nella data
    function CompletaData(e, obj) {

        var sKeyPressed;
        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
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
    function SostPuntVirg(e, obj) {
        var keyPressed;
        keypressed = (window.event) ? event.keyCode : e.which;
        if (keypressed == 46) {
            event.keyCode = 0;
            obj.value += ',';
            obj.value = obj.value.replace('.', '');
        }

    };
    function AutoDecimal2(obj) {

        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a != 'NaN') {
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali
                    //document.getElementById(obj.id).value = a.replace('.', ',')
                    document.getElementById(obj.id).value = risultato
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }

            }
            else
                document.getElementById(obj.id).value = ''
        }
    };

    function apriRicerca() {

        var win = null;
        LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
        LeftPosition = LeftPosition - 20;
        TopPosition = TopPosition - 20;
        window.open('RicercaOccupante.aspx', 'RicercaOccu', 'height=450,top=100,left=180,width=800,scrollbars=no');
    }

    function apriRicercaComp() {

        var win = null;
        LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
        LeftPosition = LeftPosition - 20;
        TopPosition = TopPosition - 20;
        window.open('Inserimento.aspx', 'RicercaComp', 'height=460,top=100,left=180,width=501,scrollbars=no');
    }

    function apriRicercaComponenti() {

        var win = null;
        LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
        LeftPosition = LeftPosition - 20;
        TopPosition = TopPosition - 20;
        window.open('RicercaComp.aspx', 'RicercaComp2', 'height=460,top=100,left=180,width=501,scrollbars=no');

    }

    function ElencoIstanze() {
        var annoEvento;
        if (document.getElementById('txtDataEvento')) {
            if (document.getElementById('txtDataEvento').value != "") {
                annoEvento = document.getElementById('txtDataEvento').value.substring(6, 10)
            }
        }
        else {
            if (document.getElementById('txtDataPr')) {
                annoEvento = document.getElementById('txtDataPr').value.substring(6, 10)
            }
        }
        if (typeof annoEvento == 'undefined') {
            alert('Attenzione...Valorizzare prima i campi DATA per visualizzare l\'elenco delle istanze!')
        }
        else {
            window.open('ElencoDich.aspx?COD=' + document.getElementById('LBLcodUI').value + '&ANNO=' + annoEvento + '', 'RisultatoDich', 'height=520,top=200,left=550,width=820,resizable=no,menubar=no,toolbar=no,scrollbars=no');
        }
    }

</script>
</html>
