<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Trasparenza.aspx.vb" Inherits="Trasparenza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagina senza titolo</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <strong><span style="color: blue; font-family: Arial">Modulo di Trasparenza<br />
            </span></strong>
            <br />
            <table align="center" style="z-index: 100; left: 0px; width: 80%; position: static;
                top: 0px; border-right: black 1px solid; border-top: black 1px solid; border-bottom-width: 1px; border-bottom-color: black; border-left: black 1px solid;">
                <tr>
                    <td style="height: 13px; text-align: center">
                        <span style="font-size: 7pt; color: black"><span style="font-family: Arial">L'introduzione
                            ed elaborazione dei dati con questo modulo <b>non</b> costituisce una modalità di
                            presentazione della domanda. Il risultato ottenuto dipende dalla precisione dei
                            dati introdotti. Per questo motivo il risultato definitivo potrebbe scostarsi da
                            questa elaborazione. <b>Lo scopo del presente modulo è di determinare
                            l'idoneità ISE e ISEE alla presentazione della domanda e valutare l'affitto oneroso.</b></span></span></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="text-align: center">
                        <span style="font-size: 10pt; font-family: Arial">ANNO DI RIFERIMENTO:<asp:DropDownList ID="txtAnno" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="53px">
                        </asp:DropDownList></span></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td bgcolor="skyblue" style="color: black; text-align: center">
                        <span><strong>Dati relativi al nucleo familiare</strong></span></td>
                </tr>
            </table>
        
    
    
        <table align="center" style=" border-right: black 1px solid; border-left: black 1px solid;z-index: 100; left: 0px; width: 80%; position: static;
            top: 0px; font-size: 12pt;">
            <tr>
                <td style="width: 232px; height: 24px;">
                    <span style="font-size: 10pt">Numero di componenti</span></td>
                <td style="text-align: center; height: 24px;">
                    <asp:DropDownList ID="cmbComp" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="53px">
                        <asp:ListItem Selected="True">1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="width: 195px; height: 24px;">
                    <span style="font-size: 10pt">Numero di maggiorenni</span></td>
                <td style="text-align: center; height: 24px;">
                    <asp:DropDownList ID="cmbMaggiorenni" runat="server" Style="z-index: 100; left: 0px;
                        position: static; top: 0px" Width="53px">
                        <asp:ListItem Selected="True">1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 232px">
                    <span style="font-size: 10pt">Numero di minorenni al di sotto dei 15 anni di età</span></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="cmbMinorenni" runat="server" Style="z-index: 100; left: 0px;
                        position: static; top: 0px" Width="53px">
                        <asp:ListItem Selected="True">0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="width: 195px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 232px">
                    <span style="font-size: 10pt">N. invalidi al 100% con indennità</span></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="cmbInvAcc" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="53px">
                        <asp:ListItem Selected="True">0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="width: 195px">
                    <span style="font-size: 10pt">Spese sostenute per invalidi al 100% con indennità*</span></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtSpese" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="62px">0</asp:TextBox>
                    <span style="font-size: 10pt">€</span></td>
            </tr>
            <tr>
                <td style="width: 232px; height: 21px;">
                    <span style="font-size: 10pt">N. invalidi al 100% senza indennità</span></td>
                <td style="font-size: 12pt; height: 21px; text-align: center">
                    <asp:DropDownList ID="cmbInvNoAcc" runat="server" Style="z-index: 100; left: 0px;
                        position: static; top: 0px" Width="53px">
                        <asp:ListItem Selected="True">0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="font-size: 12pt; width: 195px; height: 21px">
                    <span style="font-size: 10pt">N. invalidi tra il 66% e il 99%</span></td>
                <td style="font-size: 12pt; height: 21px; text-align: center">
                    <asp:DropDownList ID="cmbInv66_100" runat="server" Style="z-index: 100; left: 0px;
                        position: static; top: 0px" Width="53px">
                        <asp:ListItem Selected="True">0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 232px">
                </td>
                <td>
                </td>
                <td style="width: 195px">
                </td>
                <td>
                </td>
            </tr>
        </table><table align="center" style="z-index: 100; left: 0px; width: 80%; position: static;
                top: 0px; border-right: black 1px solid; font-size: 12pt; border-left: black 1px solid;">
            <tr>
                <td bgcolor="skyblue" style="color: black; text-align: center">
                    <span><strong>Dati situazione economica</strong></span></td>
            </tr>
        </table>
        <table align="center" style=" border-right: black 1px solid; border-left: black 1px solid;z-index: 100; left: 0px; width: 80%; position: static;
            top: 0px">
            <tr>
                <td style="width: 232px">
                    <span style="font-size: 10pt">Reddito lordo complessivo del nucleo familiare</span></td>
                <td style="width: 124px; text-align: center">
                    <asp:TextBox ID="txtReddito" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="62px">0</asp:TextBox>
                    <span style="font-size: 10pt">€</span></td>
                <td style="width: 195px">
                    <span style="font-size: 10pt">Detrazioni al reddito <font class="note"><span style="font-size: 7pt">
                        (spese mediche, IRPEF, rette per case di riposo)</span></font></span></td>
                <td style="width: 121px; text-align: center">
                    <asp:TextBox ID="txtDetrazioni" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="62px">0</asp:TextBox>
                    <span style="font-size: 10pt">€</span></td>
            </tr>
            <tr>
                <td style="width: 232px">
                    <span style="font-size: 10pt">Canone affitto complessivo annuo</span></td>
                <td style="width: 124px; text-align: center">
                    <asp:TextBox ID="txtCanone" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="62px">0</asp:TextBox>
                    <span style="font-size: 10pt">€</span></td>
                <td style="width: 195px">
                    <span style="font-size: 10pt">Spese accessorie per l'abitazione <font class="note"><span
                        style="font-size: 7pt">(condominiali e riscaldamento)</span></font></span></td>
                <td style="width: 121px; text-align: center">
                    <asp:TextBox ID="txtAccessorie" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="62px">0</asp:TextBox>
                    <span style="font-size: 10pt">€</span></td>
            </tr>
            <tr>
                <td style="width: 232px">
                    <span style="font-size: 10pt">Patrimonio immobiliare <font class="note"><span style="font-size: 7pt">
                        (valore ai fini ICI)</span></font></span></td>
                <td style="width: 124px; text-align: center">
                    <asp:TextBox ID="txtImmobiliare" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="62px">0</asp:TextBox>
                    <span style="font-size: 10pt">€</span></td>
                <td style="width: 195px">
                    <span style="font-size: 10pt">Detrazione per mutui contratti </span>
                </td>
                <td style="width: 121px; text-align: center">
                    <asp:TextBox ID="txtMutuo" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="62px">0</asp:TextBox>
                    <span style="font-size: 10pt">€</span></td>
            </tr>
            <tr>
                <td style="width: 232px; height: 21px;">
                    <span style="font-size: 10pt">Patrimonio mobiliare <font class="note"><span style="font-size: 7pt">
                        (4.000&nbsp; € = 0 ; 7.000 € = 5.165,00)</span></font></span></td>
                <td style="font-size: 12pt; width: 124px; height: 21px; text-align: center">
                    <asp:DropDownList ID="cmbMobiliare" runat="server" Style="z-index: 100; left: 0px;
                        position: static; top: 0px" Width="100px">
                    </asp:DropDownList></td>
                <td style="font-size: 12pt; width: 195px; height: 21px">
                    <span style="font-size: 10pt"></span>
                </td>
                <td style="font-size: 12pt; width: 121px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 232px">
                </td>
                <td style="width: 124px">
                </td>
                <td style="width: 195px">
                </td>
                <td style="width: 121px">
                </td>
            </tr>
        </table>
        <table align="center" style="z-index: 100; left: 0px; width: 80%; position: static;
            top: 0px; border-right: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
            <tr>
                <td style="font-size: 12pt; height: 21px; text-align: center;">
                    <asp:Button ID="btnElabora" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Text="Elabora" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAnnulla" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Text="Annulla Input" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnChiudi" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Text="Chiudi" /></td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 7pt; font-family: Arial">* inserire per gli invalidi al 100%
                        con indennità di accompagnamento o cieco civile assoluto, o invalido di guerra o
                        per servizio con indennità di assistenza e accompagnamento, nonché "grande" invalido
                        del lavoro che usufruisce dell'assegno di assistenza personale e continuativa l'importo
                        effettivamente sostenuto per spese di assistenza documentate (se la spesa sostenuta
                        per il signolo invalido è inferiore a 10.000, va comunque inserito il valore
                        di 10.000 euro).</span></td>
            </tr>
        </table>
        </div>
        <asp:Label ID="Label1" runat="server" Style="z-index: 100; left: 83px; position: absolute;
            top: 544px" Width="725px"></asp:Label>
    </form>
</body>
</html>
