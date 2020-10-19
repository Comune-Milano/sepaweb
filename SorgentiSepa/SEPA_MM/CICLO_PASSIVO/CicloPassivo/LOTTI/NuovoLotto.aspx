<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoLotto.aspx.vb" Inherits="LOTTI_GestioneLotto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">

    function ApriEventi() {
        window.open('Report/Eventi.aspx?ID_LOTTO=' + document.getElementById('HiddenID').value, "WindowPopup", "scrollbars=1, width=800px, height=600px, resizable");
    };


    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }


    function $onkeydown() {

        if (event.keyCode == 13) {
            event.keyCode = 0;
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\-\,]/g
    }


    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
        //        o.value = o.value.replace('.', ',');
        document.getElementById('txtModificato').value = '1';
    }


    function DelPointer(obj) {
        obj.value = obj.value.replace('.', '');
        document.getElementById(obj.id).value = obj.value;

    }


    function AutoDecimal2(obj) {
        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
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
    }


    function mostraDiv() {


        document.getElementById('caricamento').style.visibility = 'visible';


    }

    var Selezionato;

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>MODULO GESTIONE LOTTI</title>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <script type="text/javascript" src="../../../Contratti/prototype.lite.js"></script>
    <script type="text/javascript" src="../../../Contratti/moo.fx.js"></script>
    <script type="text/javascript" src="../../../Contratti/moo.fx.pack.js"></script>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
    <script type="text/javascript">
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');
        //var tabberOptions = {'onClick':function(){alert("clicky!");}};
        var tabberOptions = {
            /* Optional: code to run when the user clicks a tab. If this
            function returns boolean false then the tab will not be changed
            (the click is canceled). If you do not return a value or return
            something that is not boolean false, */

            'onClick': function (argsObj) {

                var t = argsObj.tabber; /* Tabber object */
                var id = t.id; /* ID of the main tabber DIV */
                var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                var e = argsObj.event; /* Event object */

                document.getElementById('txttab').value = i + 1;
            },
            'addLinkId': true
        };

    </script>
    <script language="javascript" type="text/javascript">

        //window.onbeforeunload = confirmExit; 

        function EliminaPagamento() {
            var sicuro = confirm('Sei sicuro di voler eliminare questo Pagamento? Tutti i dati andranno persi.');
            if (sicuro == true) {
                document.getElementById('txtElimina').value = '1';
            }
            else {
                document.getElementById('txtElimina').value = '0';
            }
        }

        function AnnullaPagamento() {
            var sicuro = confirm('Sei sicuro di voler annullare questa pagamento? L\'ordine visualizzato non sarà più modificabile!');
            if (sicuro == true) {
                document.getElementById('txtElimina').value = '1';
            }
            else {
                document.getElementById('txtElimina').value = '0';
            }
        }


        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                //if (document.getElementById('txtStatoPagamento').value<=3){
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                    // document.getElementById('USCITA').value='0';
                }
                // }
            }
        }



        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dalla scheda Lotti premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dalla scheda manutenzione premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
            }
        }




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
</head>
<body class="sfondo">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindow ID="RadWindowLotto" runat="server" CenterIfModal="true" Modal="true"
        Title="Gestione servizi voci" Width="680px" Height="480px" VisibleStatusbar="false"
        Behaviors="Pin, Maximize, Move, Resize">
        <ContentTemplate>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
                Localization-Cancel="Annulla">
            </telerik:RadWindowManager>
            <asp:Panel runat="server" ID="PanelLotto" Style="height: 100%;" class="sfondo">
                <table>
                    <tr>
                        <td class="TitoloModulo">
                            <asp:Label ID="Label2" runat="server">ELENCO EDIFICI o IMPIANTI</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="BtnConfermaComplessoTutti" Text="Seleziona tutti" runat="server"
                                            ToolTip="Seleziona tutti gli edifici o impianti">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="BtnConfermaComplesso" Text="Aggiorna" runat="server" ToolTip="Aggiorna ed esci">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnChiudi" Text="Esci" runat="server" OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowLotto');}"
                                            AutoPostBack="false" ToolTip="Esci senza inserire">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;&nbsp; &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp; &nbsp;
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" TabIndex="-1" Width="110px">Tipo Impianto *</asp:Label>
                            &nbsp;
                            <telerik:RadComboBox ID="cmbTipoImpianto" Width="500px" AppendDataBoundItems="true"
                                Enabled="false" Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left">
                            &nbsp;<asp:Label ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Red" Visible="False" Width="234px">Nessun edificio o impianto disponibile</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; ì">
                            <div style="overflow: auto;">
                                <!-- <asp:CheckBoxList ID="lstcomplessi" runat="server" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Width="500px" TabIndex="12">
                            </asp:CheckBoxList>

                            --->
                                <telerik:RadGrid ID="DataGridLstcomplessi" runat="server" GroupPanelPosition="Top"
                                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                    Height="300" AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%"
                                    AllowSorting="True" IsExporting="False" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                                    PageSize="50">
                                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                        Width="100%">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="SELEZIONE" HeaderStyle-Width="13%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelezione" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.SELEZIONATO") %>' />
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE" HeaderStyle-Width="87%"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="SELEZIONATO" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SELEZIONATO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SELEZIONATO") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                        <Excel FileExtension="xls" Format="Xlsx" />
                                    </ExportSettings>
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        <Selecting AllowRowSelect="True" />
                                      <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                            AllowResizeToFit="false" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="Selezionati" runat="server" Value="0" />
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <div style="width: 100%; overflow: auto">
        <table style="width: 100%" cellpadding="0" cellspacing="0">
             <tr>
                    <td class="TitoloModulo">Gestione - Lotti
                    </td>
                </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnINDIETRO" runat="server" Text="Indietro" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';ConfermaEsci();}"
                                    ToolTip="Indietro" />
                            </td>
                            <td>
                                <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}"
                                    ToolTip="Salva" />
                            </td>
                            <td>
                                <telerik:RadButton ID="ImgEventi" runat="server" Style="top: 0px; left: -1px" Text="Eventi"
                                    OnClientClicking="function(sender, args){ApriEventi();}" ToolTip="Eventi Scheda Lotto" />
                            </td>
                            <td>
                                <telerik:RadButton ID="imgUscita" runat="server" Style="top: 0px; left: -1px" Text="Esci"
                                    OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';ConfermaEsci();}"
                                    ToolTip="Esci" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset style="border-width: 2px">
                        <legend>&nbsp;&nbsp;Dettagli Lotto&nbsp;&nbsp;</legend>
                        <table>
                            <tr>
                                <td style="height: 21px; width: 10%;">
                                    <asp:Label ID="lblES" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" TabIndex="-1" Width="110px">Esercizio Finanziario *</asp:Label>
                                </td>
                                <td style="height: 21px;">
                                    <telerik:RadComboBox ID="cmbesercizio" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="TRUE" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                       Width="60%">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="lblFiliale" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="110px">Sede Territoriale*</asp:Label>
                                </td>
                                <td style="height: 21px;">
                                    <telerik:RadComboBox ID="cmbfiliale" runat="server" AppendDataBoundItems="true" AutoPostBack="TRUE"
                                        Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="60%">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="lblServizi" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="110px">Lista Servizi *</asp:Label><br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                                <td style="height: 21px; width: 80%;">
                                    <div style="border-left-color: #ccccff; right: 246px; left: 14px; border-bottom-color: #ccccff;
                                        overflow: auto; width: 650px; border-top-style: solid; border-top-color: #ccccff;
                                        border-right-style: solid; border-left-style: solid; top: 168px; height: 110px;
                                        border-right-color: #ccccff; border-bottom-style: solid">
                                        <asp:CheckBoxList ID="lstservizi" runat="server" AutoPostBack="True" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black" TabIndex="2">
                                        </asp:CheckBoxList>
                                        <asp:Label ID="lblnoservizi" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label>&nbsp;</div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="lblDescrizione" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        TabIndex="-1" Width="110px">Nome Lotto *</asp:Label><br />
                                    <br />
                                </td>
                                <td style="height: 21px; width: 80%;">
                                    <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Height="55px" MaxLength="200" Style="left: 80px; top: 88px" TabIndex="3" TextMode="MultiLine"
                                        Width="650px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset style="border-width: 2px">
                        <legend>&nbsp;&nbsp;Composizione&nbsp;&nbsp;</legend>
                        <table style="height: 210px; width: 100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 10%;">
                                    <asp:Label ID="lblComplessi" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        TabIndex="-1" Width="110px">Lista Complessi *</asp:Label><br />
                                    <br />
                                    <br />
                                </td>
                                <td style="width: 95%; vertical-align: top">
                                    <div style="overflow: auto; width: 100%; height: 100%;">
                                        <telerik:RadGrid ID="tabcomplessi" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                            AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                            IsExporting="False" PagerStyle-AlwaysVisible="true" AllowPaging="true" PageSize="50">
                                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                                CommandItemDisplay="Top" Width="100%">
                                                <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                                    ShowRefreshButton="true" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <CommandItemTemplate>
                                                    <a id="addServizio" style="cursor: pointer" onclick="document.getElementById('txtcomplessi').value!='1';openWindow(null, null, 'RadWindowLotto')">
                                                        <img style="border: 0px" alt="" src="../../Immagini/addRecord.gif" />
                                                        Aggiungi nuovo record</a>
                                                </CommandItemTemplate>
                                            </MasterTableView>
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                            <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                                <Excel FileExtension="xls" Format="Xlsx" />
                                            </ExportSettings>
                                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                <Selecting AllowRowSelect="True" />
                                                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                                    AllowResizeToFit="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                              
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="cmbtipo" runat="server" BackColor="White" Font-Names="arial"
                                        Font-Size="10pt" Height="1px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 10; left: 712px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 276px" TabIndex="4" Visible="False" Width="50px">
                                        <asp:ListItem Value="E">EDIFICI</asp:ListItem>
                                        <asp:ListItem Value="I">IMPIANTI</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Red" Height="16px" Style="z-index: 104; left: 7px; top: 537px" Visible="False"
                                        Width="312px"></asp:Label>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:TextBox ID="USCITA" runat="server" Style="left: 0px; position: absolute; top: 200px;
            visibility: hidden; z-index: -1;" TabIndex="-1">0</asp:TextBox>
        <asp:TextBox ID="txtModificato" runat="server" BackColor="White" BorderStyle="None"
            ForeColor="White" Style="left: 0px; position: absolute; visibility: hidden; top: 200px;
            z-index: -1;">0</asp:TextBox>
        <asp:TextBox ID="txtElimina" runat="server" BackColor="White" BorderStyle="None"
            ForeColor="White" Style="z-index: -1; left: 0px; position: absolute; visibility: hidden;
            top: 200px">0</asp:TextBox>
        <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="z-index: -1; left: 0px; position: absolute;
            visibility: hidden; top: 200px" TabIndex="-1" Width="48px">0</asp:TextBox>
        <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute;
            visibility: hidden; top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>
        <asp:TextBox ID="txtIdComponente" runat="server" Style="left: 0px; position: absolute;
            visibility: hidden; top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>
        <asp:TextBox ID="SOLO_LETTURA" runat="server" Style="left: 0px; position: absolute;
            visibility: hidden; top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>
        <asp:HiddenField ID="TipoStruttura" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtcomplessi" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtedifici" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtTIPO_LOTTO" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="HiddenID" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <asp:HiddenField ID="HFAltezzaSottratta" runat="server" />
    </div>
    <div align='center' id='caricamento' runat="server" style='position: absolute; visibility: hidden;
        background-color: #ffffff; text-align: center; width: 793px; height: 574px; top: 6px;
        left: 2px; z-index: 10; font-size: 10px;'>
        <div align='center' runat="server" style='position: absolute; background-color: #ffffff;
            text-align: center; width: 200px; height: 100px; top: 200px; left: 300px; z-index: 10;
            border: 1px dashed #660000; font: verdana; font-size: 10px;'>
            <br />
            <img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' /><br />
            caricamento in corso...
        </div>
    </div>
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();

        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);


        //myOpacity.hide();






        document.getElementById('caricamento').style.visibility = 'hidden';

    </script>
</body>
</html>
