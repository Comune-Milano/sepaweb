Imports System.IO

Partial Class Contratti_CONTRATTI_LIGHT_AnteprimaBolletta
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim NomeFile As String
        Dim IdBolletta As String = Request.QueryString("ID")
        If Session.Item("OPERATORE_AU_LIGHT") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            PAR.OracleConn.Open()
            PAR.SettaCommand(PAR)

            NomeFile = Format(Now, "yyyyMMddHHmmss")

            'apro e memorizzo il testo base del contratto

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\LetteraFattura_1.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            PAR.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,BOL_BOLLETTE.*,RAPPORTI_UTENZA.COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID (+)=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO (+)=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND BOL_BOLLETTE.ID=" & IdBolletta
            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReaderJ.Read Then
                If PAR.IfNull(myReaderJ("id"), "0") < 0 Then
                    contenuto = Replace(contenuto, "$id$", PAR.IfNull(myReaderJ("num_bolletta"), "---"))
                Else
                    contenuto = Replace(contenuto, "$id$", Format(PAR.IfNull(myReaderJ("id"), "0"), "0000000000"))
                End If

                If PAR.IfNull(myReaderJ("rif_bollettino"), "") <> "" Then
                    contenuto = Replace(contenuto, "$mav$", PAR.IfNull(myReaderJ("rif_bollettino"), "---"))
                Else
                    contenuto = Replace(contenuto, "$mav$", "---")
                End If

                contenuto = Replace(contenuto, "$dataemissione$", PAR.FormattaData(PAR.IfNull(myReaderJ("data_emissione"), "")))
                contenuto = Replace(contenuto, "$codaffittuario$", PAR.IfNull(myReaderJ("cod_affittuario"), ""))
                contenuto = Replace(contenuto, "$codimmobile$", PAR.IfNull(myReaderJ("COD_UNITA_IMMOBILIARE"), ""))
                'COD_UNITA_IMMOBILIARE

                contenuto = Replace(contenuto, "$mese$", Format(Now, "MMMM") & " " & Year(Now))
                If PAR.IfNull(myReaderJ("PRESSO"), "") <> "" Then
                    contenuto = Replace(contenuto, "$nominativo$", PAR.IfNull(myReaderJ("INTESTATARIO"), "") & "<br />c/o " & PAR.IfNull(myReaderJ("PRESSO"), ""))
                Else
                    contenuto = Replace(contenuto, "$nominativo$", PAR.IfNull(myReaderJ("INTESTATARIO"), ""))
                End If

                contenuto = Replace(contenuto, "$indirizzo$", PAR.IfNull(myReaderJ("INDIRIZZO"), ""))
                contenuto = Replace(contenuto, "$capcitta$", PAR.IfNull(myReaderJ("CAP_CITTA"), ""))

                If PAR.IfNull(myReaderJ("rif_file"), "") = "MAV" Or PAR.IfNull(myReaderJ("rif_file"), "") = "FIN" Or PAR.IfNull(myReaderJ("rif_file"), "") = "MOD" Or PAR.IfNull(myReaderJ("rif_file"), "") = "REC" Then
                    contenuto = Replace(contenuto, "$oggetto$", PAR.IfNull(myReaderJ("NOTE"), ""))
                    contenuto = Replace(contenuto, "$periodo$", "Dal " & PAR.FormattaData(PAR.IfNull(myReaderJ("RIFERIMENTO_DA"), "")) & " al " & PAR.FormattaData(PAR.IfNull(myReaderJ("RIFERIMENTO_A"), "")))
                    If PAR.IfNull(myReaderJ("N_RATA"), "") <> "99" And PAR.IfNull(myReaderJ("N_RATA"), "") <> "9999" And PAR.IfNull(myReaderJ("N_RATA"), "") <> "999" Then
                        contenuto = Replace(contenuto, "$causale$", "RATA N. " & PAR.IfNull(myReaderJ("N_RATA"), ""))
                    Else
                        contenuto = Replace(contenuto, "$causale$", "")
                    End If
                Else
                    contenuto = Replace(contenuto, "$oggetto$", "")
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
                contenuto = Replace(contenuto, "MM/MM_113_84.png", "../MM/MM_113_84.png")
            End If
            myReaderJ.Close()

            Dim IMPORTO As Double = 0
            Dim DETTAGLIO As String = ""
            Dim TOTALE As Double = 0

            PAR.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA=" & IdBolletta & " ORDER BY t_voci_bolletta.descrizione asc"
            myReaderJ = PAR.cmd.ExecuteReader()
            DETTAGLIO = "<table width='100%'>"

            Do While myReaderJ.Read
                TOTALE = TOTALE + myReaderJ("IMPORTO")
                IMPORTO = IMPORTO + myReaderJ("IMPORTO")
                DETTAGLIO = DETTAGLIO & "<tr><td style='border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #000000;' width='80%'><span style='font-size: 10pt; font-family: Courier New'>" & myReaderJ("DESCRIZIONE") & " " & myReaderJ("NOTE") & "</span></td><td width='20%' style='border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right'><span style='font-size: 10pt; font-family: Courier New'>" & Format(myReaderJ("IMPORTO"), "##,##0.00") & "</span></td></tr>"
            Loop
            myReaderJ.Close()

            DETTAGLIO = DETTAGLIO & "<tr><td width='80%'><span style='font-size: 10pt; font-family: Courier New'> </span></td><td width='20%' style='text-align: right'><span style='font-size: 10pt; font-family: Courier New'> </span></td></tr>"
            If IMPORTO > 0 Then
                DETTAGLIO = DETTAGLIO & "<tr><td width='80%'><span style='font-size: 10pt; font-family: Courier New'>TOTALE</span></td><td width='20%' style='text-align: right'><span style='font-size: 10pt; font-family: Courier New'>" & Format(IMPORTO, "##,##0.00") & "</span></td></tr>"
            Else
                DETTAGLIO = DETTAGLIO & "<tr><td width='80%'><span style='font-size: 10pt; font-family: Courier New'>TOTALE A CREDITO DELL'INQUILINO</span></td><td width='20%' style='text-align: right'><span style='font-size: 10pt; font-family: Courier New'>" & Format(IMPORTO, "##,##0.00") & "</span></td></tr>"
            End If
            DETTAGLIO = DETTAGLIO & "</table><br /><span style='font-size: 8pt; font-family: Arial'>Tutti gli importi sono espressi in Euro</span>"

            contenuto = Replace(contenuto, "$importo$", Format(IMPORTO, "##,##0.00") & " Euro")
            contenuto = Replace(contenuto, "$dettaglio$", DETTAGLIO)
            Response.Write(contenuto)

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
        Catch ex As Exception
            PAR.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try


    End Sub
End Class
