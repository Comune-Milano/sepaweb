Imports System.IO

Partial Class Contratti_AnteprimaBolletta
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim NomeFile As String
        Dim IdBolletta As String = Request.QueryString("ID")

        Try
            PAR.OracleConn.Open()
            par.SettaCommand(par)

            NomeFile = Format(Now, "yyyyMMddHHmmss")

            'apro e memorizzo il testo base del contratto

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\LetteraFattura.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            PAR.cmd.CommandText = "select BOL_BOLLETTE.*,RAPPORTI_UTENZA.COD_CONTRATTO " _
                               & " from  SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.BOL_BOLLETTE " _
                               & " where RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO " _
                               & "   and BOL_BOLLETTE.ID=" & IdBolletta

            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderJ.Read Then
                contenuto = Replace(contenuto, "$id$", Format(PAR.IfNull(myReaderJ("id"), "0"), "0000000000"))
                contenuto = Replace(contenuto, "$mese$", Format(Now, "MMMM") & " " & Year(Now))
                If PAR.IfNull(myReaderJ("PRESSO"), "") <> "" Then
                    contenuto = Replace(contenuto, "$nominativo$", PAR.IfNull(myReaderJ("INTESTATARIO"), "") & "<br />presso " & PAR.IfNull(myReaderJ("PRESSO"), ""))
                Else
                    contenuto = Replace(contenuto, "$nominativo$", PAR.IfNull(myReaderJ("INTESTATARIO"), ""))
                End If

                contenuto = Replace(contenuto, "$indirizzo$", PAR.IfNull(myReaderJ("INDIRIZZO"), ""))
                contenuto = Replace(contenuto, "$capcitta$", PAR.IfNull(myReaderJ("CAP_CITTA"), ""))

                If PAR.IfNull(myReaderJ("rif_file"), "") = "MAV" Or PAR.IfNull(myReaderJ("rif_file"), "") = "FIN" Or PAR.IfNull(myReaderJ("rif_file"), "") = "MOD" Or PAR.IfNull(myReaderJ("rif_file"), "") = "REC" Then
                    contenuto = Replace(contenuto, "$oggetto$", PAR.IfNull(myReaderJ("NOTE"), ""))
                    contenuto = Replace(contenuto, "$periodo$", "---")
                    If PAR.IfNull(myReaderJ("N_RATA"), "") <> "99" And PAR.IfNull(myReaderJ("N_RATA"), "") <> "9999" And PAR.IfNull(myReaderJ("N_RATA"), "") <> "999" Then
                        contenuto = Replace(contenuto, "$causale$", "RATA N. " & PAR.IfNull(myReaderJ("N_RATA"), ""))
                    Else
                        contenuto = Replace(contenuto, "$causale$", PAR.IfNull(myReaderJ("NOTE"), ""))
                    End If
                Else
                    contenuto = Replace(contenuto, "$oggetto$", "Bollettazione $periodo$")
                    contenuto = Replace(contenuto, "$periodo$", "Dal " & PAR.FormattaData(PAR.IfNull(myReaderJ("RIFERIMENTO_DA"), "")) & " al " & PAR.FormattaData(PAR.IfNull(myReaderJ("RIFERIMENTO_A"), "")))
                    If PAR.IfNull(myReaderJ("N_RATA"), "") <> "99" And PAR.IfNull(myReaderJ("N_RATA"), "") <> "9999" And PAR.IfNull(myReaderJ("N_RATA"), "") <> "999" Then
                        contenuto = Replace(contenuto, "$causale$", "RATA N. " & PAR.IfNull(myReaderJ("N_RATA"), ""))
                    Else
                        contenuto = Replace(contenuto, "$causale$", PAR.IfNull(myReaderJ("NOTE"), ""))
                    End If
                End If




                contenuto = Replace(contenuto, "$codcontratto$", PAR.IfNull(myReaderJ("COD_CONTRATTO"), ""))
                contenuto = Replace(contenuto, "$testolettera$", "")
                contenuto = Replace(contenuto, "$note$", "")
                contenuto = Replace(contenuto, "$scadenza$", PAR.FormattaData(PAR.IfNull(myReaderJ("DATA_SCADENZA"), "")))

            End If
            myReaderJ.Close()


            Dim IMPORTO As Double = 0
            Dim DETTAGLIO As String = ""
            Dim TOTALE As Double = 0

            PAR.cmd.CommandText = "select BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.DESCRIZIONE " _
                               & " from   SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA " _
                               & " where  T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE " _
                               & "   and  BOL_BOLLETTE_VOCI.ID_BOLLETTA=" & IdBolletta _
                               & " order by t_voci_bolletta.descrizione asc"

            myReaderJ = PAR.cmd.ExecuteReader()
            DETTAGLIO = "<table width='80%'>"

            Do While myReaderJ.Read
                TOTALE = TOTALE + myReaderJ("IMPORTO")
                IMPORTO = IMPORTO + myReaderJ("IMPORTO")
                DETTAGLIO = DETTAGLIO & "<tr><td><span style='font-size: 10pt; font-family: Courier New'>" & myReaderJ("DESCRIZIONE") & " " & myReaderJ("NOTE") & "</span></td><td style='text-align: right'><span style='font-size: 10pt; font-family: Courier New'>" & Format(myReaderJ("IMPORTO"), "##,##0.00") & "</span></td></tr>"
            Loop
            myReaderJ.Close()

            DETTAGLIO = DETTAGLIO & "<tr><td><span style='font-size: 10pt; font-family: Courier New'> </span></td><td style='text-align: right'><span style='font-size: 10pt; font-family: Courier New'> </span></td></tr>"
            If IMPORTO > 0 Then
                DETTAGLIO = DETTAGLIO & "<tr><td><span style='font-size: 10pt; font-family: Courier New'>TOTALE</span></td><td style='text-align: right'><span style='font-size: 10pt; font-family: Courier New'>" & Format(IMPORTO, "##,##0.00") & "</span></td></tr>"
            Else
                DETTAGLIO = DETTAGLIO & "<tr><td><span style='font-size: 10pt; font-family: Courier New'>TOTALE A CREDITO DELL'INQUILINO</span></td><td style='text-align: right'><span style='font-size: 10pt; font-family: Courier New'>" & Format(IMPORTO, "##,##0.00") & "</span></td></tr>"
            End If
            DETTAGLIO = DETTAGLIO & "</table><br /><span style='font-size: 8pt; font-family: Arial'>Tutti gli importi sono espressi in Euro</span>"

            'If IMPORTO > 0 Then
            contenuto = Replace(contenuto, "$importo$", Format(IMPORTO, "##,##0.00") & " Euro")
            '    Else
            'contenuto = Replace(contenuto, "$importo$", Format(IMPORTO * -1, "##,##0.00") & " Euro")
            '    End If
            contenuto = Replace(contenuto, "$dettaglio$", DETTAGLIO)
            Response.Write(contenuto)

            ''scrivo il nuovo contratto compilato
            'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("StampeLettere\") & NomeFile & ".htm", True, System.Text.Encoding.Default)
            'sr.WriteLine(contenuto)
            'sr.Close()


            'Response.Write("<script>var fin;fin=window.open('StampeLettere/" & NomeFile & ".htm" & "','" & Format(Now, "yyyyMMddHHmmss") & "','top=0,left=0,resizable=yes,scrollbars=yes,menubar=yes,toolbar=yes');fin.focus();</script>")
            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
        Catch ex As Exception
            PAR.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
                   
    End Sub
End Class
