
Partial Class Contabilita_CicloPassivo_Plan_ConvalidaAler
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set

    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        If Not IsPostBack Then
            idPianoF.Value = Request.QueryString("IDP")
            idfiliale.Value = Request.QueryString("IDF")
            idvoce.Value = Request.QueryString("IDV")
            idvoce1.Value = Request.QueryString("IDV1")

            per.Value = ""
            CaricaStato()
            CaricaTabella()

        End If
        txtImporto.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

    End Sub

    Function CaricaTabella()
        Try
            Dim TestoPagina As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            TestoPagina = TestoPagina & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>COD.</td><td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>RICHIESTO</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;&nbsp;COMP.&nbsp;&nbsp;</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>APPROVAZIONE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>APPROVATO GESTORE</td></tr>"

            If idvoce.Value = "" Then
                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
            End If
            Dim COMPLETO As String = ""
            Dim immagine As String = ""
            Dim importovoce As String = ""
            Dim Approvazione_aler As String = ""
            Dim Totale As Double = 0
            Dim TotaleGenerale As Double = 0
            Dim ImportoVoceRichiesto As String = "0,00"

            If idvoce.Value = "" Then
                par.cmd.CommandText = "select pf_voci_struttura.valore_lordo_aler,pf_voci.id,pf_voci_struttura.valore_lordo,pf_voci_struttura.completo,pf_voci.codice,pf_voci.descrizione,pf_voci.indice from siscom_mi.pf_voci_struttura,siscom_mi.pf_voci where pf_voci.id=pf_voci_struttura.id_voce and  pf_voci.id_piano_finanziario=" & idPianoF.Value & " and pf_voci_struttura.id_struttura=" & idfiliale.Value & " order by codice asc,indice asc"
            Else
                par.cmd.CommandText = "select pf_voci_struttura.valore_lordo_aler,pf_voci.id,pf_voci_struttura.valore_lordo,pf_voci_struttura.completo,pf_voci.codice,pf_voci.descrizione,pf_voci.indice from siscom_mi.pf_voci_struttura,siscom_mi.pf_voci where pf_voci.id=pf_voci_struttura.id_voce and  pf_voci.id_piano_finanziario=" & idPianoF.Value & " and pf_voci_struttura.id_struttura=" & idfiliale.Value & " and pf_voci_struttura.id_voce=" & idvoce.Value & " order by codice asc,indice asc"
            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            tuttocompleto.Value = "0"
            While myReader1.Read
                If idvoce.Value = "" Then
                    If myReader1("codice") = "2.01" Then
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                        Totale = 0
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>2</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    End If
                    If myReader1("codice") = "3.01" Then

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                        Totale = 0
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>3</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    End If
                    If myReader1("codice") = "4.01" Then

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                        Totale = 0
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border: 2px dashed #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>4</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    End If
                End If

                If par.IfNull(myReader1("COMPLETO"), "0") = "0" Then
                    If myReader1("codice") <> "1.02" And myReader1("codice") <> "1.03" And myReader1("codice") <> "2.02" And myReader1("codice") <> "2.03" And myReader1("codice") <> "2.04" Then
                        COMPLETO = "NO"
                        tuttocompleto.Value = "1"
                    End If

                Else
                    COMPLETO = "SI"

                End If

                Dim somma As Double = 0

                par.cmd.CommandText = "select * from siscom_mi.pf_voci where id_voce_madre=" & myReader1("id")
                Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader123.HasRows = False Then
                    ImportoVoceRichiesto = par.IfNull(myReader1("valore_lordo"), "0")
                    importovoce = "<a href='DettagliSpesaAler.aspx?IDS=" & idfiliale.Value & "&IDV=" & par.IfNull(myReader1("id"), "") & "&P=" & per.Value & "' target='_blank'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo"), "0")), "##,##0.00") & "</a>"
                    'Totale = Totale + CDbl(par.IfNull(myReader1("valore_lordo"), "0"))
                    'TotaleGenerale = TotaleGenerale + CDbl(par.IfNull(myReader1("valore_lordo"), "0"))
                Else
                    importovoce = "&nbsp;"
                    If Len(par.IfNull(myReader1("codice"), "")) = 4 Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE LENGTH(CODICE)=7 AND SUBSTR(CODICE,1,4)='" & par.IfNull(myReader1("codice"), "") & "' and id_voce_madre=" & myReader1("id")
                        Dim myReader12345 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader12345.Read
                            par.cmd.CommandText = "select SUM(VALORE_LORDO) from siscom_mi.pf_voci_STRUTTURA where ID_STRUTTURA=" & idfiliale.Value & " AND ID_VOCE=" & myReader12345("id")
                            Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1234.Read Then
                                SOMMA = SOMMA + par.IfNull(myReader1234(0), 0)
                            End If
                            myReader1234.Close()
                            ImportoVoceRichiesto = somma
                            If somma <> 0 Then
                                importovoce = "<a href='DettagliSpesaAler.aspx?IDS=" & idfiliale.Value & "&IDV=" & par.IfNull(myReader1("id"), "") & "&P=" & per.Value & "' target='_blank'>" & Format(somma, "##,##0.00") & "</a>"
                            Else
                                importovoce = Format(somma, "##,##0.00")
                            End If
                            'Totale = Totale + somma
                            'TotaleGenerale = TotaleGenerale + somma
                        Loop
                        myReader12345.Close()
                        importovoce = "<span class='style2'>" & Format(SOMMA, "##,##0.00") & "</span>"
                    End If

                    If Len(par.IfNull(myReader1("codice"), "")) = 7 Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE LENGTH(CODICE)=10 AND SUBSTR(CODICE,1,7)='" & par.IfNull(myReader1("codice"), "") & "' and id_voce_madre=" & myReader1("id")
                        Dim myReader12345 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader12345.Read
                            par.cmd.CommandText = "select SUM(VALORE_LORDO) from siscom_mi.pf_voci_STRUTTURA where  ID_STRUTTURA=" & idfiliale.Value & " AND ID_VOCE=" & myReader12345("id")
                            Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1234.Read Then
                                SOMMA = SOMMA + par.IfNull(myReader1234(0), 0)
                            End If
                            myReader1234.Close()
                        Loop
                        myReader12345.Close()
                        ImportoVoceRichiesto = somma
                        If somma <> 0 Then
                            importovoce = "<a href='DettagliSpesaAler.aspx?IDS=" & idfiliale.Value & "&IDV=" & par.IfNull(myReader1("id"), "") & "&P=" & per.Value & "' target='_blank'>" & Format(somma, "##,##0.00") & "</a>"
                        Else
                            importovoce = Format(somma, "##,##0.00")
                        End If
                        'Totale = Totale + somma
                        'TotaleGenerale = TotaleGenerale + somma
                    End If
                    End If
                myReader123.Close()

                If Len(myReader1("codice")) = 4 Then
                    Totale = Totale + somma
                    TotaleGenerale = TotaleGenerale + somma
                End If

                    If idvoce.Value <> "" Then
                        If COMPLETO = "SI" And importovoce <> "&nbsp;" Then
                            If par.IfNull(myReader1("valore_lordo_aler"), "0") <> par.IfNull(myReader1("valore_lordo"), "0") Then
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'><img alt='Approva Importo' src='Immagini/Approva.png' style='cursor: pointer' onclick='ConfermaImporto(" & par.IfNull(myReader1("id"), "0") & "," & ImportoVoceRichiesto.Replace(",", ".") & ");'/>&nbsp;&nbsp;&nbsp;<img alt='Imposta altro Importo' src='Immagini/Non_Approva.png' style='cursor: pointer' onclick='NonConfermaImporto(" & par.IfNull(myReader1("id"), "0") & "," & ImportoVoceRichiesto.Replace(",", ".") & ");'/></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                            Else
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img alt='Imposta altro Importo' src='Immagini/Non_Approva.png' style='cursor: pointer' onclick='NonConfermaImporto(" & par.IfNull(myReader1("id"), "0") & "," & ImportoVoceRichiesto.Replace(",", ".") & ");'/></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                            End If
                        Else
                            If importovoce <> "&nbsp;" Then
                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                            Else
                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>&nbsp;</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td></tr>"
                            End If
                        End If

                    Else

                        If par.IfNull(myReader1("id"), "0") <> idvoce1.Value Then

                            If COMPLETO = "SI" And importovoce <> "&nbsp;" Then
                                If par.IfNull(myReader1("valore_lordo_aler"), "0") <> par.IfNull(myReader1("valore_lordo"), "0") Then
                                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                                Else
                                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                                End If
                            Else
                                If importovoce <> "&nbsp;" Then
                                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                                Else
                                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>&nbsp;</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td></tr>"
                                End If
                            End If
                        Else
                            If COMPLETO = "SI" And importovoce <> "&nbsp;" Then
                                If par.IfNull(myReader1("valore_lordo_aler"), "0") <> par.IfNull(myReader1("valore_lordo"), "0") Then
                                TestoPagina = TestoPagina & "<tr style='background-color: #FF5050;border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'><img alt='Approva Importo' src='Immagini/Approva.png' style='cursor: pointer' onclick='ConfermaImporto(" & par.IfNull(myReader1("id"), "0") & "," & ImportoVoceRichiesto.Replace(",", ".") & ");'/>&nbsp;&nbsp;&nbsp;<img alt='Imposta altro Importo' src='Immagini/Non_Approva.png' style='cursor: pointer' onclick='NonConfermaImporto(" & par.IfNull(myReader1("id"), "0") & "," & ImportoVoceRichiesto.Replace(",", ".") & ");'/></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                                Else
                                TestoPagina = TestoPagina & "<tr style='background-color: #FF5050;border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img alt='Imposta altro Importo' src='Immagini/Non_Approva.png' style='cursor: pointer' onclick='NonConfermaImporto(" & par.IfNull(myReader1("id"), "0") & "," & ImportoVoceRichiesto.Replace(",", ".") & ");'/></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                                End If
                            Else
                                If importovoce <> "&nbsp;" Then
                                    TestoPagina = TestoPagina & "<tr style='background-color: #FF5050;border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(CDbl(par.IfNull(myReader1("valore_lordo_aler"), "0")), "##,##0.00") & "</td></tr>"
                                Else
                                    TestoPagina = TestoPagina & "<tr style='background-color: #FF5050;border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>&nbsp;</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td></tr>"
                                End If
                            End If
                        End If

                    End If



            End While
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
            TestoPagina = TestoPagina & "<tr></tr><tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE GENERALE</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleGenerale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
            myReader1.Close()
            TestoPagina = TestoPagina & "</table>"

            Tabella = TestoPagina



            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try

    End Function

    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                per.Value = Label1.Text
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
                stato.Value = par.IfNull(myReader5("id_stato"), "")
            End If
            myReader5.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function




    'Protected Sub imgNonConfermare_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNonConfermare.Click
    '    If salvaok.Value = "1" Then
    '        Try
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)

    '            par.cmd.CommandText = "update siscom_mi.pf_voci set completo='0',completo_aler='1',valore_lordo_aler=" & par.VirgoleInPunti(txtImporto.Text) & " where id=" & idvoce.Value
    '            par.cmd.ExecuteNonQuery()

    '            par.cmd.CommandText = "select * from siscom_mi.pf_voci where id=" & idvoce.Value
    '            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReader5.Read Then
    '                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
    '                                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
    '                                    & "'F86','Voce " & par.PulisciStrSql(par.IfNull(myReader5("cod"), "")) & "-" & par.IfNull(myReader5("descrizione"), "") & " - Euro " & Format(par.IfNull(myReader5("valore_lordo"), "0"), "##,##0.00") & "')"
    '                par.cmd.ExecuteNonQuery()
    '            End If
    '            myReader5.Close()

    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '            CaricaTabella()

    '        Catch ex As Exception
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '        End Try
    '    End If
    'End Sub

    'Protected Sub imgConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgConferma.Click
    
    'End Sub


    Protected Sub imgConfermare_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgConfermare.Click
        If salvaok.Value = "1" Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "update siscom_mi.pf_voci_struttura set completo_aler='1',valore_lordo_aler=valore_lordo where id_voce=" & idvoce.Value & " AND ID_STRUTTURA=" & idfiliale.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select pf_voci.*,valore_lordo from siscom_mi.pf_voci,siscom_mi.pf_voci_struttura where pf_voci.id=" & idvoce.Value & " and pf_voci_struttura.id_voce=pf_voci.id and pf_voci_struttura.id_struttura=" & idfiliale.Value
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader5.Read Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                        & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F86','Voce " & par.PulisciStrSql(par.IfNull(myReader5("codice"), "")) & "-" & par.IfNull(myReader5("descrizione"), "") _
                                        & " - Euro " & Format(par.IfNull(myReader5("valore_lordo"), "0"), "##,##0.00") & "'," & idfiliale.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
                myReader5.Close()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaTabella()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End Try
        End If
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If salvaok.Value = "1" Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "update siscom_mi.pf_voci_STRUTTURA set completo='0',completo_aler='1',valore_lordo_aler=" & par.VirgoleInPunti(txtImporto.Text) & " where id_voce=" & idvoce.Value & " and id_struttura=" & idfiliale.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select pf_voci.*,valore_lordo from siscom_mi.pf_voci,siscom_mi.pf_voci_struttura where pf_voci.id=" & idvoce.Value & " and pf_voci_struttura.id_voce=pf_voci.id and pf_voci_struttura.id_struttura=" & idfiliale.Value
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader5.Read Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                        & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F87','Voce " & par.PulisciStrSql(par.IfNull(myReader5("codice"), "")) & "-" & par.IfNull(myReader5("descrizione"), "") _
                                        & " - Richiesto " & Format(par.IfNull(myReader5("valore_lordo"), "0"), "##,##0.00") & " - Approvato " _
                                        & Format(CDbl(txtImporto.Text), "##,##0.00") & "'," & idfiliale.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
                myReader5.Close()

                txtImporto.Text = "0,00"

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaTabella()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End Try
        End If
    End Sub

    
End Class
