<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ModificaContratto.aspx.vb" Inherits="GestioneModelli_ModificaContratto" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript">
    function ConfermaUscita() {
        if (document.getElementById('txtModificato').value == '1') {
            var sicuro = window.confirm('Attenzione, sono state fatte delle modifiche al testo. Sei sicuro di voler CHIUDERE senza salvare?');
            if (sicuro == true) {
                document.getElementById('txtAttiva').value = '0';
            }
            else {
                document.getElementById('txtAttiva').value = '1';
            }
        }
    }

    function ConfermaStampa() {
        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione, sono state apportate delle modifiche. Salvare prima di stampare!');
            document.getElementById('txtAttiva').value = '1';
        }
        else {
            var sicuro = window.confirm('Attenzione, Sei sicuro di voler stampare il contratto? Assicurarsi di aver apportato tutte le modifiche necessarie al caso prima di stampare.\nQuesto contratto sarà trasmesso alle agenzie competenti.');
            if (sicuro == true) {
                document.getElementById('txtAttiva').value = '0';
            }
            else {
                document.getElementById('txtAttiva').value = '1';
            }
        }
    }

    function ConfermaAnnullo() {

        var sicuro = window.confirm('Attenzione, Sei sicuro di voler ANNULLARE LA STAMPA? Si ricorda che le bollette di attivazione create e valide saranno annullate!\nQuesta operazione sarà registrata!');
        if (sicuro == true) {
            document.getElementById('txtAttiva').value = '0';
        }
        else {
            document.getElementById('txtAttiva').value = '1';
        }

    }

    function aprichiudi(item) {
        elem = document.getElementById(item);
        visibile = (elem.style.display != "none")
        prefisso = document.getElementById("menu" + item);
        if (visibile) {
            elem.style.display = "none";
            prefisso.innerHTML = "<img src='cartella.gif' width='16' height='16' hspace='0' vspace='0' border='0'>";
        } else {
            elem.style.display = "block";
            prefisso.innerHTML = "<img src='cartellaaperta.gif' width='16' height='16' hspace='0' vspace='0' border='0'>";
        }
    }

    function espanditutto() {
        divs = document.getElementsByTagName("div");
        for (i = 0; i < divs.length; i++) {
            if (divs[i].id == 'SEZ1' || divs[i].id == 'SEZ2' || divs[i].id == 'SEZ3' || divs[i].id == 'SEZ4' || divs[i].id == 'SEZ5') {
                divs[i].style.display = "block";
                prefisso = document.getElementById("menu" + divs[i].id);
                prefisso.innerHTML = "<img src='cartellaaperta.gif' width='16' height='16' hspace='0' vspace='0' border='0'>";
            }
        }
    }

    function chiuditutto() {
        divs = document.getElementsByTagName("div");
        for (i = 0; i < divs.length; i++) {
            if (divs[i].id == 'SEZ1' || divs[i].id == 'SEZ2' || divs[i].id == 'SEZ3' || divs[i].id == 'SEZ4' || divs[i].id == 'SEZ5') {
                divs[i].style.display = "none";
                prefisso = document.getElementById("menu" + divs[i].id);
                prefisso.innerHTML = "<img src='cartella.gif' width='16' height='16' hspace='0' vspace='0' border='0'>";
            }
        }
    }
</script>
<head id="Head1" runat="server">
    <title>Modifica Modello</title>
    <style type="text/css">
        .style1 {
            width: 157px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="imgSalva">

        <table style="width: 100%;">
            <tr>
                <td bgcolor="Maroon" style="text-align: center">
                    <asp:Label ID="LBLcONTRATTO" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="14pt" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table style="width: 100%;" bgcolor="#E5E5E5" cellpadding="0"
            cellspacing="0">
            <tr>
                <td>
                    <asp:ImageButton ID="ImgEsci" runat="server"
                        ImageUrl="~/NuoveImm/Img_Esci_AMM.png" OnClientClick="ConfermaUscita();"
                        Style="height: 20px" />
                </td>
                <td>
                    <asp:ImageButton ID="imgSalva" runat="server"
                        ImageUrl="~/NuoveImm/img_SalvaModelli.png" ToolTip="Salva il testo"
                        OnClientClick="window.opener.document.forms['form1'].elements['txtModificato'].value = '1';" />
                </td>
                <td>
                    <asp:ImageButton ID="imgStampa" runat="server"
                        ImageUrl="~/NuoveImm/Img_StampaContratto.png"
                        ToolTip="Stampa il Contratto in formato XML, questo file dovrà poi essere inviato all'agenzia delle entrate tramite apposito software."
                        OnClientClick="ConfermaStampa();" />
                    &nbsp;
                    <a href="Info.htm" target="_blank">
                        <asp:Image ID="ImgInfo" runat="server" ImageUrl="~/GestioneModelli/info.png"
                            ToolTip="Informazioni sulla procedura di stampa del contratto in 2 pagine"
                            Visible="False" /></a>
                    <asp:ImageButton ID="imgConduttore" runat="server"
                        ImageUrl="~/NuoveImm/Img_Copia_Conduttore.png"
                        ToolTip="Stampa il Contratto in 2 pagine. Visualizzare le note premeno il pulsante &quot;i&quot; a Sinistra."
                        Visible="False" />
                    <asp:ImageButton ID="imgAllegatoContratto" runat="server"
                        ImageUrl="~/NuoveImm/Img_AllegatoContratto.png"
                        ToolTip="Stampa l'allegato del contratto da consegnare alla Sede Territoriale."
                        Visible="False"
                        OnClientClick="document.getElementById('HiddenField2').value='1';"
                        Style="height: 20px" />
                </td>
                <td>
                    <asp:ImageButton ID="imgAnnullaStampa" runat="server"
                        ImageUrl="~/NuoveImm/Img_Annulla_Stampa.png"
                        ToolTip="Annulla la stampa e permette di effettuare una nuova stampa."
                        Visible="False" OnClientClick="ConfermaAnnullo();" />
                </td>
                <td>
                    <img id="imgSegnaPosto" alt="Segna Posto" src="../NuoveImm/Img_SegnaPosto.png"
                        border="0"
                        onclick="javascript:window.open('Segnaposto.aspx?ID='+document.getElementById('HiddenField1').value,'SegnaPosto','');"
                        style="cursor: pointer" /></td>
                <td>
                    <asp:ImageButton ID="imgAnteprima" runat="server"
                        ImageUrl="~/NuoveImm/Img_AnteprimaModelli.png"
                        ToolTip="Anteprima Contratto" />
                </td>
            </tr>
            <tr bgcolor="White">
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td class="style1"
                    style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                <td>&nbsp;
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt"
                                    Text="Quota Preventiva:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpreventiva" runat="server" Font-Names="arial"
                                    Font-Size="10pt" TabIndex="1" Width="70px" Enabled="false">0,00</asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt"
                                    Text="Riscaldamento:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtriscaldamento" runat="server" Font-Names="arial"
                                    Font-Size="10pt" TabIndex="2" Width="70px" Enabled="false">0,00</asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt"
                                    Text="Ascensore:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAscensore" runat="server" Font-Names="arial"
                                    Font-Size="10pt" TabIndex="3" Width="70px" Enabled="false">0,00</asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt"
                                    Text="Serv.Comuni:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComuni" runat="server" Font-Names="arial" Font-Size="10pt"
                                    TabIndex="4" Width="70px" EnableTheming="True" Enabled="false">0,00</asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="10pt"
                                    Text="Amm. e Gen."></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGenerali" runat="server" Font-Names="arial"
                                    Font-Size="10pt" TabIndex="5" Width="70px" Enabled="false">0,00</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1"
                    style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;&nbsp;</td>
                <td>
                    <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="10pt"
                        Text="Annotazioni:"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtNoteContratto" runat="server" Font-Names="arial"
                        Font-Size="10pt" TabIndex="6" Width="793px" MaxLength="200"></asp:TextBox>
                    &nbsp;<br />
                    <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="8pt"
                        Text="(max 200 caratteri - Se la stampa della copia conduttore non dovesse rientrare in 2 pagine, agire sulle impostazioni dei margini di stampa)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1"
                    style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr bgcolor="#E5E5E5">
                <td class="style1"
                    style="font-family: arial, Helvetica, sans-serif; font-size: 12px">TITOLO DEL CONTRATTO</td>
                <td>
                    <asp:TextBox ID="txtTitoloContratto" runat="server" Font-Names="arial" Font-Size="10pt"
                        MaxLength="100" ToolTip="Inserire il Titolo del Contratto" Width="95%"></asp:TextBox>
                </td>
            </tr>
        </table>

        <table cellpadding='1' cellspacing='1' style="width: 100%;" bgcolor="#E5E5E5">
            <tr>
                <td width='16'>
                    <a id="menuSEZ1" href="javascript:aprichiudi('SEZ1');">
                        <img alt='Cartella' src='cartella.gif' width='16' height='16' hspace='0' vspace='0' border='0' /></a>
                </td>
                <td><strong>SEZIONE 1</strong></td>
            </tr>
        </table>
        <div id="SEZ1" style="display: none; margin-left: 2em; background-color: #E5E5E5;">
            <table style="width: 100%;" border="0" cellpadding='1' cellspacing='1'>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">TITOLO</td>
                    <td>
                        <asp:TextBox ID="txtTitoloSezione1" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="100" ToolTip="Titolo Sezione" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S1P1" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S1P2" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S1P3" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S1P4" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S1P5" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <table cellpadding='1' cellspacing='1' style="width: 100%;">
            <tr>
                <td width='16'>
                    <a id="menuSEZ2" href="javascript:aprichiudi('SEZ2');">
                        <img alt='Cartella' src='cartella.gif' width='16' height='16' hspace='0' vspace='0' border='0' /></a>
                </td>
                <td><strong>SEZIONE 2</strong></td>
            </tr>
        </table>
        <div id="SEZ2" style="display: none; margin-left: 2em;">
            <table style="width: 100%;" border='0' cellpadding='1' cellspacing='1'>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">TITOLO</td>
                    <td>
                        <asp:TextBox ID="txtTitoloSezione2" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="100" ToolTip="Titolo Sezione" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S2P1" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S2P2" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S2P3" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S2P4" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S2P5" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <table cellpadding='1' cellspacing='1' style="width: 100%;" bgcolor="#E5E5E5">
            <tr>
                <td width='16'>
                    <a id="menuSEZ3" href="javascript:aprichiudi('SEZ3');">
                        <img alt='Cartella' src='cartella.gif' width='16' height='16' hspace='0' vspace='0' border='0' /></a>
                </td>
                <td><strong>SEZIONE 3</strong></td>
            </tr>
        </table>
        <div id="SEZ3" style="display: none; margin-left: 2em; background-color: #E5E5E5;">
            <table style="width: 100%;" border='0' cellpadding='1' cellspacing='1'>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">TITOLO</td>
                    <td>
                        <asp:TextBox ID="txtTitoloSezione3" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="100" ToolTip="Titolo Sezione" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P1" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P2" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P3" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P4" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P5" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P6" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P7" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P8" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P9" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P10" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P11" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P12" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P13" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P14" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P15" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P16" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P17" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P18" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P19" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P20" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P21" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P22" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P23" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P24" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P25" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P26" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P27" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P28" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P29" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S3P30" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <table cellpadding='1' cellspacing='1' style="width: 100%;">
            <tr>
                <td width='16'>
                    <a id="menuSEZ4" href="javascript:aprichiudi('SEZ4');">
                        <img alt='Cartella' src='cartella.gif' width='16' height='16' hspace='0' vspace='0' border='0' /></a>
                </td>
                <td><strong>SEZIONE 4</strong></td>
            </tr>
        </table>
        <div id="SEZ4" style="display: none; margin-left: 2em;">
            <table style="width: 100%;" border='0' cellpadding='1' cellspacing='1'>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">TITOLO</td>
                    <td>
                        <asp:TextBox ID="txtTitoloSezione4" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="100" ToolTip="Titolo Sezione" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P1" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P2" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P3" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P4" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P5" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P6" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P7" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P8" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P9" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P10" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P11" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P12" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P13" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P14" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P15" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P16" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P17" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P18" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P19" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P20" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P21" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P22" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P23" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P24" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P25" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P26" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P27" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P28" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P29" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P30" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                </tr>
                            <tr>
                                <td class="style1"
                                    style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="S4P31" runat="server" Font-Names="arial" Font-Size="10pt"
                                        MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P32" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P33" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P34" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P35" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P36" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P37" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P38" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P39" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P40" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P41" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P42" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P43" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P44" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P45" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P46" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P47" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P48" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P49" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P50" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P51" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P52" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P53" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P54" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P55" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P56" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P57" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P58" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P59" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S4P60" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <table cellpadding='1' cellspacing='1' style="width: 100%;" bgcolor="#E5E5E5">
            <tr>
                <td width='16'>
                    <a id="menuSEZ5" href="javascript:aprichiudi('SEZ5');">
                        <img alt='Cartella' src='cartella.gif' width='16' height='16' hspace='0' vspace='0' border='0' /></a>
                </td>
                <td><strong>SEZIONE 5</strong></td>
            </tr>
        </table>
        <div id="SEZ5" style="display: none; margin-left: 2em; background-color: #E5E5E5;">
            <table style="width: 100%;" border='0' cellpadding='1' cellspacing='1'>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">TITOLO</td>
                    <td>
                        <asp:TextBox ID="txtTitoloSezione5" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="100" ToolTip="Titolo Sezione" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S5P1" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S5P2" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S5P3" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S5P4" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo" Width="95%" Height="52px"
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"
                        style="font-family: arial, Helvetica, sans-serif; font-size: 12px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="S5P5" runat="server" Font-Names="arial" Font-Size="10pt"
                            MaxLength="10" ToolTip="Paragrafo " Width="95%" Height="52px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <p>
            <a href="javascript:espanditutto();">Espandi Tutto</a><br />
            <a href="javascript:chiuditutto();">Chiudi Tutto</a>
        </p>



        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
        <asp:HiddenField ID="txtAttiva" runat="server" Value="0" />
        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
        <asp:TextBox ID="TextBox1" runat="server" Font-Names="Courier New"
            Font-Size="8pt" Height="171px" MaxLength="80" TextMode="MultiLine"
            Width="1017px" Visible="False"></asp:TextBox>
    </form>
    <script type="text/javascript">
        if (document.getElementById('HiddenField2').value == '0') {
            window.focus();
            self.focus();
        }
    </script>
</body>
</html>
