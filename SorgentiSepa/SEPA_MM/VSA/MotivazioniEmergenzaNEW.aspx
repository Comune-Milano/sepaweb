<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MotivazioniEmergenzaNEW.aspx.vb"
    Inherits="VSA_MotivazioniEmergenzaNEW" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Motivazione Cambio</title>
    <style type="text/css">
        .Legenda
        {
            font-family: Arial;
            font-size: 9pt;
        }
        .style1
        {
            height: 42px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function visibileTabella(obj) {
            
            if (document.getElementById('divForteSott')) {
                if (obj.value == 13) {
                    document.getElementById('divForteSott').style.visibility = 'visible';
                }
                else if (obj.value == 14) {
                    document.getElementById('divSott').style.visibility = 'visible';
                }
                else {
                    document.getElementById('divForteSott').style.visibility = 'hidden';
                }
            }

            if (document.getElementById('divSott')) {
                if (obj.value == 14) {
                    document.getElementById('divSott').style.visibility = 'visible';
                }
                else if (obj.value  == 13) {
                    document.getElementById('divForteSott').style.visibility = 'visible';
                }
                else {
                    document.getElementById('divSott').style.visibility = 'hidden';
                }

            }


        }
        function vediDiv(option_value) {
            if (option_value == '13') {
                document.getElementById('divForteSott').style.visibility = 'visible';
                document.getElementById('divSott').style.visibility = 'hidden';
            }
            else if (option_value == '14') {

                document.getElementById('divForteSott').style.visibility = 'hidden';
                document.getElementById('divSott').style.visibility = 'visible';
            }
            else {
                document.getElementById('divForteSott').style.visibility = 'hidden';
                document.getElementById('divSott').style.visibility = 'hidden';
            }

        }
        function ControlliAbilita(radiolist, check) {
            var radioObj = document.getElementById(radiolist);
            var radioLength = radioObj.rows.length;
            if (document.getElementById(check).checked == true) {
                for (var i = 0; i < radioLength; i++) {
                    radioObj.rows[i].disabled = false;
                };
            } else {
                for (var i = 0; i < radioLength; i++) {
                    radioObj.rows[i].disabled = true;
                    document.getElementById(radiolist+'_' + i).checked = false;
                };
            };
        };


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; MOTIVAZIONI
                        CAMBIO IN EMERGENZA (ART.22 C.10 RR 1/2004)</strong></span><br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCat" runat="server" Font-Names="arial" Font-Size="10pt" Width="0px"
                        Visible="False" Height="0px"></asp:Label>
                    <table width="100%" cellpadding="2" cellspacing="0" border="1px">
                        <tr style='font-family: arial, Helvetica, sans-serif; font-size: 11pt;'>
                            <td width='120px' align='center'>
                                <b>CATEGORIA</b>
                            </td>
                            <td width='120px' align='center'>
                                  <b>ATTRIBUISCI REQUISITO</b></td>
                            <td align='center' colspan="2">
                                <b>MOTIVAZIONI</b>
                            </td>
                        </tr>
                        <tr>
                            <td width='80px'>
                                1) <b>AI</b> *
                            </td>
                            <td width='80px' align="center" style="text-indent: 40px">
                                <asp:CheckBox ID="chkAbilitaAI" runat="server"  />
                                </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rdbAI" runat="server" Font-Names="Arial" 
                                    Font-Size="9pt">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr bgcolor="#EAFFD5">
                            <td width='80px'>
                                2) <b>EA</b> *
                            </td>
                            <td width='80px' align="center">
                                &nbsp;</td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chkEA" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td width='80px'>
                                3) <b>AA</b> *
                            </td>
                            <td width='80px' align="center" style="text-indent: 40px">
                                <asp:CheckBox ID="chkAbilitaAA" runat="server"/>
                                </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rdbAA" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr bgcolor="#EAFFD5">
                            <td width='80px'>
                                4) <b>IV</b>
                            </td>
                            <td width='80px' align="center">
                                &nbsp;</td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chkIV" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td width='80px'>
                                5) <b>H</b> **
                            </td>
                            <td width='80px' align="center" style="text-indent: 40px">
                                <asp:CheckBox ID="chkAbilitaH" runat="server" />
                                </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rdbH" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr bgcolor="#EAFFD5">
                            <td width='80px'>
                                6) <b>AN</b> ***
                            </td>
                            <td width='80px' align="center" style="text-indent: 40px">
                                <asp:CheckBox ID="chkAbilitaAN" runat="server" />
                                </td>
                            <td colspan="2">
                                 <asp:RadioButtonList ID="rdbAN" runat="server" Font-Names="Arial" Font-Size="9pt">
                              </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td width='80px'>
                                7) <b>CD</b> ****
                            </td>
                            <td width='80px' align="center" style="text-indent: 40px">
                                <asp:CheckBox ID="chkAbilitaCD" runat="server" />
                                </td>
                            <td style="vertical-align: top">
                                <asp:CheckBoxList ID="chkCD" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:CheckBoxList>
                                <asp:RadioButtonList ID="rdbCD" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:RadioButtonList>
                            </td>
                            <td style="border-left-width: 0px">
                                <div style="overflow: auto; font-family: Arial; font-size: 9pt;" id="divForteSott">
                                    <span style="text-align: center; font-weight: bold;">Situazioni di forte sottoutilizzo:</span><br />
                                    1 persona in alloggio con metratura superiore a mq 72,46<br />
                                    2 persone in alloggio con metratura superiore a mq 83,35
                                    <br />
                                    3 persone in alloggio con metratura superiore a mq 95,50
                                    <br />
                                    4 persona in alloggio con metratura superiore a mq 113,60
                                </div>
                                <div style="overflow: auto; font-family: Arial; font-size: 9pt;" id="divSott">
                                    <span style="text-align: center; font-weight: bold;">
                                        <br />
                                        Situazioni di sottoutilizzo:</span><br />
                                    1 persona in alloggio con metratura superiore a mq 55,66<br />
                                    2 persone in alloggio con metratura superiore a mq 66,55<br />
                                    3 persone in alloggio con metratura superiore a mq 78,65<br />
                                    4 persone in alloggio con metratura superiore a mq 96,80
                                </div>
                            </td>
                        </tr>
                        <tr bgcolor="#EAFFD5">
                            <td width='80px'>
                                8) <b>FS</b>
                            </td>
                            <td width='80px' align="center" style="text-indent: 40px">
                                <asp:CheckBox ID="chkAbilitaFS" runat="server" />
                                </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rdbFS" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td width='80px'>
                                9) <b>PV</b>
                            </td>
                            <td width='80px' align="center">
                                &nbsp;</td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chkPV" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td width='80px'>
                                10) <b>BA</b>
                            </td>
                            <td width='80px' align="center">
                                &nbsp;</td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chkBA" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr bgcolor="#EAFFD5">
                            <td width='80px'>
                                11) <b>ACC</b>
                            </td>
                            <td width='80px' align="center">
                                &nbsp;</td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chkACC" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td width='80px'>
                                12) <b>AN+</b>
                            </td>
                            <td width='80px' align="center">
                                &nbsp;</td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chkANN" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr bgcolor="#EAFFD5">
                            <td width='80px'>
                                13) <b>H+</b>
                            </td>
                            <td width='80px' align="center">
                                &nbsp;</td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chkHH" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td width='80px'>
                                14) <b>NE</b>
                            </td>
                            <td width='80px' align="center">
                                &nbsp;</td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chkNE" runat="server" Font-Names="Arial" Font-Size="9pt">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td class="Legenda">
                    * i punteggi non sono cumulabili tra di loro
                </td>
            </tr>
            <tr>
                <td class="Legenda">
                    ** Non saranno ritenuti validi, ai fini dell'accettazione della domanda di cambio
                    per invalidità, certificati ASL attestanti invalidità anche uguale o superiore al
                    66% qualora l'alloggio risulti comunque idoneo, ovvero privo di barriere architettoniche.
                </td>
            </tr>
            <tr>
                <td class="Legenda">
                    *** Tale requisito sarà ritenuto valido, ai fini dell'accettazione della domanda
                    di cambio per anzianità, in caso di:
                    <br />
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp- mononucleo con esigenza di avvicinamento a familiari
                    per ragioni di assistenza<br />
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp- nuclei con l'esigenza di avvicinamento al luogo
                    di cura
                    <br />
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp- inidoneità dell'alloggio per presenza di barriere
                    architettoniche o problemi di accessibilità (es.: UI ubicata ad un piano alto senza
                    ascensore)<br />
                </td>
            </tr>
            <tr>
                <td class="Legenda">
                    **** i punteggi sono cumulabili
                </td>
            </tr>
            <tr>
                <td class="Legenda">
                    <u>Ove sussista il solo requisito di anzianità le domande non saranno accolte.
                        <br />
                        <br />
                        Tutti i punteggi sono cumulabili nell'ambito di più categorie fatta eccezione delle
                        categ
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:ImageButton runat="server" ID="ImgSalva" ImageUrl="../NuoveImm/img_SalvaModelli.png" />
                    <img onclick="ConfermaEsci();" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" style="cursor: pointer;" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="Modificato" runat="server" Value="0" />
    <asp:HiddenField ID="iddomanda" runat="server" />
    <asp:HiddenField ID="iddichiarazione" runat="server" />
    </form>
</body>
<script type="text/javascript">
    function ConfermaEsci() {

        if (document.getElementById('Modificato').value == '1') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
            if (chiediConferma == true) {
                self.close();
            }
        }
        else {
            self.close();
        }
    }
   vediDiv(document.getElementById('rdbCD').value);

   ControlliAbilita('rdbAI', 'chkAbilitaAI')
   ControlliAbilita('rdbAA', 'chkAbilitaAA')
   ControlliAbilita('rdbH', 'chkAbilitaH')
   ControlliAbilita('rdbAN', 'chkAbilitaAN')
   ControlliAbilita('rdbCD', 'chkAbilitaCD')
   ControlliAbilita('rdbFS', 'chkAbilitaFS')

</script>
</html>
