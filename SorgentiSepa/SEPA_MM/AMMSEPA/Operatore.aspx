<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Operatore.aspx.vb" Inherits="AMMSEPA_Operatore"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Operatore</title>
    <script type="text/javascript">
        function ConfermaElimina() {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Eliminare questo operatore? Non sarà possibile ripristinare.");
            if (chiediConferma == true) {
                document.getElementById('sicuro').value = '1';

            }
            else {
                document.getElementById('sicuro').value = '0';
            }
        }

    </script>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <script type="text/javascript">
            var xPos, yPos;
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);
            function BeginRequestHandler(sender, args) {
                xPos = 0;
                yPos = document.documentElement.scrollTop;
            }
            function EndRequestHandler(sender, args) {
                //            $get('scrollDiv').scrollLeft = xPos;
                document.documentElement.scrollTop = yPos;
                //window.scrollTo(xPos, yPos);
                //            window.scrollBy(0, 50)
            }
        </script>
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Gestione Operatori" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="Immagini/SfondoHome.jpg" height="75px" width="100%" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%;">
                                <table width="60%" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImgSalva" runat="server" ImageUrl="../NuoveImm/Img_SalvaGrande.png"
                                                ToolTip="Salva i dati inseriti" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgAzzera" runat="server" ImageUrl="../NuoveImm/Img_Azzera.png"
                                                ToolTip="Azzera la Password corrente" Enabled="False" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgRevoca" runat="server" ImageUrl="../NuoveImm/Img_Revoca.png"
                                                ToolTip="Revoca Utenza" Enabled="False" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgAnnullaRevoca" runat="server" ImageUrl="../NuoveImm/Img_Annulla_Revoca.png"
                                                ToolTip="Annulla Revoca Utenza" Enabled="False" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgAzzera0" runat="server" ImageUrl="../NuoveImm/Img_EliminaOperatore.png"
                                                ToolTip="Elimina" OnClientClick="ConfermaElimina();" Enabled="False" />
                                        </td>
                                        <td>
                                            <asp:Image ID="btnEventi" runat="server" ImageUrl="~/NuoveImm/Img_Eventi_Grande.png"
                                                Style="cursor: pointer" onclick="window.open('EventiOp.aspx?ID=' + document.getElementById('idOperatore').value,'Eventi','top=0,left=0,resizable=yes');"
                                                ToolTip="Eventi" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImgEsci" runat="server" ImageUrl="../NuoveImm/Img_Esci_AMM.png"
                                                ToolTip="Esci" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;">
                                &nbsp;
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 603px">
                                            <asp:Label ID="Label13" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Nome Utente"></asp:Label>
                                        </td>
                                        <td style="width: 603px">
                                            <asp:TextBox ID="txtUtente" runat="server" Columns="30" Font-Names="Arial" Font-Size="10pt"
                                                MaxLength="30" Style="z-index: 100; left: 94px; position: static; top: 26px"
                                                TabIndex="1"></asp:TextBox>
                                        </td>
                                        <td style="width: 213px">
                                        </td>
                                        <td style="width: 378px">
                                        </td>
                                        <td class="style1">
                                        </td>
                                        <td style="width: 136px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Cognome"></asp:Label>
                                        </td>
                                        <td style="width: 603px">
                                            <asp:TextBox ID="txtCognome" runat="server" Columns="30" Font-Names="Arial" Font-Size="10pt"
                                                MaxLength="30" Style="z-index: 100; left: 94px; position: static; top: 26px"
                                                TabIndex="2"></asp:TextBox>
                                        </td>
                                        <td style="width: 213px">
                                        </td>
                                        <td style="width: 378px">
                                        </td>
                                        <td class="style1">
                                        </td>
                                        <td style="width: 136px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 24px;">
                                            <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Nome"></asp:Label>
                                        </td>
                                        <td style="width: 603px; height: 24px;">
                                            <asp:TextBox ID="txtNome" runat="server" Columns="30" Font-Names="Arial" Font-Size="10pt"
                                                MaxLength="30" Style="z-index: 100; left: 94px; position: static; top: 26px"
                                                TabIndex="3"></asp:TextBox>
                                        </td>
                                        <td style="width: 213px; height: 24px;">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="Immagini/Alert.gif" Visible="False" /><asp:Label
                                                ID="lblRevoca" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                                                Text="UTENZA REVOCATA" Visible="False" Width="157px"></asp:Label>
                                        </td>
                                        <td style="width: 378px; height: 24px">
                                        </td>
                                        <td class="style2">
                                        </td>
                                        <td style="width: 136px; height: 24px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Codice Fiscale"></asp:Label>
                                        </td>
                                        <td style="width: 603px">
                                            <asp:TextBox ID="txtCF" runat="server" Columns="30" Font-Names="Arial" Font-Size="10pt"
                                                MaxLength="30" Style="z-index: 100; left: 94px; position: static; top: 26px"
                                                TabIndex="4"></asp:TextBox>
                                        </td>
                                        <td style="width: 213px">
                                            <asp:Label ID="lblRevocaMotivo" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                                Font-Size="10pt" Text="MOTIVO" Visible="False" Width="222px"></asp:Label>
                                        </td>
                                        <td style="width: 378px">
                                        </td>
                                        <td class="style1">
                                        </td>
                                        <td style="width: 136px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 24px;">
                                            <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Codice Anagrafico"></asp:Label>
                                        </td>
                                        <td style="width: 603px; height: 24px;">
                                            <asp:TextBox ID="txtAnagrafico" runat="server" Columns="30" Font-Names="Arial" Font-Size="10pt"
                                                MaxLength="30" Style="z-index: 100; left: 94px; position: static; top: 26px"
                                                TabIndex="5"></asp:TextBox>
                                        </td>
                                        <td style="width: 213px; height: 24px;">
                                        </td>
                                        <td style="width: 378px; height: 24px">
                                        </td>
                                        <td class="style2">
                                        </td>
                                        <td style="width: 136px; height: 24px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Utente Campus"></asp:Label>
                                        </td>
                                        <td style="width: 603px">
                                            <asp:TextBox ID="txtCampus" runat="server" Columns="30" Font-Names="Arial" Font-Size="10pt"
                                                MaxLength="30" Style="z-index: 100; left: 94px; position: static; top: 26px"
                                                TabIndex="6"></asp:TextBox>
                                        </td>
                                        <td style="width: 213px">
                                        </td>
                                        <td style="width: 378px">
                                        </td>
                                        <td class="style1">
                                        </td>
                                        <td style="width: 136px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 21px;">
                                            <asp:Label ID="Label11" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Interrogazione Anagrafe" Width="141px"></asp:Label>
                                        </td>
                                        <td style="width: 603px; height: 21px;">
                                            <asp:CheckBox ID="ChAnagrafe" runat="server" TabIndex="7" />
                                        </td>
                                        <td style="width: 213px; height: 21px;">
                                        </td>
                                        <td style="width: 378px; height: 21px">
                                        </td>
                                        <td class="style3">
                                        </td>
                                        <td style="width: 136px; height: 21px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 21px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 21px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; height: 21px">
                                        </td>
                                        <td style="width: 378px; height: 21px">
                                        </td>
                                        <td class="style3">
                                        </td>
                                        <td style="width: 136px; height: 21px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 21px">
                                            <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Scadenza Password" Width="141px"></asp:Label>
                                        </td>
                                        <td style="height: 21px" colspan="2">
                                            <asp:TextBox ID="txtScadenzaPW" runat="server" Columns="10" Font-Names="Arial" Font-Size="10pt"
                                                MaxLength="30" Style="z-index: 100; left: 94px; position: static; top: 26px"
                                                TabIndex="8"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtScadenzaPW"
                                                Display="Dynamic" ErrorMessage="gg/mm/aaaa" Font-Names="arial" Font-Size="8pt"
                                                TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                            &nbsp;<asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                Text="(Se non specificata la data, l'utente potrà sempre accedere)"></asp:Label>
                                        </td>
                                        <td class="style3">
                                        </td>
                                        <td style="width: 136px; height: 21px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 21px">
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 21px">
                                        </td>
                                        <td class="style3">
                                        </td>
                                        <td style="width: 136px; height: 21px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 21px">
                                            <asp:Label ID="Label12" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Gestione Operatori" Width="141px"></asp:Label>
                                        </td>
                                        <td style="height: 21px" colspan="2">
                                            <asp:CheckBox ID="ChGestOp" runat="server" TabIndex="7" />
                                            &nbsp;<asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Gestione completa degli operatori, valido solo per operatori comunali. Permette di gestire gli operatori come ADMINISTRATOR"
                                                Font-Bold="True" Width="400px"></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 21px">
                                            &nbsp;
                                        </td>
                                        <td class="style3">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 21px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 21px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 21px">
                                            <asp:Label ID="Label17" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Livello" Width="141px"></asp:Label>
                                        </td>
                                        <td style="width: 213px; height: 21px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 21px">
                                            &nbsp;
                                        </td>
                                        <td class="style3">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 21px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 22px;">
                                            <asp:CheckBox ID="chSEPA" runat="server" Text="Operatore SEPA Client" Font-Names="arial"
                                                Font-Size="10pt" TextAlign="Left" Width="156px" TabIndex="9" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; text-align: left; height: 22px;">
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <asp:DropDownList ID="cmbLivello" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Width="270px" AutoPostBack="True" CausesValidation="True" TabIndex="10">
                                                    <asp:ListItem Value="0">Amministratore di sistema</asp:ListItem>
                                                    <asp:ListItem Value="1">Gestione Dati (pu&#242; inserire e modificare)</asp:ListItem>
                                                    <asp:ListItem Value="2">Consultazione (non pu&#242; modificare i dati)</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 22px;">
                                            <asp:CheckBox ID="chAssEsterna" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Text="Invio in Ass.Esterna" ToolTip="Invio domande ERP per assegnazione tramite ente esterno"
                                                TabIndex="11" />
                                        </td>
                                        <td style="width: 378px; height: 22px; background-color: #DCDCDC">
                                            <asp:CheckBox ID="ChPGwEB" runat="server" Font-Names="arial" Font-Size="10pt" Text="Pg@Web"
                                                Width="81px" ToolTip="Richiesta protocollo al sistema Pg@Web" TabIndex="12" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style4">
                                            <asp:CheckBox ID="chQAlloggi" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Text="Query Alloggi" TabIndex="13" />
                                        </td>
                                        <td style="width: 136px; height: 22px; background-color: #DCDCDC">
                                            <asp:CheckBox ID="ChCompilazioneERP" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Text="Auto Compilazione" Width="135px" ToolTip="Import delle domande Compilate dai citadini in maniera autonoma"
                                                TabIndex="14" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px">
                                            <table cellpadding="0" cellspacing="0" style="background-color: #DCDCDC" width="100%">
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTesto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                                            Style="position: static" Text="SEZIONI ABILITATE" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                        <asp:Label ID="lblArt22" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                            Text="Art.22 Decadenza" Visible="False" Width="105px"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="A22" runat="server" Font-Names="arial" Font-Size="10pt" Text="Formalizzazione"
                                                            Visible="False" TabIndex="15" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="B22" runat="server" Font-Names="arial" Font-Size="10pt" Text="Istruttoria"
                                                            Visible="False" TabIndex="16" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                        <asp:Label ID="lblArt25" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                            Text="Art.25 Occ.Abusiva" Visible="False" Width="114px"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="A25" runat="server" Font-Names="arial" Font-Size="10pt" Text="Formalizzazione"
                                                            Visible="False" TabIndex="17" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="B25" runat="server" Font-Names="arial" Font-Size="10pt" Text="Istruttoria"
                                                            Visible="False" TabIndex="18" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                        <asp:Label ID="lblArt43" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                            Text="Art.43 Sanatoria" Visible="False" Width="114px"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="A43" runat="server" Font-Names="arial" Font-Size="10pt" Text="Formalizzazione"
                                                            Visible="False" TabIndex="19" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="B43" runat="server" Font-Names="arial" Font-Size="10pt" Text="Istruttoria"
                                                            Visible="False" TabIndex="20" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                        <asp:Label ID="lblContenzioso" runat="server" Font-Names="arial" Font-Size="10pt"
                                                            Style="position: static" Text="Contenzioso" ToolTip="Contenzioso" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chAmm" runat="server" Font-Names="arial" Font-Size="10pt" Text="Amministrativo"
                                                            Visible="False" TabIndex="21" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chPenale" runat="server" Font-Names="arial" Font-Size="10pt" Text="Penale"
                                                            Visible="False" TabIndex="22" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                        <asp:Label ID="lblA" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                            Text="Dichiarazioni" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="A" runat="server" Font-Names="arial" Font-Size="10pt" Text="Gestione Completa"
                                                            Visible="False" TabIndex="23" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                        <asp:Label ID="lblB" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                            Text="Domande" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="B" runat="server" Font-Names="arial" Font-Size="10pt" Text="Gestione Completa"
                                                            Visible="False" TabIndex="24" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px; height: 21px">
                                                        <asp:Label ID="lblC" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                            Text="Assegnazione" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="height: 21px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="C" runat="server" Font-Names="arial" Font-Size="10pt" Text="Gestione Completa"
                                                            Visible="False" TabIndex="25" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                        <asp:Label ID="lblD" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                            Text="Commissione" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="D" runat="server" Font-Names="arial" Font-Size="10pt" Text="Segreteria"
                                                            Visible="False" TabIndex="26" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 92px">
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="EE" runat="server" Font-Names="arial" Font-Size="10pt" Text="Commissione"
                                                            Visible="False" TabIndex="27" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 213px">
                                        </td>
                                        <td style="width: 378px">
                                            &nbsp;
                                        </td>
                                        <td class="style1">
                                        </td>
                                        <td style="width: 136px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                        </td>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px">
                                        </td>
                                        <td style="width: 378px">
                                        </td>
                                        <td class="style1">
                                        </td>
                                        <td style="width: 136px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chSepaWeb" runat="server" Text="Operatore Web" Font-Names="arial"
                                                Font-Size="10pt" TextAlign="Left" Width="149px" TabIndex="28" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <span style="font-size: 10pt; font-family: Arial">Ente&nbsp;
                                                <asp:DropDownList ID="cmbEnte" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Width="270px" TabIndex="29" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChResponsabile" runat="server" Text="Responsabile" TabIndex="30" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            <asp:Label ID="lblStruttura" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Width="304px" Height="16px"></asp:Label>
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="chkGestAllegati" runat="server" Text="Abilitato alla gestione allegati" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 22px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 22px; text-align: left">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; height: 22px">
                                        </td>
                                        <td style="width: 378px; height: 22px">
                                        </td>
                                        <td class="style4">
                                        </td>
                                        <td style="width: 136px; height: 22px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; text-align: left;">
                                            <span style="font-size: 10pt; font-family: Arial"><strong>MODULI Sepa@web</strong></span>
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; text-align: left;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="Cherp" runat="server" Text="ERP" TabIndex="31" Font-Bold="True"
                                                Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChERPVerificaReq" runat="server" Text="Verifica Mantenimento Requisiti"
                                                Width="200px" TabIndex="37" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="display: none; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;" class="style1">
                                            <asp:CheckBox ID="ChPosizGrad" runat="server" Text="Cambio Posizione Graduatoria"
                                                Width="200px" TabIndex="37" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; text-align: left;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="Chcambi" runat="server" Text="CAMBI" TabIndex="32" Font-Bold="True"
                                                Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="Chfsa" runat="server" Text="FSA" TabIndex="33" Font-Bold="True"
                                                Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; height: 20px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="Chau" runat="server" Text="ANAGRAFE UTENZA" TabIndex="34" Font-Bold="True"
                                                Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUconsulta" runat="server" Text="Solo Lettura" Width="138px"
                                                TabIndex="35" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            <asp:CheckBox ID="ChAUpropDec" runat="server" Text="Proposta Decadenza" Width="169px"
                                                TabIndex="36" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUDecDec" runat="server" Text="Decisione Decadenza" Width="169px"
                                                TabIndex="37" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAURicerca" runat="server" Text="Ricerca" Width="186px" TabIndex="37"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            <asp:CheckBox ID="ChAUReport" runat="server" Text="Report" Width="186px" TabIndex="37"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUCercaInquilino" runat="server" Text="AGENDA AU-Cerca Inquilino"
                                                Width="186px" TabIndex="37" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            <asp:CheckBox ID="ChAUSospeseSind" runat="server" Text="AGENDA AU-Sospese Sind."
                                                Width="186px" TabIndex="37" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUGestMotSosp" runat="server" Text="AGENDA AU-Gest.Mot.Sosp."
                                                Width="186px" TabIndex="37" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUDocNecessaria" runat="server" Text="Documentazione Necessaria"
                                                Width="186px" TabIndex="37" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            <asp:CheckBox ID="ChAUCalcolaCF" runat="server" Text="Calcolo CF" Width="186px" TabIndex="37"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUDiffida" runat="server" Text="Diffida per mancata presentazione"
                                                Width="231px" TabIndex="37" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            <asp:CheckBox ID="ChAUCreaGruppo" runat="server" Text="Crea Gruppo di Lavoro" Width="169px"
                                                TabIndex="37" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUSimulaApplica" runat="server" Text="Simula/Applica AU Gruppo di Lavoro"
                                                Width="241px" TabIndex="37" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUGestione" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="38" Text="Gestione Anagrafe Utenza (Apertura, chiusura,etc)" Width="302px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            <asp:CheckBox ID="ChAUGestione0" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="38" Text="Gestione Anagrafe Utenza (ELIMINA)" Width="302px" />
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUGestioneMod" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="38" Text="Gestione Modelli Comunicazione" Width="302px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUGestioneStr" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="38" Text="Gestione Associazione Str/Sport./Oper." Width="302px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUGestioneEsclusione" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="38" Text="Gestione Motivi Esclusione Convocabili" Width="302px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUGestioneConvocabili" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="38" Text="Gestione Convocabili" Width="302px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUGestioneGrConv" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="38" Text="Gestione Gruppi Convocabili" Width="302px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUGestioneLista" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="38" Text="Gestione Liste Convocazione" Width="302px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUDefConv" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="38" Text="Simulazione/Definitivo Convocazioni" Width="302px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            <asp:CheckBox ID="ChAUEliminaConv" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="38" Text="Elimina File Convocazioni" Width="302px" />
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUConvTutti" runat="server" Text="Convocazione AU - Ricerca Tutti"
                                                Width="241px" TabIndex="38" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUConvIns" runat="server" Text="Convocazione AU - Inserimento Scheda AU"
                                                Width="272px" TabIndex="39" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUConvSposta" runat="server" Text="Convocazione AU - Sposta Appuntamento"
                                                Width="272px" TabIndex="40" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUConvAnnulla" runat="server" Text="Convocazione AU - Annulla Appuntamento"
                                                Width="272px" TabIndex="41" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUConvRip" runat="server" Text="Convocazione AU - Ripristina App.Annullato"
                                                Width="272px" TabIndex="42" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUConvReimposta" runat="server" Text="Convocazione AU - Reimposta App."
                                                Width="272px" TabIndex="43" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUConvSind" runat="server" Text="Convocazione AU - Sospese Sindacati"
                                                Width="272px" TabIndex="43" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; height: 20px; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAUAnnullaStampa" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="43" Text="Diffide AU - Annulla Stampa Lettere" Width="272px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; height: 20px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; height: 20px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="Chabbinamento" runat="server" Text="ABBINAMENTO" TabIndex="38"
                                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="cERP" runat="server" Text="ERP" Width="61px" TabIndex="39" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                            <asp:CheckBox ID="c392" runat="server" Text="392" Width="61px" TabIndex="40" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                            <asp:CheckBox ID="c431" runat="server" Text="431" Width="60px" TabIndex="41" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                            <asp:CheckBox ID="cUD" runat="server" Text="UD" TabIndex="42" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                            <asp:CheckBox ID="cOA" runat="server" Text="O.A." TabIndex="43" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            &nbsp;<asp:CheckBox ID="ChFO" runat="server" Text="F.O." TabIndex="44" ToolTip="FORZE DELL'ORDINE"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                            <asp:CheckBox ID="ChCS" runat="server" Text="C.S." TabIndex="45" ToolTip="CAMBI CONSENSUALI"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                            <asp:CheckBox ID="ChConvenzionato" runat="server" Text="Can. Conv." TabIndex="45"
                                                ToolTip="CANONE CONVENZIONATO" Font-Names="ARIAL" Font-Size="9pt" />
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chPROVV" runat="server" Text="Provv.Assegnazione" TabIndex="46"
                                                Width="152px" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC">
                                            <asp:CheckBox ID="ChABB_DEC" runat="server" Text="GESTIONE LOCATARI" TabIndex="47"
                                                Width="315px" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC">
                                            <asp:CheckBox ID="ch_OP_VSA" runat="server" Text=" Operatore" Width="78px" TabIndex="39"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC" class="style1">
                                            <asp:CheckBox ID="ch_OP_RESP_VSA" runat="server" Text=" Parere Decisionale" Width="145px"
                                                TabIndex="39" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px;">
                                            &nbsp;
                                        </td>
                                        <td style="" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC">
                                            <asp:CheckBox ID="ChABB_EMRI" runat="server" Text="CAMBI ALLOGGIO" TabIndex="48"
                                                Width="340px" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px;">
                                            &nbsp;
                                        </td>
                                        <td style="" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="Chped" runat="server" Text="PED" Width="228px" TabIndex="49" Visible="False"
                                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="Chconsultazione" runat="server" Text="CONSULTAZIONE" Width="228px"
                                                TabIndex="50" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChManutenzioni" runat="server" Text="MANUTENZIONI" TabIndex="51"
                                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chPED2Completa" runat="server" Text="ANAGRAFE PATRIMONIO" TabIndex="52"
                                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChAPConsultazione" runat="server" Text="Solo Lettura" Width="138px"
                                                TabIndex="53" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="CHPED2ESTERNA" runat="server" Text="Solo IV e V Lotto" Width="172px"
                                                TabIndex="54" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chPedParametri" runat="server" Text="Parametri" Width="172px" TabIndex="54"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="MOD_CENS_MANUT" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                TabIndex="55" Text="Censimento Stato Manutentivo" Width="322px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="ChCensManutSL" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="56" Text="Solo Lettura" Width="138px" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChCensPRGInterventi" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="56" Text="Programma Interventi" Width="138px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkCaricMassProgrInt" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="56" Text="Caricamento massivo Programma Interventi" Width="360px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="chkMMModificaPatr" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="56" Text="Modifica tutte le UI" Width="360px" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chContabilita" runat="server" Text="CONTABILITA'" TabIndex="57"
                                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChContRagioneria" runat="server" Text="Ragioneria" Width="191px"
                                                TabIndex="58" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="ChContPatrimoniali" runat="server" Text="Cons.Patrimoniali" Width="191px"
                                                TabIndex="59" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChContFlussi" runat="server" Text="Flussi Finanz." Width="191px"
                                                TabIndex="60" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChContRimborsi" runat="server" Text="Rimborso Spese Gestore" Width="191px"
                                                TabIndex="61" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="ChContPrelievi" runat="server" Text="Prelievi" Width="191px" TabIndex="62"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChContCompensi" runat="server" Text="Compensi Gestore" Width="191px"
                                                TabIndex="63" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChContAllega" runat="server" Text="Allega Documenti Accertato"
                                                Width="191px" TabIndex="61" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="chGestTipoPag" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="117" Text="Gestione tabelle tipo contabilita" Width="219px" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkAnomalieRendicontazione" runat="server" Text="Anomalie Rendicontazione"
                                                Width="191px" TabIndex="61" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="chkLogRendicontazione" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="117" Text="Log Rendicontazione" Width="219px" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chCicloP" runat="server" Text="CICLO PASSIVO" TabIndex="64" Font-Bold="True"
                                                Font-Names="ARIAL" Font-Size="10pt" />
                                            <br />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChBPNuovo" runat="server" Text="BP-Nuovo Piano Finanziario" Width="183px"
                                                TabIndex="65" Font-Names="arial" Font-Size="9pt" />
                                            <br />
                                            <asp:CheckBox ID="ChBPResidui" runat="server" Text="BP-Residui" Width="183px" TabIndex="65"
                                                Font-Names="arial" Font-Size="9pt" />
                                            <br />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1" valign="top">
                                            <asp:CheckBox ID="ChBPGenerale" runat="server" Text="BP-Generale" Width="183px" TabIndex="66"
                                                Font-Names="arial" Font-Size="9pt" />
                                            <br />
                                            <asp:CheckBox ID="ChBPAppPreventivi" runat="server" Text="BP-Applicazione Preventivi"
                                                Width="183px" TabIndex="66" Font-Names="arial" Font-Size="9pt" />
                                            <br />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px; vertical-align: top; text-align: left;">
                                            <asp:CheckBox ID="ChSpReversibili" runat="server" Text="BP-Spese Reversibili" Width="183px"
                                                TabIndex="65" Font-Names="arial" Font-Size="9pt" />
                                            <asp:CheckBox ID="ChSpReversibiliSl" runat="server" Text="BP-Spese Reversibili (solo lettura)"
                                                Width="215px" TabIndex="65" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:Label ID="Label14" runat="server" Text="(per abilitare in sola lettura mettere segno di spunta su entrambe le caselle della funzione) "></asp:Label>
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChBPFormalizzazione" runat="server" Text="BP-Formalizzazione" Width="155px"
                                                        TabIndex="66" Font-Names="arial" Font-Size="9pt" />
                                                    <asp:CheckBox ID="ChBPFormalizzazione0" runat="server" Text="BP-Formalizzazione (solo lettura)"
                                                        Width="229px" TabIndex="67" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChBPCompilazione" runat="server" Text="BP-Compilazione" Width="155px"
                                                        TabIndex="68" Font-Names="arial" Font-Size="9pt" />
                                                    <asp:CheckBox ID="ChBPCompilazione0" runat="server" Text="BP-Compilazione (solo lettura)"
                                                        Width="190px" TabIndex="69" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChVariazioni" runat="server" Text="BP-Variazioni" Width="183px"
                                                        TabIndex="65" Font-Names="arial" Font-Size="9pt" />
                                                    <asp:CheckBox ID="ChVariazioniSL" runat="server" Text="BP-Variazioni (solo lettura)"
                                                        Width="183px" TabIndex="65" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChBPConvalidaAler" runat="server" Text="BP-Convalida Gestore" Width="126px"
                                                        TabIndex="70" Font-Names="arial" Font-Size="9pt" />
                                                    <br />
                                                    <asp:CheckBox ID="ChBPConvalidaAler0" runat="server" Text="BP-Convalida Gestore (solo lettura)"
                                                        Width="224px" TabIndex="71" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChBPCapitoli" runat="server" Text="BP-Assegn. Capitoli" Width="155px"
                                                        TabIndex="72" Font-Names="arial" Font-Size="9pt" />
                                                    <br />
                                                    <asp:CheckBox ID="ChBPCapitoli0" runat="server" Text="BP-Assegn. Capitoli (solo lettura)"
                                                        Width="207px" TabIndex="73" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChBPConvalidaComune" runat="server" Text="BP-Convalida Comune"
                                                        Width="175px" TabIndex="74" Font-Names="arial" Font-Size="9pt" />
                                                    <asp:CheckBox ID="ChBPConvalidaComune0" runat="server" Text="BP-Convalida Comune (solo lettura)"
                                                        Width="220px" TabIndex="75" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChBPVociServizio" runat="server" Text="BP-Gestione Voci Servizi"
                                                        Width="175px" TabIndex="76" Font-Names="arial" Font-Size="9pt" />
                                                    <br />
                                                    <asp:CheckBox ID="ChBPVociServizio0" runat="server" Text="BP-Voci Servizi (solo lettura)"
                                                        Width="197px" TabIndex="77" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="ChASSNuovo" runat="server" Text="BP-Nuovo Assestamento" Width="209px"
                                                TabIndex="79" Font-Names="arial" Font-Size="9pt" />
                                            <asp:CheckBox ID="ChASSCompila" runat="server" Text="BP-Compila Assestamento" Width="209px"
                                                TabIndex="79" Font-Names="arial" Font-Size="9pt" />
                                            <br />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChASSConvAler" runat="server" Text="BP-Convalida Assest. Gestore"
                                                Width="209px" TabIndex="79" Font-Names="arial" Font-Size="9pt" />
                                            <asp:CheckBox ID="ChASSConvComune" runat="server" Text="BP-Convalida Assest. COMUNE"
                                                Width="209px" TabIndex="79" Font-Names="arial" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChMS" runat="server" Text="Manutenzioni e Servizi" Width="175px"
                                                        TabIndex="78" Font-Names="arial" Font-Size="9pt" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChOP" runat="server" Text="Ordini e Pagamenti" Width="175px" TabIndex="80"
                                                        Font-Names="arial" Font-Size="9pt" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChPC" runat="server" Text="Pagamenti a Canone" Width="175px" TabIndex="82"
                                                        Font-Names="arial" Font-Size="9pt" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChMSL" runat="server" Text="Manutenzioni e Servizi (solo lettura)"
                                                Width="217px" TabIndex="79" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="ChOPL" runat="server" Text="Ordini e Pagamenti (solo lettura)"
                                                Width="208px" TabIndex="81" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChPCL" runat="server" Text="Pagamenti a Canone (solo lettura)"
                                                Width="214px" TabIndex="83" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkRielaboraCDP" runat="server" Text="Manutenzioni e Servizi - Rielabora CDP"
                                                Width="257px" TabIndex="188" Font-Names="arial" Font-Size="9pt" AutoPostBack="false" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="chkPagRielabCDP" runat="server" Text="Ordini e Pagamenti - Rielabora CDP"
                                                Width="238px" TabIndex="192" Font-Names="arial" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkPagCanoneRielCDP" runat="server" Text="Pagamenti a Canone - Rielabora CDP"
                                                Width="244px" TabIndex="196" Font-Names="arial" Font-Size="9pt" AutoPostBack="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkRielaboraSal" runat="server" Text="Manutenzioni e Servizi - Rielabora SAL"
                                                Width="257px" TabIndex="188" Font-Names="arial" Font-Size="9pt" AutoPostBack="false" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkPagCanoneRielSAL" runat="server" Text="Pagamenti a Canone - Rielabora SAL"
                                                Width="244px" TabIndex="196" Font-Names="arial" Font-Size="9pt" AutoPostBack="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChLO" runat="server" Text="Lotti" Width="175px" TabIndex="78" Font-Names="arial"
                                                        Font-Size="9pt" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="ChCC" runat="server" Text="Contratti" Width="175px" TabIndex="78"
                                                        Font-Names="arial" Font-Size="9pt" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChCCV" runat="server" Text="Contratti Variaz. Config.Patrimoniale"
                                                Width="239px" TabIndex="78" Font-Names="arial" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChLOL" runat="server" Text="Lotti (solo lettura)" Width="175px"
                                                TabIndex="78" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="ChCCL" runat="server" Text="Contratti (solo lettura)" Width="175px"
                                                TabIndex="78" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkParamCP" runat="server" Text="PARAMETRI" Width="239px" TabIndex="78"
                                                Font-Names="arial" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="chRSS" runat="server" Text="Non Patrimoniale" Width="175px" TabIndex="79"
                                                        Font-Names="arial" Font-Size="9pt" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="CHMAND_PAG" runat="server" Text="Mandati di Pagamento" Width="175px"
                                                TabIndex="79" Font-Names="arial" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChBuildingM" runat="server" Text="Building Manager" Width="175px"
                                                TabIndex="78" Font-Names="arial" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkRRSRielabCDP" runat="server" Text="Non patrimoniale - Rielabora CDP"
                                                Width="175px" TabIndex="212" Font-Names="arial" Font-Size="9pt" AutoPostBack="false" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="chkRRSRielabSAL" runat="server" Text="Non patrimoniale - Rielabora SAL"
                                                Width="175px" TabIndex="212" Font-Names="arial" Font-Size="9pt" AutoPostBack="false" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkCambioIvaOdl" runat="server" Text="Cambio IVA ODL" Width="100%"
                                                TabIndex="80" Font-Names="arial" Font-Size="9pt" Style="visibility: hidden" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chRSSSl" runat="server" Text="Non patrimoniale (solo lettura)"
                                                Width="175px" TabIndex="80" Font-Names="arial" Font-Size="9pt" AutoPostBack="True" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="chAutorizzazione" runat="server" Text="Direttore Lavori" Width="100%"
                                                TabIndex="80" Font-Names="arial" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chSuperDirettore" runat="server" Text="Gestore DL" Width="100%"
                                                TabIndex="80" Font-Names="arial" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chFQM" runat="server" Font-Names="arial" Font-Size="9pt" TabIndex="80"
                                                Text="Coordinatore qualità" Width="175px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="chTecnicoAmm" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="80" Text="Tecnico amministrativo" Width="175px" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkAnnullaSal" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="80" Text="Annulla SAL con CDP" Width="175px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="chkUtenze" runat="server" Text="Utenze/Multe/Custodi" Width="100%"
                                                TabIndex="80" Font-Names="arial" Font-Size="9pt" AutoPostBack="false" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                            <asp:CheckBox ID="chkAnnullaODL" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="80" Text="Annulla ODL in Fine Gestione" Width="200px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChEstraiSTR" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="80" Text="Estrazione STR" Width="175px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="ChImportaSTR" runat="server" Text="Consuntivazione STR" Width="100%"
                                                TabIndex="80" Font-Names="arial" Font-Size="9pt" AutoPostBack="false" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chModPagamento" runat="server" Text="Modifica modalità di pagamento"
                                                Width="100%" TabIndex="220" Font-Names="arial" Font-Size="9pt" AutoPostBack="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChVariazioneImporti" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="80" Text="Variazione importi" Width="175px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="chVariazioneComp" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="80" Text="Variazione composizione appalto" Width="175px" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chkRitornaInBozza" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="80" Text="Ritorna in bozza" Width="175px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chDashdoard" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="80" Text="Dashboard" Width="175px" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="chAnticipoSingolaVoce" runat="server" Font-Names="arial" Font-Size="9pt"
                                                TabIndex="80" Text="Anticipo su singola voce" Width="175px" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chGC" runat="server" Text="AGENDA E SEGNALAZIONI" TabIndex="84"
                                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChGC_SL" runat="server" Text="Solo Lettura" Width="191px" TabIndex="85"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="ChGC_TabelleSupporto" runat="server" Text="Gestione Tabelle Supporto"
                                                Width="191px" TabIndex="86" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChGC_Report" runat="server" Text="Report" Width="191px" TabIndex="86"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChGC_Segnalazioni" runat="server" Text="Inserimento/Modifica Segnalazione"
                                                Width="100%" TabIndex="85" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            <asp:CheckBox ID="ChGC_APPUNTAMENTI" runat="server" Text="Inserimento/Modifica Appuntamenti"
                                                Width="100%" TabIndex="86" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChGC_Calendario" runat="server" Text="Gestione Calendario" Width="191px"
                                                TabIndex="86" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChGC_Supervisore" runat="server" Text="Supervisore" Width="191px"
                                                TabIndex="85" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom-style: dashed; border-bottom-width: 1px;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; border-bottom-style: dashed; border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="chCondominio" runat="server" Text="CONDOMINIO" TabIndex="87" Font-Bold="True"
                                                Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 378px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            <asp:CheckBox ID="ChCondominioSL" runat="server" Text="Solo Lettura" Width="191px"
                                                TabIndex="88" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC; border-bottom-style: dashed; border-bottom-width: 1px;"
                                            class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 136px; background-color: #DCDCDC; border-bottom-style: dashed;
                                            border-bottom-width: 1px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                        </td>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 213px">
                                        </td>
                                        <td style="width: 378px">
                                        </td>
                                        <td class="style1">
                                        </td>
                                        <td style="width: 136px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chDemanio" runat="server" Text="IMPIANTI" TabIndex="89" Font-Bold="True"
                                                Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChDemanioSL" runat="server" Text="Solo Lettura" Width="191px" TabIndex="90"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChDemEl" runat="server" Text="Imp. ELETTRICO" Width="191px" TabIndex="91"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="ChDemID" runat="server" Text="Imp. IDRICO" Width="191px" TabIndex="96"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChDemTC" runat="server" Text="Imp. CENTRALE TERM." Width="191px"
                                                TabIndex="100" ToolTip="Impianto Termico Centralizzato" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChDemTA" runat="server" Text="Imp. TERM.AUTONOMO" Width="196px"
                                                TabIndex="92" ToolTip="Impianto Termico Autonomo" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="ChDemTR" runat="server" Text="Imp. TELERISCALD." Width="170px"
                                                TabIndex="97" ToolTip="Impianto Teleriscaldamento" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChDemSO" runat="server" Text="Imp. SOLLEVAMENTO" Width="185px"
                                                TabIndex="101" ToolTip="Impianto Sollevamento" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChDemAM" runat="server" Text="Imp. ACQ. METEORICHE" Width="194px"
                                                TabIndex="93" ToolTip="Impianto Acque Meteoriche" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="ChDemAI" runat="server" Text="Imp. ANTINCENDIO" Width="185px" TabIndex="98"
                                                ToolTip="Impianto Antiincendio" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChDemTI" runat="server" Text="Imp. TUTELA IMM." Width="185px" TabIndex="102"
                                                ToolTip="Impianto Tutela Immobile" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChDemCF" runat="server" Text="Imp. CANNA FUM." Width="185px" TabIndex="94"
                                                ToolTip="Impianto Canna Fumaria" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="ChDemGAS" runat="server" Text="Imp. GAS" Width="185px" TabIndex="99"
                                                ToolTip="Impianto GAS" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChDemCITOFONICO" runat="server" Text="Imp. CITOFONICO" Width="185px"
                                                TabIndex="103" ToolTip="Impianto Citofonico" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChDemTV" runat="server" Text="Imp. TV" Width="185px" TabIndex="95"
                                                ToolTip="Impianto TV" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                        </td>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 378px">
                                        </td>
                                        <td class="style1">
                                        </td>
                                        <td style="width: 136px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChContratti" runat="server" Text="RAPPORTI UTENZA" TabIndex="104"
                                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTILETTURA" runat="server" Text="Solo Lettura" Width="191px"
                                                TabIndex="105" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            <asp:CheckBox ID="ChCONTRATTIins" runat="server" Text="Ins. Contratti" Width="191px"
                                                TabIndex="106" ToolTip="Inserimento Contratti" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChVirtuali" runat="server" Text="Ins. Contratti VIRTUALI" Width="191px"
                                                TabIndex="107" ToolTip="Inserimento Contratti" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px">
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                        </td>
                                        <td style="background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 378px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTIreg" runat="server" Text="Registrazione Cont." Width="191px"
                                                TabIndex="108" ToolTip="Registrazione massiva Contratti" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td class="style5" style="background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTIimp" runat="server" Text="Calcolo Imposte" Width="141px"
                                                TabIndex="109" ToolTip="Calcolo imposte per agenzia entrate" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="width: 136px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTIist" runat="server" Text="Calcolo Agg. ISTAT" Width="191px"
                                                TabIndex="110" ToolTip="Calcolo massivo degli aggiornamenti istat" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTIPARAMETRI" runat="server" Text="Parametri" Width="191px"
                                                TabIndex="111" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="ChCONTRATTItesto" runat="server" Text="Testo Contratti" Width="191px"
                                                TabIndex="112" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTId" runat="server" Text="Op.Sede Territoriale - Dis./Recup. UI"
                                                Width="191px" TabIndex="113" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTIint" runat="server" Text="Calcolo Interessi Leg." Width="191px"
                                                TabIndex="114" ToolTip="Calcolo Massivo degli interessi Legali" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="ChCONTRATTIbollette" runat="server" Text="Emissione Bollettazione Massiva"
                                                Width="226px" TabIndex="115" ToolTip="Emissione Bollette e Creazione file Banca"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTIMor" runat="server" Text="Morosità" Width="191px" TabIndex="116"
                                                ToolTip="Emissione Bollette e Creazione file Banca" Font-Names="ARIAL" Font-Size="9pt"
                                                Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTIRinnoviUSD" runat="server" Text="Rinnovo USD" Width="191px"
                                                TabIndex="117" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="ChCONTRATTICambiBox" runat="server" Text="Cambio Intestazione BOX"
                                                Width="191px" TabIndex="118" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTIPEXTRA" runat="server" Text="Inserim. Pag. EXTRA MAV"
                                                Width="191px" TabIndex="116" ToolTip="Inserimento pagamenti extra mav" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCONTRATTIRateizzazione" runat="server" Text="Rateizzazione Bollette"
                                                Width="191px" TabIndex="117" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="ChSpostaAnnullo" runat="server" Text="Spostamento/Annullamento"
                                                Width="191px" TabIndex="117" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChAmmRptPagExtraMav" runat="server" Text="Supervisore Report Pag. EXTRA MAV"
                                                Width="226px" TabIndex="116" ToolTip="Report Pagamenti EXTRA MAV " Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChElaborazMass" runat="server" Text="Elaboraz. Massiva Partita Gest."
                                                Width="191px" TabIndex="123" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="ChElaborazSing" runat="server" Text="Elaboraz. Singola Doc. Gest."
                                                Width="191px" TabIndex="124" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChTrasfPag" runat="server" Text="Trasferim. Pagam. in nuovo RU"
                                                Width="250px" TabIndex="125" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chDistanzaKM" runat="server" Text="Distanza km Comuni" Width="191px"
                                                TabIndex="126" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="chRecaGest" runat="server" Text="Parametrizzazione ReCa" Width="191px"
                                                TabIndex="126" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chRUAU" runat="server" Text="Report RU-AU" Width="191px" TabIndex="126"
                                                Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChkAnnullaRAT" runat="server" Text="Annulla Rateizzazione" Width="191px"
                                                TabIndex="117" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="chRUExport" runat="server" Font-Names="ARIAL" Font-Size="9pt" Height="20px"
                                                TabIndex="126" Text="Export Ricerca Contratti" Width="191px" Visible="False" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chRUSALDI" runat="server" Font-Names="ARIAL" Font-Size="9pt" Height="20px"
                                                TabIndex="126" Text="Report RU-SALDI" Width="191px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chRUNuovaBolletta" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                Height="20px" TabIndex="126" Text="Creazione/Modifica bolletta" Width="191px" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="ChRUMAV" runat="server" Font-Names="ARIAL" Font-Size="9pt" Height="20px"
                                                TabIndex="126" Text="Generazione MAV on Line" Width="191px" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chRUNote" runat="server" Text="Inserimento massivo note" Width="191px"
                                                TabIndex="126" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chCrDe" runat="server" Text="Abilita/disabilita scritt. gestionali"
                                                Width="219px" TabIndex="126" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style1">
                                            <asp:CheckBox ID="chRimbDep" runat="server" Font-Names="ARIAL" Font-Size="9pt" Height="20px"
                                                TabIndex="126" Text="Rimborso dep. Cauzionale" Width="191px" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chRUDatiAE" runat="server" Font-Names="ARIAL" Font-Size="9pt" Height="20px"
                                                TabIndex="322" Text="Modifica Dati Registrazione A.E." Width="219px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkPagCOMUNE" runat="server" Text="Visualizza pag. Contr. Sol. COMUNALI"
                                                Width="250px" TabIndex="116" ToolTip="Inserimento pagamenti extra mav" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkInserimRuoli" runat="server" Text="Inserim. Pag. Ruoli" Width="191px"
                                                TabIndex="116" ToolTip="Inserimento pagamenti ruoli" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkReportRuoli" runat="server" Text="Report Pag. RUOLI" Width="226px"
                                                TabIndex="116" ToolTip="Report Pagamenti RUOLI" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkSbloccoBoll" runat="server" Text="Sblocco bollettazione singolo RU"
                                                Width="250px" TabIndex="116" ToolTip="Inserimento pagamenti extra mav" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkForzaRestituz" runat="server" Text="Forza restituzione crediti"
                                                Width="191px" TabIndex="116" ToolTip="Forza restituzione crediti" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkRestIntDep" runat="server" Text="Parametri - Rest. Interessi Dep. Cauz."
                                                Width="227px" TabIndex="116" ToolTip="Gestione parametri Restituzione interessi deposito cauzionale"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkSceltaDestEcc" runat="server" Text="Scelta destinazione eccedenza"
                                                Width="250px" TabIndex="116" ToolTip="Scelta destinazione eccedenza" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkInserimIng" runat="server" Text="Inserim. Pag. Ingiunzioni"
                                                Width="191px" TabIndex="116" ToolTip="Inserimento Pagamenti Ingiunzioni" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkReportIng" runat="server" Text="Report Pag. Ingiunzioni" Width="191px"
                                                TabIndex="126" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chMassIngiunz" runat="server" Text="Inserim. massivo boll. ingiunte"
                                                Width="191px" TabIndex="126" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chSingIngiunz" runat="server" Text="Inserim. sing. boll. ingiunta"
                                                Width="191px" TabIndex="126" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkModificaANA" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="116" Text="Modifica Anagrafiche" ToolTip=" Modifica dati anagrafici degli inquilini"
                                                Width="250px" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChSpalmatore" runat="server" Text="Compensazione crediti" Width="191px"
                                                TabIndex="126" Font-Names="ARIAL" Font-Size="9pt" Height="20px" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkForzaScad" runat="server" Text="Forza scadenza boll. ripartibili"
                                                Width="191px" TabIndex="116" ToolTip="Forza restituzione crediti" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkMotiviDec" runat="server" Text="Parametri - Motivi Decisioni"
                                                Width="227px" TabIndex="116" ToolTip="Gestione parametri Motivi Decisioni" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chkAnnullaIncaGest" runat="server" Text="Annulla Inca. Gestionali"
                                                Width="191px" TabIndex="116" ToolTip="Inserimento pagamenti extra mav" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                           <asp:CheckBox ID="chkOA" runat="server" Text="Gestione OA"
                                                Width="250px" TabIndex="116" ToolTip="Gestione tab OA" Font-Names="ARIAL"
                                                Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 603px">
                                            </td>
                                        <td style="width: 603px">
                                        </td>
                                        <td style="width: 213px">
                                            &nbsp; &nbsp;
                                        </td>
                                        <td style="width: 378px">
                                            &nbsp; &nbsp;
                                        </td>
                                        <td class="style1">
                                        </td>
                                        <td style="width: 136px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChGE" runat="server" Text="GESTIONE AUTONOMA" TabIndex="119" Font-Bold="True"
                                                Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChGEL" runat="server" Text="Solo Lettura" Width="191px" TabIndex="120"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px">
                                        </td>
                                        <td style="width: 603px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td style="width: 213px">
                                            &nbsp; &nbsp;
                                        </td>
                                        <td style="width: 378px">
                                            &nbsp; &nbsp;
                                        </td>
                                        <td class="style1">
                                            &nbsp; &nbsp;
                                        </td>
                                        <td style="width: 136px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChMOR" runat="server" Text="GESTIONE MOROSITA'" TabIndex="117"
                                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" />
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChMOR_SL" runat="server" Text="Solo Lettura" Width="191px" TabIndex="118"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            <asp:CheckBox ID="ChMOR_ANN" runat="server" Text="Annullo Morosità" Width="191px"
                                                TabIndex="118" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChSatisfaction" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                                Font-Size="10pt" TabIndex="117" Text="CUSTOMER SATISFACTION" />
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChSatisfactionL" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="118" Text="Solo Lettura" Width="191px" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            <asp:CheckBox ID="ChSatisfSuperVisore" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="118" Text="Supervisore" Width="191px" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChStampeMassive" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                                Font-Size="10pt" TabIndex="117" Text="STAMPE MASSIVE" />
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChArchivio" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                                Font-Size="10pt" TabIndex="117" Text="ARCHIVIO" />
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChArchIM" runat="server" Font-Names="ARIAL" Font-Size="9pt" TabIndex="118"
                                                Text="Inserimento/Modifica" Width="191px" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            <asp:CheckBox ID="ChArchC" runat="server" Font-Names="ARIAL" Font-Size="9pt" TabIndex="119"
                                                Text="Cancellazione" Width="191px" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chModRilievo" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                                Font-Size="10pt" TabIndex="120" Text="RILIEVO" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            <asp:CheckBox ID="chRilievoGEST" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="121" Text="Gestione" Width="191px" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chRilievoCDati" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="122" Text="Caricamento Dati" Width="191px" />
                                        </td>
                                        <td style="background-color: #DCDCDC;">
                                            <asp:CheckBox ID="chRilievoParam" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                                                TabIndex="123" Text="Parametri" Width="191px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChSiraper" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                                                TabIndex="124" Text="SIRAPER" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="cbARPA" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                                                TabIndex="124" Text="A.R.P.A. Lombardia" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChSicurezza" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                                Font-Size="10pt" TabIndex="124" Text="SICUREZZA" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            <asp:CheckBox ID="ChTpa_SL" runat="server" Text="Solo Lettura" Width="191px" TabIndex="85"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCreaSegn" runat="server" Text="Creazione Segnalazione" Width="100%"
                                                TabIndex="85" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChModificaSegn" runat="server" Text="Modifica Segnalazione" Width="100%"
                                                TabIndex="85" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChAssegnOperatori" runat="server" Text="Assegnazione Operatori"
                                                Width="100%" TabIndex="85" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChVisualizzaAgenda" runat="server" Text="Visualizzazione Agenda"
                                                Width="100%" TabIndex="85" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChCreaSegnIntProc" runat="server" Text="Creazione Segnalaz./Interv./Procedim."
                                                Width="100%" TabIndex="85" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChModFornitori" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                                Font-Size="10pt" TabIndex="124" Text="FORNITORI" />
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5" valign="middle">
                                            <asp:CheckBox ID="ChFO_SLE" runat="server" Text="Solo Lettura" Width="191px" TabIndex="124"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                            <asp:Label ID="Label18" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Fornitore di appartenenza:" Width="158px"></asp:Label>
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            <span style="font-size: 8pt; font-family: Arial">
                                                <asp:DropDownList ID="cmbFornitori" runat="server" Font-Names="arial" Font-Size="8pt"
                                                    Width="350px" TabIndex="86" CssClass="chzn-select">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;<asp:CheckBox ID="ChFO_RDO" runat="server" Text="Interventi" Width="191px"
                                                TabIndex="85" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;<asp:CheckBox ID="ChFO_ODL" runat="server" Text="Calendario Lavori" Width="191px"
                                                TabIndex="85" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;<asp:CheckBox ID="ChFO_RPT" runat="server" Text="Reportistica" Width="191px"
                                                TabIndex="85" Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;">
                                            <asp:CheckBox ID="ChFO_Log" runat="server" Text="Log Eventi" Width="191px" TabIndex="85"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 603px; height: 20px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; height: 20px; background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                        <td style="background-color: #DCDCDC;" class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="width: 603px; background-color: #DCDCDC;">
                                            &nbsp;<asp:CheckBox ID="ChFO_PAR" runat="server" Text="Parametri" Width="191px" TabIndex="85"
                                                Font-Names="ARIAL" Font-Size="9pt" />
                                        </td>
                                        <td style="background-color: #DCDCDC;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="height: 30px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                                                ForeColor="Red" Visible="False" Width="501px"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="idOperatore" runat="server" />
        <asp:HiddenField ID="nomeoperatore" runat="server" />
        <asp:HiddenField ID="nomeutente" runat="server" />
        <asp:HiddenField ID="sicuro" runat="server" />
        <asp:HiddenField ID="hidScroll" runat="server" />
    </div>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
