<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MotivazioniEmergenza.aspx.vb" Inherits="VSA_MotivazioniEmergenza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Motivazione Cambio</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" 
            Font-Size="10pt"></asp:Label>
        <br />
&nbsp;<br />
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" 
            Font-Size="8pt">clicca qui per visualizzare i Dati Edificio</asp:HyperLink>
        <br />
&nbsp;
    
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        
                        
                        Text="AA) Alloggio antigienico (privo di servizi igienici o non regolamentari oppure privo di servizi di rete oppure presenza di dichiarazione ASL)" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        Text="AI) Stato Manutentivo Carente (Inagibile-Inabitabile)" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        Text="Conferma da parte del gestore" />
                &nbsp;(neccessario per attribuire punteggio)</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        Text="CD) Cambio in diminuzione" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox6" runat="server" Font-Names="arial" Font-Size="10pt" 
                        Text="IV) Problemi con Vicinato" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        Text="Gravi" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox7" runat="server" Font-Names="arial" Font-Size="10pt" 
                        Text="AE) Altre esigenze" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox8" runat="server" Font-Names="arial" Font-Size="10pt" 
                        Text="Avvicinamento a luogo di cura (solo per cambi in comuni diversi)" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox9" runat="server" Font-Names="arial" Font-Size="10pt" 
                        Text="PV) Piano vendita" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox10" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="RI) Esigenze Aziendali (ristrutturazione)" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox11" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="RU) Urgente" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox12" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="RI) Manutenzione straordinaria" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox13" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="AN) Anziani" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox23" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="HA) Disabili 100% con acc." Enabled="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox24" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="HT) Disabili 100%" Enabled="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox25" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="HP) Disabili 66%-99%" Enabled="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox14" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="Invalidi" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox15" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="HM) Handicap Motorio" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox16" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="FS) Sovraffollamento" />
                &nbsp;
                    <asp:Label ID="lblSuperficie" runat="server" Text="0"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <b>BARRIERE ARCHITETTONICHE DOVUTE A:</b></td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox17" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="Piano alto senza ascensore" />
                &nbsp;&nbsp;
                    <asp:Label ID="lblpiano" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 10pt; ">
                    <asp:CheckBox ID="CheckBox18" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="Ascensore non idoneo" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox19" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="Inaccessibilità dei locali" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox20" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="Inaccessibilità dello stabile" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox21" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="Bagno non idoneo" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox22" runat="server" Font-Names="arial" 
                        Font-Size="10pt" Text="Altro" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="TXTaLTRO" runat="server" Font-Names="ARIAL" Font-Size="10pt" 
                        Height="63px" MaxLength="300" TextMode="MultiLine" Width="354px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;</td>
                <td>
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td style="text-align: right">
                    <asp:ImageButton ID="ImgSalva" runat="server" 
                        ImageUrl="~/NuoveImm/img_SalvaModelli.png" TabIndex="24" 
                        style="cursor:pointer;"/>
                    &nbsp;&nbsp;
                    <img onclick="ConfermaEsci();" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" style="cursor:pointer;"/></td>
            </tr>
        </table>
    
    </div>
    <script type="text/javascript">
        function ConfermaEsci() {

            if (document.getElementById('Modificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == true) {
                    self.close();
                    //document.getElementById('Modificato').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
            else {
                self.close();
            }
        }
    </script>
    <asp:HiddenField ID="Modificato" runat="server" Value="0" />
    <asp:HiddenField ID="iddomanda" runat="server" />
    <asp:HiddenField ID="iddichiarazione" runat="server" />
    </form>
    </body>
</html>
