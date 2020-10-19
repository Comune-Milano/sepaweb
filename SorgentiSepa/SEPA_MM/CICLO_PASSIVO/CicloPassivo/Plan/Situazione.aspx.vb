
Partial Class Contabilita_CicloPassivo_Plan_Situazione
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            idPianoF.Value = Request.QueryString("IDP")
            per.Value = Request.QueryString("P")
            CaricaTabella()
        End If

    End Sub

    Function CaricaTabella()
        Try
            Dim TestoPagina As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim Intestazione As String = ""


            If Session.Item("ID_OPERATORE") = "1" Then
                Intestazione = ""
            Else
                par.cmd.CommandText = "select OPERATORI.OPERATORE,TAB_FILIALI.NOME from OPERATORI,SISCOM_MI.TAB_FILIALI where OPERATORI.ID=" & Session.Item("ID_OPERATORE") & " AND OPERATORI.ID_UFFICIO=TAB_FILIALI.ID (+)"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    Intestazione = par.IfNull(myReaderA("operatore"), "") & " - " & par.IfNull(myReaderA("NOME"), "")
                End If
                myReaderA.Close()
            End If

            par.cmd.CommandText = "select * from siscom_mi.t_esercizio_finanziario,siscom_mi.pf_main where pf_main.id=" & idPianoF.Value & " and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                TestoPagina = "<table style='width:100%;'>"
                TestoPagina = TestoPagina & "<tr><td align='center' style='font-family: arial; font-size: 12pt; font-weight: bold'>Piano Finanziario " & par.FormattaData(myReader2("inizio")) & " - " & par.FormattaData(myReader2("fine")) & "<br>" & Intestazione & "</td></tr>"
                TestoPagina = TestoPagina & "</table></br>"
            End If
            myReader2.Close()

            TestoPagina = TestoPagina & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='8%'>COD.</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO (iva compresa)</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;&nbsp;COMP.</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

            Dim COMPLETO As String = ""
            Dim immagine As String = ""
            Dim importovoce As String = ""
            Dim operatore As String = ""


            'If Session.Item("ID_OPERATORE") = "1" Then
            '    par.cmd.CommandText = "select * from siscom_mi.pf_voci where id_piano_finanziario=" & idPianoF.Value & operatore & " order by codice asc,indice asc"
            'Else
            par.cmd.CommandText = "select pf_voci.ID,pf_voci.ID_PIANO_FINANZIARIO,pf_voci.CODICE,pf_voci.DESCRIZIONE,PF_VOCI.ID_CAPITOLO,PF_VOCI.INDICE,PF_VOCI.ID_VOCE_MADRE,PF_VOCI_STRUTTURA.VALORE_NETTO,PF_VOCI_STRUTTURA.COMPLETO,PF_VOCI_STRUTTURA.IVA,PF_VOCI_STRUTTURA.VALORE_LORDO,PF_VOCI_STRUTTURA.COMPLETO_ALER,PF_VOCI_STRUTTURA.VALORE_LORDO_ALER from siscom_mi.pf_voci,SISCOM_MI.PF_VOCI_STRUTTURA where pf_voci.id_piano_finanziario=" & idPianoF.Value & " AND PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID AND PF_VOCI_STRUTTURA.ID_STRUTTURA=(SELECT ID_UFFICIO FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE") & ") AND (PF_VOCI.ID_VOCE_MADRE IS NULL OR PF_VOCI.ID_VOCE_MADRE IN (SELECT id_voce FROM siscom_mi.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ") OR PF_VOCI.ID_VOCE_MADRE IN (SELECT id_voce_MADRE FROM siscom_mi.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & "))  order by codice asc"

            'par.cmd.CommandText = ""

            'AND (PF_VOCI.ID_VOCE_MADRE IN (SELECT id_voce FROM siscom_mi.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ") OR PF_VOCI.ID_VOCE_MADRE IN (SELECT id_voce_MADRE FROM siscom_mi.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & "))


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            Dim Capitolo1 As Boolean = False
            Dim Capitolo2 As Boolean = False
            Dim Capitolo3 As Boolean = False
            Dim Capitolo4 As Boolean = False


            Dim SOMMA As Double = 0

            While myReader1.Read

                If Capitolo1 = False Then
                    If Mid(myReader1("CODICE"), 1, 1) = "1" Then
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                        Capitolo1 = True
                    End If
                End If

                If Capitolo2 = False Then
                    If Mid(myReader1("CODICE"), 1, 1) = "2" Then
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>2</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                        Capitolo2 = True
                        Capitolo1 = True
                    End If
                End If


                If Capitolo3 = False Then
                    If Mid(myReader1("CODICE"), 1, 1) = "3" Then
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>3</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                        Capitolo3 = True
                        Capitolo1 = True
                        Capitolo2 = True
                    End If
                End If

                If Capitolo4 = False Then
                    If Mid(myReader1("CODICE"), 1, 1) = "4" Then
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border: 2px dashed #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>4</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                        Capitolo4 = True
                        Capitolo1 = True
                        Capitolo2 = True
                        Capitolo3 = True
                    End If
                End If

                COMPLETO = ""

                If Len(par.IfNull(myReader1("CODICE"), "0")) <= 7 And Len(par.IfNull(myReader1("CODICE"), "0")) > 4 Then
                    If par.IfNull(myReader1("COMPLETO"), "0") = "0" Then
                        COMPLETO = "NO"
                    Else
                        COMPLETO = "SI"
                    End If
                Else
                    COMPLETO = "&nbsp;"
                End If

                Dim IDS As String = Session.Item("ID_STRUTTURA")

                par.cmd.CommandText = "select * from siscom_mi.pf_voci where id_voce_madre=" & myReader1("id")
                Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader123.HasRows = False Then
                    importovoce = "<a href='DettagliSpesaAler.aspx?IDS=" & IDS & "&IDV=" & par.IfNull(myReader1("id"), "") & "&P=" & per.Value & "' target='_blank'>" & Format(CDbl(par.IfNull(myReader1("VALORE_LORDO"), "0")), "##,##0.00") & "</a>"
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & COMPLETO & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & immagine & "</td></tr>"

                Else
                    SOMMA = 0
                    importovoce = "&nbsp;"
                    If Len(par.IfNull(myReader1("codice"), "")) = 4 Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE pf_voci.id_piano_finanziario=" & idPianoF.Value & " AND LENGTH(CODICE)=7 AND SUBSTR(CODICE,1,4)='" & par.IfNull(myReader1("codice"), "") & "'"
                        Dim myReader12345 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader12345.Read
                            par.cmd.CommandText = "select SUM(VALORE_LORDO) from siscom_mi.pf_voci_STRUTTURA where ID_VOCE=" & myReader12345("id") & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                            Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1234.Read Then
                                SOMMA = SOMMA + par.IfNull(myReader1234(0), 0)
                            End If
                            myReader1234.Close()
                        Loop
                        myReader12345.Close()
                        importovoce = "<span class='style2'>" & Format(SOMMA, "##,##0.00") & "</span>"
                    End If

                    If Len(par.IfNull(myReader1("codice"), "")) = 7 Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE pf_voci.id_piano_finanziario=" & idPianoF.Value & " AND LENGTH(CODICE)=10 AND SUBSTR(CODICE,1,7)='" & par.IfNull(myReader1("codice"), "") & "'"
                        Dim myReader12345 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader12345.Read
                            par.cmd.CommandText = "select SUM(VALORE_LORDO) from siscom_mi.pf_voci_STRUTTURA where ID_VOCE=" & myReader12345("id") & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                            Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1234.Read Then
                                SOMMA = SOMMA + par.IfNull(myReader1234(0), 0)
                            End If
                            myReader1234.Close()
                        Loop
                        myReader12345.Close()
                        importovoce = "<span class='style2'>" & Format(SOMMA, "##,##0.00") & "</span>"
                    End If

                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & COMPLETO & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & immagine & "</td></tr>"

                End If
                myReader123.Close()


            End While
            myReader1.Close()
            TestoPagina = TestoPagina & "</table>"

            Response.Write(TestoPagina)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try

    End Function
End Class
