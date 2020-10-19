<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CodFiscale.aspx.vb" Inherits="cf_CodFiscale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
 <base target="_self"/>
     <title>Codice Fiscale</title>
    
    <script language="Javascript" src="wind.js"></script>
<script language="Javascript" src="wind2.js"></script>

</head>
<body bgcolor="#F2F5F1">
    <form id="FormCodFis"  name="FormCodFis" runat="server">
          <table border="0">
            <tr> 
              <td><b><i><font color="#000080" face="Arial" size="4">SEP@Com</font></i></b></td>
              <td> 
                <p align="center">&nbsp; <font face="Arial" size="2"><b>Calcolo
                del codice fiscale</b></font>
              </td>
            </tr>
            <tr> 
              <td></td>
              <td> 
              </td>
            </tr>
            <tr> 
              <td><font class="voce">Cognome</font></td>
              <td> 
                <input id="Cognome" name="Cognome" runat="server" size="20" value="" onChange="CheckCognome()" class="voce5"/>
              </td>
            </tr>
            <tr> 
              <td><font class="voce">Nome</font></td>
              <td> 
                <input id="Nome" runat="server" name="Nome" size="20" value="" onChange="CheckNome()" class="voce5"/>
              </td>
            </tr>
            <tr> 
              <td><font class="voce">Data di nascita</font></td>
              <td> 
                <select name="Giorno" size=1 value="" onChange="" class="voce5">
                  <option selected value="1">1</option>
                  <option value="2">2</option>
                  <option value="3">3</option>
                  <option value="4">4</option>
                  <option value="5">5</option>
                  <option value="6">6</option>
                  <option value="7">7</option>
                  <option value="8">8</option>
                  <option value="9">9</option>
                  <option value="10">10</option>
                  <option value="11">11</option>
                  <option value="12">12</option>
                  <option value="13">13</option>
                  <option value="14">14</option>
                  <option value="15">15</option>
                  <option value="16">16</option>
                  <option value="17">17</option>
                  <option value="18">18</option>
                  <option value="19">19</option>
                  <option value="20">20</option>
                  <option value="21">21</option>
                  <option value="22">22</option>
                  <option value="23">23</option>
                  <option value="24">24</option>
                  <option value="25">25</option>
                  <option value="26">26</option>
                  <option value="27">27</option>
                  <option value="28">28</option>
                  <option value="29">29</option>
                  <option value="30">30</option>
                  <option value="31">31</option>
                </select>
                <select name="Mese" size=1 value="" onChange="" class="voce5">
                  <option selected value="A">Gennaio</option>
                  <option value="B">Febbraio</option>
                  <option value="C">Marzo</option>
                  <option value="D">Aprile</option>
                  <option value="E">Maggio</option>
                  <option value="H">Giugno</option>
                  <option value="L">Luglio</option>
                  <option value="M">Agosto</option>
                  <option value="P">Settembre</option>
                  <option value="R">Ottobre</option>
                  <option value="S">Novembre</option>
                  <option value="T">Dicembre</option>
                </select>
                <select name="AnnoCento" size="1" value="" onChange="" class="voce5">
                  <option value="18">18</option>
                  <option selected value="19">19</option>
                  <option value="20">20 
                </select>
                <select name="AnnoDieci" size="1" value="" onChange="" class="voce5">
                  <option selected value="0">0</option>
                  <option value="1">1</option>
                  <option value="2">2</option>
                  <option value="3">3</option>
                  <option value="4">4</option>
                  <option value="5">5</option>
                  <option value="6">6</option>
                  <option value="7">7</option>
                  <option value="8">8</option>
                  <option value="9">9</option>
                </select>
                <select name="AnnoZero" size="1" value="" onChange="" class="voce5">
                  <option selected value="0">0</option>
                  <option value="1">1</option>
                  <option value="2">2</option>
                  <option value="3">3</option>
                  <option value="4">4</option>
                  <option value="5">5</option>
                  <option value="6">6</option>
                  <option value="7">7</option>
                  <option value="8">8</option>
                  <option value="9">9</option>
                </select>
              </td>
            </tr>
            <tr> 
              <td><font class="voce">Sesso</font></td>
              <td> 
                <select name="Sesso" size=1 value="" onChange="" class="voce5">
                  <option selected value="0">Maschio</option>
                  <option value="1">Femmina</option>
                </select>
              </td>
            </tr>
            <tr> 
              <td><font class="voce">Scegli il Comune</font></td>
                <td><a href="javascript:wind('a.htm')" onClick = "javascript:wind('a.htm');" onFocus="if(this.blur)this.blur()" ><b>A</b></a> 
                  <a href="javascript:wind('b.htm')" onClick = "javascript:wind('b.htm');" onFocus="if(this.blur)this.blur()" ><b>B</b></a> 
                  <a href="javascript:wind('c.htm')" onClick = "javascript:wind('c.htm');" onFocus="if(this.blur)this.blur()" ><b>C</b></a> 
                  <a href="javascript:wind('d.htm')" onClick = "javascript:wind('d.htm');" onFocus="if(this.blur)this.blur()" ><b>D</b></a> 
                  <a href="javascript:wind('e.htm')" onClick = "javascript:wind('e.htm');" onFocus="if(this.blur)this.blur()" ><b>E</b></a> 
                  <a href="javascript:wind('f.htm')" onClick = "javascript:wind('f.htm');" onFocus="if(this.blur)this.blur()" ><b>F</b></a> 
                  <a href="javascript:wind('g.htm')" onClick = "javascript:wind('g.htm');" onFocus="if(this.blur)this.blur()" ><b>G</b></a> 
                  <a href="javascript:wind('hij.htm')" onClick = "javascript:wind('hij.htm');" onFocus="if(this.blur)this.blur()" ><b>H</b></a> 
                  <a href="javascript:wind('hij.htm')" onClick = "javascript:wind('hij.htm');" onFocus="if(this.blur)this.blur()" ><b>I</b></a> 
                  <a href="javascript:wind('hij.htm')" onClick = "javascript:wind('hij.htm');" onFocus="if(this.blur)this.blur()" ><b>J</b></a> 
                  <a href="javascript:wind('l.htm')" onClick = "javascript:wind('l.htm');" onFocus="if(this.blur)this.blur()" ><b>L</b></a> 
                  <a href="javascript:wind('m.htm')" onClick = "javascript:wind('m.htm');" onFocus="if(this.blur)this.blur()" ><b>M</b></a> 
                  <a href="javascript:wind('n.htm')" onClick = "javascript:wind('n.htm');" onFocus="if(this.blur)this.blur()" ><b>N</b></a> 
                  <a href="javascript:wind('o.htm')" onClick = "javascript:wind('o.htm');" onFocus="if(this.blur)this.blur()" ><b>O</b></a> 
                  <a href="javascript:wind('p.htm')" onClick = "javascript:wind('p.htm');" onFocus="if(this.blur)this.blur()" ><b>P</b></a> 
                  <a href="javascript:wind('q.htm')" onClick = "javascript:wind('q.htm');" onFocus="if(this.blur)this.blur()" ><b>Q</b></a> 
                  <a href="javascript:wind('r.htm')" onClick = "javascript:wind('r.htm');" onFocus="if(this.blur)this.blur()" ><b>R</b></a> 
                  <a href="javascript:wind('s.htm')" onClick = "javascript:wind('s.htm');" onFocus="if(this.blur)this.blur()" ><b>S</b></a> 
                  <a href="javascript:wind('t.htm')" onClick = "javascript:wind('t.htm');" onFocus="if(this.blur)this.blur()" ><b>T</b></a> 
                  <a href="javascript:wind('u.htm')" onClick = "javascript:wind('u.htm');" onFocus="if(this.blur)this.blur()" ><b>U</b></a> 
                  <a href="javascript:wind('v.htm')" onClick = "javascript:wind('v.htm');" onFocus="if(this.blur)this.blur()" ><b>V</b></a> 
                  <a href="javascript:wind('z.htm')" onClick = "javascript:wind('z.htm');" onFocus="if(this.blur)this.blur()" ><b>Z</b></a> 
                  <a href="javascript:wind('estero.htm')" onClick = "javascript:wind('estero.htm');" onFocus="if(this.blur)this.blur()" ><b>ESTERO</b></a> 
                </td>
            </tr>
            <tr> 
              <td><font class="voce">Codice Comune</font></td>
              <td> 
                <input runat="server" id="Comune" type="text" maxlength=4 size=4 name="Comune" value=""  onChange="CheckComune()" READONLY class="voce5">
              </td>
            </tr>
            <tr> 
              <td><font class="voce">Codice Fiscale</font></td>
              <td> 
                <input  runat="server" id = "CodiceFiscale" name="CodiceFiscale" size="22" value="" onChange="" READONLY class="voce5">
              </td>
            </tr>
              <tr>
                  <td>
                  </td>
                  <td>
                  </td>
              </tr>
          </table>
              <table align="left" style="z-index: 100; left: 11px; position: absolute; top: 288px">
                  <tr>
                      <td style="width: 501px">
                      </td>
                  </tr>
                  <tr>
                      <td style="width: 501px; text-align: center;">
                          <%--<asp:Button ID="btnChiudi" runat="server" Text="Chiudi"  />--%>
&nbsp;&nbsp;&nbsp;
                          &nbsp;&nbsp;
<%--          <input type="button" value="Cancella Campi" onclick="ReloadButton()" class="voce5" name="BUTTON" style="z-index: 101; left: 0px; position: static; top: 0px"/>  &nbsp;&nbsp;
--%>                          <asp:Button ID="Calcola" name="Calcola" runat="server" onclientclick="CalcolaCodiceFiscale();" 
                              Text="Calcola e Applica" />
                          </td>
                  </tr>
              </table>
          
    </form>
    <script>        function ReloadButton() { location.href = "codice.htm"; }</script>
        
<script language="JavaScript" src="codicefiscale.js">
<!--    alert("ATTENZIONE: Non posso caricare il programma!. Questo programma richiede un browser che supporti JavaScript."); //-->

    function Applica() {
        // opener.form1.ctl00_ContentPlaceHolder1_TabDomanda_TabRichiedente_txtCF.value = 'ciao';       
    } 

</script>

</body>
</html>
