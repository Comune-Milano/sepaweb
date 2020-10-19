<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PG0913.aspx.vb" Inherits="Contratti_PG0913" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="jquery-1.8.2.js"></script>
<script type="text/javascript" src="jquery-impromptu.4.0.min.js"></script>
<script type="text/javascript" src="jquery.corner.js"></script>
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="impromptu.css" />
    <title>Risultato Partite Gestionali</title>
    <style type="text/css">
        .allineamento
        {
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr style="font-size: 12px; font-family: arial, Helvetica, sans-serif; color: #990000;">
                <td height="20px" style="color: #801f1c; font-family: ARIAL, Helvetica, sans-serif;
                    font-size: 14pt; font-weight: bold;">
                    (v. 1.1)
                    RISULTATI PARTITE GESTIONALI
                    <asp:Label ID="lblNumRisult" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td width="3%">
                                &nbsp
                            </td>
                            <td width="2%">
                                &nbsp;</td>
                            <td width="10%">
                                &nbsp;</td>
                            <td width="2%">
                                &nbsp;</td>
                            <td width="90%">
                                &nbsp;</td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="../NuoveImm/Img_Procedi.png"
                                    Style="" TabIndex="1" OnClientClick="controllaSelezioneCheck();ControllaProcedi();" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                        Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" CellPadding="1" ForeColor="#333333"
                        Width="100%" AllowPaging="True" PageSize="500">
                        <%--<AlternatingItemStyle BackColor="White" />--%>
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" 
                            Position="TopAndBottom" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" 
                                ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelezionato_click"  />
                                </HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChSelezionato" runat="server" OnClick="" />
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO" 
                                Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO_CONT" HeaderText="TIPO CONTRATTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO_SPEC" HeaderText="TIPO SPECIFICO" 
                                Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO_UI" HeaderText="TIPOLOGIA UNITA'" 
                                Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO_DOC" HeaderText="TIPO DOC. GEST.">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMP_EMESSO" HeaderText="IMP. EMESSO €">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_EMISS" HeaderText="DATA EMISSIONE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_RIFERIM1" HeaderText="DATA RIFERIM. DAL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_RIFERIM2" HeaderText="DATA RIFERIM. AL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CREDITO" Visible="False"></asp:BoundColumn>
                            <%--<asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Text="SCELTA APPLIC." CssClass="allineamento"></asp:Label>
                                    <asp:Image ID="ImgHelp" runat="server" ImageUrl="Immagini/Img_Help.png" Width="18px"
                                        CssClass="allineamento" onmouseout="javascript:NascondiDettagliCanone();" onmouseover="javascript:VisualizzaDettagliCanone();" />
                                </HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RADIO") %>'
                                        ID="rdb1"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RADIO") %>'
                                        ID="rdb2"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>--%>
                            <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Wrap="False" Visible="False">
                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Text="ELABORAZ.PARZIALE" CssClass="allineamento"></asp:Label>
                                    <asp:Image ID="ImgHelp" runat="server" ImageUrl="Immagini/Img_Help.png" Width="18px"
                                        CssClass="allineamento" onmouseout="javascript:NascondiDettagliCanone();" onmouseover="javascript:VisualizzaDettagliCanone();" />
                                    <br />
                                    <asp:CheckBox ID="CheckAll1" OnCheckedChanged="chkParziale_click" runat="server"
                                        AutoPostBack="True" />
                                </HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChParziale" runat="server" OnClick="ScegliPercentuale();" />
                                    <asp:Label ID="Label11" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID_BOLL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="importoTOT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DETTAGLI" HeaderText="" Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO_APPL" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                        <ItemStyle BackColor="#DCE3F5" />
                        <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <div id="DettagliElaborazione" style="border: 1px double #808080; background-position: center top;
            position: absolute; width: 424px; visibility: hidden; height: 132px; background-repeat: no-repeat;
            top: 132px; left: 73%; background-color: white;">
            <asp:Label ID="lblInfo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="7pt"
                Style="position: absolute; top: 6px; left: 4px; height: 14px;" Width="100%"></asp:Label>
        </div>
        <div style="display: none; z-index: 50px;">
            <asp:DataGrid ID="DataGrid1FINE" runat="server" Width="100%" BackColor="White" BorderColor="#E7E7FF"
                BorderStyle="None" BorderWidth="1px" PageSize="20" AutoGenerateColumns="False"
                CellPadding="3">
                <AlternatingItemStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundColumn DataField="ID_BOLLETTA" ReadOnly="True" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONTRATTO" ReadOnly="True" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" Width="0px"
                            BackColor="White" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD.CONTRATTO">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO_BOLLETTA" HeaderText="TIPO DOC.ELABORATO">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO ELABORATO">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FL_PARZIALE" HeaderText="ELABORAZ.PARZIALE">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_ELABORAZIONE" HeaderText="DATA ELABORAZ.">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle Font-Bold="True" Font-Names="Courier New" Font-Size="8pt" Wrap="False"
                    BackColor="#006699" ForeColor="#F7F7F7" />
                <ItemStyle ForeColor="black" BackColor="#E7E7FF" />
                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            </asp:DataGrid>
        </div>
        <asp:HiddenField ID="errore" runat="server" Value="0" />
        <asp:HiddenField ID="numRigheDT" runat="server" Value="0" />
        <asp:HiddenField ID="idBolletta" runat="server" Value="0" />
        <asp:HiddenField ID="importoBolletta" runat="server" Value="0" />
        <asp:HiddenField ID="idContratto" runat="server" Value="0" />
        <asp:HiddenField ID="conferma" runat="server" Value="0" />
        <asp:HiddenField ID="percentuale" runat="server" Value="0" />
        <asp:HiddenField ID="conferma0" runat="server" Value="1" />
        <asp:HiddenField ID="elabora" runat="server" Value="0" />
        <asp:HiddenField ID="controlloSelezione" runat="server" Value="0" />
        <asp:HiddenField ID="controlloParziale" runat="server" Value="0" />
        <asp:HiddenField ID="tipoDocGest" runat="server" Value="0" />
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        if (document.getElementById('dvvvPre')) {
            document.getElementById('dvvvPre').style.visibility = 'hidden';
        }
        var Selezionato;
        var OldColor;
        var SelColo;
        //        function ValorizzaACascataRB(radioElaboraz, radioValue) {
        //            var chiediConferma;
        //            var i = 0;
        //            chiediConferma = window.confirm("Applicare la scelta per tutti i documenti?");
        //            if (chiediConferma == true) {
        //                radioElaboraz.checked = true;
        //                var modulo = document.getElementById('form1').elements;
        //                for (i = 0; i < modulo.length; i++) {
        //                    if (modulo[i].type == "radio" && modulo[i].value == radioValue) {
        //                        modulo[i].checked = true;
        //                    }
        //                }
        //                if (radioValue == 'P') {
        //                    ScegliElaborazione();
        //                }
        //            }
        //            else {
        //                if (radioValue == 'P') {
        //                    ScegliElaborazione();
        //                }
        //            }
        //        }

        function ValorizzaACascataRB(radioElaboraz, radioValue) {
            var chiediConferma;
            var i = 0;
            chiediConferma = window.confirm("Applicare la scelta per tutti i documenti?");
            if (chiediConferma == true) {
                radioElaboraz.checked = true;
                var modulo = document.getElementById('form1').elements;
                for (i = 0; i < modulo.length; i++) {
                    if (modulo[i].type == "radio" && modulo[i].value == radioValue) {
                        modulo[i].checked = true;
                    }
                }
            }
        }

        function MantieniCheck(modulo) {

            for (i = 0; i < modulo.length; i++) {
                if (modulo[i].type == "radio" && modulo[i].checked == true) {
                    modulo[i].checked = true;
                }
            }

        }

        function VisualizzaDettagliCanone() {
            document.getElementById('DettagliElaborazione').style.visibility = 'visible';
        }

        function NascondiDettagliCanone() {
            document.getElementById('DettagliElaborazione').style.visibility = 'hidden';
        }

        if (document.getElementById('divPre')) {
            document.getElementById('divPre').style.visibility = 'hidden';
        }

        function Seleziona_deseleziona(checkTitolo) {
            var i = 0;
            checkTitolo.checked = true;
            var modulo = document.getElementById('form1').elements;
            for (i = 0; i < modulo.length; i++) {
                if (modulo[i].type == "checkbox") {
                    if (modulo[i].checked == true) {
                        modulo[i].checked = false;
                    } else {
                        modulo[i].checked = true;
                    }
                }
            }
            return checkTitolo;
        }

        function select_deselectAll(chkVal, idVal) {
            var frm = document.forms[0];
            for (i = 0; i < frm.length; i++) {
                if (idVal.indexOf('CheckAll') != -1) {
                    if (chkVal == true) {
                        if (frm[i].type == "checkbox") {
                            //if (frm.elements[i].disabled == false) {
                            frm.elements[i].checked = true;
                            //}
                        }
                    }
                    else {
                        if (frm[i].type == "checkbox") {
                            //if (frm.elements[i].disabled==false)
                            //{
                            frm.elements[i].checked = false;
                            //}
                        }
                    }
                }
                else if (idVal.indexOf('DeleteThis') != -1) {
                    if (frm.elements[i].checked == false) {
                        //if (frm.elements[i].disabled == false) {
                        frm.elements[i].checked = false;
                        //}
                    }
                }
            }
        }

        function controllaSelezioneCheck() {

            document.getElementById('errore').value = 0;

        }

        function ControllaProcedi() {
            if (document.getElementById('errore').value == '0') {

                var sicuro = window.confirm('Attenzione...procedendo, i gruppi di partite gestionali coinvolti nell\'elaborazione saranno aggiornati\ncon l\'eliminazione totale/parziale dei documenti scelti.\n\nSei sicuro di voler continuare?');
                if (sicuro == true) {
                    document.getElementById('conferma').value = '1';
                }
                else {
                    return false;
                }
            }
        }

        function controllaSelezioneCheckTest() {
            //var frmCheck = document.getElementById('form1').DataGrid1.rows;
            var CheckTot = document.getElementById('numRigheDT').value;
            var CheckObj = document.getElementById('form1').elements;
            var frmRadio = document.getElementById('form1');

            var frm = document.forms[0];

            var errore = '1';
            var errore2 = '0';

            var contaRadio = 0;
            var contaCheck = 0;

            //            var modulo = document.getElementById('form1').elements;
            //            for (i = 0; i < modulo.length; i++) {
            //                if (modulo[i].type == "radio") {
            //                    contaRadio = contaRadio + 1;
            //                }
            //            }
            for (var i = 0; i < contaCheck; i++) {
                if (CheckObj[i].checked) {
                    errore = '0';
                    if (document.forms[0].rdb + (i + 1).checked == false) {
                        errore2 = '1';
                    }
                }
            }
        }

        //        function radioChecked(radioObj) {
        //            if (!radioObj)
        //                return false;
        //            var radioLength = radioObj.length;
        //            if (radioLength == undefined)
        //                if (radioObj.checked)
        //                    return true;
        //                else
        //                    return false;
        //            for (var i = 0; i < radioLength; i++) {
        //                if (radioObj[i].checked) {
        //                    return true;
        //                }
        //            }
        //            return false;
        //        }

        function ScegliPercentuale() {
            if (document.getElementById('percentuale').value == '0') {
                var txt2 = 'INSERIRE LA PERCENTUALE DI SOSTENIBILITA\' NEL CASO DI RIPARTIZIONE DI UN DEBITO IN PIU\' MENSILITA\'<br /><br />Percentuale di sostenibilità:<br /><input type="text" id="Percentuale" name="Percentuale" value="<%=percenSost %>" style="font-family: arial; font-size: 10pt; width: 50px;"/>%';
                jQuery.prompt(txt2, {
                    submit: mycallScegliElaborazione,
                    buttons: { Ok: '1', Annulla: '0' },
                    show: 'slideDown',
                    focus: 0
                });
            }
        }


        function mycallScegliElaborazione(e, v, m, f) {

            var controllaPerc = /^\d+?$/;

            if (v != '0') {
                txtPerc = m.children('#Percentuale');
                if (!controllaPerc.test(f.Percentuale)) {

                    txtPerc.css("border", "solid #ff0000 1px");
                    errore = '1';
                    return false;
                }
                else {
                    if (!(f.Percentuale >= 1 && f.Percentuale <= 100)) {
                        txtPerc.css("border", "solid #ff0000 1px");
                        errore = '1';
                        return false;
                    }
                    else {
                        txtPerc.css("border", "solid #7f9db9 1px");
                    }
                }
                var sicuro = window.confirm('Attenzione...La percentuale inserita sarà utilizzata in tutti i casi di scelta di elaborazione PARZIALE degli importi a debito. Confermare?');
                if (sicuro == true) {
                    document.getElementById('percentuale').value = f.Percentuale;
                    return true;
                }
                else {
                    return false;
                }

            }
        }




        function CheckAll() {
            all = document.getElementsByTagName("input");
            for (i = 0; i < all.length; i++) {
                if (all[i].type == "checkbox" && all[i].id.indexOf("DataGrid1_ct") > -1) {
                    all[i].checked = true;
                }
            }
        }
        
          
    </script>
</body>
</html>

